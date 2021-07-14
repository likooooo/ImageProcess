#ifndef IMAGE_GDI_PLUS_HPP
#define IMAGE_GDI_PLUS_HPP

#include "Mat2D.hpp"
#include <string>
#include <gdiplus.h>
#include <io.h>
#include <map>

#pragma comment(lib, "gdiplus.lib")
using namespace std;
using namespace Gdiplus;

std::map<int, PixelFormat> MappingToPixelFormat = 
{
        {1, PixelFormat8bppIndexed},
        {3, PixelFormat24bppRGB}
};

class ImageGdiPlus/*GDI+实现的图片读写功能*/
{
private:
    ULONG_PTR gdiplustoken;
    UINT num;          // 图像编码器数量
    UINT size;         // 图像编码器数组大小
    Gdiplus::ImageCodecInfo* pImageCodecInfo;
public:
    ImageGdiPlus():gdiplustoken(NULL), num(0), size(0), pImageCodecInfo(NULL){}
    void Begin()
    {
        VOIDRET_ASSERT(NULL == gdiplustoken);
        printf("Try to start GDI+\n");
        Gdiplus::GdiplusStartupInput gdiplusstartupinput; 
	    Gdiplus::GdiplusStartup(&gdiplustoken, &gdiplusstartupinput, NULL);
        
        Gdiplus::GetImageEncodersSize(&num, &size); // 获取编码器数量
        ERROR_ASSERT(0 != size, 1);
        pImageCodecInfo = (Gdiplus::ImageCodecInfo*)(malloc(size));
        ERROR_ASSERT(NULL != pImageCodecInfo, 1);
        Gdiplus::GetImageEncoders(num, size, pImageCodecInfo);// 获取本机支持的编码器
        printf("EncoderNums:%d \nEncoderSize:%d\n", num, size);
        printf("GDI+ is running...\n");
    }

    void End()
    {
        VOIDRET_ASSERT(NULL != gdiplustoken);
        printf("Try to shutdown GDI+...\n");
        free(pImageCodecInfo);
        pImageCodecInfo = NULL;
        Gdiplus::GdiplusShutdown(gdiplustoken);
        printf("GDI+ End...\n");
    }

    std::wstring  StoWs(const std::string& s)
    {
        int len;
        int slength = (int)s.length() + 1;
        len = MultiByteToWideChar(CP_ACP, 0, s.c_str(), slength, 0, 0); 
        wchar_t* buf = new wchar_t[len];
        MultiByteToWideChar(CP_ACP, 0, s.c_str(), slength, buf, len);
        std::wstring r(buf);
        delete[] buf;
        return r;
    }

    template<typename T>
    int ReadImage(string strFilePath, Mat2D<T>& mat)
    {
        struct stat buffer;   
        VALRET_ASSERT(stat (strFilePath.c_str(), &buffer) == 0, 0);
        wstring infilename = StoWs(strFilePath);
        Gdiplus::Bitmap* bmp = new Gdiplus::Bitmap(infilename.c_str());
        int width  = bmp->GetWidth();
        int height = bmp->GetHeight();
        mat = Mat2D<T>(width, height);
        
        Gdiplus::BitmapData bmpData;
        int pixelFormat = bmp->GetPixelFormat();
        bmp->LockBits(&Gdiplus::Rect(0, 0, width, height), Gdiplus::ImageLockModeWrite, pixelFormat, &bmpData); 
        byte* destPtr = (byte*) mat.Scan0; 
        byte* srcPtr  = (byte*) bmpData.Scan0;
        int rowLength = width * mat.GetElementSize();
        for(int i = 0;i < height;i++)
        {
            memcpy(destPtr, srcPtr, rowLength);
            destPtr += rowLength;
            srcPtr  += bmpData.Stride;
        }
        bmp->UnlockBits(&bmpData);
        delete bmp;
        return pixelFormat;
    }

    template<typename T>
    int WriteImage(string strFilePath,Mat2D<T>& mat)
    {
        VALRET_ASSERT((MappingToPixelFormat.count(mat.GetElementSize()) > 0), 0);
        int width  = mat.GetWidth();
        int height = mat.GetHeight();
        PixelFormat currentPixelFormat = MappingToPixelFormat[mat.GetElementSize()];
        Gdiplus::Bitmap* bmp = new Gdiplus::Bitmap(width, height, currentPixelFormat);
        Gdiplus::BitmapData bmpData;
        bmp->LockBits(&Gdiplus::Rect(0, 0, width, height), Gdiplus::ImageLockModeWrite, currentPixelFormat, &bmpData); 
        byte* srcPtr = (byte*) mat.Scan0; 
        byte* destPtr = (byte*) bmpData.Scan0;
        int rowLength = width * mat.GetElementSize();
        for(int i = 0;i < height;i++)
        {
            memcpy(destPtr, srcPtr, rowLength);
            srcPtr  += rowLength;
            destPtr += bmpData.Stride;
        }
        bmp->UnlockBits(&bmpData);
        CLSID pngClsid;
        GetEncoderClsid(L"image/bmp", &pngClsid);
        wstring infilename = StoWs(strFilePath);
        bmp->Save(infilename.c_str(), &pngClsid, NULL); 
        struct stat buffer;   
        VALRET_ASSERT(stat (strFilePath.c_str(), &buffer) == 0, 0);
        return currentPixelFormat;
    }

    int GetEncoderClsid(const WCHAR* format, CLSID* pClsid)
    {
        ERROR_ASSERT(NULL != pImageCodecInfo, 1);
        for (UINT j = 0; j < num; ++j)
        {
            if (_wcsicmp(pImageCodecInfo[j].MimeType, format) == 0)    // 找到该格式就将对应的CLSID给*pClsid
            {
                *pClsid = pImageCodecInfo[j].Clsid;
                return j;
            }
        }
        ERROR_ASSERT(true, 2);
        return -1;
    }
};


#endif