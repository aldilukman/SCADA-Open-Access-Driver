#!/bin/sh
rm -rf *.class
javac -cp ./as.jar main.java
java -verbose:jni -cp ".:./as.jar" main

