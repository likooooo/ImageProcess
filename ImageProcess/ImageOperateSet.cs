// namespace ImageProcess
// {
//     public static class ImageOperateSet<T> where T:unmanaged
//     {
//         public static bool ReadImage(this PixelMap<T> mat, string strFilePath)
//         {
//             if(!File.Exists(strFilePath))
//             {
//                 return false;
//             }
//             using(Bitmap bmp = new Bitmap(strFilePath))
//             {
//                 BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);
//                 mat.Width       = bmp.Width;
//                 mat.Height      = bmp.Height;
//                 mat.PixelFormat = bmp.PixelFormat;
//                 mat.BitCount    = mat.PixelFormatMappingBitCount[bmp.PixelFormat];
//                 mat.RowLength   = bmp.Width*mat.BitCount>>3;
//                 mat.Stride      = ((bmp.Width*mat.BitCount + 31)>>5)<<2;
//                 mat.Length      = mat.RowLength*bmp.Height;
//                 if(mat.Length > 0)
//                 {
//                     mat.Scan0 = Marshal.AllocHGlobal(mat.Length);
//                 }
//                 unsafe
//                 {  
//                     byte* src  = (byte*)bmpData.Scan0.ToPointer();      
//                     byte* dest = (byte*)mat.Scan0.ToPointer();         
//                     for(int i = 0;i < Height;i++)
//                     {
//                         MemoryOperateSet.memcpy<byte>(dest, src, RowLength);
//                         dest += mat.RowLength;
//                         src  += bmpData.Stride;
//                     }
//                 }
//                 bmp.UnlockBits(bmpData);
//             }
//             return true;
//         }
//     }
// }