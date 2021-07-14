#ifndef THRESHOLD_HPP
#define THRESHOLD_HPP

#include "MatOperateDef.hpp"
#include "HistogramProcessing.hpp"
#include <Mat2D.hpp>
#include <vector>

namespace MatOperateSet
{
    int neighboorType = 8;
    bool IsNeighboor4(RLC origin, RLC n, int width) /* 四领域判断是否相邻 */
    {
        bool res = false;
        VALRET_ASSERT((width > n.index - origin.index), false); /* 不是相邻行*/
        /* 以下展示两种不相邻的状态*/
        /* 1:
        ---
            x----
        */
        if(origin.index + origin.length <= n.index - width)
        {
            return false;
        }
        /* 2:
            ----
        --x
        */
        if(origin.index >= n.index + n.length - width)
        {
            return false;
        }
        return true;
    }
    bool IsNeighboor8(RLC origin, RLC n, int width) /* 八领域判断是否相邻 */
    {
        bool res = false;
        VALRET_ASSERT((width > n.index - origin.index), false); /* 不是相邻行*/
        if(origin.index + origin.length <= n.index + 1 - width)
        {
            return false;
        }
        if(origin.index >= n.index + 1 + n.length - width)
        {
            return false;
        }
        return true;
    }

    bool Connection(IN std::vector<RLC>& rlcs, IN int imageWidth, OUT std::vector<std::vector<RLC>>& connectedRlcs)
    {
        int numOfRuns = 0;
        std::vector<RLC> currentRlcs;
        for(vector<RLC>::iterator it = rlcs.begin() + 1;it != rlcs.end();it++)
        {
            
        }
        // VALRET_ASSERT(BW_Label(mat, numOfRuns, rlcs), false);
        // for(int i = 0; i < numOfRuns; i++)
        // {
        //     for(int j = 0; i < i; j++)
        //     {
                
        //     }
        //     rlcs.at(i);
        // }
        return true;
    }

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

    bool Threshold(IN Mat2D<BYTE>* mat, IN int min, IN int max, OUT int& numOfRuns, OUT std::vector<RLC>& rlcs) /*计算游程编码，对标Halcon::Threshold*/
    {
        numOfRuns = 0;
        BYTE* src  = mat->Scan0;
        size_t loopCount = mat->GetLength() - 1;
        int index = 0;
        int matWidth = mat->GetWidth();
        RLC rlc(0, 0);
        /*1~(n - 1)个点*/
        while(index++ < loopCount)
        {
            if(0 == index%matWidth)/*行末，连续点需要强制截断*/
            {
                if(min <= *src && max >= *src)
                {
                    rlc.length++;
                    rlc.index = index - rlc.length;
                }
                else
                {             
                    rlc.index = index - 1 - rlc.length;
                }
                if(0 < rlc.length)
                {
                    rlcs.push_back(rlc);
                    numOfRuns++;
                    rlc.length = 0;
                }
                src++;
                continue;
            }
            if(min > *src || max < *src) 
            {
                if(rlc.length > 0)
                {
                    rlc.index = index - 1 - rlc.length;
                    rlcs.push_back(rlc);
                    numOfRuns++;
                    rlc.length = 0;
                }
                src++;
                continue;
            }
            rlc.length++;
            src++;
        }
        /*第n个点*/
        if(min <= *src && max >= *src)
        {
            rlc.length++;
        }     
        if(rlc.length > 0)
        {
            rlc.index = index - rlc.length;
            rlcs.push_back(rlc);
            numOfRuns++;
        }
        return true;
    }

    template<typename T>
    bool Threshold(IN_OUT Mat2D<T>* mat, int min, int max)
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
    bool Threshold(IN Mat2D<T>* mat, int min, int max, OUT Mat2D<unsigned char>* binary)
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

    bool ImageToBin(IN_OUT Mat2D<BYTE>* mat)
    {
        BYTE* p = mat->Scan0;
        size_t loopCount = mat->GetLength();
        VALRET_ASSERT((loopCount > 0), false);
        while(loopCount-- > 0)
        {
            if(*p > 0)
            {
                *p = 1;
            }
            p++;
        }
        return true;
    }
}

#endif