/* using System;

namespace ELib
{   
    public class EventTest
    {
		public class TestEventEventArgs : EventArgs { }

		public delegate void TestEventDelegate(object sender, TestEventEventArgs eventArgs);
        public event TestEventDelegate TestEvent = delegate { };
  
		public event EventHandler TestEvent2 = delegate { };

        public event EventHandler<EventArgs> TestEvent3 = delegate { };
        
		public void RaiseTestEvent()
        {
            TestEvent(this, new TestEventEventArgs());
        }

		public void RaiseTestEvent2()
        {
			TestEvent2(this, EventArgs.Empty);
        }

		public void RaiseTestEvent3()
        {
            TestEvent3(this, EventArgs.Empty);
        }
    }
} */