using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ImageProcess.MatOperateSet
{
    public static partial class MatOperateSet<T> where T:unmanaged
    {
        /*危险的矩阵加法，未判断元素越界*/
        unsafe public static bool AddOffset(Mat2D mat,byte val)
        {
            if(8 != mat.BitCount)
            {
                return false;
            }
            int loopCount = mat.Length;
            byte* ptr = (byte*)mat.Scan0.ToPointer();
            while (loopCount-- > 0)
            {
                *ptr++ += val;
            }
            return true;
        }
        
        /*危险的矩阵加法，未判断元素越界*/
        unsafe public static bool AddOffset(Mat2D mat,Mat2D other)
        {
            if(mat.BitCount != other.BitCount && mat.Width != other.Width && mat.Height != other.Height)
            {
                return false;
            }
            MemoryOperateSet.memcpy(mat.Scan0, other.Scan0, new UIntPtr((uint)mat.Length));
            return true;
        }
        
        unsafe public static bool AddOffsetSavety(Mat2D mat,byte val)
        {
            if(8 != mat.BitCount)
            {
                return false;
            }
            int loopCount = mat.Length;
            byte* ptr = (byte*)mat.Scan0.ToPointer();
            Int32 container;
            while (loopCount-- > 0)
            {
                container = *ptr + val;
                if(0 > container || 255 < container)
                {
                    return false;
                }
                *ptr++ = (byte)container;
            }
            return true;
        }

        unsafe public static bool AddOffsetSavety(Mat2D mat,Mat2D other)
        {
            if(mat.BitCount != other.BitCount && mat.Width != other.Width && mat.Height != other.Height)
            {
                return false;
            }
            int loopCount = mat.Length;
            byte* ptr = (byte*)mat.Scan0.ToPointer();
            byte* ptrOther = (byte*)other.Scan0.ToPointer();
            Int32 container;
            while (loopCount-- > 0)
            {
                container = *ptr + *ptrOther++;
                if(0 > container || 255 < container)
                {
                    return false;
                }
                *ptr++ = (byte)container;
            }
            return true;
        }
    }
}