using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ImageProcess.MatOperateSet
{
    /*����Ӽ���*/
    unsafe public static partial class MatOperateSet
    {
        /*Σ�յľ���ӷ���δ�ж�Ԫ��Խ��*/
        public static bool AddOffset(Mat2D<byte> mat,byte val)
        {
            int loopCount = mat.Length;
            byte* ptr = (byte*)mat.Scan0.ToPointer();
            while (loopCount-- > 0)
            {
                *ptr++ += val;
            }
            return true;
        }
        
        /*Σ�յľ���ӷ���δ�ж�Ԫ��Խ��*/
        public static bool AddOffset(Mat2D<byte> mat, Mat2D<byte> other)
        {
            if(mat.Width != other.Width && mat.Height != other.Height)
            {
                return false;
            }
            byte* ptr = (byte*)mat.Scan0.ToPointer();
            byte* ptrOffset = (byte*)other.Scan0.ToPointer();
            int loopCount = mat.Width*mat.Height;
            while (loopCount-- > 0)
            {
                *ptr++ += *ptrOffset++;
            }
            return true;
        }
        
        public static bool AddOffsetSavety(Mat2D<byte> mat,byte val)
        {
            int loopCount = mat.Length;
            byte* ptr = (byte*)mat.Scan0.ToPointer();
            Int32 container;
            while (loopCount-- > 0)
            {
                container = *ptr + val;
                if(0 > container)
                {
                    container = 0;
                }
                else if(255 < container)
                {
                    container = 255;
                }
                *ptr++ = (byte)container;
            }
            return true;
        }

        public static bool AddOffsetSavety(Mat2D<byte> mat, Mat2D<byte> other)
        {
            if(mat.Width != other.Width && mat.Height != other.Height)
            {
                return false;
            }
            byte* ptr = (byte*)mat.Scan0.ToPointer();
            byte* ptrOffset = (byte*)other.Scan0.ToPointer();
            int container;
            int loopCount = mat.Width*mat.Height;
            while (loopCount-- > 0)
            {
                container = *ptr + *ptrOffset++;
                if(0 > container)
                {
                    container = 0;
                }
                else if(255 < container)
                {
                    container = 255;
                }
                *ptr++ = (byte)container;
            }
            return true;
        }
    }
}