#define Debug_PixelMap

using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace ImageProcess
{

    public class PixelMap<T> : Mat2D<T> where T:unmanaged  
    {
        public static Dictionary<PixelFormat,int> PixelFormatMappingBitCount = new Dictionary<PixelFormat, int>()
        {
            /*https://docs.microsoft.com/zh-cn/dotnet/api/system.drawing.imaging.pixelformat?view=net-5.0*/
            {PixelFormat.DontCare , 0},             /*没有指定像素格式。*/
            {PixelFormat.Format1bppIndexed , 1},    /*指定像素格式为每像素 1 位，并指定它使用索引颜色。 因此颜色表中有两种颜色。*/
            {PixelFormat.Format4bppIndexed , 4},    /*指定格式为每像素 4 位而且已创建索引。*/
            {PixelFormat.Format8bppIndexed , 8},    /*指定格式为每像素 8 位而且已创建索引。 因此颜色表中有 256 种颜色。*/
            {PixelFormat.Format16bppArgb1555 , 16}, /*像素格式为每像素 16 位。 该颜色信息指定 32,768 种色调，其中 5 位为红色，5 位为绿色，5 位为蓝色，1 位为 alpha。*/
            {PixelFormat.Format16bppGrayScale , 16},/*像素格式为每像素 16 位。 该颜色信息指定 65536 种灰色调。*/
            {PixelFormat.Format16bppRgb555 , 16},   /*指定格式为每像素 16 位；红色、绿色和蓝色分量各使用 5 位。 剩余的 1 位未使用。*/
            {PixelFormat.Format16bppRgb565 , 16},   /*指定格式为每像素 16 位；红色分量使用 5 位，绿色分量使用 6 位，蓝色分量使用 5 位。*/
            {PixelFormat.Format24bppRgb , 24},       /*指定格式为每像素 24 位；红色、绿色和蓝色分量各使用 8 位。*/
            {PixelFormat.Format32bppArgb , 32},     /*指定格式为每像素 32 位；alpha、红色、绿色和蓝色分量各使用 8 位。*/
            {PixelFormat.Format32bppPArgb , 32},    /*指定格式为每像素 32 位；alpha、红色、绿色和蓝色分量各使用 8 位。 根据 alpha 分量，对红色、绿色和蓝色分量进行自左乘。*/
            {PixelFormat.Format32bppRgb , 32},      /*指定格式为每像素 32 位；红色、绿色和蓝色分量各使用 8 位。 剩余的 8 位未使用。*/
            {PixelFormat.Format48bppRgb , 48},      /*指定格式为每像素 48 位；红色、绿色和蓝色分量各使用 16 位。*/
            {PixelFormat.Format64bppArgb , 64},     /*指定格式为每像素 64 位；alpha、红色、绿色和蓝色分量各使用 16 位。*/
            {PixelFormat.Format64bppPArgb , 64},    /*指定格式为每像素 64 位；alpha、红色、绿色和蓝色分量各使用 16 位。 根据 alpha 分量，对红色、绿色和蓝色分量进行自左乘。*/
            {PixelFormat.Gdi , 0},      /*像素数据包含 GDI 颜色。*/
            {PixelFormat.Indexed , 0},  /*该像素数据包含颜色索引值，这意味着这些值是系统颜色表中颜色的索引，而不是单个颜色值。*/
            {PixelFormat.PAlpha , 0},   /*像素格式包含自左乘的 alpha 值。  */
            {PixelFormat.Alpha , 0},    /*像素数据包含没有进行过自左乘的 alpha 值。*/
        };

        public ColorPalette Palette{get;protected set;}     /*调色盘，可选*/
        public PixelFormat PixelFormat{get;protected set;}  /*图像格式，通过PixelFormatMappingBitCount图像格式可以获取位深度BitCount*/
        public int RowLength{get;protected set;}            /*一行数据长*/
        public int Stride{get;protected set;}               /*4字节对齐后，一行数据长*/

        public PixelMap():base(){}
        public PixelMap(int widht,int height,int bitCount):base(widht,height,bitCount){}

        protected override void Dispose(bool disposeing)
        {
            base.Dispose(disposeing);
        }

        /*GDI+解码图片文件转换Mat*/
        public virtual bool ReadImage(string strFilePath)
        {
            if(!File.Exists(strFilePath))
            {
                return false;
            }
            using(Bitmap bmp = new Bitmap(strFilePath))
            {
                BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);
                Width       = bmp.Width;
                Height      = bmp.Height;
                PixelFormat = bmp.PixelFormat;
                Debug.Assert(PixelFormatMappingBitCount.Keys.Contains(PixelFormat));
                BitCount    = PixelFormatMappingBitCount[PixelFormat];
                if(BitCount < 9 ||PixelFormat == PixelFormat.Format16bppArgb1555 )
                {
                    Palette     = bmp.Palette;
                }
                RowLength= Width*ElementSize;
                Stride   = bmpData.Stride;
                #if Debug_PixelMap
                if(strFilePath.Contains("637_475_16"))
                {
                    Debug.Assert(PixelFormat == PixelFormat.Format4bppIndexed);
                }
                if(7 < BitCount)
                {
                    Debug.Assert(Stride == ((Width*BitCount + 31)>>5)<<2);//Stride计算方法
                }
                #endif
                Length = RowLength*Height;
                if(Length > 0)
                {
                    Scan0 = Marshal.AllocHGlobal(Length);
                }
                unsafe
                {  
                    byte* src  = (byte*)bmpData.Scan0.ToPointer();      
                    byte* dest = (byte*)Scan0.ToPointer();         
                    for(int i = 0;i < Height;i++)
                    {
                        MemoryOperateSet.memcpy<byte>(dest, src, RowLength);
                        #if Debug_PixelMap
                            for(int j = 0; j < RowLength; j++)
                            {
                                Debug.Assert(*(dest + j) == *(src + j));
                            }
                        #endif
                        dest += RowLength;
                        src  += bmpData.Stride;
                    }
                }
                bmp.UnlockBits(bmpData);
            }
            return true;
        }
        
        /*GDI+ Mat转图片*/
        public virtual bool WriteImage(string strFilePath)
        {
            Stride = ((Width*BitCount + 31)>>5)<<2;
            using(Bitmap bmp = new Bitmap(Width, Height, PixelFormat))
            {
                BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);
                if(BitCount < 9 ||PixelFormat == PixelFormat.Format16bppArgb1555 )
                {
                    bmp.Palette = Palette;
                }
                unsafe
                {  
                    byte* dest= (byte*)bmpData.Scan0.ToPointer();      
                    byte* src = (byte*)Scan0.ToPointer();         
                    for(int i = 0;i < Height;i++)
                    {
                        MemoryOperateSet.memcpy<byte>(dest, src, RowLength);
                        #if Debug_PixelMap
                            for(int j = 0; j < RowLength; j++)
                            {
                                Debug.Assert(*(dest + j) == *(src + j));
                            }
                        #endif
                        src  += RowLength;
                        dest += bmpData.Stride;
                    }
                }
                bmp.UnlockBits(bmpData);
                Encoder myEncoder = Encoder.Quality;
                EncoderParameters myEncoderParameters = new EncoderParameters(1);
                myEncoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
                bmp.Save(strFilePath, GetImageCodecInfoFromPath(strFilePath), myEncoderParameters);
            }
            return true;
        }

        /*从字符串中解析*.bmp/*.jpeg/*.png等，*/
        private ImageCodecInfo GetImageCodecInfoFromPath(string strFilePath)
        {
            int startIdx = strFilePath.LastIndexOf('.') + 1;
            ReadOnlySpan<char> span = strFilePath.AsSpan().Slice(startIdx);
            string mineType = "image/" + span.ToString();
            return ImageCodecInfo.GetImageEncoders().First(s=>s.MimeType == mineType);
        }
    }

    //bmp,jpg,tiff基类，默认类型为bmp
    // public abstract class PixelMap:Rectangle,IDisposable
    // { 
    //     public int Width{get;protected set;}
    //     public int Height{get;protected set;}
    //     //sizeof(T)
    //     public ushort BitCount{get;protected set;}
    //     //非托管内存起始指针
    //     public IntPtr Scan0{get;protected set;}   
    //     //元素个数 Width * Height
    //     public int ElementCount{get;protected set;}

    //     public PixelMap(){}

    //     public virtual void GenEmptyPixelMap(int width,int height,ushort bitcount)
    //     {    
    //         Width = width;
    //         Height = height;
    //         BitCount = bitcount;    
    //         ElementSize = bitcount > 7 ? bitcount>>3 : 1;
    //         Stride = ((Width*BitCount + 31)>>5)<<2;
    //         RankBytesCount = (Width*BitCount)>>3;
    //         Count = RankBytesCount*Height;
    //         Compression = compression;
    //         if(Count > 0)
    //         {
    //             Scan0 = Marshal.AllocHGlobal(Count);
    //         }
    //     }

    //     #region IDisposable接口实现     
    //     public void Dispose()
    //     {
    //         Dispose(true);
    //         GC.SuppressFinalize(this);
    //     }
        
    //     protected virtual void Dispose(bool disposeing)
    //     {
    //         Marshal.FreeHGlobal(Scan0);
    //     }
    //     #endregion    
 
    //     public virtual void ReadImage(string filepath)
    //     {
    //         Bitmap bitmap;
    //         throw new System.Exception("No Realization");
    //     }
    //     public virtual void WriteImage(string filepath){ throw new System.Exception("No Realization");}

    // }
}