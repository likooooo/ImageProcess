#ifndef TRANSPOSE_HPP
#define TRANSPOSE_HPP

#include "MatOperateDef.hpp"
#include <Mat2D.hpp>

namespace MatOperateSet
{
    /*��̣�ԭʼ�����->�¾����*/   
    int GetNext(int i, int m, int n){return (i%n)*m + i / n;}/*�¾����row * �¾���� + �¾����col*/
    /*ǰ�����¾����->ԭʼ�����*/
    int GetPre(int i, int m, int n){return (i%m)*n + i / m;}

    /*��ԭ�ء�ת�þ��� https://www.cnblogs.com/jcchan/p/10402403.html*/
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

        mat->SetWidth(newWidth);
        mat->SetHeight(newHeight);
        return true;
    }
}

#endif