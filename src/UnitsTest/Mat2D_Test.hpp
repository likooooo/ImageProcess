#include "Cpp_Test_Head.h"

namespace Mat2D_Test
{
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

};