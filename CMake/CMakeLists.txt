# 编译c++
## refrence  : https://zhuanlan.zhihu.com/p/59161370
## 指定编译器 : cmake CmakeList.txt -G "Visual Studio 8 2005" 
cmake_minimum_required(VERSION 3.0.0)
project(ImageCore VERSION 0.1.0)
## Release模式设置生成pdb
set(CMAKE_CXX_FLAGS_RELEASE "${CMAKE_CXX_FLAGS_RELEASE} /Zi")
set(CMAKE_EXE_LINKER_FLAGS_RELEASE "${CMAKE_EXE_LINKER_FLAGS_RELEASE} /DEBUG /OPT:REF /OPT:ICF")

include(ImageCore.cmake)
# # 1. 设置编译变量
# set(CPP_LIB_NAME ImageCore)
# set(INCLUDE_PATH ../include)
# set(SRC_PATH ../src/cpp)
# set(UNITS_TEST_PATH ../src/cpp/UnitsTest)
# set(RELEASE_PATH ../releases)
# # 2. 设置输出路径
# set(LIBRARY_OUTPUT_PATH  ${RELEASE_PATH})
# set(EXECUTABLE_OUTPUT_PATH ${RELEASE_PATH})
# # 3. 添加文件目录
# include_directories(${INCLUDE_PATH})
# link_directories(${RELEASE_PATH}) #添加动态连接库的路径
# link_directories(../third-party) #添加动态连接库的路径
# # 4.1 编译ImageCore
# file(GLOB SRC_LIST_C ${SRC_PATH}/*.c)
# file(GLOB SRC_LIST_CPP ${SRC_PATH}/*.cpp)
# add_library(${CPP_LIB_NAME} ${SRC_LIST_C} ${SRC_LIST_CPP})
# # 4.2 编译单元测试
# file(GLOB SRC_LIST_C ${UNITS_TEST_PATH}/*.c)
# file(GLOB SRC_LIST_CPP ${UNITS_TEST_PATH}/*.cpp)
# add_executable(UNIT_TEST ${SRC_LIST_C} ${SRC_LIST_CPP})

# 设置可执行文件的输出路径


# #添加连接库
#     # 添加lib子目录
#     add_subdirectory(lib)
#     # 枚举指定目录下的源文件，并添加到变量DIR_LIB_SRCS中
#     aux_source_directory(. DIR_LIB_SRCS)
#     # 将源文件枚举变量添加到LIB_SRCS库文件中
#     add_library(LIB_SRCS ${DIR_LIB_SRCS})
# target_link_libraries(${PROJECT_NAME} LIB_SRCS)

# add_executable(HelloCMake main.cpp)


