#ifndef _STANDARD_INCLUDES_HPP_
#define _STANDARD_INCLUDES_HPP_

#include <map>
#include <list>
#include <set>
#include <stdexcept>
#include <string>
#include <vector>
#include <iostream>
#include <sstream>
#include <stdexcept>
#include <algorithm>
#include <stdio.h>
#include <string.h>
#include <stdlib.h>

#ifdef WIN32
#include "windows.h"

// Multiple header files have function declarations which indicate that the function throws some exception.
// A function declared using exception specification, which Visual C++ accepts but does not implement.
// Code with exception specifications that are ignored during compilation may need to be recompiled and
// linked to be reused in future versions supporting exception specifications.
// When compliling, the following warning is shown - warning C4290: C++ exception specification ignored except to indicate a function is not __declspec(nothrow)
// however, we will disable this warning by using the following pragma.
#pragma warning(disable : 4290)
#endif
#ifndef WIN32
#include <sys/ioctl.h>
#include <net/if.h>
#include <netinet/in.h>
#include <stdio.h>
#include <arpa/inet.h>
#endif
#include "threadClass.h"
#include "localMutex.h"
#include "dvalist.h"
#include "termInfo.h"
#include "callInfo.h"
#include "priorityInfo.h"

#endif // _STANDARD_INCLUDES_HPP_
