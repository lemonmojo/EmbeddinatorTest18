using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace ELib
{
	public class ReferenceCountedObject : IDisposable
    {
		private GCHandle m_handle;
        private int m_retainCount;
		private object m_lock = new object();

        internal long NativeHandle
        {
            get {
                if (m_handle.IsAllocated) {
                    return GCHandle.ToIntPtr(m_handle).ToInt64();
                }

                return 0;
            }
        }

		public ReferenceCountedObject()
        {
			Alloc();
        }

		public static ReferenceCountedObject FromPtr(long ptr)
        {
            GCHandle handle = GCHandle.FromIntPtr((IntPtr)ptr);

			ReferenceCountedObject managedObject = handle.Target as ReferenceCountedObject;

			return managedObject;
        }

		private void Alloc()
		{
			lock (m_lock) {
                m_handle = GCHandle.Alloc(this, GCHandleType.Pinned);
            }

            ManagedRetain();
		}

        public void Dispose()
        {
			lock (m_lock) {
                GCHandle handle = m_handle;

				Interlocked.Decrement(ref m_retainCount);
			    
                if (handle.IsAllocated &&
                    m_retainCount == 0) {
                    handle.Free();

    				Debug.Print("DEBUG - {0} disposed", GetType().Name);
                }
			}
        }

        public void ManagedRetain()
        {
			lock (m_lock) {
				Interlocked.Increment(ref m_retainCount);
			}
        }
    }
}