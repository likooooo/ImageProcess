#ifndef TRANSPOSE_HPP
#define TRANSPOSE_HPP

#include "MatOperateDef.hpp"
#include <Mat2D.hpp>

namespace MatOperateSet
{
    /*后继：原始矩阵点->新矩阵点*/   
    int GetNext(int i, int m, int n){return (i%n)*m + i / n;}/*新矩阵的row * 新矩阵宽 + 新矩阵的col*/
    /*前驱，新矩阵点->原始矩阵点*/
    int GetPre(int i, int m, int n){return (i%m)*n + i / m;}

    /*【原地】转置矩阵 https://www.cnblogs.com/jcchan/p/10402403.html*/
    template<typename T>
    bool Transpose(IN_OUT Mat2D<T>* mat)
    {  
        int newWidth    = mat->GetHeight();
        int newHeight   = mat->GetWidth();
        size_t elementCount= mat->GetLength();
        T* span = mat->Scan0;
        
        int m = mat->GetHeight();
        int n = mat->GetWidth();
        for(int i = 0;i<elementCount;++i)
        {
            int next = GetNext(i, m, n);
            while (next > i)
            {
                next = GetNext(next, m, n);
            }
            if(next == i)
            {
                T temp = span[i]; // 暂存 
                int cur = i;    // 当前下标 
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

        mat->SetWidth(newWidth);
        mat->SetHeight(newHeight);
        return true;
    }
}

#endif