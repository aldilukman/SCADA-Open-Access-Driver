#ifndef _FTP_WORKERTHREAD_HPP_
#define _FTP_WORKERTHREAD_HPP_

#include "StandardIncludes.hpp"
#include "Device.hpp"

class FTPWorkerThread: public threadClass
{
public:
	bool ftpIsBusy;
	Device *targetServer;
	std::string fileFullPathAndName;
	int threadFunction(void *param);

	FTPWorkerThread()
	{
		this->ftpIsBusy = false;
		this->targetServer = 0;
		this->fileFullPathAndName.clear();
	}
};

#endif // _FTP_WORKERTHREAD_HPP_
