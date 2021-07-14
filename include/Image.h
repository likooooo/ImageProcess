#ifndef IMAGE_H
#define IMAGE_H

#include "ImageAttrDef.h"
// #include "ImageGdiPlus.hpp"
#include "Mat2D.hpp"

class Image/*ͼ�����ӿ�*/
{
public:
    ColorPalette Palette;/*��ɫ�̣���ѡ*/
    PixelFormat pixelFormat; /*ͼ���ʽ��ͨ��PixelFormatMappingBitCountͼ���ʽ���Ի�ȡλ���BitCount*/
    int RowLength;      /*һ�����ݳ�*/
    int Stride;         /*4�ֽڶ����һ�����ݳ�*/
    Image(){}
    Image(PixelFormat p):pixelFormat(p){}
    virtual bool ReadImage(string filepath)=0;
    virtual bool WriteImage(string filepath)=0;
    static bool IsPixelFormatIndexed(PixelFormat pixelFormat)  { return (0 != (pixelFormat&PixelFormatIndexed)); }
    static bool IsPixelFormatGDI(PixelFormat pixelFormat)      { return (0 != (pixelFormat&PixelFormatGDI)); }
    static bool IsPixelFormatAlpha(PixelFormat pixelFormat)    { return (0 != (pixelFormat&PixelFormatAlpha)); }
    static bool IsPixelFormatPAlpha(PixelFormat pixelFormat)   { return (0 != (pixelFormat&PixelFormatPAlpha)); }
    static bool IsPixelFormatExtended(PixelFormat pixelFormat) { return (0 != (pixelFormat&PixelFormatExtended)); }
    static bool IsCanonicalPixelFormat(PixelFormat pixelFormat){ return (0 != (pixelFormat&PixelFormatCanonical)); }
};

class BmpImage8 : public Image,public Mat2D<BYTE>
{
public:
    BmpImage8(): Mat2D<BYTE>(),Image(PixelFormat8bppIndexed){}
    BmpImage8(int width, int height): Mat2D<BYTE>(width, height),Image(PixelFormat8bppIndexed){}
    ~BmpImage8();
    bool ReadImage(string filepath) override;
    bool WriteImage(string filepath) override; 
};

class BmpImage16 : public Image,public Mat2D<WORD>
{
public:
    BmpImage16(): Mat2D<WORD>(),Image(PixelFormat16bppRGB555){}
    BmpImage16(int width, int height): Mat2D<WORD>(width, height),Image(PixelFormat16bppRGB555){}
    ~BmpImage16();
    bool ReadImage(string filepath) override;
    bool WriteImage(string filepath) override; 
};
class BmpImage24 : public Image,public Mat2D<ColorBGR>
{
public:
    BmpImage24(): Mat2D<ColorBGR>(),Image(PixelFormat24bppRGB){}
    BmpImage24(int width, int height): Mat2D<ColorBGR>(width, height),Image(PixelFormat24bppRGB){}
    ~BmpImage24();
    bool ReadImage(string filepath) override;
    bool WriteImage(string filepath) override; 
};
class BmpImage32 : public Image,public Mat2D<ColorBGRA>
{
public:
    BmpImage32(): Mat2D<ColorBGRA>(),Image(PixelFormat32bppARGB){}
    BmpImage32(int width, int height): Mat2D<ColorBGRA>(width, height),Image(PixelFormat32bppARGB){}
    ~BmpImage32();
    bool ReadImage(string filepath) override;
    bool WriteImage(string filepath) override; 
};
#endif