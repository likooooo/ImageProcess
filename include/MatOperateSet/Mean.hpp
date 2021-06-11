#ifndef ADD_HPP
#define ADD_HPP

#include "MatOperateDef.hpp"
#include <Mat2D.hpp>

namespace MatOperateSet
{
    bool Mean(IN Mat2D<BYTE>* mat,OUT BYTE* mean)
    {
        mean = 0;
        BYTE* p = mat->Scan0;
        mean = 0;
        for(int i = 0; i < mat->GetLength(); i++,p++)
        {
            mean += *p;
        }
        return true;
    }
}

#endif