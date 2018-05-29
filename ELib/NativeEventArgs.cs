using System;
using System.Runtime.InteropServices;

namespace ELib
{
	public class NativeEventArgs : IDisposable
    {
        private GCHandle m_handle;

        internal long NativeHandle
        {
            get {
                if (m_handle.IsAllocated) {
                    return GCHandle.ToIntPtr(m_handle).ToInt64();
                }

                return 0;
            }
        }

        public NativeEventArgs()
        {
            m_handle = GCHandle.Alloc(this, GCHandleType.Pinned);
        }

        public static NativeEventArgs FromPtr(long ptr)
        {
            GCHandle handle = GCHandle.FromIntPtr((IntPtr)ptr);

            NativeEventArgs eventArgs = handle.Target as NativeEventArgs;

            return eventArgs;
        }

        public void Dispose()
        {
            GCHandle handle = m_handle;

            if (handle.IsAllocated) {
                handle.Free();
            }
        }
    }
}