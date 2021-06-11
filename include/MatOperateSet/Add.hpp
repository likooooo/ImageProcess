#ifndef ADD_HPP
#define ADD_HPP

#include "MatOperateDef.hpp"
#include <Mat2D.hpp>

namespace MatOperateSet
{
    template<typename T>
    bool AddOffset(IN_OUT Mat2D<T>* mat, IN T offset)
    {
        T* p = mat->Scan0;
        for(int i = 0; i < mat->GetLength(); i++,p++)
        {
            *p += offset;
        }
        return true;
    }
}

#endif