#ifndef COMPIERDEF_H
#define COMPIERDEF_H

#include <stdio.h>
#include <malloc.h>
#include <Windows.h>

/*¶ÏÑÔÊ¹ÄÜ*/
#define ASSERT_ENABLE
#ifdef ASSERT_ENABLE
#define VOIDRET_ASSERT(condtion)\
        do\
        {\
        if(!(condtion)){return;}\
        } while (0)
#define VALRET_ASSERT(condtion, retVal)\
        do\
        {\
            if(!condtion){return retVal;}\
        }while(0)
#define ERROR_ASSERT(condtion, errorIndex)\
        do\
        {\
            if(!condtion){throw errorIndex;}\
        }while(0)

#else
#define VOIDRET_ASSERT(condtion)
#define VALRET_ASSERT(condtion, retVal)
#endif


#define DECLARE_GET(Type, param)\
        private:\
        Type param;\
        public:\
        Type Get##param##(){return param;}\

#define DECLARE_GETSET(Type, param)\
        private:\
        Type param;\
        public:\
        Type Get##param##(){return param;}\
        void Set##param##(Type val){param = val;}

#define DECLARE_PTR(type, param)\
        public:\
            type param;\
            void Init##param## (int length)\
            {\
                if(NULL == param)\
                {\
                    param = (type)malloc(length);\
                }\
            }\
            void Dispose##param## ()\
            {\
                if(NULL != param)\
                {\
                    free(param);\
                    param = NULL;\
                }\
            }

#define DECLARE_TEMPLATE_PTR(param)\
        public:\
        T* param;\
        void Init##param## (int length)\
        {\
            if(NULL == param)\
            {\
                param = (T*)malloc(length);\
            }\
        }\
        void Dispose##param## ()\
        {\
            if(NULL != param)\
            {\
                free(param);\
                param = NULL;\
            }\
        }

        
#endif