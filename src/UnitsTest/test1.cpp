#include <Mat2D.hpp>
#include <ImageAttrDef.h>
#include<ImageGdiPlus.hpp>
#include <iostream>
using namespace std;

class Test1
{
public:
    Test1(){}
    ~Test1(){}

    void Mat2D_Test()
    {
        cout << "Mat2D Test Start..."  << endl;
        Mat2D<int> mat(3, 2);
        mat.Memset(255);
        string strMat = mat.ToMatString();
        cout << strMat << endl;

        vector<string> vecStrMat;
        mat.ToMatStringLines(vecStrMat, ",");
        for(int i = 0;i < mat.GetHeight();i++)
        {        
            cout << vecStrMat.at(i) << endl;
        }
        cout << "Width       : " << mat.GetWidth() << endl;
        cout << "Height      : " << mat.GetHeight()<< endl;   
        cout << "ElementSize : " << mat.GetElementSize()<< endl; 
        cout << "Length      : " << mat.GetLength()<< endl;
        mat.DisposeScan0();
        cout << "Mat2D Test Finished !"  << endl;
    }

    void GdiPlus_Test()
    {
        cout << "GdiPlus Test Start..." << endl;
        ImageGdiPlus gdi;
        gdi.Begin();
        Mat2D<ColorBGR>* mat = NULL;
        bool ret = gdi.ReadImage("C:\\Users\\like\\Desktop\\ImageProcess\\resources\\UnitTests\\570_544_24.bmp",&mat);   
        if(!ret )
        {
            printf("Test Failed");
        }
        printf("%d, %d, %d, %d\n", ret, mat->GetWidth(), mat->GetHeight(), mat->GetElementSize());   
        gdi.WriteImage("C:\\Users\\like\\Desktop\\ImageProcess\\resources\\UnitTests\\Output\\570_544_24.bmp",mat);
        delete mat;
        cout << "GdiPlus Test Finished !"  << endl;
        gdi.End();
    }
};

int main()
{
    Test1 t1;
    // t1.Mat2D_Test();
    t1.GdiPlus_Test();
    return 0;
}