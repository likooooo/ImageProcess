using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace ImageProcess.MatOperateSet
{
    /*ת�þ��� https://www.cnblogs.com/jcchan/p/10402403.html*/
    unsafe public static partial class MatOperateSet
    {
        public static bool Transpose<T>(this Mat2D<T> mat) where T:unmanaged
        {
            if(sizeof(T) != mat.BitCount>>3)
            {
                return false;
            }
            int newWidth    = mat.Height;
            int newHeight   = mat.Width;
            int elementCount= mat.Width*mat.Height;
            Span<T> span = new Span<T>(mat.Scan0.ToPointer(), elementCount);
            
            int m = mat.Height;
            int n = mat.Width;
            for(int i = 0;i<elementCount;++i)
            {
                int next = GetNext(i, m, n);
                while (next > i)
                {
                    next = GetNext(next, m, n);
                }
                if(next == i)
                {
                    T temp = span[i]; // �ݴ� 
                    int cur = i;    // ��ǰ�±� 
                    int pre = GetPre(cur, m, n); 
                    while(pre != i) 
                    { 
                        span[cur] = span[pre]; 
                        cur = pre; 
                        pre = GetPre(cur, m, n); 
                    } 
                    span[cur] = temp; 
                }
            }

            mat.Width = newWidth;
            mat.Height = newHeight;
            return true;
            /*��̣�ԭʼ�����->�¾����*/   
            int GetNext(int i, int m, int n)=>(i%n)*m + i / n;/*�¾����row * �¾���� + �¾����col*/
            /*ǰ�����¾����->ԭʼ�����*/
            int GetPre(int i, int m, int n)=>(i%m)*n + i / m;
        }   

        public static T[,] MatToArray<T>(this Mat2D<T> mat)where T:unmanaged
        {
            T[,] array = new T[mat.Height,mat.Width];
            Span<T> src = new Span<T>(mat.Scan0.ToPointer(), mat.Width*mat.Height);
            for(int row = 0;row<mat.Height;row++)
            {
                for(int col = 0;col<mat.Width;col++)
                {
                    array[row,col] = src[row*mat.Width + col];
                }
            }
            return array;
        } 
    }
}