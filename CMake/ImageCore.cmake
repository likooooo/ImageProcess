# 1. 设置编译变量
set(INCLUDE_PATH ../include)
set(SRC_PATH ../src)
set(UNITS_TEST_PATH ../src/UnitsTest)
set(RELEASE_PATH ../releases)
# 2. 设置输出路径
set(LIBRARY_OUTPUT_PATH  ${RELEASE_PATH})
set(EXECUTABLE_OUTPUT_PATH ${RELEASE_PATH})
# 3. 添加文件目录
include_directories(${INCLUDE_PATH})
# link_directories(${RELEASE_PATH}) #添加动态连接库的路径
# link_directories(../third-party) 
# 4.1 编译ImageCore
file(GLOB SRC_LIST_HPP ${INCLUDE_PATH}/*.hpp)
file(GLOB SRC_LIST_CPP ${SRC_PATH}/*.cpp)
add_library(${PROJECT_NAME} ${SRC_LIST_HPP} ${SRC_LIST_CPP})
# 4.2 编译单元测试
file(GLOB SRC_LIST_HPP ${UNITS_TEST_PATH}/*.hpp)
file(GLOB SRC_LIST_CPP ${UNITS_TEST_PATH}/*.cpp)
add_executable(${PROJECT_NAME}_Test1 ${SRC_LIST_HPP} ${SRC_LIST_CPP})

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


