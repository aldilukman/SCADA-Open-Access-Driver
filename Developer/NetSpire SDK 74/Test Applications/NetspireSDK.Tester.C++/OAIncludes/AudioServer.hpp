#ifndef _AUDIO_SERVER_HPP_
#define _AUDIO_SERVER_HPP_

#include "StandardIncludes.hpp"
#include "lock.hpp"
#include "Gain.hpp"
#include "Device.hpp"
#include "PAController.hpp"
#include "CallController.hpp"
#include "ConfigController.hpp"
#include "SignallingController.hpp"
#include "DictionaryWorkerThread.hpp"
#include "FTPWorkerThread.hpp"

// Globals
extern int gMaxServerWaitTimeSeconds;

// Class to store detailed information of each alarm.
class Alarm
{
public:
	int key;
	int almNo;
	int almDstNo;
	std::string almDstName;
	int almResourceId;
	std::string almResourceName;
	std::string almDeviceIndex;
	std::string almDeviceName;
	std::string almFirstUpdateTime;
	std::string almLastUpdateTime;
	int almRepeatCount;
	bool isAlmStateful;
	bool almCurrentState;
	int almLevel;
	bool almProtected;
	int almErrorNo;
	std::string almErrorText;
	bool isAlmAcknowledged;
	std::string almAckTimeStamp;
	std::string almAckUserEndPoint;
	std::string almAckUser;
	std::string almAckDescription;

	Alarm()
	{
		this->key = 0;
		this->almNo = 0;
		this->almDstNo = 0;
		this->almDstName.clear();
		this->almResourceId = 0;
		this->almResourceName.clear();
		this->almDeviceIndex.clear();
		this->almDeviceName.clear();
		this->almFirstUpdateTime.clear();
		this->almLastUpdateTime.clear();
		this->almRepeatCount = 0;
		this->isAlmStateful = false;
		this->almCurrentState = false;
		this->almLevel = 0;
		this->almProtected = false;
		this->almErrorNo = 0;
		this->almErrorText.clear();
		this->isAlmAcknowledged = false;
		this->almAckTimeStamp.clear();
		this->almAckUserEndPoint.clear();
		this->almAckUser.clear();
		this->almAckDescription.clear();
	}

	Alarm(const Alarm &alarmAnother)
	{
		this->key = alarmAnother.key;
		this->almNo = alarmAnother.almNo;
		this->almDstNo = alarmAnother.almDstNo;
		this->almDstName = alarmAnother.almDstName;
		this->almResourceId = alarmAnother.almResourceId;
		this->almResourceName = alarmAnother.almResourceName;
		this->almDeviceIndex = alarmAnother.almDeviceIndex;
		this->almDeviceName = alarmAnother.almDeviceName;
		this->almFirstUpdateTime = alarmAnother.almFirstUpdateTime;
		this->almLastUpdateTime = alarmAnother.almLastUpdateTime;
		this->almRepeatCount = alarmAnother.almRepeatCount;
		this->isAlmStateful = alarmAnother.isAlmStateful;
		this->almCurrentState = alarmAnother.almCurrentState;
		this->almLevel = alarmAnother.almLevel;
		this->almProtected = alarmAnother.almProtected;
		this->almErrorNo = alarmAnother.almErrorNo;
		this->almErrorText = alarmAnother.almErrorText;
		this->isAlmAcknowledged = alarmAnother.isAlmAcknowledged;
		this->almAckTimeStamp = alarmAnother.almAckTimeStamp;
		this->almAckUserEndPoint = alarmAnother.almAckUserEndPoint;
		this->almAckUser = alarmAnother.almAckUser;
		this->almAckDescription = alarmAnother.almAckDescription;
	}

	void acknowledgeAlarm(const std::string &endPt, const std::string &operatorName, const std::string &ackDescription) throw (std::invalid_argument);
};

// DictionaryChangesetItem
class DictionaryChangesetItem
{
public:
	int key;
	int versionSeq;
	std::string operationId;
	int newVersion;
	int itemNo;
	std::string deviceType;
	std::string description;
	std::string category;
	std::string audioSegment;
	std::string image;
	std::string video;
	std::string displayText;
	std::string metadata;
	int lastUpdate;

	// Default Constructor
	DictionaryChangesetItem()
	{
		this->key = 0;
		this->versionSeq = 0;
		this->operationId.clear();
		this->newVersion = 0;
		this->itemNo = 0;
		this->deviceType.clear();
		this->description.clear();
		this->category.clear();
		this->audioSegment.clear();
		this->image.clear();
		this->video.clear();
		this->displayText.clear();
		this->metadata.clear();
		this->lastUpdate = 0;
	};
};

// DictionaryItem
class DictionaryItem
{
public:
	int itemNo;
	std::string description;
	std::string category;
	std::string displayText;
	std::string fileListStr;
	std::vector<std::string> fileList;
	std::string metadata;

	// Default Constructor
	DictionaryItem()
	{
		this->itemNo = 0;
		this->description.clear();
		this->category.clear();
		this->displayText.clear();
		this->fileListStr.clear();
		this->fileList.clear();
		this->metadata.clear();
	}

	// Requests access to the media contents stored in the specified Dictionary Item.
	// Each DictionaryItem can contain zero or more media files.The public attribute fileList contains the list of media files stored for the DicitonaryItem.
	// This method returns a list of HTTP URIs for each media file stored for the DictionaryItem. The list can have zero or more entries.
	// RETURNS: std::vector<std::string> a list of HTTP URIs for each media file stored for the DictionaryItem.The list can have zero or more entries.
	// The URL to be used will be:
	// -> http://<cxs_ip_address>/dictionary/audio/<audioFileName>
	// -> http://<cxs_ip_address>/dictionary/video/<videoFileName>
	// -> http://<cxs_ip_address>/dictionary/image/<imageFileName>
	std::vector<std::string> getContentsURI() const;
};

class AUDIOSERVER_INFO
{
public:
	std::vector<Device *> deviceList;
	bool isMaster;
	std::map<std::string, std::string> dynamicConfigurationVars;
	std::string systemRevision;
	bool systemRevisionConsistent;
	AUDIOSERVER_INFO()
	{
		this->deviceList.clear();
		this->isMaster = false;
		this->dynamicConfigurationVars.clear();
		this->systemRevision.clear();
		this->systemRevisionConsistent = false;
	}
};

// Event Item
class EventItem
{
public:
	EventItem() { }

	~EventItem() { }

	enum EVENT_TYPE
	{
		COMMS_LINK_UP = 0,
		COMMS_LINK_DOWN,
		AUDIO_SERVER_CONNECTED,
		AUDIO_SERVER_DISCONNECTED,
		DEVICE_STATE_CHANGED,
		DEVICE_DELETED,
		PA_SOURCE_UPDATED,
		PA_SOURCE_DELETED,
		PA_SINK_UPDATED,
		PA_SINK_DELETED,
		PA_TRIGGER_UPDATED,
		PA_TRIGGER_DELETED,
		PA_SELECTOR_UPDATED,
		PA_SELECTOR_DELETED,
		CALL_UPDATED,
		CALL_DELETED,
		CDR_MESSAGE_UPDATED,
		CDR_MESSAGE_DELETED,
		TERMINAL_UPDATED,
		TERMINAL_DELETED,
		SIPTRUNK_UPDATED,
		SIPTRUNK_DELETED,
		ISDNTRUNK_UPDATED,
		ISDNTRUNK_DELETED,
		PIS_SERVER_ONLINE,
		PIS_SERVER_OFFLINE,
		PIS_LINE_UPDATED,
		PIS_LINE_DELETED,
		PIS_VEHICLE_UPDATED,
		PIS_VEHICLE_DELETED,
		PIS_STATION_UPDATED,
		PIS_STATION_DELETED,
		PIS_PLATFORM_UPDATED,
		PIS_PLATFORM_DELETED,
		PIS_SERVICE_UPDATED,
		PIS_SERVICE_DELETED,
		PIS_TRIP_UPDATED,
		PIS_TRIP_DELETED,
		SCHEDULE_UPDATED,
		SCHEDULE_DELETED,
		PA_ZONE_UPDATED,
		PA_ZONE_DELETED
	};

	std::string dateTime;
	EVENT_TYPE type;
	int id;
	Device device;                  // to support eventType = DEVICE_STATE_CHANGED and DEVICE_DELETED
	PaSource paSource;              // to support eventType = PA_SOURCE_UPDATED and PA_SOURCE_DELETED
	PaSink paSink;                  // to support eventType = PA_SINK_UPDATED and PA_SINK_DELETED
	PaTrigger paTrigger;            // to support eventType = PA_TRIGGER_UPDATED and PA_TRIGGER_DELETED
	PaSelector paSelector;          // to support eventType = PA_SELECTOR_UPDATED and PA_SELECTOR_DELETED
	CallInfo callInfo;              // to support eventType = CALL_UPDATED and CALL_DELETED
	CDRInfo cdrInfo;                // to support eventType = CDR_MESSAGE_UPDATED and CDR_MESSAGE_DELETED
	TerminalInfo termInfo;          // to support eventType = TERMINAL_UPDATED and TERMINAL_DELETED
	SIPTrunk sipTrunk;              // to support eventType = SIPTRUNK_UPDATED and SIPTRUNK_DELETED
	ISDNTrunk isdnTrunk;            // to support eventType = ISDNTRUNK_UPDATED and ISDNTRUNK_DELETED
	Line pisLine;                   // to support eventType = PIS_LINE_UPDATED and PIS_LINE_DELETED
	Vehicle pisVehicle;             // to support eventType = PIS_VEHICLE_UPDATED and PIS_VEHICLE_DELETED
	Station pisStation;             // to support eventType = PIS_STATION_UPDATED and PIS_STATION_DELETED
	PlatformInfo pisPlatform;       // to support eventType = PIS_PLATFORM_UPDATED and PIS_PLATFORM_DELETED
	Service pisServices;            // to support eventType = PIS_SERVICE_UPDATED and PIS_SERVICE_DELETED
	Trip pisTrip;                   // to support eventType = PIS_TRIP_UPDATED and PIS_TRIP_DELETED
	ScheduleDefinition schedule;    // to support eventType = SCHEDULE_UPDATED and SCHEDULE_DELETED
	PaZone paZone;                  // to support eventType = PA_ZONE_UPDATED and PA_ZONE_DELETED
};

/**
* <p>The interface to the Audio Server is encapsulated by this AudioServer
* class.  It is to be instantiated on application start, and deleted
* on application completion.</p>
**/
class AudioServer
{
public:
	AudioServer ();

	~AudioServer ();

	// Specifies a role for a given location
	enum Role
	{
		NONE,    ///< Location disabled
		LEVEL1,  ///< Level 1 escalation
		LEVEL2   ///< Level 2 escalation
	};

	// Specifies the train certificate test state. CSS can only get this state via AudioServer::getTCTState() method.
	enum TCTState
	{
		TCT_IDLE,       // Idle, no train certification test initiated.
		TCT_STARTED,    // Started, train certification test ongoing.
		TCT_FINISHED    // Finished, train certification test has finished.
	};

	/**
	* A collection of information containing string to string mappings. Are
	* used in mapping device IDs and their IP addresses as wells as the key-value pairs sent to
	* connect.
	**/
	typedef std::map<std::string, std::string> KeyValueMap;

	/**
	* A collection of strings.
	* Note:
	* The "clear" method is required to be called when the StringArray is declared
	* with size specified, for example, new StringArray(8). The "clear" method
	* needs to be called once, before any "add" actions.
	**/
	typedef std::vector<std::string> StringArray;

	/**
	* A collection of numbers
	* Note:
	* The "clear" method is required to be called when the NumberArray is declared
	* with size specified, for example, new NumberArray(8). The "clear" method
	* needs to be called once, before any "add" actions.
	**/
	typedef std::vector<unsigned int> NumberArray;

	/**
	* A collection of information about zero or more devices with their current state &
	* supplementary state information.
	* Note:
	* The "clear" method is required to be called when the DeviceStateArray is declared
	* with size specified, for example, new DeviceStateArray(8). The "clear" method
	* needs to be called once, before any "add" actions.
	**/
	typedef std::vector<Device> DeviceStateArray;

	// A collection of Dictionary Change Items (history) that is currenly stored in the server
	typedef std::vector<DictionaryChangesetItem> DictionaryChangesetItemArray;

	// A collection of Dictionary Items that is currently stored in the server
	typedef std::vector<DictionaryItem> DictionaryItemArray;

	// A collection of Event Items that is currently stored in the server
	typedef std::vector<EventItem> EventItemArray;

	typedef std::map<std::string, Device::DeviceClass> DeviceClassMap;

	/**
	* <p>Connects to the primary AS and allows a set of configuration variables to be
	* sent as Key Value Pairs so that the system will initialise in a known
	* state.</p>
	* <p>Please note this method only needs to be called once after startup (and again after
	* disconnect() is called) and the API library will continuously attempt to connect to the
	* primary server until a successful connection is established.</p>
	* <p>The API library will try to connect to all ip addresses in the
	* serverAddresses list until a primary Audio Server is found. If only one IP
	* address is sent to the connect method, the library will connect to the
	* specified IP address regardless of the Primary/Secondary state of the
	* specified Audio Server.</p>
	* <p>The method AudioServer::isAudioConnected() can be used to check if there
	* is currently a connection to an audio server.</p>
	* <p>If the currently connected AudioServer becomes the Secondary server for
	* any reason, the API library will automatically connect to the new Primary
	* server.</p>
	* @param serverAddresses List of Audio Server IP Addresses.
	* @param config Key Value Pair List of configuration variables with format
	*   [ {Config Variable, Value } ]+  Values incorporating commas, or
	*   leading/trailing spaces must use double quotations to delimit the
	*   argument. The list of accepted configuration keys are:
	*   "NETSPIRE_SDK_CONFIGURE_FILE" - This configuration key is depreciated and not recommended to be used
	*   "NETSPIRE_SDK_SOCKET_PORT" - Allows application to define a port number to which the API will bound to. Default port number is 20770
	*   "NETSPIRE_SDK_CONNECT_TO_PROXY" - This configuration key is for internal use only
	*   "NETSPIRE_SERVER_HAS_CONFIG_INFO" - This configuration key is for internal use only
	*   "NETSPIRE_EXPORT_LOCAL_STATE" - This configuration key is for internal use only
	*
	* @throw std::invalid_argument,std::out_of_range (In Java,
	*   IllegalArgumentException or IndexOutOfBoundsException respectively). If the key value pairs
	*   are outside the bounds specified in Section 1.7 \ref configvars_sec,
	*   an will be thrown.
	**/
	void connect (const StringArray &serverAddresses, const KeyValueMap& config)
		throw (std::runtime_error, std::invalid_argument, std::out_of_range);

	/**
	* <p>Allows a set of configuration variables to be sent as Key Value Pairs
	* so that the system will initialise in a known state.
	* @param config Key Value Pair List of configuration variables with format
	*   [ {Config Variable, Value } ]+  Values incorporating commas, or
	*   leading/trailing spaces must use double quotations to delimit the
	*   argument. The list of accepted dynamic configuration keys and their accepted values are given in
	*   Section 1.7 \ref configvars_sec.<br>
	*   Please note all supported configuration items do not need to be sent
	*   as this argument. A partial or empty list can also be sent. Any
	*   configuration items not specified as part of this argument will not be
	*   modified and they will keep their current values.
	*   Their current values will be either the specified default values,
	*   or the last values assigned to them using AudioServer::connect().
	*
	**/
	void setDynamicConfiguration(const KeyValueMap& dynamicConfig)
		throw (std::invalid_argument, std::out_of_range);

	// Returns the existing configuration variables as Key Value Pairs
	KeyValueMap getDynamicConfiguration();

	/**
	* Registers an observer with the AudioServer instance. Callback methods on the
	* observer will be called on state changes.
	* @param observer Observer instance
	**/
	void registerObserver(AudioServerObserver *observer);

	/**
	* Disconnects from the AS. This function will cause the API library to disconnect from the current
	* server. It can be called as part of an orderly shutdown sequence or to connect to a different
	* server. Clients can re-connect after calling disconnect() using connect().
	**/
	void disconnect ();

	/**
	* <p>Determines if the connection between the Audio Server API Library and an
	* Audio Server has been successfully established.</p>
	* @return Boolean true if Connected, false when Not Connected
	**/
	bool isAudioConnected();

	/**
	* <p>Determines if the communications link between the Audio Server API Library and the
	* device it is connecting (e.g. Audio Server, Proxy Server) has been successfully established.</p>
	* @return Boolean true if Comms Established, false when otherwise
	**/
	bool isCommsEstablished();

	// Get an instance of PAController
	PAController *getPAController();

	// Get an instance of Call Controller
	CallController *getCallController();

	// Get an instance of Config Controller
	ConfigController *getConfigController();

	// Get an instance of Signalling Controller
	PassengerInformationServer *getPassengerInformationServer();

	/**
	* Initiate the testing of specified devices are managed by the
	* Audio Server.
	*
	* @param deviceId Mnemonic associated with a specific device in system.
	*   <br>Please refer to Section 1.5 \ref ids_sec for a list of device
	*   IDs and device types.
	*   <br>An empty string can be used to specify all devices, in which case health test will be initiated
	*   on all devices".
	*   <br>Valid device types for this argument are AS, NAC, Master Station, Monitor Speaker, and IC.
	*
	* @throw std::invalid_argument (an IllegalArgumentException in Java)
	**/
	void initiateDeviceHealthTest (const std::string &deviceId)
		throw (std::invalid_argument);

	/**
	* Allows device to be isolated (disabled) or re-enabled. The device state will be set to ISOLATED
	* after this method call.
	* @param deviceId Mnemonic associated with a specific device in system.
	*   Specifies the device to be isolated or re-enabled.
	*   <br>Please refer to Section 1.5 \ref ids_sec for a list of device
	*   IDs and device types.
	*   <br>Valid device types for this argument are AS, NAC, Master Station, Monitor Speaker, and IC.
	* @param enable Isolate or re-enable device
	* @throw std::invalid_argument (an IllegalArgumentException in Java)
	**/
	void setDeviceIsolation (const std::string &deviceId, bool enable)
		throw (std::invalid_argument);

	/** 
	* Sets the list of devices that will be returned when AudioServer::getDeviceStates() is called
	* with an empty argument.
	* If the returned device list is set using this method, AudioServer::getDeviceStates() will only include
	* the devices specified in the list specified in the last invocation of AudioServer::setDeviceList and will
	* always include these devices. If the devices are not currently connected and their attributes as included
	* in the Device object is unknown, the attrributes will be returned as Unknown and the device state will be
	* returned as COMMS_FAULT. 
	* @param deviceList A list of DeviceClass and Device names 
	*/
	void setDeviceList(const DeviceClassMap &deviceList)
		throw (std::runtime_error);

	/**
	* Queries states of all the devices on the Audio Server including IC and CI states.
	* Also includes the supplementary information for all states.
	* @param deviceId Mnemonic associated with a specific device in system.
	*   Specifies the Device ID to return the state of.
	*   <br>Please refer to Section 1.5 \ref ids_sec for a list of device
	*   IDs and device types.
	*   <br>An empty string can be used to specify all devices, in which case health test will be initiated
	*   on all devices".
	*   <br>Valid device types for this argument are AS, NAC, Master Station, Monitor Speaker, 
	*   and IC.
	* @return Information on device states for devices requested (a collection of information about zero
	* or more devices), along with supplementary fields
	* @throw std::invalid_argument (an IllegalArgumentException in Java)
	**/
	DeviceStateArray getDeviceStates (const std::string &deviceId = "")
		throw (std::invalid_argument);

	/**
	* <p>Updates the dictionary stored in the Audio Servers with the specified
	* changeset definition file.</p>
	*
	* <p>The Client API Library will initiate the dictionary update by transferring the changeset
	* definition file to both Audio Servers. The transfer will be done by the API Library and not the Client.
	* The transfer will be achieved using sftp as the transfer mechanism and the network will need
	* to allow sftp between the API Library and both Audio Servers.
	* The command will fail and return false if both Audio Servers are not available at the time.</p>
	*
	* <p>The Audio Servers will then unpack the changeset file and verify its contents.
	* Please see Section 1.8 \ref dict_sec for the naming and contents requirements of the file.
	* The changeset defines a sequential list of changes that need to be applied to the existing
	* dictionary loaded on the Audio Servers. It can be used to create a patch to the existing
	* dictionary as well as to force rebuild the entire dictionary from scratch by instructing
	* the server to clear the dictionary and recreate all items.
	* Using it as a small patch can be advantageous in situations where there is limited time
	* or bandwidth to upload a large file to the system
	* e.g. using a wireless network connection where only a limited range or time is available.</p>
	*
	* <p>The Audio Servers internally manage a similar list of dictionary changes.
	* Once the Audio Servers verify the validity of the new changeset, they will update their internal
	* dictionary by sequentially executing the actions defined in the changeset.
	* They will then distribute the changes to connected devices
	* by applying the required changes to device dictionaries.
	* Once the process is successfully completed, the Audio Servers and all devices in the
	* system will have the same dictionary items and media files, and
	* they will report identical dictionary versions.</p>
	*
	* The dictionary version number reported by devices after executing a changeset will
	* be updated to the versionSeq of the last executed change in the changeset.
	* Please note Audio Server and audio output devices (NACs and MSPKRs)
	* will have the versionSeq of "0" if there had not been any dictionary loaded yet. The current
	* versionSeq can be reset to 0 again in the future using the ResetDictionary operation defined
	* in the operation element of changeset.xml document.</p>
	*
	* <p>If AudioServer.updateDictionary fails for any reason the dictionary update will be abandoned
	* and the existing dictionary and reported dictionary version number will not be affected</p>
	*
	* <p>Dictionary item number range 99000 to 99999 is reserved for the default dictionary to store
	* test tones, built-in messages and chimes.
	* Note the system retains the default dictionary; and if an individual item is overwritten it will
	* simply play the file provided instead of the built-in default. This ensures if no default files
	* are overwritten in a subsequent update the system will revert back to using the default dictionary.</p>
	*
	* @param filename Filename (including full system path) of changeset definition file.
	* The contents of the file must conform to the specification given in Section 1.8 \ref dict_sec.
	*
	* @return Returns false and aborts operation if both Audio Servers are not
	* online at the time the command is received. Returns true if both Audio Servers are online.
	*
	**/
	bool updateDictionary (const std::string &filename);

	/**
	* <p>Returns the curren version history of the dictionary.</p>
	*
	* @return Returns a list of Dictionary Changeset Item containing the full list of version history
	**/
	AudioServer::DictionaryChangesetItemArray getDictionaryChangeset();

	/**
	* <p>Returns the current dictionary that is stored in the Audio Server.</p>
	*
	* @return Returns a list of Dictionary Items
	**/
	AudioServer::DictionaryItemArray getDictionaryItems();

	/**
	* Set output gain to NAC amplifiers.  Gain adjustment will be applied to all
	* amplifiers equally including inductive loops. This will affect DVA and PA gain for all
	* announcements Zones.
	* @param dB Output gain in decibels.
	* @throw std::invalid_argument,std::out_of_range (In Java,
	*   IllegalArgumentException or IndexOutOfBoundsException respectively)
	**/
	void setOutputGain (const Gain &gain)
		throw (std::invalid_argument, std::out_of_range);

	/**
	* Query output gain on all NAC amplifiers including inductive loop amplifiers. This gain
	* setting affects DVA and PA gain for all announcements Zones.
	* @param deviceId Mnemonic associated with a specific device in system.
	*   Specifies the NAC device.
	*   <br>Please refer to Section 1.5 \ref ids_sec for a list of device
	*   IDs and device types.
	*   <br>Valid device type that can be specified in this argument is NAC only.
	* @throw std::invalid_argument (In Java, IllegalArgumentException)
	* @return Output gain in decibels.
	**/
	Gain getOutputGain (const std::string &deviceId)
		throw (std::invalid_argument);

	/**
	* <p>Indicates the escalation level. Note the same role will be assigned
	* to all Master Station in one cabin.
	* @param deviceId Mnemonic associated with a specific device in system.
	*   Specifies a Master Station.
	*   <br>Please refer to Section 1.5 \ref ids_sec for a list of device
	*   IDs and device types.
	*   <br>Valid device type that can be specified in this argument is Master Station only.
	* @return Role of the device (LEVEL1, LEVEL2 or NONE)
	* @throw std::invalid_argument (In Java, IllegalArgumentException)
	**/
	Role getRole (const std::string &deviceId)
		throw (std::invalid_argument);

	// Allows the user to upload file to the server
	bool uploadFile (const std::string &fileName);

	// Returns the entire system version. This identifier is commonly expressed as a string value with the format:
	// XX.YY.ZZ, where XX, YY, and ZZ are integer values.
	std::string getSystemRevision();

	// Returns true if all devices revision controlled by the Audio Server have the correct software revision.
	// Returns false if a subset of the devices revision controlled by the Audio Server has a different software revision.
	// This function can temporarily return false occur while software upgrade is in progress and should not occur
	// during normal operation as devices are automatically updated to the current revision by the TCX when a component
	// is replaced. If this function returns false during normal operation, it indicates the automatic configuration
	// management system is not operating correctly.
	bool isSystemRevisionConsistent();

	/**
	* <p>Initiate the shutdown process on the Audio Server so that a graceful
	* shutdown can be achieved.</p>
	* <p>The initiateShutdown() command should be called before the power is
	* shut down in the train.
	* Please note that if initiate-Shutdown-() is called it must be done when
	* connected to the server (as with any other function except
	* AudioServer::connect()), therefore it should be called before
	* AudioServer::disconnect() is called.</p>
	**/
	void initiateShutdown();

	/**
	* <p>Return the current list of all events that is stored in the Audio Server.</p>
	*
	* @return Returns a list of Dictionary Items
	**/
	AudioServer::EventItemArray getEventList();

	// This function will return the current revision of the SDK.
	// Revision will be incremented every time a new version is submitted to the customer.
	std::string getSDKRevision();

	/**
	* Initiate message on Audio Zones and or Visual Displays.  A single function is
	* used to allow queuing and synchronisation of audio and visual information.
	* @param sinks List of audio sinks (audio zones) in the system to be used for audio output.
	* @param visualDevices List of specific device in system to be used for visual output.
	*   <br>Please refer to Section 1.5 \ref ids_sec for a list of device
	*   IDs and device types.
	* @param gain The output gain level to be applied to the amplifiers when playing audio.
	* @param dictionaryItems List of dictionary items to be played.
	*   The list of dictionary items passed to AudioServer::playMessage will be compared to the
	*   latest dictonary loaded on the server. If some of the items in the list do not exist in the
	*   dictionary, then an IllegalArgumentException (std::invalid_argument) exception will be raised.
	* @param visualText Textual information displayed on destination
	*   indicators specified in the visualDevices argument. A NULL pointer can be sent to indicate
	*   the visual components of the dictionary items specified in the dictionaryItems argument should be
	*   displayed on visualDevices. An empty string can be sent to clear the displays.
	* @param resumeOnInterruptFlag allows DVA announcements that are interrupted
	*   by PA announcements to be restarted from the beginning. This argument is reserved for future use
	*   and the current API release only supports
	*   "false" as a valid value for this argument. Sending true will raise an invalid_argument exception.
	* @param overrideExisting When set to true, any currently playing DVA or PA message will be interrupted
	* and the specified DVA message will start playing immediately. Set to false in most instances, which
	* will cause the DVA to be queued to play after the currently queued items. Set to true when the DVA
	* message has to be played immediately e.g. to play a door obstruction message
	* @param validityPeriod indicates the time in milli-seconds of how long a message will be displayed on a
	* display such as IDI/EDI/VCU. A value of 0 indicates display message forever or until overwritten by
	* another message.
	* @return A message ID which can be used by cancelMessage().
	* @throw std::invalid_argument,std::out_of_range (In Java,
	*   IllegalArgumentException or IndexOutOfBoundsException respectively)
	**/
	unsigned int playMessage (const std::vector<std::string> &zones, const std::vector<std::string> &visualDevices, const Gain &gain,
		const std::vector<unsigned int> &dictionaryItems, const char *visualText,
		bool resumeOnInterruptFlag, bool overrideExisting, unsigned int validityPeriod, unsigned int priority, unsigned int mode)
		throw (std::invalid_argument, std::out_of_range);

	/**
	* Retrieve the train certification test state.
	* @return train certification test state (AudioServer::TCTState).
	**/
	AudioServer::TCTState getTCTState();

	// Enables alarm management functions and enables receiving alarm information from NetSpire servers.
	void enableAlarmManagement();

	// Disables alarm management functions and disables receiving alarm information from NetSpire servers.
	void disableAlarmManagement();

	// Indicates if alarm management is enabled/disabled
	bool getAlarmManagement();

	// Returns a list of alarms in the system, including past alarms.
	std::vector<Alarm> getAlarms();

	// HELPER-FUNCTIONS
	// Following functions will be filtered by SWIG. These need to be explictly
	// declared in the csharp.i and java.i.
	// We need them to be public for ease of implementation but don't wish to 
	// expose them to the client/end-user.
	Device *findByDeviceId(const std::string &deviceId);
	Device *findByDstNo(int dstNo);
	std::string getUUID();
	void initDictionaryResources();
	void updateDictionaryStatusForIPPA(int dstNo, int elemNo, DVAlist *elemArgList);
	void addUpdateDictionaryVersionHistoryItem(int dstNo, int elemNo, DVAlist *elemArgList);
	void addUpdateDictionaryItem(int dstNo, int elemNo, DVAlist *elemArgList);
	void deleteDictionaryItem(int dstNo, int elemId, int elemNo);
	void updateDeviceModelList(int dstNo, int elemId, int elemNo, DVAlist *elemArgList);
	void clearDeviceModelList();
	DeviceModel *getDeviceModelObject(const std::string &modelName);
	void updateOnResDSCOnline(int dstNo);
	void updateResDSCDeviceList(int dstNo, int elemNo, DVAlist *elemArgList);
	void updateResDSCDynamicConfigurationList(int dstNo, DVAlist *elemArgList);
	void updateResDSCTrainCertificationTestState(int dstNo, DVAlist *elemArgList);
	void updateResDSCSystemStatus(int dstNo, DVAlist *elemArgList);
	void updateResDSCMasterSlaveStatus(int dstNo, DVAlist *elemArgList);
	void deleteResDSCDeviceList(int dstNo, int elemNo);
	void updateOnResDSCOffline(int dstNo);
	void clearDeviceStates(int dstNo);
	void updateFromResICIDigitalInputForIPPA(int dstNo, int elemNo, int state);
	void updateFromResICIDigitalInput(int dstNo, int elemNo, int state);
	void updateFromResICIDigitalInput(int dstNo, int elemNo, DVAlist *elemArgList);
	void updateFromResICIDigitalOutput(int dstNo, int elemNo, int state);
	void updateFromResICIDigitalOutput(int dstNo, int elemNo, DVAlist *elemArgList);
	void updatePreviewSpeakerStatusForIPPA(int dstNo, int elemNo, DVAlist *elemArgList);
	void updateStreamingServerStatusForIPPA(int dstNo, int elemNo, DVAlist *elemArgList);
	void addUpdateAlarm(int elemNo, DVAlist *elemArgList);
	void deleteAlarm(int elemNo);

	// Obsolete methods
	PAController::PaSinkArray getAudioSinks(int sourceId) throw (std::out_of_range);
	PAController::PaSinkArray getAudioSinks();

private:
	oaLocalMutex _asMtx;
	std::string netspireSDKVersion;
	double outputGain;								// Gain settings
	std::string myUUID;
	DictionaryChangesetItemArray dictionaryChangesetItemList;
	DictionaryItemArray dictionaryItemList;
	std::map<int, DeviceModel> deviceModelList;		// map resAdminMaster elemNo to DeviceModel struct
	std::map<int, AUDIOSERVER_INFO> resDSCList;		// map resDSC dstNo to all other information
	std::vector<Device *> userProvidedDeviceList;	// list of devices specified by the user.
	int trainCertificationTestState;				// used by AudioServer::initiateDeviceHealthTest

	Device *findUserProvidedDevice(const std::string &deviceId);
	void resetResDSCList();
	void resetUserProvidedDeviceList();
	Device *createDevice(const std::string &deviceId, Device::DeviceClass devClass) const;

	int messageId;

	bool alarmManagementEnabled;
	std::vector<Alarm> alarmList;
};

#endif // _AUDIO_SERVER_HPP_
