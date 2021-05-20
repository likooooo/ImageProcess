using System;
using System.Collections.Generic;

namespace ImageProcess.Images
{
    /*https://docs.microsoft.com/zh-cn/windows/win32/gdi/bitmap-compression*/
    /*https://blog.csdn.net/weixin_41336592/article/details/109710440?depth_1-*/
    public sealed class RLE
    {
        /*4位数据的游程编码*/
        public static bool EncoderRLE4(byte[] data, out byte[] rle)
        {
            int loopCount = data.Length;
            Span<byte> span = stackalloc byte[loopCount*2];/*RLE压缩占用最大内存*/
            int  rleLength = 0;
            byte length    = 1;
            byte tempVal   = data[0];
            int  index     = 0;
            while(++index < loopCount)
            {
                if(tempVal == data[index])
                {
                    length++;
                } 
                else
                {
                    span[rleLength] = tempVal;
                    rleLength++;
                    span[rleLength] = length;
                    rleLength++;
                }
            } 
            rle = span.Slice(0, rleLength).ToArray();
            return true;

        }
       
        /*8位数据的游程编码*/
        public static bool EncoderRLE8(byte[] data, out byte[] rle)
        {
            int loopCount = data.Length;
            Span<byte> span = stackalloc byte[loopCount*2];/*RLE压缩占用最大内存*/
            
            int  rleOutputLength  = 0;
            byte length = 0;
            byte tempVal0  = data[0];
            byte tempVal1  = data[1];
            int  index = 2;
            while(++index < loopCount)
            {
                if(IsContinuous(data, index, out length))
                {            
                    span[rleOutputLength] = length;
                    rleOutputLength++;
                    span[rleOutputLength] = tempVal0;
                    rleOutputLength++;
                    index += length;
                }
                else
                {
                    span[rleOutputLength] = 0;
                    rleOutputLength++;
                    span[rleOutputLength] = length;
                    rleOutputLength++;
                    while(length-- > 0)
                    {
                        span[rleOutputLength] = data[index];
                        rleOutputLength++;
                        index++;
                    }
                }
            }
            rle = span.Slice(0, rleOutputLength).ToArray();
            return true;
        }

        /*判断是否连续，并且返回连续/不连续的最大长度*/
        private static bool IsContinuous(byte[] data, int index, out byte length)
        {
            if(index >= data.Length - 1)
            {
                length = 1;
                return false;
            }
            
            bool isContinue = data[index] == data[++index];
            length = 2;
            while(++index < data.Length)
            {
                if(isContinue && data[index] == data[++index] || !isContinue)
                {
                    length++;
                    if(length == byte.MaxValue)/*最大支持记录255个连续*/
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            return isContinue;
        }

        /*8位数据的游程解码*/
        public static bool DecoderRLE8(byte[] rle, int dataLength, out byte[] data)
        {
            Span<byte> span = stackalloc byte[dataLength];
            int loopCount = rle.Length;
            while(--loopCount > 0)
            {
                span[--dataLength] = rle[loopCount];
                int tempValLength   = rle[--loopCount];
                if(0 == tempValLength)
                {
                    tempValLength = rle[--loopCount];
                    while(--tempValLength > 0)
                    {
                        span[--dataLength] = rle[--loopCount];
                    }
                    continue;
                }
                while(--tempValLength > 0)
                {
                    span[--dataLength] = rle[loopCount];
                }
            }
            data = span.ToArray();
            return true;
        }

    }
}