// using Xunit;
// using System;
// using System.Drawing;
// using System.Drawing.Imaging;
// using System.Diagnostics;
// using ImageProcess.Images;
// using System.Collections.Generic;
// using System.Runtime.InteropServices;

// namespace ImageProcess.Tests
// {
//     public class Test_ImageIO
//     {
//         [StructLayout(LayoutKind.Explicit, Size = 3)]
//         public struct SampleColor
//         {
//             [FieldOffset(0)]public byte R;
//             [FieldOffset(1)]public byte G;
//             [FieldOffset(2)]public byte B;
//         }

//         [Fact]
//         public void Test_PixelMapIsUnmanaged()
//         {
//             string dir = "..\\..\\..\\resources\\";
//             string suffix = ".bmp";
//             string copySuffix = "_copy" + suffix;
//             var bmp24 = ReadWriteImage<SampleColor>($"{dir}570_544_24{suffix}", $"{dir}output\\570_544_24{copySuffix}");
//             var bmp8 = ReadWriteImage<byte>($"{dir}637_475_8{suffix}", $"{dir}output\\637_475_8{copySuffix}");          
//             PaletteToBin(bmp8, $"{dir}output\\637_475_8.bin");
//             var bmp8Gray = ReadWriteImage<byte>($"{dir}570_554_24_gray{suffix}", $"{dir}output\\570_554_24_gray{copySuffix}");
//             PaletteToBin(bmp8Gray, $"{dir}output\\570_554_24_gray.bin");
//             // var bmp16 = ReadWriteImage<Int16>($"{dir}637_475_16{suffix}", $"{dir}output\\637_475_16{copySuffix}");

//             // var bmp1 = ReadWriteImage($"{dir}637_475_1{suffix}", $"{dir}output\\637_475_1{copySuffix}");
//             // PaletteToBin(bmp1, $"{dir}output\\637_475_1.bin");

//             PixelMap<T> ReadWriteImage<T>(string filePathIn,string filepathOut) where T:unmanaged
//             {
//                 Debug.Assert(System.IO.File.Exists(filePathIn));
//                 var bmp = new PixelMap<T>();
//                 Debug.Assert(bmp.ReadImage(filePathIn));
//                 Debug.Assert(bmp.WriteImage(filepathOut));
//                 return bmp;
//             }

//             void PaletteToBin<T>(PixelMap<T> bmp,string filepath) where T:unmanaged
//             {
//                 System.Drawing.Color[] colors = bmp.Palette.Entries;
//                 using(System.IO.StreamWriter sw = new System.IO.StreamWriter(filepath, false))
//                 {
//                     sw.WriteLine("#region");
//                     sw.WriteLine($"(ColorPalette)new Color[{colors.Length}]");
//                     sw.WriteLine("{");
//                     for(int i  = 0;i<colors.Length;i++)
//                     {
//                         sw.WriteLine($"    new Color({colors[i].R}, {colors[i].G}, {colors[i].B}),");
//                     }
//                     sw.WriteLine("}");
//                     sw.WriteLine("#endregion");
//                 }
//             }
//         }

//         // [Fact(Skip="reason")]
//         // public void PalleteToBin()
//         // {
            
//         // }
//     }
// }