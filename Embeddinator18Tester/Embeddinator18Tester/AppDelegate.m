#import "AppDelegate.h"

#import <ELib/ELib.h>

@interface AppDelegate ()
@property (weak) IBOutlet NSWindow *window;
@property (weak) IBOutlet NSTextField *textFieldFullName;
@end

@implementation AppDelegate {
    BOOL m_eventObserverRegistered;
    __strong ELib_MessageReceivedEventArgs *m_eventArgs;
}

- (void)applicationDidFinishLaunching:(NSNotification *)aNotification
{
    ELib_Person* person = self.johnDoe;
    
    self.textFieldFullName.stringValue = person.fullName;
}

- (ELib_Person*)johnDoe
{
    ELib_Person* person = [[ELib_Person alloc] initWithFirstName:@"John" lastName:@"Doe"];
    person.age = 31;
    
    return person;
}

- (IBAction)buttonThrowException_action:(id)sender
{
    ELib_Person* person = self.johnDoe;
    
    @try {
        [person throwException];
    } @catch (NSException* ex) {
        NSString* message = ex.reason;
        NSString* typeFullName = ex.name;
        NSString* stackTrace = ex.callStackSymbols.description;
        
        NSLog(@"Ex Message: %@; Type Name: %@", message, typeFullName);
        NSLog(@"Ex StackTrace: %@", stackTrace);
    }
}

- (IBAction)buttonDoIntensiveWork_action:(id)sender
{
    ELib_Person* person = self.johnDoe;
    
    dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_BACKGROUND, 0), ^{
        [person doIntesiveWork];
    });
}

- (IBAction)buttonEventTest_action:(id)sender
{
    [self registerEventObservers];
    
    NSLog(@"Previous event args message: %@", self.eventArgs.message);
    
    self.eventArgs = nil;
    
    ELib_NativeEventsTest* eventTest = [[ELib_NativeEventsTest alloc] init];
    
    [eventTest raiseMessageReceived];
}

- (void)registerEventObservers
{
    if (m_eventObserverRegistered) {
        return;
    }
    
    [NSNotificationCenter.defaultCenter addObserver:self
                                           selector:@selector(messageReceived:)
                                               name:ELib_NativeEventsTest.nOTIFICATIONNAME_MESSAGERECEIVED
                                             object:nil];
    
    m_eventObserverRegistered = YES;
}

- (void)messageReceived:(NSNotification*)notification
{
    NSInteger eventArgsPtr = ((NSNumber*)notification.userInfo[ELib_NativeEvent.kEY_EVENTARGS]).intValue;
    ELib_MessageReceivedEventArgs *eventArgs = [ELib_MessageReceivedEventArgs fromPtrPtr:eventArgsPtr];
    
    NSLog(@"Message received in native: %@", eventArgs.message);
    
    eventArgs.message = @"Message was set in native code";
    
    [self setEventArgs:eventArgs];
}

- (ELib_MessageReceivedEventArgs*)eventArgs
{
    return m_eventArgs;
}

- (void)setEventArgs:(ELib_MessageReceivedEventArgs*)eventArgs
{
    if (m_eventArgs) {
        [m_eventArgs dispose]; m_eventArgs = nil;
    }
    
    [eventArgs managedRetain];
    
    m_eventArgs = eventArgs;
}

@end
