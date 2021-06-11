#ifndef THRESHOLD_HPP
#define THRESHOLD_HPP

#include "MatOperateDef.hpp"
#include "HistogramProcessing.hpp"
#include <Mat2D.hpp>

namespace MatOperateSet
{
    bool AutoThreshold(IN Mat2D<BYTE>* mat, OUT int& thresholdVal)/*https://blog.csdn.net/liyuanbhu/article/details/49387483*/
    {
        double hist[HISTOGRAM_LENGTH];
        NormalizedHistogram(mat, hist);
        PrintHistogram(hist);
        double cdf[HISTOGRAM_LENGTH] = {hist[0]};   /* 累积分布函数 */
        double gpa[HISTOGRAM_LENGTH] = {0};         /* 0~i 加权平均加权平均 */
        for(int i = 1; i < HISTOGRAM_LENGTH; i++)
        {
            cdf[i] = hist[i - 1] + hist[i];
            gpa[i] = gpa [i - 1] + i * hist[i];
        }
        double mean = gpa[255];
        double icv  = 0;
        int icvIndex= 0;
        for(int i = 1; i < HISTOGRAM_LENGTH; i++)
        {
            double PA = hist[i];
            double PB = 1 - PA;
            if( PA > 0.001 && PA < 0.999)
            {
                double MA   = gpa[i] / PA;
                double MB   = mean - gpa[i]; 
                double _icv = PA * (MA - mean) * (MA - mean) + PB * (MB - mean) * (MB - mean);
                if(_icv > icv)
                {
                    icv      = _icv;
                    icvIndex = i;
                }
            }
        }
        thresholdVal = icvIndex;
        return true;
    }

    bool AutoThreshold(IN_OUT Mat2D<BYTE>* mat)
    {
        int split;
        if(!AutoThreshold(mat, split))
        {
            return false;
        }
        BYTE* p = mat->Scan0;
        for(int i = 0; i < mat->GetLength(); i++, p++)
        {
            if(*p < split)
            {
                *p = split;
            }
        }
        return true;
    }

    void GetICV()
    {

    }

    template<typename T>
    bool Threshold(IN_OUT Mat2D<T>* mat, T min, T max)
    {
        T* p  = mat->Scan0;
        for(int i = 0; i < mat->GetLength(); i++,p++)
        {
            if(min > *p || *p > max)
            {
                *p = 0;
            }
        }
        return true;
    }
    
    template<typename T>
    bool Threshold(IN Mat2D<T>* mat, T min, T max, OUT Mat2D<unsigned char>* binary)
    {
        binary = new Mat2D<unsigned char>(mat->GetWidth(), mat->GetHeight());
        T* src  = mat->Scan0;
        T* dest = binary->Scan0;
        for(int i = 0; i < mat->GetLength(); i++,src++,dest++)
        {
            *dest =  (min < *p && *p > max)? *p : 0; 
        }
        return true;
    }
}

#endif