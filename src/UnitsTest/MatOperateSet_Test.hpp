// #include "Cpp_Test_Head.h"

// namespace Mat2D_Test
// {
//     void MatOperateSet_Test()
//     {
//         cout << "MatOperateSet Test Start..." << endl;
//         ImageGdiPlus gdi;
//         Mat2D<BYTE> mat;
//         gdi.Begin();
//         bool ret = gdi.ReadImage("../../../resources/UnitTests/570_544_24.bmp",mat);   
//         if(!ret)
//         {
//             printf("MatOperateSet Test Failed");
//         }
//         // cout << "1. ԭʼ����"  << endl;
//         // printf(mat->ToMatString().c_str());
//         // MatOperateSet::AddOffset<BYTE>(mat, 10);
//         // MatOperateSet::AddOffset<BYTE>(mat, -5);
//         // MatOperateSet::Scala<BYTE>(mat, 2);
//         // cout << "2. ���ϼ�����((Mat + 10 - 5)*2)"  << endl;
//         // printf(mat->ToMatString().c_str());
//         // cout << "3. ԭ��ת�þ���"  << endl;
//         // MatOperateSet::Transpose(mat);
//         // printf(mat->ToMatString().c_str());
//         // cout << "4. ��ֵ�ָ�"  << endl;
//         // // MatOperateSet::Threshold(mat, 16, 20);
//         // cout << "4.1 �Զ���ֵ�ָ�"  << endl;
//         // MatOperateSet::AutoThreshold(mat);
//         cout << "5 ��ֵ��"  << endl;
//         Mat2D<BYTE>* bin = mat->DepthCopy();
//         MatOperateSet::ImageToBin(bin);
//         printf(bin->ToMatString().c_str());
//         int numberOfRuns = 0;
//         std::vector<RLC> rlcs;
//         MatOperateSet::Threshold(bin, 1, 255, numberOfRuns, rlcs);
//         for(int i = 0; i < numberOfRuns; i++)
//         {
//             printf("(%d, %d)\n", rlcs.at(i).index, rlcs.at(i).length);
//         }
//         // delete mat;
//         // cout << "5. ֱ��ͼ���⻯"  << endl;
//         // bool ret = gdi.ReadImage("C:\\Users\\like\\Desktop\\ImageProcess\\resources\\UnitTests\\637_475_8.bmp",&mat);   
//         // gdi.WriteImage("C:\\Users\\like\\Desktop\\ImageProcess\\resources\\UnitTests\\Output\\637_475_8_copy.bmp",mat);
//         // MatOperateSet::EqualizeHistogram(mat);
//         // gdi.WriteImage("C:\\Users\\like\\Desktop\\ImageProcess\\resources\\UnitTests\\Output\\637_475_8.bmp",mat);
//         // delete mat;
//         gdi.End();
//         cout << "MatOperateSet Test Finished !"  << endl;
//     }
// };
