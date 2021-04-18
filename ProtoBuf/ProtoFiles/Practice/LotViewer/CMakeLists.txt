cmake_minimum_required(VERSION 3.19)
project(LotViewerProject)

find_package(Protobuf CONFIG REQUIRED)

include_directories(./Inc/; c:/vcpkg/packages/protobuf/x64-windows/include/)

set_property(GLOBAL PROPERTY USE_FOLDERS ON)

set(CMAKE_CXX_STANDARD 11)

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

if(CMAKE_BUILD_TYPE STREQUAL "Debug")
    message(STATUS "Using static debug library for protobuf.")
    set(GENERATED_PROTOBUF_FOLDER cmake-build-debug)
    configure_file(c:/vcpkg/packages/protobuf_x64-windows/debug/bin/libprotobufd.dll libprotobufd.dll COPYONLY)
else()
    message(STATUS "adding static release library for protobuf.")
    set(GENERATED_PROTOBUF_FOLDER cmake-build-release)
endif()



add_executable(LotViewer Src/App/main.cpp Src/ProtoBuf/Practice.pb.cc Inc/App/Reader.h Src/App/Reader.cpp)
target_link_libraries(LotViewer ${PROTOBUF_LIB})