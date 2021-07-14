# 1. ���ñ������
set(INCLUDE_PATH ../include)
set(SRC_PATH ../src)
set(UNITS_TEST_PATH ../src/UnitsTest)
set(RELEASE_PATH ../releases)
# 2. �������·��
set(LIBRARY_OUTPUT_PATH  ${RELEASE_PATH})
set(EXECUTABLE_OUTPUT_PATH ${RELEASE_PATH})
# 3. ����ļ�Ŀ¼
include_directories(${INCLUDE_PATH})
# link_directories(${RELEASE_PATH}) #��Ӷ�̬���ӿ��·��
# link_directories(../third-party) 
# 4.1 ����ImageCore
file(GLOB SRC_LIST_HPP ${INCLUDE_PATH}/*.hpp)
file(GLOB SRC_LIST_CPP ${SRC_PATH}/*.cpp)
add_library(${PROJECT_NAME} ${SRC_LIST_HPP} ${SRC_LIST_CPP})
# 4.2 ���뵥Ԫ����
file(GLOB SRC_LIST_HPP ${UNITS_TEST_PATH}/*.hpp)
file(GLOB SRC_LIST_CPP ${UNITS_TEST_PATH}/*.cpp)
add_executable(${PROJECT_NAME}_Test1 ${SRC_LIST_HPP} ${SRC_LIST_CPP})

# ���ÿ�ִ���ļ������·��


# #������ӿ�
#     # ���lib��Ŀ¼
#     add_subdirectory(lib)
#     # ö��ָ��Ŀ¼�µ�Դ�ļ�������ӵ�����DIR_LIB_SRCS��
#     aux_source_directory(. DIR_LIB_SRCS)
#     # ��Դ�ļ�ö�ٱ�����ӵ�LIB_SRCS���ļ���
#     add_library(LIB_SRCS ${DIR_LIB_SRCS})
# target_link_libraries(${PROJECT_NAME} LIB_SRCS)

# add_executable(HelloCMake main.cpp)


