cmake_minimum_required(VERSION 3.19)
project(LotViewer)

find_package(Protobuf CONFIG REQUIRED)

set(CMAKE_CXX_STANDARD 11)

include_directories(c:/vcpkg/packages/protobuf_x64/include)

if(MSVC AND protobuf_MSVC_STATIC_RUNTIME)
    foreach(flag_var
            CMAKE_CXX_FLAGS CMAKE_CXX_FLAGS_DEBUG CMAKE_CXX_FLAGS_RELEASE
            CMAKE_CXX_FLAGS_MINSIZEREL CMAKE_CXX_FLAGS_RELWITHDEBINFO)
        if(${flag_var} MATCHES "/MD")
            string(REGEX REPLACE "/MD" "/MT" ${flag_var} "${${flag_var}}")
        endif(${flag_var} MATCHES "/MD")
    endforeach()
endif()

set(PROTOBUF_LIB protobuf::libprotobuf)

add_executable(LotViewer main.cpp)