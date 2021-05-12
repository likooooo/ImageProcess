using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ImageProcess
{
    public static class MemoryOperateSet
    {
        /*非托管内存之间Copy*/
        [DllImport("msvcrt.dll", EntryPoint = "memcpy",CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        public static extern IntPtr memcpy(IntPtr dest, IntPtr src, UIntPtr count);
                
        /*标准仿C memcpy函数*/ 
        unsafe public static void* memcpy(void* dst, void* src, int count)
        {
            System.Diagnostics.Debug.Assert(dst != null);
            System.Diagnostics.Debug.Assert(src != null);
        
            void* ret = dst;
        
            /*
            * copy from lower addresses to higher addresses
            */
            while (count-- > 0)
            {
                *(char*)dst = *(char*)src;
                dst = (char*)dst + 1;
                src = (char*)src + 1;
            }
        
            return (ret);
        }
        unsafe public static T* memcpy<T>(T* dst, T* src, int count) where T:unmanaged
        {
            System.Diagnostics.Debug.Assert(dst != null);
            System.Diagnostics.Debug.Assert(src != null);
        
            T* ret = dst;
        
            /*
            * copy from lower addresses to higher addresses
            */
            while (count-- > 0)
            {
                *dst = *src;
                dst++;
                src++;
            }
        
            return (ret);
        }
        
        /*标准仿C memmove函数*/
        unsafe public static void* memmove(void* dst, void* src, uint count)
        {
            System.Diagnostics.Debug.Assert(dst != null);
            System.Diagnostics.Debug.Assert(src != null);
        
            void* ret = dst;
        
            if (dst <= src || (char *)dst >= ((char *)src + count))
            {
                while (count-- > 0)
                {
                    *(char*)dst = *(char*)src;
                    dst = (char*)dst + 1;
                    src = (char*)src + 1;
                }
            }
            else
            {
                dst = (char *)dst + count - 1;
                src = (char *)src + count - 1;
                while (count-- > 0)
                {
                    *(char*)dst = *(char*)src;
                    dst = (char*)dst - 1;
                    src = (char*)src - 1;
                }
            }
        
            return(ret);
        }
        
        /*标准仿C memset函数*/ 
        unsafe public static void* memset(void* s, /*int*/byte c, uint n)
        {
            byte* p = (byte*)s;
        
            while (n > 0)
            {
                *p++ = c;
                --n;
            }
        
            return s;
        }
    
        
        unsafe public static bool memcpy<T_In,T_Out>(in T_In[] src,out T_Out[] dest) where T_In :unmanaged ,IEquatable<T_In> where T_Out:unmanaged,IEquatable<T_Out>
        {
            int T_OutLength = src.Length * sizeof(T_In)/sizeof(T_Out);
            Span<T_Out> spanDest = stackalloc T_Out[T_OutLength];
    
            fixed(T_In* ptrSrc = &src[0])
            fixed(T_Out* ptrDest = &spanDest[0])
            {
                T_Out* ptrSrc1  = (T_Out*)ptrSrc;
                T_Out* ptrDest1 = ptrDest;
                while(T_OutLength-- > 0)
                {
                    *ptrDest1++ = *ptrSrc1++;
                }
                
            }

            dest = spanDest.ToArray();
            return true;
        }
    }
}