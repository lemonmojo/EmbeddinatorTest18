#import "AppDelegate.h"

#import <ELib/ELib.h>

@interface AppDelegate ()
@property (weak) IBOutlet NSWindow *window;
@property (weak) IBOutlet NSTextField *textFieldFullName;
@end

@implementation AppDelegate

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

@end
