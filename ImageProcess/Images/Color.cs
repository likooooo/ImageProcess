using System;
using System.Runtime.InteropServices;

namespace ImageProcess.Images
{
    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public struct Color/*位图像素 小端存储*/
    {
        [FieldOffset(0)]public byte B;
        
        [FieldOffset(1)]public byte G;
        
        [FieldOffset(2)]public byte R;
        
        [FieldOffset(3)]public byte A;

        public Color(byte r, byte g, byte b){R = r;G = g;B = b;A = byte.MaxValue;}
        
        public static Color FromRGB(byte r, byte g,byte b)=> new Color(){B = b, G = g, R = r, A = byte.MaxValue};

        public static explicit operator Color(UInt32 val) => new Color(){B = (byte)val, G = (byte)(val>>8), R = (byte)(val>>16), A = (byte)(val>>24)};     
        public static explicit operator Color(Int32 val) => new Color(){B = (byte)val, G = (byte)(val>>8), R = (byte)(val>>16), A = (byte)(val>>24)};   
        public static explicit operator Color(System.Drawing.Color gdiPlusColor) => new Color(){B = gdiPlusColor.B, G = gdiPlusColor.G, R = gdiPlusColor.R, A = gdiPlusColor.A};    
        
        public static implicit operator UInt32(Color val) =>(UInt32)( val.B + val.G>>8 + val.R>>16 + val.A>>24);
        public static implicit operator Int32(Color val) => ((((((0x00|val.A)<<8)|val.R)<<8)|val.G)<<8)|val.B; /*0xaarrggbb*/
    }
    
    public class ColorPalette
    {
        public Color[] Entries{get;set;}

        internal protected ColorPalette(){}

        public static explicit operator ColorPalette(Color[] colors) 
        {
            ColorPalette palette = new ColorPalette();
            palette.Entries = colors;
            return palette;
        }

        public static explicit operator ColorPalette(System.Drawing.Imaging.ColorPalette  gdiPlusPallete)
        {
            ColorPalette palette = new ColorPalette();
            int colorLength = gdiPlusPallete.Entries.Length;
            Span<Color> span = stackalloc Color[colorLength];
            while(--colorLength > -1)
            {
                span[colorLength].A = gdiPlusPallete.Entries[colorLength].A;
                span[colorLength].R = gdiPlusPallete.Entries[colorLength].R;
                span[colorLength].G = gdiPlusPallete.Entries[colorLength].G;
                span[colorLength].B = gdiPlusPallete.Entries[colorLength].B;
            }
            palette.Entries = span.ToArray();
            return palette;
        }
    }
}