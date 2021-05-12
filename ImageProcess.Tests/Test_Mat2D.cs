using System;
using Xunit;
using ImageProcess.Images;
using System.Diagnostics;

namespace ImageProcess.Tests
{
    public class Test_Mat2D
    {
        [Fact]
        public void Test_Length()
        {
            Mat2D mat = new Mat2D(10,10,8);
            Debug.Assert(mat.Length == 100);
            Mat2D mat2 = new Mat2D(10,10,16);
            Debug.Assert(mat2.Length == 200);
            Mat2D mat3 = new Mat2D(10,10,24);
            Debug.Assert(mat3.Length == 300);
        }

        [Fact]
        public void Test_1()
        {
            byte[] dest;
            Debug.Assert(MemoryOperateSet.memcpy<int,byte>(new int[]{0,0xffff},out dest));
            Debug.Assert(dest[0] == 0);
            Debug.Assert(dest[4] == 0xff);
            Debug.Assert(dest[5] == 0xff);
        }

        [Fact]
        public void Test_PixelMapIsUnmanaged()
        {
            Console.WriteLine("asd");
            PixelMap bmp24 = new PixelMap();
            Debug.Assert(bmp24.ReadImage(@"D:\ImageProcess\ImageProcess.Tests\resources\570_544_24.bmp"));
            Debug.Assert(bmp24.WriteImage(@"D:\ImageProcess\ImageProcess.Tests\resources\output\570_544_24_copy.bmp"));
            
            PixelMap bmp16 = new PixelMap();
            Debug.Assert(bmp16.ReadImage(@"D:\ImageProcess\ImageProcess.Tests\resources\637_475_16.bmp"));
            Debug.Assert(bmp16.WriteImage(@"D:\ImageProcess\ImageProcess.Tests\resources\output\637_475_16_copy.bmp"));
            
            PixelMap bmp8 = new PixelMap();
            Debug.Assert(bmp8.ReadImage(@"D:\ImageProcess\ImageProcess.Tests\resources\637_475_8.bmp"));
            Debug.Assert(bmp8.WriteImage(@"D:\ImageProcess\ImageProcess.Tests\resources\output\637_475_8_copy.bmp"));
            
        }
    }
}
