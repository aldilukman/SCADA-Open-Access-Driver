#!/bin/sh
rm -rf *.class
javac -cp ./as.jar main3.java
java -verbose:jni -cp ".:./as.jar" main3

