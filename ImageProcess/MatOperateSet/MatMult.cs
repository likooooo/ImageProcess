using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ImageProcess.MatOperateSet
{
    /*æÿ’Û≥À∑®  */
    unsafe public static partial class MatOperateSet
    {
        public static bool MatMult(this Mat2D mat, byte mult)
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
        
        public static int RowAt(this Mat2D mat, int row, out IntPtr startPtr)
        {
            int offset = 0;
            if(0 > row || row >= mat.Length)
            {
                startPtr = default(IntPtr);
                return offset;
            }
            offset = row * mat.Width * mat.BitCount>>3;
            startPtr = IntPtr.Add(mat.Scan0, offset);
            return offset;
        }
    }
}