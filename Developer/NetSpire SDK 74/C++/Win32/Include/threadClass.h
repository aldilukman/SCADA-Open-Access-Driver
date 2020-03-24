// portable thread class which is intended to be overloaded by something
// that actually does something!

// This thread class does not support detached threads.

#ifndef THREAD_CLASS_H
#define THREAD_CLASS_H

#ifdef WIN32
#ifndef WAIT_FAILED
#error  "one of the following include files must be included before threadClass.h file ace.h, windows.h or afx.h"
#endif
/* May be needed for gcc version >2.95 or egcs on UW7
#elif defined UNIXWARE7 && defined __GNUC__ && !defined _BOOLEAN_T
#define _BOOLEAN_T
typedef enum boolean { B_FALSE, B_TRUE } boolean_t;
*/
#elif defined UNIXWARE2
#include <synch.h>
#elif defined PTHREAD
#include <pthread.h>
#else
#error "thread type not defined please #define the thread type eg WIN32, UNIXWARE2, PTHREAD"
#endif


class threadClass
{
public:

	threadClass() :_bRunning(false), _stackSize(0) {};

	threadClass(int stackSize) :_bRunning(false), _stackSize(stackSize) {};

	bool start(void* parm); // will fail if thread is already running

	void terminateThread(); // only use to kill unresponsive thread.

	bool waitForDeathAndGetReturnCode(int& threadRetVal);

	bool hasGotValidThreadId(int& threadId);  //if true sets threadId.
	
	virtual ~threadClass();

	int threadFunctionInt()
	{
		return this->threadFunction(_parm);
	}
	virtual int threadFunction(void* parm) = 0; //overload this to do what you want

	static long getCurrentThreadId();


private:
	bool _bRunning;
	void* _parm;
	int  _stackSize;

	
#ifdef WIN32
	HANDLE _threadHandle;
	DWORD _threadId;
#elif defined UNIXWARE2
	thread_t _threadId;
#elif defined PTHREAD
	pthread_t _threadId;
#else
#error 
#endif


};

#endif
