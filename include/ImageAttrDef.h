#ifndef IMAGEATTRDEF_H
#define IMAGEATTRDEF_H

#include "CompierDef.h"

enum ImageFormat
{
    Bmp,        /*��ȡλͼ (BMP) ͼ���ʽ��*/
    Emf,        /*��ȡ��ǿ��ͼԪ�ļ� (WMF) ͼ���ʽ��*/
    Exif,       /*��ȡ�ɽ���ͼ���ļ� (Exif) ��ʽ��*/
    Gif,        /*��ȡͼ�ν�����ʽ (GIF) ͼ���ʽ��*/
    Guid,       /*��ȡ��ʾ�� Guid ����� ImageFormat �ṹ��*/
    Icon,       /*��ȡ Windows ͼ��ͼ���ʽ��*/
    Jpeg,       /*��ȡ����ͼ��ר���� (JPEG) ͼ���ʽ��*/
    MemoryBmp,  /*��ȡ�ڴ��е�λͼ�ĸ�ʽ��*/
    Png,        /*��ȡ W3C ����ֲ����ͼ�� (PNG) ͼ���ʽ��*/
    Tiff,       /*��ȡ���ͼ���ļ���ʽ (TIFF) ͼ���ʽ��*/
    Wmf        /*��ȡ Windows ͼԪ�ļ� (WMF) ͼ���ʽ��*/
};

enum PaletteFlags
{
    HasAlpha  = 1,  /*Alpha ���ݡ�*/
    GrayScale = 2,  /*�Ҷ����ݡ�*/
    Halftone  = 4   /*��ɫ�����ݡ�*/
};

typedef INT PixelFormat;

#define    PixelFormatIndexed      0x00010000 // Indexes into a palette
#define    PixelFormatGDI          0x00020000 // Is a GDI-supported format
#define    PixelFormatAlpha        0x00040000 // Has an alpha component
#define    PixelFormatPAlpha       0x00080000 // Pre-multiplied alpha
#define    PixelFormatExtended     0x00100000 // Extended color 16 bits/channel
#define    PixelFormatCanonical    0x00200000 

#define    PixelFormatUndefined       0
#define    PixelFormatDontCare        0

#define    PixelFormat1bppIndexed     (1 | ( 1 << 8) | PixelFormatIndexed | PixelFormatGDI)
#define    PixelFormat4bppIndexed     (2 | ( 4 << 8) | PixelFormatIndexed | PixelFormatGDI)
#define    PixelFormat8bppIndexed     (3 | ( 8 << 8) | PixelFormatIndexed | PixelFormatGDI)
#define    PixelFormat16bppGrayScale  (4 | (16 << 8) | PixelFormatExtended)
#define    PixelFormat16bppRGB555     (5 | (16 << 8) | PixelFormatGDI)
#define    PixelFormat16bppRGB565     (6 | (16 << 8) | PixelFormatGDI)
#define    PixelFormat16bppARGB1555   (7 | (16 << 8) | PixelFormatAlpha | PixelFormatGDI)
#define    PixelFormat24bppRGB        (8 | (24 << 8) | PixelFormatGDI)
#define    PixelFormat32bppRGB        (9 | (32 << 8) | PixelFormatGDI)
#define    PixelFormat32bppARGB       (10 | (32 << 8) | PixelFormatAlpha | PixelFormatGDI | PixelFormatCanonical)
#define    PixelFormat32bppPARGB      (11 | (32 << 8) | PixelFormatAlpha | PixelFormatPAlpha | PixelFormatGDI)
#define    PixelFormat48bppRGB        (12 | (48 << 8) | PixelFormatExtended)
#define    PixelFormat64bppARGB       (13 | (64 << 8) | PixelFormatAlpha  | PixelFormatCanonical | PixelFormatExtended)
#define    PixelFormat64bppPARGB      (14 | (64 << 8) | PixelFormatAlpha  | PixelFormatPAlpha | PixelFormatExtended)
#define    PixelFormat32bppCMYK       (15 | (32 << 8))
#define    PixelFormatMax             16

#pragma pack(1)
struct ColorBGRA
{
    BYTE B;
    BYTE G;
    BYTE R;
    BYTE A;
};
struct ColorBGR
{
    BYTE B;
    BYTE G;
    BYTE R;
};
#pragma pack()

class ColorPalette
{
public:
    DECLARE_PTR(ColorBGRA*, Entries)
    int Length;
    PaletteFlags Flag;
    ColorPalette();
    ~ColorPalette();
};

#endif