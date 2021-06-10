#ifndef MAT_HPP
#define MAT_HPP

#include "CompierDef.h"
#include <string>
#include <vector>
using namespace std;

template<typename T>
class Mat2D
{
    DECLARE_GETSET(int, Width)
    DECLARE_GETSET(int, Height)
    DECLARE_GET(int, ElementSize)
    DECLARE_GET(size_t, Length)
    DECLARE_TEMPLATE_PTR(Scan0)
public:
    Mat2D():Scan0(NULL),Length(0),ElementSize(sizeof(T))
    {
    }
    ~Mat2D()
    {
        DisposeScan0();
    }
    Mat2D(int width, int height):Width(width),Height(height)
    {
        ElementSize = sizeof(T);
        Length = width*height;
        Scan0 = (T*)malloc(Length*sizeof(T));
    }  

    Mat2D(int widht, int height, T array[])
    {
        ElementSize = sizeof(T);
        Length = width*height*ElementSize;
        VOIDRET_ASSERT(array.size() >= Length)
        T* p = Scan0;
        for(int i = 0;i < Length;i++,p++)
        {
            *p = array[i];
        }
    }
    
    void Memset(T val)
    {
        T* p = Scan0;
        for(int i = 0; i < Length; i++,p++)
        {
            *p = val;
        }
    }

    string ToMatString(string strSplit = " ")
    {
        int loopCount = Width*Height;
        T* p = Scan0;
        string str = to_string(*p);
        for(int i = 1; i < loopCount; i++,p++)
        {     
            if(0 == i%Width)
            {
                str += "/n";
            }  
            else
            {
                str += strSplit;
            }    
            str += to_string(*p);
        }
        return str;
    }

    void ToMatStringLines(vector<string>& vec, string strSplit = " ")
    {
        T* p = Scan0;
        string str = to_string(*p);
        for(int i = 1; i < Length; i++,p++)
        {     
            if(0 == i%Width)
            {
                vec.push_back(str);
                str = "";
            }  
            else
            {
                str += strSplit;
            }    
            str += to_string(*p);
        }
        vec.push_back(str);
    }
};

#endif