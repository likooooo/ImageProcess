#include "Cpp_Test_Head.h"

namespace Mat2D_Test
{
    void GdiPlus_Test()
    {
        printf("GdiPlus Test Start...");  
        ImageGdiPlus gdi;
        gdi.Begin();
        int ret;
        // Mat2D<ColorBGR> matBgr;
        // ret = gdi.ReadImage("C:\\Users\\like\\Desktop\\ImageProcess\\resources\\UnitTests\\570_544_24.bmp",matBgr);   
        // printf("ReadImage:%d \nWidth£º%d\nHeight: %d\nElementSize: %d\n", ret, matBgr.GetWidth(), matBgr.GetHeight(), matBgr.GetElementSize());   
        // ret = gdi.WriteImage("C:\\Users\\like\\Desktop\\ImageProcess\\resources\\UnitTests\\Output\\570_544_24_Copy.bmp",matBgr);
        // printf("WriteImage:%d \n", ret);  
        // matBgr.DisposeScan0();
        Mat2D<BYTE> matGray;
        ret = gdi.ReadImage("C:\\Users\\like\\Desktop\\ImageProcess\\resources\\UnitTests\\637_475_8.bmp",matGray);   
        printf("ReadImage:%d \nWidth£º%d\nHeight: %d\nElementSize: %d\n", ret, matGray.GetWidth(), matGray.GetHeight(), matGray.GetElementSize());   
        ret = gdi.WriteImage("C:\\Users\\like\\Desktop\\ImageProcess\\resources\\UnitTests\\Output\\637_475_8_Copy.bmp",matGray);
        printf("WriteImage:%d \n", ret);  
        // matGray.DisposeScan0();
        gdi.End();
        printf("GdiPlus Test Finished !");  
    }

}