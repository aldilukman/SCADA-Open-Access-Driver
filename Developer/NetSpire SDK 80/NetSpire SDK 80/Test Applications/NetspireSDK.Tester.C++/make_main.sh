#!/bin/sh
rm ./main
g++ -I. -IOAIncludes -L. -LOALib -o main -DPTHREAD PIController.cpp main.cpp -laudioserver -lpthread

#/usr/local/android/ndk_toolchain/bin/arm-linux-androideabi-g++ -ggdb -static -I. -IOAIncludes -L. -LOALib -o main -DPTHREAD -DNO_SIGNALS PIController.cpp main.cpp -laudioserver
#/usr/local/android/ndk_toolchain/bin/arm-linux-androideabi-strip ./main

