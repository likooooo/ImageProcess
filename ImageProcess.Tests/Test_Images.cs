using Xunit;
using System;
using System.Diagnostics;
using ImageProcess.Images;

namespace ImageProcess.Tests
{
    public class Test_Images
    {
        [Fact]
        public void Test_RLE()
        {
            byte[] src = new byte[]{1,1,1,1,1,2,3,4,5,5,4,5};
            byte[] rle4;
            Debug.Assert(RLE.EncoderRLE4(src, out rle4));
            PrintRle(rle4);
            byte[] rle8;
            Debug.Assert(RLE.EncoderRLE4(src, out rle8));
            PrintRle(rle8);
            
            void PrintRle(byte[] rle)
            {
                int length = rle.Length/2;
                for(int i = 0;i < length;)
                {
                    Console.WriteLine($"{rle[i++]},{rle[i++]}");
                }
            }
        }
    }
}