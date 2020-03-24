#ifndef _AUDIO_SERVER_OBSERVER_HPP_
#define _AUDIO_SERVER_OBSERVER_HPP_

#include "StandardIncludes.hpp"

/**
* <p>The AudioServerObserver provides an interface to observe changes
* in the audio system including device status changes.
* </p>
*/
class AudioServerObserver
{
public:
	// Default Constructor
	AudioServerObserver()
	{
	}

	// Callback function - called when connection to the system is established.
	// This method will be executed in a separate thread created by the SDK library.
	// @param device Reference to the device that has changed state
	virtual void onCommsLinkUp()
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "AudioServerObserver::onCommsLinkUp()");
#else
		std::cout << "AudioServerObserver::onCommsLinkUp()" << std::endl;
#endif
	}

	// Callback function - called when connection to the system is lost.
	// This method will be executed in a separate thread created by the SDK library.
	// @param device Reference to the device that has changed state
	virtual void onCommsLinkDown()
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "AudioServerObserver::onCommsLinkDown()");
#else
		std::cout << "AudioServerObserver::onCommsLinkDown()" << std::endl;
#endif
	}

	// Callback function - called when connection to the system is established.
	// This method will be executed in a separate thread created by the SDK library.
	// @param device Reference to the device that has changed state
	virtual void onAudioConnected()
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "AudioServerObserver::onAudioConnected()");
#else
		std::cout << "AudioServerObserver::onAudioConnected()" << std::endl;
#endif
	}

	// Callback function - called when connection to the system is lost.
	// This method will be executed in a separate thread created by the SDK library.
	// @param device Reference to the device that has changed state
	virtual void onAudioDisconnected()
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "AudioServerObserver::onAudioDisconnected()");
#else
		std::cout << "AudioServerObserver::onAudioDisconnected()" << std::endl;
#endif
	}

	// Callback function - called when a Device state is updated.
	// This method will be executed in a separate thread created by the SDK library.
	// @param device Reference to the device that has changed state
	virtual void onDeviceStateChange(Device device)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "AudioServerObserver::onDeviceStateChange(): deviceId >%s<", device.getName().c_str());
#else
		std::cout << "AudioServerObserver::onDeviceStateChange(): deviceId >" << device.getName() << "<" << std::endl;
#endif
	}

	// Callback function - called when a Device is removed from the system.
	// This method will be executed in a separate thread created by the SDK library.
	// @param device Reference to the device that has changed state
	virtual void onDeviceDelete(Device device)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "AudioServerObserver::onDeviceDelete(): deviceId >%s<", device.getName().c_str());
#else
		std::cout << "AudioServerObserver::onDeviceDelete(): deviceId >" << device.getName() << "<" << std::endl;
#endif
	}

	// Callback function - called when VU level is updated during PA
	// This method will be executed in a separate thread created by the SDK library.
	// @param level of input as received from the DSP
	virtual void onVUMeterUpdate(int level)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "AudioServerObserver::onVUMeterUpdate()");
#else
		std::cout << "AudioServerObserver::onVUMeterUpdate()" << std::endl;
#endif
	}

	// Callback function - called when update is received from resCfgManager
	// This method will be executed in a separate thread created by the SDK library.
	virtual void onConfigUpdate(std::string deviceId, std::string resId, int instanceId, std::string progress, std::string key, std::string value)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "AudioServerObserver::onConfigUpdate(): deviceId >%s< resId >%s< instanceId >%d< progress >%s< key >%s< value >%s<", deviceId.c_str(), resId.c_str(), instanceId, progress.c_str(), key.c_str(), value.c_str());
#else
		std::cout << "AudioServerObserver::onConfigUpdate(): deviceId >" << deviceId << "< resId >" << resId << "< instanceId >" << instanceId << "< progress >" << progress << "< key >" << key << "< value >" << value << "<" << std::endl;
#endif
	}

	/**
	* ========= NOTE: this callback is deprecated and should not be used =========
	* Callback function - called when update is received from resDataManager
	* This method will be executed in a separate thread created by the SDK library.
	*/
	virtual void onStateUpdate(bool isImportData, int key, std::string value)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "AudioServerObserver::onStateUpdate()");
#else
		std::cout << "AudioServerObserver::onStateUpdate()" << std::endl;
#endif
	}

	// Callback function - called when update is received from resDataManager
	// This method will be executed in a separate thread created by the SDK library.
	virtual void onUserDataUpdate(int elemId, int key, std::string value)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "AudioServerObserver::onUserDataUpdate()");
#else
		std::cout << "AudioServerObserver::onUserDataUpdate()" << std::endl;
#endif
	}

	// Callback function - called when an item is deleted from resDataManager
	// This method will be executed in a separate thread created by the SDK library.
	virtual void onUserDataDelete(int elemId, int key)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "AudioServerObserver::onUserDataDelete()");
#else
		std::cout << "AudioServerObserver::onUserDataDelete()" << std::endl;
#endif
	}

	// Callback function - 
	// This method will be executed in a separate thread created by the SDK library.
	virtual void onDebugMessage(std::string msg)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "AudioServerObserver::onDebugMessage()");
#else
		std::cout << "AudioServerObserver::onDebugMessage()" << std::endl;
#endif
	}

	// Callback function - 
	// This method will be executed in a separate thread created by the SDK library.
	virtual void updateIPPAOnDictionaryUpdate(std::string dictionaryStateStr)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "AudioServerObserver::updateIPPAOnDictionaryUpdate()");
#else
		std::cout << "AudioServerObserver::updateIPPAOnDictionaryUpdate()" << std::endl;
#endif
	}

	// Callback function - 
	// This method will be executed in a separate thread created by the SDK library.
	virtual void updateIPPAOnICIDigitalInputStateChange(int inputNo, bool active)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "AudioServerObserver::updateIPPAOnICIDigitalInputStateChange()");
#else
		std::cout << "AudioServerObserver::updateIPPAOnICIDigitalInputStateChange()" << std::endl;
#endif
	}

	// Callback function - 
	// This method will be executed in a separate thread created by the SDK library.
	virtual void updateIPPAOnPreviewSpeakerStateChange(bool active)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "AudioServerObserver::updateIPPAOnPreviewSpeakerStateChange()");
#else
		std::cout << "AudioServerObserver::updateIPPAOnPreviewSpeakerStateChange()" << std::endl;
#endif
	}

	// Callback function - 
	// This method will be executed in a separate thread created by the SDK library.
	virtual void updateIPPAOnStreamingServerStateChange(bool streaming)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "AudioServerObserver::updateIPPAOnStreamingServerStateChange()");
#else
		std::cout << "AudioServerObserver::updateIPPAOnStreamingServerStateChange()" << std::endl;
#endif
	}

	// Callback function - called when a new alarm is added/updated
	// This method will be executed in a separate thread created by the SDK library.
	virtual void onAlarmUpdate(Alarm alarm)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "AudioServerObserver::onAlarmUpdate(): key=%d", alarm.key);
#else
		std::cout << "AudioServerObserver::onAlarmUpdate(): key=" << alarm.key << std::endl;
#endif
	}

	// Callback function - called when a new alarm is deleted
	// This method will be executed in a separate thread created by the SDK library.
	virtual void onAlarmDelete(Alarm alarm)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "AudioServerObserver::onAlarmDelete(): key=%d", alarm.key);
#else
		std::cout << "AudioServerObserver::onAlarmDelete(): key=" << alarm.key << std::endl;
#endif
	}

	/// Callback function - called when deleting the observer object
	virtual ~AudioServerObserver()
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "AudioServerObserver::~AudioServerObserver()");
#else
		std::cout << "AudioServerObserver::~AudioServerObserver()" << std::endl;
#endif
	}
};

#endif	// _AUDIO_SERVER_OBSERVER_HPP_
