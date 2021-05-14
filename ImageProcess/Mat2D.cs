#define Debug_Mat2D

using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ImageProcess
{
    public interface IMat2D
    {
        IntPtr Scan0{get;}  /*非托管内存（图像）起始地址*/
        int Length{get;}    /*矩阵字节数*/
        int Width{get;}     /*图像宽*/
        int Height{get;}    /*图像高*/
        int BitCount{get;}  /*位深度,最大值为 BitCount<<3 */
    }

    public class Mat2D:IMat2D,IDisposable
    {
        public IntPtr Scan0{get;internal  set;}
        public int Length{get;internal set;}
        public int Width{get;internal set;}
        public int Height{get;internal set;}
        public int BitCount{get;internal set;}

        public Mat2D()
        {
            Scan0   = default(IntPtr);
            Length  = 0;
            Width   = 0;
            Height  = 0;
            BitCount= 0;
        }

        public Mat2D(int widht,int height,int bitCount)
        {
            Width   = widht;
            Height  = height;
            BitCount= bitCount;
            Length  = widht*height*bitCount >> 3;
            #if Debug_Mat2D
            Console.WriteLine("widht*height*bitCount >> 3 = %1", Length);
            #endif
            if(Length > 0)
            {
                Scan0 = Marshal.AllocHGlobal(Length);
            }
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
                Marshal.FreeHGlobal(Scan0);
            }
        }
        #endregion
    }
}