#ifndef SCALA_HPP
#define SCALA_HPP

#include <Mat2D.hpp>

namespace MatOperateSet
{
    template<typename T>
    bool Scala(IN_OUT Mat2D<T>* mat, T rate)
    {
        T* p = mat->Scan0;
        for(int i = 0; i < mat->GetLength(); i++,p++)
        {
            *p *= rate;
        }
        return true;
    }
}

#endif