using System;

namespace ELib
{
	public class NativeEventArgs : ReferenceCountedObject
    {
		public new static ReferenceCountedObject FromPtr(long ptr)
        {
			return ReferenceCountedObject.FromPtr(ptr) as ReferenceCountedObject;
        }
    }
}