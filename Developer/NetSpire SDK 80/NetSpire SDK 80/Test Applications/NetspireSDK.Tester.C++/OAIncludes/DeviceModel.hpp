#ifndef _DEVICE_MODEL_HPP_
#define _DEVICE_MODEL_HPP_

#include "StandardIncludes.hpp"

class DeviceModel
{
public:
	DeviceModel(const std::string &modelName = "Unknown", const std::string &modelRevision = "0.0", int digitalInputCnt = 0, int digitalOutputCnt = 0, int audioOutputChannels = 0);

	DeviceModel(const DeviceModel &deviceModelAnother);

	~DeviceModel();

	std::string getName() const;								//

	std::string getRevision() const;							//

	int getDigitalInputCount() const;							// return number of DIns

	int getDigitalOutputCount() const;							// return number of DOuts

	int getAudioOutputChannels() const;							// return number of audio output channels

	// HELPER-FUNCTIONS
	// Following functions will be filtered by SWIG. These need to be explictly
	// declared in the csharp.i and java.i.
	// We need them to be public for ease of implementation but don't wish to 
	// expose them to the client/end-user.
	void updateDeviceModelParameters(const std::string &modelName, const std::string &modelRevision, int digitalInputCnt, int digitalOutputCnt, int audioOutputChannels);

private:
	std::string name;
	std::string revision;
	int digitalInputCnt;
	int digitalOutputCnt;
	int audioOutputChannels;
};

#endif // _DEVICE_MODEL_HPP_
