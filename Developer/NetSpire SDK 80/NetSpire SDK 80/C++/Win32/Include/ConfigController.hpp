#ifndef _CONFIG_CONTROLLER_HPP_
#define _CONFIG_CONTROLLER_HPP_

#include "StandardIncludes.hpp"

class AudioServer;
class AudioServerObserver;

class CFG_MANAGER_RESOURCE_INFO
{
public:
	CFG_MANAGER_RESOURCE_INFO()
	{
		this->resInstanceId = 0;
		this->version.clear();
		this->configurationStreamIndex = 0;
		this->configKeyValueList.clear();
	}

	int resInstanceId;
	std::string version;
	int configurationStreamIndex;
	std::map<std::string, std::string> configKeyValueList;
};

class ConfigController
{
public:
	ConfigController();

	~ConfigController();

	void updateEntryInCfgManager(const std::string &deviceId, const std::string &resId, const std::string &key, const std::string &value);

	void deleteEntryInCfgManager(const std::string &deviceId, const std::string &resId, const std::string &key);

	void commitChangesToCfgManager(const std::string &deviceId, const std::string &resId, const std::string &commitDescription);

	void updateUserData(int elemId, int key, const std::string &msgArgs);

	void deleteUserData(int elemId, int key);

	// ===================================================
	// Following 2 functions are deprecated and are
	// used by MEA only
	void updateUserData1(int key, const std::string &msgArgs);
	void deleteUserData1(int key);
	// ===================================================

	// HELPER-FUNCTIONS
	// Following functions will be filtered by SWIG. These need to be explictly
	// declared in the csharp.i and java.i.
	// We need them to be public for ease of implementation but don't wish to 
	// expose them to the client/end-user.
	bool configurationSubscriptionRequired();
	void addDeviceId(const std::string &deviceId_And_ListOfResources);
	void initConfigCache();
	void versionUpdate(int elemNo, DVAlist *elemArgList);
	void informationUpdate(int elemNo, DVAlist *elemArgList);
	void configurationUpdate(int elemNo, DVAlist *elemArgList);
	std::string getCampaignUploadDirectory();
	std::string getVersion(const std::string &_deviceId, const std::string &_resId);
	void getConfiguration(const std::string &_deviceId, const std::string &_resId, KeyValueMap &_configList);

private:
	oaLocalMutex configControllerMtx;
	std::string ippaDeviceId;
	std::map<std::string, std::map<std::string, CFG_MANAGER_RESOURCE_INFO> > deviceConfigurationList;		// maps deviceId -> map of resource configuration (key = resId)
	std::string campaignUplodDirectory;
};

#endif // _CONFIG_CONTROLLER_HPP_
