#ifndef _FTP_WORKERTHREAD_HPP_
#define _FTP_WORKERTHREAD_HPP_

#include "StandardIncludes.hpp"
#include "Device.hpp"

class FTPWorkerThread: public threadClass
{
public:
	bool ftpIsBusy;
	std::string fileFullPathAndName;
	Device *targetServer;
	int threadFunction(void *param);
};

#endif // _FTP_WORKERTHREAD_HPP_
