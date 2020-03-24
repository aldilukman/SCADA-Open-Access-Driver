#ifndef _CAMPAIGN_WORKERTHREAD_HPP_
#define _CAMPAIGN_WORKERTHREAD_HPP_

#include "StandardIncludes.hpp"

class CampaignWorkerThread: public threadClass
{
public:
	bool isBusy;
	std::string serverIPAddress;
	std::string opArgs;
	std::map<std::string, std::string> fileList;	// maps localFileWithPath to remoteFileWithPath
	int threadFunction(void *param);

	CampaignWorkerThread()
	{
		this->isBusy = false;
		this->serverIPAddress.clear();
		this->opArgs.clear();
		this->fileList.clear();
	}
};

#endif // _CAMPAIGN_WORKERTHREAD_HPP_
