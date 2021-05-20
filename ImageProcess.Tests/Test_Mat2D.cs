using System;
using Xunit;
using System.IO;
using ImageProcess.Images;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ImageProcess.Tests
{
    public class Test_Mat2D
    {
        [StructLayout(LayoutKind.Explicit, Size = 3)]
        public struct SampleColor
        {
            [FieldOffset(0)]public byte R;
            [FieldOffset(1)]public byte G;
            [FieldOffset(2)]public byte B;
        }

        [Fact]
        public void Test_Length()
        {
            Mat2D<byte> mat = new Mat2D<byte>(10,10,8);
            Debug.Assert(mat.Length == 100);
            var mat2 = new Mat2D<ushort>(10,10,16);
            Debug.Assert(mat2.Length == 200);
            var mat3 = new Mat2D<SampleColor>(10,10,24);
            Debug.Assert(mat3.Length == 300);
        }
    }
}
