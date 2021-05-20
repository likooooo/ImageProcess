using System;
using System.IO;
using System.Runtime.InteropServices;

namespace ImageProcess
{
    unsafe public class Tuple
    {
        internal byte* src;
        internal int Length;
        public Tuple this[int index]
        {
            get=>this;
        }
        public Tuple(){}
        public Tuple(byte b)
        {
        }
        public Tuple(short s){}
        public Tuple(int i){}
        public Tuple(float f){}
        public Tuple(double d){}

        private void InitData()
        {

        }
        #region IDisposable接口实现     
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected virtual void Dispose(bool disposeing)
        {
            if(Length > 0 && disposeing)
            {
                Marshal.FreeHGlobal(new IntPtr((void*)src));
            }
        }
        #endregion


        public static implicit operator Tuple(double d)
        {
            return new Tuple(d);
        }
    }
}