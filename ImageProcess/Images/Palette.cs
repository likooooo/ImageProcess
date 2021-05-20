using System;
using System.Collections.Generic;

namespace ImageProcess.Images
{
    public enum PalleteImageType : int
    {
        Format1bppIndexed = 196865,
        Format4bppIndexed = 197634,
        Format8bppIndexed = 198659,
    }

    public class StandardColor
    {
        internal StandardColor(){}
        public static Color Transparent => new Color(){A = 0};
        public static Color Black => new Color(0, 0, 0);
        public static Color Red => new Color(byte.MaxValue, 0, 0);
        public static Color Green => new Color(0, byte.MaxValue, 0);
        public static Color Blue => new Color(0, 0, byte.MaxValue);
        public static Color White => new Color(byte.MaxValue, byte.MaxValue, byte.MaxValue);
    }

    public delegate StandardPalette GetStandardPalette();

    /*标准调色盘,调色盘格式0xbbggrraa,a默认为255*/
    public abstract class StandardPalette:StandardColor
    {
        public virtual ImageProcess.Images.ColorPalette GetStandardRgbPalette(){throw new NotImplementedException();}
        public virtual ImageProcess.Images.ColorPalette GetStandardGrayPalette(){throw new NotImplementedException();}
    }

    public sealed class StandardFormat1bppPalette:StandardPalette
    {
        internal StandardFormat1bppPalette(){}
        public override ColorPalette GetStandardGrayPalette() => (ColorPalette)new Color[2]{Black, White};
        
        public ColorPalette[] GetRegionPalette() => new ColorPalette[]/*region 颜色组最多StandardColor个*/
        {
            (ColorPalette)new Color[2]{Black, Red},
            (ColorPalette)new Color[2]{Black, Green},
            (ColorPalette)new Color[2]{Black, Blue},
            (ColorPalette)new Color[2]{Black, White}
        };
    }

    public sealed class StandardFormat4bppPalette:StandardPalette
    {
        internal StandardFormat4bppPalette(){}
        public override ColorPalette GetStandardGrayPalette() => (ColorPalette)new Color[2]{Black, White};
    }


    // public static class StandardPalette
    // {
    //     /*标准调色盘*/
    //     public static Dictionary<int,GetStandardPalette> palleteGenerator = new Dictionary<int, GetStandardPalette>()
    //     {
    //         {1, ()=>new UInt32[2]{0x000000ff,0xffffffff}},
    //         {4, GetGrayPallete16},
    //         {8, GetRgbPallete256}
    //     };

    //     /*二值图的12色支持*/
    //     public static Dictionary<int,GetPallete> binaryPalleteGenerator = new Dictionary<int, GetPallete>()
    //     {
    //         {0x00, ()=>new UInt32[]{0, 0x0000ff}},
    //         {0x01, ()=>new UInt32[]{0, 0x00ff00}},
    //         {0x02, ()=>new UInt32[]{0, 0xff0000}},
    //         {0x03, ()=>new UInt32[]{0, 0x0000f0}},
    //         {0x04, ()=>new UInt32[]{0, 0x00f000}},
    //         {0x05, ()=>new UInt32[]{0, 0xf00000}},
    //         {0x06, ()=>new UInt32[]{0, 0x00ffff}},
    //         {0x07, ()=>new UInt32[]{0, 0x00f0f0}},
    //         {0x08, ()=>new UInt32[]{0, 0xf000f0}},
    //         {0x09, ()=>new UInt32[]{0, 0xf0f0f0}},
    //         {0x0a, ()=>new UInt32[]{0, 0xffffff}},
    //         {0x0b, ()=>new UInt32[]{0, 0xffffff}},
    //     };

    //     public static UInt32[] GetPaletteByImageType(PalleteImageType imageType) =>palleteGenerator[imageType]();
        
    //     public static UInt32[] GetBinaryPalette(int index) =>binaryPalleteGenerator[index]();

    //     #region 调色盘定义
    //     public static UInt32[] GetGrayPallete16()
    //     {
    //         return new UInt32[16]
    //         {
    //             0x00,
    //             0x1f1f1f,
    //             0x2f2f2f,
    //             0x3f3f3f,
    //             0x4f4f4f,
    //             0x5f5f5f,
    //             0x6f6f6f,
    //             0x7f7f7f,
    //             0x8f8f8f,
    //             0x9f9f9f,
    //             0xafafaf,
    //             0xbfbfbf,
    //             0xcfcfcf,
    //             0xdfdfdf,
    //             0xefefef,
    //             0xffffff
    //         };
    //     }
    //     public static UInt32[] GetRgbPallete16()
    //     {
    //         return new UInt32[16]
    //         {
    //             0x0,
    //             0x0000f0,
    //             0x0000ff,
    //             0x00f000,
    //             0x00ff00,
    //             0xf00000,
    //             0xff0000,
    //             0x00f0f0,
    //             0x00ffff,
    //             0xf000f0,
    //             0xff00ff,
    //             0xf0f000,
    //             0xffff00,
    //             0xf0f0f0,
    //             0xf8f8f8,
    //             0xffffff
    //         };
    //     }

    //     public static UInt32[] GenGrayPallete256()
    //     {
    //         return new UInt32[256]
    //         {            
    //             0x0,
    //             0x10101,
    //             0x20202,
    //             0x30303,
    //             0x40404,
    //             0x50505,
    //             0x60606,
    //             0x70707,
    //             0x80808,
    //             0x90909,
    //             0xa0a0a,
    //             0xb0b0b,
    //             0xc0c0c,
    //             0xd0d0d,
    //             0xe0e0e,
    //             0xf0f0f,
    //             0x101010,
    //             0x111111,
    //             0x121212,
    //             0x131313,
    //             0x141414,
    //             0x151515,
    //             0x161616,
    //             0x171717,
    //             0x181818,
    //             0x191919,
    //             0x1a1a1a,
    //             0x1b1b1b,
    //             0x1c1c1c,
    //             0x1d1d1d,
    //             0x1e1e1e,
    //             0x1f1f1f,
    //             0x202020,
    //             0x212121,
    //             0x222222,
    //             0x232323,
    //             0x242424,
    //             0x252525,
    //             0x262626,
    //             0x272727,
    //             0x282828,
    //             0x292929,
    //             0x2a2a2a,
    //             0x2b2b2b,
    //             0x2c2c2c,
    //             0x2d2d2d,
    //             0x2e2e2e,
    //             0x2f2f2f,
    //             0x303030,
    //             0x313131,
    //             0x323232,
    //             0x333333,
    //             0x343434,
    //             0x353535,
    //             0x363636,
    //             0x373737,
    //             0x383838,
    //             0x393939,
    //             0x3a3a3a,
    //             0x3b3b3b,
    //             0x3c3c3c,
    //             0x3d3d3d,
    //             0x3e3e3e,
    //             0x3f3f3f,
    //             0x404040,
    //             0x414141,
    //             0x424242,
    //             0x434343,
    //             0x444444,
    //             0x454545,
    //             0x464646,
    //             0x474747,
    //             0x484848,
    //             0x494949,
    //             0x4a4a4a,
    //             0x4b4b4b,
    //             0x4c4c4c,
    //             0x4d4d4d,
    //             0x4e4e4e,
    //             0x4f4f4f,
    //             0x505050,
    //             0x515151,
    //             0x525252,
    //             0x535353,
    //             0x545454,
    //             0x555555,
    //             0x565656,
    //             0x575757,
    //             0x585858,
    //             0x595959,
    //             0x5a5a5a,
    //             0x5b5b5b,
    //             0x5c5c5c,
    //             0x5d5d5d,
    //             0x5e5e5e,
    //             0x5f5f5f,
    //             0x606060,
    //             0x616161,
    //             0x626262,
    //             0x636363,
    //             0x646464,
    //             0x656565,
    //             0x666666,
    //             0x676767,
    //             0x686868,
    //             0x696969,
    //             0x6a6a6a,
    //             0x6b6b6b,
    //             0x6c6c6c,
    //             0x6d6d6d,
    //             0x6e6e6e,
    //             0x6f6f6f,
    //             0x707070,
    //             0x717171,
    //             0x727272,
    //             0x737373,
    //             0x747474,
    //             0x757575,
    //             0x767676,
    //             0x777777,
    //             0x787878,
    //             0x797979,
    //             0x7a7a7a,
    //             0x7b7b7b,
    //             0x7c7c7c,
    //             0x7d7d7d,
    //             0x7e7e7e,
    //             0x7f7f7f,
    //             0x808080,
    //             0x818181,
    //             0x828282,
    //             0x838383,
    //             0x848484,
    //             0x858585,
    //             0x868686,
    //             0x878787,
    //             0x888888,
    //             0x898989,
    //             0x8a8a8a,
    //             0x8b8b8b,
    //             0x8c8c8c,
    //             0x8d8d8d,
    //             0x8e8e8e,
    //             0x8f8f8f,
    //             0x909090,
    //             0x919191,
    //             0x929292,
    //             0x939393,
    //             0x949494,
    //             0x959595,
    //             0x969696,
    //             0x979797,
    //             0x989898,
    //             0x999999,
    //             0x9a9a9a,
    //             0x9b9b9b,
    //             0x9c9c9c,
    //             0x9d9d9d,
    //             0x9e9e9e,
    //             0x9f9f9f,
    //             0xa0a0a0,
    //             0xa1a1a1,
    //             0xa2a2a2,
    //             0xa3a3a3,
    //             0xa4a4a4,
    //             0xa5a5a5,
    //             0xa6a6a6,
    //             0xa7a7a7,
    //             0xa8a8a8,
    //             0xa9a9a9,
    //             0xaaaaaa,
    //             0xababab,
    //             0xacacac,
    //             0xadadad,
    //             0xaeaeae,
    //             0xafafaf,
    //             0xb0b0b0,
    //             0xb1b1b1,
    //             0xb2b2b2,
    //             0xb3b3b3,
    //             0xb4b4b4,
    //             0xb5b5b5,
    //             0xb6b6b6,
    //             0xb7b7b7,
    //             0xb8b8b8,
    //             0xb9b9b9,
    //             0xbababa,
    //             0xbbbbbb,
    //             0xbcbcbc,
    //             0xbdbdbd,
    //             0xbebebe,
    //             0xbfbfbf,
    //             0xc0c0c0,
    //             0xc1c1c1,
    //             0xc2c2c2,
    //             0xc3c3c3,
    //             0xc4c4c4,
    //             0xc5c5c5,
    //             0xc6c6c6,
    //             0xc7c7c7,
    //             0xc8c8c8,
    //             0xc9c9c9,
    //             0xcacaca,
    //             0xcbcbcb,
    //             0xcccccc,
    //             0xcdcdcd,
    //             0xcecece,
    //             0xcfcfcf,
    //             0xd0d0d0,
    //             0xd1d1d1,
    //             0xd2d2d2,
    //             0xd3d3d3,
    //             0xd4d4d4,
    //             0xd5d5d5,
    //             0xd6d6d6,
    //             0xd7d7d7,
    //             0xd8d8d8,
    //             0xd9d9d9,
    //             0xdadada,
    //             0xdbdbdb,
    //             0xdcdcdc,
    //             0xdddddd,
    //             0xdedede,
    //             0xdfdfdf,
    //             0xe0e0e0,
    //             0xe1e1e1,
    //             0xe2e2e2,
    //             0xe3e3e3,
    //             0xe4e4e4,
    //             0xe5e5e5,
    //             0xe6e6e6,
    //             0xe7e7e7,
    //             0xe8e8e8,
    //             0xe9e9e9,
    //             0xeaeaea,
    //             0xebebeb,
    //             0xececec,
    //             0xededed,
    //             0xeeeeee,
    //             0xefefef,
    //             0xf0f0f0,
    //             0xf1f1f1,
    //             0xf2f2f2,
    //             0xf3f3f3,
    //             0xf4f4f4,
    //             0xf5f5f5,
    //             0xf6f6f6,
    //             0xf7f7f7,
    //             0xf8f8f8,
    //             0xf9f9f9,
    //             0xfafafa,
    //             0xfbfbfb,
    //             0xfcfcfc,
    //             0xfdfdfd,
    //             0xfefefe,
    //             0xffffff
    //         };
    //     }

    //     public static UInt32[] GetRgbPallete256()
    //     {
    //         return new UInt32[]
    //         {
    //             0x0,
    //             0x800000,
    //             0x8000,
    //             0x808000,
    //             0x80,
    //             0x800080,
    //             0x8080,
    //             0xc0c0c0,
    //             0xc0dcc0,
    //             0xa6caf0,
    //             0x402000,
    //             0x602000,
    //             0x802000,
    //             0xa02000,
    //             0xc02000,
    //             0xe02000,
    //             0x4000,
    //             0x204000,
    //             0x404000,
    //             0x604000,
    //             0x804000,
    //             0xa04000,
    //             0xc04000,
    //             0xe04000,
    //             0x6000,
    //             0x206000,
    //             0x406000,
    //             0x606000,
    //             0x806000,
    //             0xa06000,
    //             0xc06000,
    //             0xe06000,
    //             0x8000,
    //             0x208000,
    //             0x408000,
    //             0x608000,
    //             0x808000,
    //             0xa08000,
    //             0xc08000,
    //             0xe08000,
    //             0xa000,
    //             0x20a000,
    //             0x40a000,
    //             0x60a000,
    //             0x80a000,
    //             0xa0a000,
    //             0xc0a000,
    //             0xe0a000,
    //             0xc000,
    //             0x20c000,
    //             0x40c000,
    //             0x60c000,
    //             0x80c000,
    //             0xa0c000,
    //             0xc0c000,
    //             0xe0c000,
    //             0xe000,
    //             0x20e000,
    //             0x40e000,
    //             0x60e000,
    //             0x80e000,
    //             0xa0e000,
    //             0xc0e000,
    //             0xe0e000,
    //             0x40,
    //             0x200040,
    //             0x400040,
    //             0x600040,
    //             0x800040,
    //             0xa00040,
    //             0xc00040,
    //             0xe00040,
    //             0x2040,
    //             0x202040,
    //             0x402040,
    //             0x602040,
    //             0x802040,
    //             0xa02040,
    //             0xc02040,
    //             0xe02040,
    //             0x4040,
    //             0x204040,
    //             0x404040,
    //             0x604040,
    //             0x804040,
    //             0xa04040,
    //             0xc04040,
    //             0xe04040,
    //             0x6040,
    //             0x206040,
    //             0x406040,
    //             0x606040,
    //             0x806040,
    //             0xa06040,
    //             0xc06040,
    //             0xe06040,
    //             0x8040,
    //             0x208040,
    //             0x408040,
    //             0x608040,
    //             0x808040,
    //             0xa08040,
    //             0xc08040,
    //             0xe08040,
    //             0xa040,
    //             0x20a040,
    //             0x40a040,
    //             0x60a040,
    //             0x80a040,
    //             0xa0a040,
    //             0xc0a040,
    //             0xe0a040,
    //             0xc040,
    //             0x20c040,
    //             0x40c040,
    //             0x60c040,
    //             0x80c040,
    //             0xa0c040,
    //             0xc0c040,
    //             0xe0c040,
    //             0xe040,
    //             0x20e040,
    //             0x40e040,
    //             0x60e040,
    //             0x80e040,
    //             0xa0e040,
    //             0xc0e040,
    //             0xe0e040,
    //             0x80,
    //             0x200080,
    //             0x400080,
    //             0x600080,
    //             0x800080,
    //             0xa00080,
    //             0xc00080,
    //             0xe00080,
    //             0x2080,
    //             0x202080,
    //             0x402080,
    //             0x602080,
    //             0x802080,
    //             0xa02080,
    //             0xc02080,
    //             0xe02080,
    //             0x4080,
    //             0x204080,
    //             0x404080,
    //             0x604080,
    //             0x804080,
    //             0xa04080,
    //             0xc04080,
    //             0xe04080,
    //             0x6080,
    //             0x206080,
    //             0x406080,
    //             0x606080,
    //             0x806080,
    //             0xa06080,
    //             0xc06080,
    //             0xe06080,
    //             0x8080,
    //             0x208080,
    //             0x408080,
    //             0x608080,
    //             0x808080,
    //             0xa08080,
    //             0xc08080,
    //             0xe08080,
    //             0xa080,
    //             0x20a080,
    //             0x40a080,
    //             0x60a080,
    //             0x80a080,
    //             0xa0a080,
    //             0xc0a080,
    //             0xe0a080,
    //             0xc080,
    //             0x20c080,
    //             0x40c080,
    //             0x60c080,
    //             0x80c080,
    //             0xa0c080,
    //             0xc0c080,
    //             0xe0c080,
    //             0xe080,
    //             0x20e080,
    //             0x40e080,
    //             0x60e080,
    //             0x80e080,
    //             0xa0e080,
    //             0xc0e080,
    //             0xe0e080,
    //             0xc0,
    //             0x2000c0,
    //             0x4000c0,
    //             0x6000c0,
    //             0x8000c0,
    //             0xa000c0,
    //             0xc000c0,
    //             0xe000c0,
    //             0x20c0,
    //             0x2020c0,
    //             0x4020c0,
    //             0x6020c0,
    //             0x8020c0,
    //             0xa020c0,
    //             0xc020c0,
    //             0xe020c0,
    //             0x40c0,
    //             0x2040c0,
    //             0x4040c0,
    //             0x6040c0,
    //             0x8040c0,
    //             0xa040c0,
    //             0xc040c0,
    //             0xe040c0,
    //             0x60c0,
    //             0x2060c0,
    //             0x4060c0,
    //             0x6060c0,
    //             0x8060c0,
    //             0xa060c0,
    //             0xc060c0,
    //             0xe060c0,
    //             0x80c0,
    //             0x2080c0,
    //             0x4080c0,
    //             0x6080c0,
    //             0x8080c0,
    //             0xa080c0,
    //             0xc080c0,
    //             0xe080c0,
    //             0xa0c0,
    //             0x20a0c0,
    //             0x40a0c0,
    //             0x60a0c0,
    //             0x80a0c0,
    //             0xa0a0c0,
    //             0xc0a0c0,
    //             0xe0a0c0,
    //             0xc0c0,
    //             0x20c0c0,
    //             0x40c0c0,
    //             0x60c0c0,
    //             0x80c0c0,
    //             0xa0c0c0,
    //             0xfffbf0,
    //             0xa0a0a4,
    //             0x808080,
    //             0xff0000,
    //             0xff00,
    //             0xffff00,
    //             0xff,
    //             0xff00ff,
    //             0xffff,
    //             0xffffff
    //         };
    //     }
    //     #endregion
    // }
}