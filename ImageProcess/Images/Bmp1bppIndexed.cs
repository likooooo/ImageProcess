using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace ImageProcess.Images
{
    public class PaletteBmp1bppIndexed
    {
        public readonly int[] palette;

        public PaletteBmp1bppIndexed()
        {
            palette = new int[]
            {
                0,0xff
            };
        }

    } 


    public class Bmp1bppIndexed:PixelMap
    {
        
    }
}