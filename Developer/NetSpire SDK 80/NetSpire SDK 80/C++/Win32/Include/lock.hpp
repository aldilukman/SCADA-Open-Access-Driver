#ifndef _AUTOLOCK_HPP_
#define _AUTOLOCK_HPP_

#include "StandardIncludes.hpp"

static bool _autoLockStarted = false;
static oaLocalMutex mtx;

class AutoLock
{
public:
	AutoLock()
	{
		if( !_autoLockStarted )
		{
			mtx.init();
			_autoLockStarted = true;
		}
		mtx.enter();
	}

	~AutoLock()
	{
		mtx.leave();
	}
};

#endif // _AUTOLOCK_HPP_
