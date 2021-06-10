#ifndef ERRORDEF_H
#define ERRORDEF_H

#include "CompierDef.h"
#include<map>
#include<string>

#ifdef ASSERT_ENABLE
#define ERROR_TABLE std::map<int, std::string> ErrorTable = \
        {\
                {},\
                {}\
        };

#endif

#endif