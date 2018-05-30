using System;

namespace ELib
{   
	public class MessageReceivedEventArgs : NativeEventArgs
	{
		public string Message { get; set; }

		public new static MessageReceivedEventArgs FromPtr(long ptr)
        {
			return ReferenceCountedObject.FromPtr(ptr) as MessageReceivedEventArgs;
        }
	}

    public class NativeEventsTest
    {      
		public const string NOTIFICATIONNAME_MESSAGERECEIVED = "MessageReceived";
        
		public void RaiseMessageReceived()
		{
			using (MessageReceivedEventArgs eventArgs = new MessageReceivedEventArgs() {
				Message = "Message was set in managed code"
			}) {
				NativeEvent.Invoke(NOTIFICATIONNAME_MESSAGERECEIVED, eventArgs);

                if (!string.IsNullOrEmpty(eventArgs.Message)) {
                    Console.WriteLine("Message Received in managed: {0}", eventArgs.Message);
                }
			}         
		}      
    }
}