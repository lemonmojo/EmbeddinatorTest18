using System;

using Foundation;

namespace ELib
{
    public class NativeEvent
    {
		public const string KEY_EVENTARGS = "EventArgs";

		public static void Invoke(string notificationName, ReferenceCountedObject eventArgs)
        {
            NSNotification notification = CreateNotification(notificationName, eventArgs);

            NSNotificationCenter.DefaultCenter.PostNotification(notification);
        }

		private static NSNotification CreateNotification(string notificationName, ReferenceCountedObject eventArgs)
        {
			long eventArgsPtr = eventArgs != null ? eventArgs.NativeHandle : 0;

            NSDictionary<NSObject, NSObject> userInfo = NSDictionary<NSObject, NSObject>.FromObjectsAndKeys(
                new [] { NSNumber.FromInt64(eventArgsPtr) }, new [] { (NSString)KEY_EVENTARGS }, 1
            );

            NSNotification notification = NSNotification.FromName(notificationName, null, userInfo);

            return notification;
        }
    }
}