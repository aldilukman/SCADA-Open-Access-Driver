#ifndef _DEVICE_HPP_
#define _DEVICE_HPP_

#include "StandardIncludes.hpp"
#include "DeviceModel.hpp"

/**
* ICI_PORT_DEF is used to store port information related to ICI Input/Output.
**/
class ICI_PORT_DEF
{
public:
	int state;
	std::string portDescription;
	std::string portIntExt;

	ICI_PORT_DEF()
	{
		this->state = 0;
		this->portDescription.clear();
		this->portIntExt.clear();
	}

	ICI_PORT_DEF(int state, const std::string& desc, const std::string& intExt)
	{
		this->state = state;
		this->portDescription = desc;
		this->portIntExt = intExt;
	}

	ICI_PORT_DEF(const ICI_PORT_DEF &iciDefAnother)
	{
		this->state = iciDefAnother.state;
		this->portDescription = iciDefAnother.portDescription;
		this->portIntExt = iciDefAnother.portIntExt;
	}
};

/**
* The Device class is used to return device states and supplementary fields.
**/
class Device
{
public:
	Device (int elemNo = 0, const std::string &name = "", int nsType = 0, int dstNo = 0, int PortNo = 0, const std::string &deviceModelName_ = "");

	Device (const Device &deviceAnother);

	virtual ~Device ();

	// Specify if this device is enabled (i.e. not isolated)
	virtual void setEnabled (bool enabled);

	/// Defines a device state.  Note that not all states are necessarily
	/// possible for each device type.
	enum State
	{
		IDLE = 0,                           // No call in progress, device is available
		ALERTING,                           // Processing a call request, no call exists yet
		ACTIVE,                             // A call is in progress
		HELD,                               // The call at this device is on hold
		ESCALATED,                          // An IC has an escalated call
		ISOLATED,                           // The device is unable to function
		FAULTY,                             // The device is unable to function due to a fault
		COMMSFAULT,                         // Device unreachable, state can't be determined
		INACTIVE                            // Device is functioning in a limited mode
	};

	/// Defines a device class which represents a device.
	enum DeviceClass
	{
		UNKNOWN_DEVICE = 0,                 // Deivce does not fit any of the supported classes
		COMMUNICATIONS_EXCHANGE,            // Responsible for centralized and control e.g. CXS, TCX, AS
		NETWORK_AUDIO_CONTROLLER,           // Network attached audio device e.g. TGU, NAC, NAM, NAR
		OPERATOR_CONSOLE,                   // Network attached devices for users to interact with e.g. IPPA, CI, CP, CC
		MONITOR_SPEAKER,                    // Devices used for external integration with provision of monitoring e.g. CAC, MSPK
		HELP_POINT,                         // Devices used for end user interaction e.g. IC, PEI, PECU, HP
		PASSENGER_INFORMATION_DISPLAY,      // Devices used to display information e.g. IDI, EDI, EIDI, VCU
		TRAIN_RADIO                         // Devices used to communicate with base
	};

	/**
	* Specifies the state of the most recent health test request
	*/
	enum HealthTestStatus
	{
		TEST_PENDING = 0,                   // AudioServer::initiateDeviceHealthTest() has been called to initiate a test. The target device has not yet responded to the command.
		TEST_NONE,                          // No health tests initiated or the last health test is completed.
		TEST_IN_PROGRESS                    // Last health test is in progress (queued or being executed)
	};

	/**
	* Specifies the state of the most recent dictionary update request
	*/
	enum DictionaryUpdateStatus
	{
		DICT_UPDATE_NONE = 0,               // No dictionary updates initiated or the last update is completed. Please query the dictionary version using getDictionaryVersion() to verify the expected dictionary version is loaded on the device.
		DICT_UPDATE_IN_PROGRESS,            // Last dictionary update is in progress
		DICT_UPDATE_FAILED,                 // Last dictionary update failed.
		DICT_UPDATE_PENDING                 // Dictionary is updating (ftp transfer/server updating/server waiting for client/client updating/client update success)
	};

	/** A collection of key value pairs for storing device state supplementary
	* fields.
	* <br>Please refer to Section 1.5 \ref supp_sec for the list of supported
	* supplementary fields for each device type.
	*/
	typedef std::map<std::string, std::string> SupplementaryFields;

	/** Queries the name of the device.
	* @return the device name
	*/
	virtual std::string getName ();

	/** Returns the current state of this device.
	* @return the device State
	*/
	virtual State getState ();

	/** Returns the current state of this device in a human-readable form.
	* @return text describing the current device State.
	*/
	std::string getStateText ();

	/** Returns the supplementary fields of this device state.
	* @return SupplementaryFields provided for the device.
	*/
	SupplementaryFields getSupplementaryFields();

	/**
	* <p>Query the status of the most recent health test requested using
	* AudioServer::initiateDeviceHealthTest().</p>
	*
	* <p>The TEST_PENDING state will be reported by the
	* Device::getHealthTestStatus() method immediately after
	* AudioServer::initiateDeviceHealthTest() is called. Once the command is
	* received by the target device it will change its state to TEST_IN_PROGRESS
	* and later to TEST_NONE when the test is executed or aborted for any
	* reason.
	* Please note the state transition from TEST_IN_PROGRESS to TEST_NONE can
	* occur very quickly in the order of milliseconds.
	* When the state is polled, the caller should not depend on receiving
	* TEST_IN_PROGRESS as this state may change to TEST_NONE within a very
	* short time.</p>
	*
	* @return A collection of health test status for devices queried.
	*/
	HealthTestStatus getHealthTestStatus ();

	/**
	* Query the device to see if dictionary is supported.
	* @return yes/no to indicate if dictionary is supported.
	*/
	bool getDictionarySupport ();

	/**
	* Query the version number of the dictionary maintained by the CXS, NACs, and MSPKs.
	* This method will always return 0 for other devices.
	* To query the dictionary version of the entire audio system, the CSS can call this
	* method on the primary Audio Server. The primary Audio Server will ensure the rest of the devices
	* are updated to the dictionary version loaded on itself.
	* @return Version number of the dictionary stored on the device.
	*/
	unsigned int getDictionaryVersion ();

	/**
	* Query the status of the most recent dictionary update requested using
	* "AudioServer::updateDictionary()". This method will always return DICT_UPDATE_NONE for devices
	* other than CXS, NACs, and MSPKs.
	*
	* @return The status of the most recent dictionary update.
	*/
	DictionaryUpdateStatus getDictionaryUpdateStatus ();

	/**
	*
	* Retrieves the externally set input state on the given device. Valid
	* device types that can be queried with this method are CXS, NAC and IC.
	* 
	* @param inputNo Numeric integer specifying one of the inputs on the
	* device.
	*
	* @return True if the input state is set on the device. False for all
	* other cases, including when the specified input number is not supported
	* on the device.
	*
	*/
	bool getInputState( int inputNo );

	/**
	*
	* Retrieves the internally set output state on the given device. Valid
	* device types that can be queried with this method are CXS, NAC and IC.
	* 
	* @param inputNo Numeric integer specifying one of the inputs on the
	* device.
	*
	* @return True if the output state is set on the device. False for all
	* other cases, including when the specified output number is not supported
	* on the device.
	*
	*/
	bool getOutputState( int outputNo );

	/**
	*
	* Sets the output state on the given device.
	* 
	* @param outputNo Numeric integer specifying one of the output on the
	* device.
	*
	* @param state 1 signifies asserting the output, 2 negating the output
	*
	*/
	void setOutputState( unsigned int outputNo, unsigned int state );

	// Retrieves the destination number of the device
	int getDstNo();

	// Retrieves the port number of the device. For displays the port number is different.
	int getPortNo();

	// Retrieves the IP Address of the device.
	std::string getIP();

	/**
	*
	* Retrieves the firmware version that is currently installed and running
	* on the device.
	* @return firmware version of the device.
	*/
	std::string getSoftwareRevision();

	// Retrieves the location name where the device is running. This name is set using the web interface
	std::string getLocationName();

	// Retrieves the location Id of the device. DstNo = 2xxxyyy. Location Id is xxx
	std::string getLocationId();

	// Retrieves the device index for the device. DstNo = 2xxxyyy. Location Id is yyy
	std::string getDeviceIndex();

	// Retrives the device class that the device belongs to.
	DeviceClass getDeviceClass(); 

	// Returns the device model
	DeviceModel getDeviceModel();

	// Sets the value of a condition variable. NetSpire devices can be configured to react to and take a specified list of actions based on user settable or event based conditions.
	void setCondition(const std::string &conditionName, bool value);
	void setCondition(const std::string &conditionName, const std::string &value);

	// Returns the value of a condition variable. NetSpire devices can be configured to react to and take a specified list of actions based on user settable or event based conditions.
	bool getCondition(const std::string &conditionName);

	// HELPER-FUNCTIONS
	// Following functions will be filtered by SWIG. These need to be explictly
	// declared in the csharp.i and java.i.
	// We need them to be public for ease of implementation but don't wish to 
	// expose them to the client/end-user.
	void delSupplementaryFields(const std::string &key);
	bool setDigitalInputState(unsigned int port, int state);
	bool setDigitalInputInfo(unsigned int port, const std::string &portDescription, const std::string &portIntExt);
	bool setDigitalOutputState(unsigned int port, int state);
	bool setDigitalOutputInfo(unsigned int port,  const std::string &portDescription, const std::string &portIntExt);
	char getRoleId();
	void setRoleId(char devRoleId);
	void setDstNo(int dstNo);
	void setState(const std::string& stateName);                            // Set the state of this device (by name)
	void setIP(const std::string &ip);
	void clearSupplementaryFields();
	void setSupplementaryFields(const std::string &key, const std::string &value);
	void setSelfTestStatus(HealthTestStatus status);
	void setDictionaryVersionSeq(int versionSeq);
	void setDictionaryUpdateStatus(DictionaryUpdateStatus status);
	int getRealDictionaryVersion();
	int getNetSpireType();
	void setPortNo(int portNo);
	void setSoftwareRevision(const std::string &softwareRevision);
	void setLocationName(const std::string &locationName);
	int getResDSCElemNo() { return this->resDSCElemNo; };
	void setDeviceId(const std::string &nameModified, const std::string &nameNotModified);
	std::string getNameNotModified();
	std::string getDeviceModelName();

protected:
	bool dictionarySupport;
	Device::DeviceClass devClass;

private:
	void initialiseStateNames();
	std::string nameModified;
	std::string nameNotModified;
	int nsType;
	int dstNo;
	int portNo;
	State state;
	std::string ip;
	char roleId;
	int dictionaryVersionSeq;
	DictionaryUpdateStatus dictionaryUpdateStatus;
	SupplementaryFields sFields;
	std::map<unsigned int, ICI_PORT_DEF> digitalInputList;
	std::map<unsigned int, ICI_PORT_DEF> digitalOutputList;
	HealthTestStatus selfTestStatus;
	std::string softwareRevision;
	std::string locationName;
	std::string locationId;
	std::string deviceIndex;
	int resDSCElemNo;
	std::string deviceModelName;
};

#endif // _DEVICE_HPP_
