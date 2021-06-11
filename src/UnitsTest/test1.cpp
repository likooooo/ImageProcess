#include <Mat2D.hpp>
#include<ImageAttrDef.h>
#include<ImageGdiPlus.hpp>
#include<MatOperateSet.h>
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

    void MatOperateSet_Test()
    {
        cout << "MatOperateSet Test Start..." << endl;
        BYTE val[6] = {0,50,100,150,200,255};
        Mat2D<BYTE>* mat = new Mat2D<BYTE>(2, 3, val);
        // cout << "1. 原始矩阵"  << endl;
        // printf(mat->ToMatString().c_str());
        // MatOperateSet::AddOffset<BYTE>(mat, 10);
        // MatOperateSet::AddOffset<BYTE>(mat, -5);
        // MatOperateSet::Scala<BYTE>(mat, 2);
        // cout << "2. 复合计算结果((Mat + 10 - 5)*2)"  << endl;
        // printf(mat->ToMatString().c_str());
        // cout << "3. 原地转置矩阵"  << endl;
        // MatOperateSet::Transpose(mat);
        // printf(mat->ToMatString().c_str());
        // cout << "4. 阈值分割"  << endl;
        // // MatOperateSet::Threshold(mat, 16, 20);
        // cout << "4.1 自动阈值分割"  << endl;
        // MatOperateSet::AutoThreshold(mat);
        cout << "5. 直方图均衡化"  << endl;
        MatOperateSet::EqualizeHistogram(mat);
        printf(mat->ToMatString().c_str());
        delete mat;
        cout << "MatOperateSet Test Finished !"  << endl;
    }
};

int main()
{
    Test1 t1;
    // t1.Mat2D_Test();
    // t1.GdiPlus_Test();
    t1.MatOperateSet_Test();
    return 0;
}