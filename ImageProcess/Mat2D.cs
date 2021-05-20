#define Debug_Mat2D

using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ImageProcess
{
    public interface IMat2D
    {
        IntPtr Scan0{get;}  /*非托管内存（图像）起始地址*/
        int Length{get;}    /*矩阵字节数*/
        int Width{get;}     /*图像宽*/
        int Height{get;}    /*图像高*/
        int ElementSize{get;}  /*矩阵元素的字节数*/
    }

    unsafe public class Mat2D<T>:IMat2D,IDisposable where T:unmanaged
    {
        public IntPtr Scan0{get;protected set;}
        public int Length{get;protected set;}
        public int Width{get;protected set;}
        public int Height{get;protected set;}
        public int ElementSize{get;protected set;}
        public int BitCount
        {
            get=> ElementSize<<3;
            protected set{ElementSize = value>>3;}
        }

        public Mat2D()
        {
            Scan0   = default(IntPtr);
            Length  = 0;
            Width   = 0;
            Height  = 0;
            ElementSize= sizeof(T);
        }

        public Mat2D(int widht, int height)
        {
            Width      = widht;
            Height     = height;
            ElementSize= sizeof(T);
            Length     = widht*height*ElementSize;
            if(Length > 0)
            {
                Scan0 = Marshal.AllocHGlobal(Length);
            }
        }

        public Mat2D(int widht, int height, T[] array):this(widht, height)
        {
            int loopCount = Width*Height;
            Debug.Assert(loopCount <= array.Length);
            T* dest = (T*)Scan0.ToPointer();
            int index = -1;
            while(++index < loopCount)
            {
                *dest++ = array[index];
            }
        }
       
        public Mat2D(int widht, int height, int bitCount)
        {
            if(bitCount>>3 > sizeof(T))
            {
                throw new Exception("对象类型容器位长小于bitCount");
            }
            Width    = widht;
            Height   = height;
            BitCount = bitCount;
            Length   = widht*height*ElementSize;
            if(Length > 0)
            {
                Scan0 = Marshal.AllocHGlobal(Length);
            }
        }

        public string ToMatString(string strSplit = " ")
        {
            string str = "";
            int loopCount = Width*Height;
            T* dest = (T*)Scan0.ToPointer();
            int index = -1;
            while(++index < loopCount)
            {
                str += (*dest++).ToString();
                str += 0 == (index%Width) && (index > 0) ? "/r/n" : strSplit;
            }
            return str;
        }

        public string[] ToMatStringLines(string strSplit = " ")
        {
            string[] str = new string[Height];
            int rowIndex = 0;
            int loopCount = Width*Height;
            T* dest = (T*)Scan0.ToPointer();
            int index = -1;
            while(++index < loopCount)
            {
                str[rowIndex] += (*dest++).ToString();
                if(0 == (index%Width) && (index > 0))
                {
                    rowIndex++;
                }
                else
                {
                    str[rowIndex] +=  strSplit;
                }
            }
            return str;
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