#ifndef HISTOGRAM_HPP
#define HISTOGRAM_HPP

#include "MatOperateDef.hpp"
#include <Mat2D.hpp>

#define HISTOGRAM_LENGTH 256

namespace MatOperateSet
{
    /*归一化*/
    void NormalizedHistogram(IN Mat2D<BYTE> *mat, OUT double hist[HISTOGRAM_LENGTH])
    {
        memset(hist, 0, sizeof(hist[0]) * HISTOGRAM_LENGTH);
        BYTE *p = mat->Scan0;
        for(int i = 0; i < mat->GetLength(); i++,p++)
        {
            hist[(int)(*p)]++;
        }
        int N = mat->GetHeight() * mat->GetWidth();
        for(int i = 0; i < HISTOGRAM_LENGTH; i++)
        {
            hist[i] /= N;
        }
    }

    void EqualizeHistogram(IN Mat2D<BYTE> *mat, OUT double piexlMap[HISTOGRAM_LENGTH]) /* https://blog.csdn.net/qq_15971883/article/details/88699218 */
    {
        double normalized[HISTOGRAM_LENGTH];
        NormalizedHistogram(mat, normalized);/* 计算分布频率 */
        
        double cdf[HISTOGRAM_LENGTH] = {normalized[0]};/*累积分布频率*/
        piexlMap[0]                  = {cdf[0] * (HISTOGRAM_LENGTH - 1)};/*index->piexlMap[index] : 新老颜色的映射*/
        for(int i = 1; i < HISTOGRAM_LENGTH; i++)
        {
            cdf[i] = cdf[i - 1] + normalized[i];
            piexlMap[i] = cdf[i] * (HISTOGRAM_LENGTH - 1);
        }
    }

    void EqualizeHistogram(IN Mat2D<BYTE> *mat) /* https://blog.csdn.net/qq_15971883/article/details/88699218 */
    {
        double pixelMap[HISTOGRAM_LENGTH];
        EqualizeHistogram(mat, pixelMap);
        BYTE*p = mat->Scan0;
        for(int i = 0; i < mat->GetLength();i++, p++)
        {
            *p = (BYTE)pixelMap[*p];
        }
    }

    void PrintHistogram(double hist[HISTOGRAM_LENGTH], int min = 0,int max = HISTOGRAM_LENGTH)
    {
        if( max > HISTOGRAM_LENGTH )
        {
            max = HISTOGRAM_LENGTH;
        }
        for(int i = min; i < max; i++)
        {
            printf("(%d, %lf) \n", i, hist[i]);
        }
    }
}

#endif