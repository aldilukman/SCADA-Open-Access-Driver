
#ifndef _LMUTEX_H
#define _LMUTEX_H

#include <assert.h>

#ifdef WIN32
#ifndef WAIT_FAILED
#error  "one of the following include files must be included before osPoll.h file ace.h, windows.h or afx.h"
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
#error "thread type not define please #define the thread type eg WIN32, UNIXWARE2, PTHREAD"
#endif

class oaMutexBase
{
public:

    oaMutexBase() {};
    virtual void enter() = 0;
	virtual  void leave() = 0;
	virtual int init() = 0;
	virtual void assertLocked(const char* debugStr = 0) {  };

    virtual ~oaMutexBase() {};
};

class oaNullMutex : public oaMutexBase
{
public:
    oaNullMutex() {};
    virtual void enter() {};
    virtual void leave() {};
    virtual int init();

    virtual ~oaNullMutex() {};
};

class oaLocalMutex : public oaMutexBase
{
public:
	virtual void activateMutex();
	virtual void enter();
	virtual  void leave();
	oaLocalMutex(bool bActive = true) :bMutexHeld(0), bMutexInited(0), _bActive(bActive) {};
	virtual int init();
	virtual void assertLocked(const char* debugStr = 0);

    virtual ~oaLocalMutex();

private:

#ifdef WIN32
	CRITICAL_SECTION _ourCriticalSection;
#elif defined UNIXWARE2
	mutex_t _ourMutex;
#elif defined PTHREAD
    pthread_mutex_t _ourMutex;
#else 
#error
#endif

int bMutexHeld;
int bMutexInited;

bool _bActive;

};

class MutexLock
{
public:
	MutexLock(oaMutexBase* pMutex) { _pMutex = pMutex; _pMutex->enter(); };
	MutexLock(oaMutexBase& RefMutex) { _pMutex = &RefMutex; _pMutex->enter(); };
	~MutexLock() { _pMutex->leave(); };

private:
	oaMutexBase* _pMutex;
};

#endif
