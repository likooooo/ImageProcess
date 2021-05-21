using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ImageProcess.MatOperateSet
{
    /*æÿ’Û≥À∑®*/
    unsafe public static partial class MatOperateSet
    {
        /*≥À∑®*/
        public static bool MatMult(this Mat2D<byte> mat, byte mult)
        {
            if(8 != mat.BitCount)
            {
                return false;
            }
            Span<byte> span = new Span<byte>(mat.Scan0.ToPointer(), mat.Length);
            int loopCount = mat.Length;
            while(--loopCount > -1)
            {
                span[loopCount]*=mult;
            }
            return true;
        }
        
        /*≥À∑®*/
        public static Mat2D<ushort> MatMult(this Mat2D<byte> mat, Mat2D<byte> other)
        {
            if(mat.Width != other.Height)
            {
                return null;
            }
            var res = new Mat2D<ushort>(mat.Height, other.Width);
            byte* ptrLeft  = (byte*)mat.Scan0.ToPointer();
            byte* ptrRight = (byte*)other.Scan0.ToPointer();
            ushort* dest   = (ushort*)res.Scan0.ToPointer();
            int loopCount = res.Length;
            int index = -1;
            while (++index < loopCount)
            {
                int row = index/res.Width;
                int col = (index - row*res.Width);
                ushort val = 0;
                for(int i = 0; i < mat.Width;i++)
                {
                    val += (ushort)
                    (
                        *(ptrLeft + row * mat.Width + i)**(ptrRight + i*other.Width + col)
                    );
                }
                *dest++ = val;
            }
            return res;   
        }
        
    }
}