#ifndef _DICTIONARY_WORKERTHREAD_HPP_
#define _DICTIONARY_WORKERTHREAD_HPP_

#include "StandardIncludes.hpp"
#include "Device.hpp"

class DictionaryWorkerThread: public threadClass
{
public:
	Device::DictionaryUpdateStatus dictionaryStatus;
	bool dictionaryIsBusy;
	std::string dictionaryFullPathAndName;
	Device *targetServer;
	int threadFunction(void *param);
};

#endif // _DICTIONARY_WORKERTHREAD_HPP_
