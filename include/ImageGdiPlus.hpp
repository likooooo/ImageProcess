#ifndef IMAGE_GDI_PLUS_HPP
#define IMAGE_GDI_PLUS_HPP

#include "Mat2D.hpp"
#include <string>
#include <gdiplus.h>
#include <io.h>

#pragma comment(lib, "gdiplus.lib")
using namespace std;

class ImageGdiPlus/*GDI+实现的图片读写功能*/
{
private:
    ULONG_PTR gdiplustoken;
public:
    ImageGdiPlus():gdiplustoken(NULL){}
    
    void Begin()
    {
        VOIDRET_ASSERT(NULL == gdiplustoken);
        Gdiplus::GdiplusStartupInput gdiplusstartupinput; 
	    Gdiplus::GdiplusStartup(&gdiplustoken, &gdiplusstartupinput, NULL);
    }

    void End()
    {
        VOIDRET_ASSERT(NULL == gdiplustoken);
        Gdiplus::GdiplusShutdown(gdiplustoken);
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
    bool ReadImage(string strFilePath, Mat2D<T>** mat)
    {
        VALRET_ASSERT((0 == _access(strFilePath.c_str(), 0)), false);
        wstring infilename = StoWs(strFilePath);
        Gdiplus::Bitmap* bmp = new Gdiplus::Bitmap(infilename.c_str());
        int width  = bmp->GetWidth();
        int height = bmp->GetHeight();
        *mat = new Mat2D<T>(width, height);
        
        Gdiplus::BitmapData bmpData;
        bmp->LockBits(&Gdiplus::Rect(0, 0, width, height), Gdiplus::ImageLockModeWrite, bmp->GetPixelFormat(), &bmpData); 
        byte* destPtr = (byte*)(*mat)->Scan0; 
        byte* srcPtr  = (byte*)bmpData.Scan0;
        int rowLength = width*(*mat)->GetElementSize();
        for(int i = 0;i < height;i++)
        {
            memcpy(destPtr, srcPtr, rowLength);
            destPtr += rowLength;
            srcPtr  += bmpData.Stride;
        }
        bmp->UnlockBits(&bmpData);
        delete bmp;
        return true;
    }

    template<typename T>
    bool WriteImage(string strFilePath,Mat2D<T>* mat)
    {
        int width  = mat->GetWidth();
        int height = mat->GetHeight();
        Gdiplus::Bitmap* bmp = new Gdiplus::Bitmap(width, height, PixelFormat24bppRGB);
        
        Gdiplus::BitmapData bmpData;
        bmp->LockBits(&Gdiplus::Rect(0, 0, width, height), Gdiplus::ImageLockModeWrite, bmp->GetPixelFormat(), &bmpData); 
        byte* destPtr = (byte*)mat->Scan0; 
        byte* srcPtr  = (byte*)bmpData.Scan0;
        int rowLength = width*mat->GetElementSize();
        for(int i = 0;i < height;i++)
        {
            memcpy(srcPtr, destPtr, rowLength);
            destPtr += rowLength;
            srcPtr  += bmpData.Stride;
        }
        bmp->UnlockBits(&bmpData);
        CLSID pngClsid;
        GetEncoderClsid(L"image/bmp", &pngClsid);
        wstring infilename = StoWs(strFilePath);
        bmp->Save(infilename.c_str(), &pngClsid, NULL); 
        delete bmp;
        return 0 == _access(strFilePath.c_str(), 0);
    }

    int GetEncoderClsid(const WCHAR* format, CLSID* pClsid)
    {
        UINT  num = 0;          // 图像编码器数量
        UINT  size = 0;         // 图像编码器数组大小

        Gdiplus::ImageCodecInfo* pImageCodecInfo = NULL;

        Gdiplus::GetImageEncodersSize(&num, &size);    // 获取编码器数量
        if (size == 0)
            return -1;

        pImageCodecInfo = (Gdiplus::ImageCodecInfo*)(malloc(size));
        if (pImageCodecInfo == NULL)
            return -1;

        GetImageEncoders(num, size, pImageCodecInfo);    // 获取本机支持的编码器

        for (UINT j = 0; j < num; ++j)
        {
            if (wcscmp(pImageCodecInfo[j].MimeType, format) == 0)    // 找到该格式就将对应的CLSID给*pClsid
            {
                *pClsid = pImageCodecInfo[j].Clsid;
                free(pImageCodecInfo);
                return j;
            }
        }

        free(pImageCodecInfo);
        return -1;
    }
};


#endif