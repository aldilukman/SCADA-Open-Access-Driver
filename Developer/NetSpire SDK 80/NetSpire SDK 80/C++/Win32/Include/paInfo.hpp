#ifndef _PAINFO_HPP_
#define _PAINFO_HPP_

#include <string>
#include <Gain.hpp>

/**
* PASource is used to define a audio source type that can be used in various PA initiation methods.
*/
class PaSource
{
public:
	PaSource()
	{
		this->id = 0;
		this->location.clear();
		this->label.clear();
		this->ipAddress = "0.0.0.0";
		this->type = PaSource::SOURCE_TYPE_NOT_DEFINED;
		this->subAddress.clear();
		this->state = PaSource::UNKNOWN;
		this->isEnabled = true;
		this->inputGain = -96.00;
		this->broadcastType = PaSource::MULTICAST_AND_UNICAST;
		this->healthState = PaSource::UNKNOWN_HEALTHSTATE;
		this->targetGainAdjustment = 0;
		this->priority = 500;
	};

	PaSource(const PaSource &paSourceAnother)
	{
		// Deep copy all attributes
		this->id = paSourceAnother.id;
		this->location = paSourceAnother.location;
		this->label = paSourceAnother.label;
		this->ipAddress = paSourceAnother.ipAddress;
		this->type = paSourceAnother.type;
		this->subAddress = paSourceAnother.subAddress;
		this->state = paSourceAnother.state;
		this->isEnabled = paSourceAnother.isEnabled;
		this->inputGain = paSourceAnother.inputGain;
		this->broadcastType = paSourceAnother.broadcastType;
		this->healthState = paSourceAnother.healthState;
		this->targetGainAdjustment = paSourceAnother.targetGainAdjustment;
		this->priority = paSourceAnother.priority;
	};

	~PaSource() {};

	/**
	* Audio Source Type
	*/
	enum Type
	{
		SOURCE_TYPE_NOT_DEFINED = 0,			// Third-party source
		NETSPIRE_ANALOG_INPUT,					// NETSPIRE_ANALOG_INPUT
		NETSPIRE_IP_PAGING_STATION,				// IP based Microphone
		PC_AUDIO_INPUT,							// Microphone connected to PC Input
		NETSPIRE_HANDSET,						// NETSPIRE_HANDSET
		SIP_HANDSET,							// Voice Over IP Input
		TRAIN_RADIO,							// Train Radio Input
		EWIS,									// Emergency Input
		BGM_SERVER,								// Background Music
		PEI_MIC									// Help Point Microphone
	};

	/**
	* Audio Source State
	*/
	enum AttachState
	{
		UNKNOWN = 0,							// Source state unknown
		NOT_ATTACHED,							// Source is not currently attached to the system
		ATTACHED								// Source is attached to the system
	};

	/**
	* Broadcast type
	*/
	enum BroadcastType
	{
		MULTICAST_AND_UNICAST = 0,
		UNICAST_ONLY
	};

	/**
	* Health State
	*/
	enum HealthState
	{
		UNKNOWN_HEALTHSTATE = 0,
		HEALTHY,
		FAULTY
	};

	/**
	* Sink Mode Type
	*/
	enum AttachMode
	{
		ADD_TO_EXISTING_SET = 0,				// Allows zone to be added to existing set using software
		REPLACE_EXISTING_SET					// Replaces existing set of zones
	};

	// Id of the PA source. This is for the application to use as an Id to request a source. For Example: 66112
	int id;

	// Descriptive name (suitable for display) for the location of the source. For Example: "Domestic Terminal: Control Room 1"
	std::string location;

	// Descriptive name given to a source. For Example: "Primary PA Console"
	std::string label;

	// Source IP Address (if this information is available)
	std::string ipAddress;

	// Audio source type. Possible values are listed in the enumeration Type
	Type type;

	/** Provides a device specific sub-address (portNo). For Example:
	*  <p><li>|-----------------------------------------------------|</li></p>
	*  <p><li>|NetSpire Analog Input:       Analog Port Number      |</li></p>
	*  <p><li>|NetSpire IP Paging Station:  0                       |</li></p>
	*  <p><li>|PC Audio Input:              VCI port number         |</li></p>
	*  <p><li>|NetSpire Handset:            Handset SIP Phone Number|</li></p>
	*  <p><li>|SIP Handset:                 Phone  Number           |</li></p>
	*  <p><li>|Train Radio:                                         |</li></p>
	*  <p><li>|-----------------------------------------------------|</li></p>
	**/
	std::string subAddress;

	// Indicates the current state of the source
	AttachState state;

	// Indicates if the PaSource is to be shown in the DVA Element
	bool isEnabled;

	// Indicates the current input gain
	double inputGain;

	// Indicates the transport method used for playing PA
	BroadcastType broadcastType;

	// Indicates the health state of the sink
	HealthState healthState;

	int targetGainAdjustment;

	int priority;

	// Allows controlling the gain level of a PaSource. This method is valid for the following PaSource types: NETSPIRE_ANALOG_INPUT, NETSPIRE_IP_PAGING_-STATION
	// Parameters: gain - New desired gain level. Must be a double value between [-96.00, +42.00].
	//             persistent - commits the gain changes to make data persistent
	//             delayPersistence - changes are made persistent only if no gain updates are received for 20 seconds. Every time a gain update is received, the timer will be extended so the changes are committed 20 seconds after the last update.
	// Exceptions:
	//      std::invalid_argument (IllegalArgumentException in Java): Raised if the PaSource is not one of the supported PaSource types.
	//      std::out_of_range (IllegalArgumentException in Java): Raised if the gain value is outside the specified range.
	void setGain(double gain, bool persistent = true, bool delayPersistence = true);

	// Returns gain level of a source. This method is valid for the following PaSource types: NETSPIRE_ANALOG_INPUT, NETSPIRE_IP_PAGING_-STATION
	// Return Value: double - Gain level of the specified PaSource. The Gain is a double in the range [-96.00, +42.00].
	// Exceptions:
	//      std::invalid_argument (IllegalArgumentException in Java): Raised if the PaSource is not one of the supported PaSource types.
	double getGain();

	// Allows a zone to be attached to a source so that when a trigger for the source is activated, the zone will be included in the output announcement.
	void attachPaZone(const std::string &zoneId, AttachMode mode);

	// Allows list of zones to be attached to a source so that when a trigger for the source is activated, the zone will be included in the output announcement.
	void attachPaZone(const std::vector<std::string> &zoneIdList, AttachMode mode);

	// Allows one zone to be detached from a source so that when a trigger for the source is activated, the zone will not be included in the output announcement.
	void detachPaZone(const std::string &zoneId);

	// Allows list of zone to be detached from a source so that when a trigger for the source is activated, the zone will not be included in the output announcement.
	void detachPaZone(const std::vector<std::string> &zoneIdList);

	// Allows all attached zones to be detached from a source so that when a trigger for the source is activated, no zones will not be included in the output announcement.
	void detachAllPaZones();

	// Returns a list of all audio zones that are currently attached to the source at the time of the call
	std::vector<std::string> getAttachedPaZones();

	// Returns the status of a PaSource. 
	PaSource::AttachState getAttachState() const;
};

/**
* PaSink is used to define an audio sink to make announcement to from a source
*/
class PaSink
{
public:
	PaSink()
	{
		this->id = 0;
		this->vci_dciPort = 0;
		this->location.clear();
		this->label.clear();
		this->ipAddress = "0.0.0.0";
		this->type = PaSink::SINK_TYPE_NOT_DEFINED;
		this->typeAsString = "SINK_TYPE_NOT_DEFINED";
		this->ampPortList.clear();
		this->state = PaSink::UNKNOWN;
		this->announcementType = PaSink::NO_ANNOUNCEMENT;
		this->outputGain.setLevel(-96.00);
		this->muteState = PaSink::NOT_MUTED;
		this->monitoringSinkId = 0;				// not used
		this->sinkHasDigitalRoute = true;
		this->serverToUseSinkId = 0;
		this->realDeviceDstNo = 0;				// obsolete (no longer used)
		this->healthState = PaSink::UNKNOWN_HEALTHSTATE;
		this->isEnabled = true;
		this->dspOutputChannelList.clear();
		this->relayDstNo = 0;
		this->broadcastType = PaSink::MULTICAST_AND_UNICAST;
		this->paZoneList.clear();
		this->monitoringZoneList.clear();
		this->monitoringEnabled = true;
		this->mediaLog.clear();
	};

	PaSink(const PaSink &paSinkAnother)
	{
		// Deep copy all attributes
		this->id = paSinkAnother.id;
		this->vci_dciPort = paSinkAnother.vci_dciPort;
		this->location = paSinkAnother.location;
		this->label = paSinkAnother.label;
		this->ipAddress = paSinkAnother.ipAddress;
		this->type = paSinkAnother.type;
		this->ampPortList = paSinkAnother.ampPortList;
		this->state = paSinkAnother.state;
		this->announcementType = paSinkAnother.announcementType;
		this->outputGain.setLevel(paSinkAnother.outputGain.getLevel());
		this->muteState = paSinkAnother.muteState;
		this->monitoringSinkId = paSinkAnother.monitoringSinkId;				// not used
		this->sinkHasDigitalRoute = paSinkAnother.sinkHasDigitalRoute;
		this->serverToUseSinkId = paSinkAnother.serverToUseSinkId;
		this->realDeviceDstNo = paSinkAnother.realDeviceDstNo;					// obsolete (no longer used)
		this->healthState = paSinkAnother.healthState;
		this->isEnabled = paSinkAnother.isEnabled;
		this->dspOutputChannelList = paSinkAnother.dspOutputChannelList;
		this->relayDstNo = paSinkAnother.relayDstNo;
		this->broadcastType = paSinkAnother.broadcastType;
		this->paZoneList = paSinkAnother.paZoneList;
		this->monitoringZoneList = paSinkAnother.monitoringZoneList;
		this->monitoringEnabled = paSinkAnother.monitoringEnabled;
		this->mediaLog = paSinkAnother.mediaLog;
	};

	~PaSink() {};

	/**
	* Sink Type
	*/
	enum Type
	{
		SINK_TYPE_NOT_DEFINED = 0,				// Third-Party sink
		HIGH_IMPEDANCE_SPEAKER_BUS,				// HIGH_IMPEDANCE_SPEAKER_BUS
		LOW_IMPEDANCE_AMPLIFIER_SPEAKER_OUTPUT,	// LOW_IMPEDANCE_AMPLIFIER_SPEAKER_OUTPUT
		INDUCTION_LOOP_OUTPUT,					// INDUCTION_LOOP_OUTPUT
		LINE_LEVEL_AUDIO_OUTPUT,				// LINE_LEVEL_AUDIO_OUTPUT
		INTERCOM_HELP_POINT,					// INTERCOM_HELP_POINT
		TELEPHONY_ENDPOINT,						// TELEPHONY_ENDPOINT
		RADIO_REBROADCAST,						// RADIO_REBROADCAST
		PACI_CLIENT,							// PACI Client
		NETSPIRE_DCI2,							// LED display unit controlled by resDCI2
		NETSPIRE_VCU4							// LCD display unit
	};

	/**
	* Sink State
	*/
	enum State
	{
		UNKNOWN = 0,							// Sink state unknown
		IDLE,									// Sink is Idle
		ACTIVE									// Sink is busy playing stream
	};

	/**
	* Announcement Type
	*/
	enum AnnouncementType
	{
		NO_ANNOUNCEMENT = 0,					// No announcemant in progress
		FILE_PLAY,								// A list of prerecorded segments is being played
		LLPA_HIGH_QUALITY,						// A high quality LLPA announcement is being played
		LLPA_EMERGENCY,							// An emergency LLPA announcement is being played
		AUDIO_SWITCHING,						// The output is currently recieveing audio from a sound card input
		VOIP_STREAMING							// The output is currently recieveing audio from a VoIP stream
	};

	/**
	* Health State
	*/
	enum HealthState
	{
		UNKNOWN_HEALTHSTATE = 0,
		HEALTHY,
		MINOR_FAULT,
		MAJOR_FAULT,
		INACTIVE
	};

	/**
	* Broadcast type
	*/
	enum BroadcastType
	{
		MULTICAST_AND_UNICAST = 0,
		UNICAST_ONLY
	};

	/**
	* Mute State
	*/
	enum MuteState
	{
		MUTED = 0,
		NOT_MUTED,
		PARTIALLY_MUTED
	};

	// Id of the PA sink. This is for the application to use as an Id to request an audio sink. For Example: 10
	int id;

	// VCI/DCI Port number responsible for this PaSink
	int vci_dciPort;

	// Descriptive name for the location of the audio sink. For Example: "Central Station" 
	std::string location;

	// A textual description of the sink (suitable for display). For Example: "Platform 1 - Induction Loop"
	std::string label;

	// Sink IP Address (if this information is available)
	std::string ipAddress;

	// Audio sink type. Possible values are listed in the enumeration Type
	Type type;

	// Audio sink type as string. Possible values are listed in the enumeration Type
	std::string typeAsString;

	/** Provides a device specific sub-address. For Example:
	*  <p><li>|--------------------------------------------------|</li></p>
	*  <p><li>|High Impedance Speaker Bus:  Amplifier Port Number|</li></p>
	*  <p><li>|Line Level Audio Output:     VCI Port Number      |</li></p>
	*  <p><li>|--------------------------------------------------|</li></p>
	**/
	std::string ampPortList;

	// Indicates the current state of the sink
	State state;

	// Indicates the currnet announcement type that is playing on the sink
	AnnouncementType announcementType;

	// Indicates the current output gain
	Gain outputGain;

	// Indicates if the sink is muted/unmuted/partially muted
	MuteState muteState;

	// Indicates the sink Id that is currently monitored by this sink (not used)
	int monitoringSinkId;

	//
	bool sinkHasDigitalRoute;

	// Internal Use
	int serverToUseSinkId;

	// Internal Use - obsolete (no longer used)
	int realDeviceDstNo;

	// Indicates the health state of the sink
	HealthState healthState;

	// Indicates if the sink is to be shown in the DVA Element
	bool isEnabled;

	// Indicates the list of DSP channels that the zone is mapped to
	std::string dspOutputChannelList;

	// Indicates the destination number that controls this PaSink for DVA playback
	int relayDstNo;

	// Indicates the transport method used for playing PA
	BroadcastType broadcastType;

	// Indicates list of zones (comma separated) that the PaSink belongs to
	std::vector<std::string> paZoneList;

	// Indicates list of zones 
	std::vector<std::string> monitoringZoneList;

	// Indicates if the sink is actively monitoring other zones
	bool monitoringEnabled;

	// Log message for display devices to indicate LOG_DATE,LOG_TIME,DEVICE_NAME,DEV_IP,CAMPAIGN_ID,VERSION_NUMBER,FILE_NAME,START_TIME,END_TIME,INFO
	std::string mediaLog;

	// Allows controlling the gain level of a sink. This method is valid for the following PaSink types: HIGH_IMPEDANCE_SPEAKER_BUS, LOW_IMPEDANCE_-AMPLIFIER_SPEAKER_OUTPUT, INDUCTION_LOOP_OUTPUT, LINE_LEVEL_AUDIO_OUTPUT
	// Parameters: gain - New desired gain level. The Gain class allows a gain range of [-96.000, 0.000] to be specified.
	//             persistent - commits the gain changes to make data persistent
	//             delayPersistence - changes are made persistent only if no gain updates are received for 20 seconds. Every time a gain update is received, the timer will be extended so the changes are committed 20 seconds after the last update.
	// Exceptions:
	//      std::invalid_argument (IllegalArgumentException in Java): Raised if the PaSink is not one of the supported PaSink types.
	void setGain(Gain gain, bool persistent = true, bool delayPersistence = true);

	// Returns gain level of a sink. This method is valid for the following PaSink types: HIGH_IMPEDANCE_SPEAKER_BUS, LOW_IMPEDANCE_AMPLIFIER_SPEAKER_OUTPUT, INDUCTION_LOOP_OUTPUT, LINE_LEVEL_AUDIO_OUTPUT
	// Return Value: Gain - Gain level of the specified PaSink. The Gain class stores a gain range of [-96.000, 0.000].
	// Exceptions:
	//      std::invalid_argument (IllegalArgumentException in Java): Raised if the PaSink is not one of the supported PaSink types.
	Gain getGain();

	// Enables/disables PA and DVA Monitoring
	void setPAMonitoring(bool enable);

	// Returns current PA and DVA monitoring state
	bool getPAMonitoring();

	// Requests PaSink of type NETSPIRE_DCI2 or NETSPIRE_VCU4 to reset/reboot
	// Exceptions: std::invalid_argument (IllegalArgumentException in Java): Raised if the PaSink is not of type NETSPIRE_DCI2 or NETSPIRE_VCU4.
	void reset();
};

/**
* PaTrigger is used to define a trigger to enable/disable PA
*/
class PaTrigger
{
public:
	PaTrigger()
	{
		this->id = 0;
		this->associatedSourceId = 0;
		this->associatedSelectorId = 0;
		this->label.clear();
		this->type = PaTrigger::DIN_NORMALLY_OPEN;
		this->subAddress.clear();
		this->priority = 500;
		this->priorityMode = PaTrigger::ABSOLUTE_PRIORITY;
		this->state = PaTrigger::UNKNOWN;
		this->resumeAfterInterruption = false;
	};

	PaTrigger(const PaTrigger &paTriggerAnother)
	{
		// Deep copy all attributes
		this->id = paTriggerAnother.id;
		this->associatedSourceId = paTriggerAnother.associatedSourceId;
		this->associatedSelectorId = paTriggerAnother.associatedSelectorId;
		this->label = paTriggerAnother.label;
		this->type = paTriggerAnother.type;
		this->subAddress = paTriggerAnother.subAddress;
		this->priority = paTriggerAnother.priority;
		this->priorityMode = paTriggerAnother.priorityMode;
		this->state = paTriggerAnother.state;
		this->resumeAfterInterruption = paTriggerAnother.resumeAfterInterruption;
	};

	~PaTrigger() {};

	/**
	* Trigger Type
	*/
	enum Type
	{
		TRIGGER_TYPE_UNDEFINED = 0,				// Third party trigger
		DIN_NORMALLY_OPEN,						// Digital Input which is normally open
		DIN_NORMALLY_CLOSED,					// Digital Input which is normally closed
		KEYPAD_KEY,								// KEYPAD_KEY
		VOX,									// VOX
		STREAM_PRESENCE,						// STREAM_PRESENCE
		TOUCHPAD_ZONE,							// TOUCHPAD_ZONE
		INCOMING_TELEPHONY_CALL,				// INCOMING_TELEPHONY_CALL
		MICROPHONE_CURRENT_SENSE,				// MICROPHONE_CURRENT_SENSE
		USER_CONDITIONAL						// USER_CONDITIONAL
	};

	/**
	* Trigger State
	*/
	enum State
	{
		UNKNOWN = 0,							// Trigger state is unknown
		INACTIVE,								// Trigger state is currently inactive
		ACTIVE									// Trigger state is currently active
	};

	/**
	* Trigger Priority Mode
	*/
	enum PRIORITY_MODE
	{
		ABSOLUTE_PRIORITY = 0,					// Use the priority level in the request as the announcement priority
		RELATIVE_PRIORITY						// Use the priority level of the audio source and add the Request Priority Level.
	};

	// Id of the PA Trigger. This is for the application to use as an Id to query the trigger. For Example: 3
	int id;

	// Id of the PA Source to which this trigger is bound to
	int associatedSourceId;

	// Id of the PA Selector to which this trigger is bound to
	int associatedSelectorId;

	// Textual description (if available) of the trigger. For Example: "NAC DIO Port 1"
	std::string label;

	// Trigger Type. Possible values are listed in the enumeration Type
	Type type;

	/** Provides a device specific sub-address. For Example:
	*  <p><li>|----------------------------------------------------|</li></p>
	*  <p><li>|Digital Input:               Input Number           |</li></p>
	*  <p><li>|Keypad Key:                  Key Number             |</li></p>
	*  <p><li>|VOX:                         VOX Level              |</li></p>
	*  <p><li>|Touch Pad Zone:              Zone Number            |</li></p>
	*  <p><li>|Incoming Telephony Call:     On_Ring, On_DTMF_Number|</li></p>
	*  <p><li>|----------------------------------------------------|</li></p>
	**/
	std::string subAddress;

	/** An integer value from 1 to 1000. 1 being lowest and 1000 highest. Can be relative to source or absolute as determined by the PRIORITY_MODE.
	**/
	int priority;

	PRIORITY_MODE priorityMode;

	// Indicates the current state of the trigger
	State state;

	// Indicates if the announcement generated by this trigger should be resumed if interruped by higher priority
	bool resumeAfterInterruption;
};

/**
* PaSelector is used to select pre-configured audio sinks.
*/
class PaSelector
{
public:
	PaSelector()
	{
		this->id = 0;
		this->associatedSourceId = 0;
		this->label.clear();
		this->type = PaSelector::SELECTOR_TYPE_UNDEFINED;
		this->number.clear();
		this->protection = PaSelector::FIXED_CONFIGURATION;
		this->mode = PaSelector::ADD_TO_EXISTING_SET;
		this->dynamicRestriction = PaSelector::UPDATES_NOT_ALLOWED;
		this->state = PaSelector::DISABLED;
		this->paZoneList.clear();
	};

	PaSelector(const PaSelector &paSelectorAnother)
	{
		// Deep copy all attributes
		this->id = paSelectorAnother.id;
		this->associatedSourceId = paSelectorAnother.associatedSourceId;
		this->label = paSelectorAnother.label;
		this->type = paSelectorAnother.type;
		this->number = paSelectorAnother.number;
		this->protection = paSelectorAnother.protection;
		this->mode = paSelectorAnother.mode;
		this->dynamicRestriction = paSelectorAnother.dynamicRestriction;
		this->state = paSelectorAnother.state;
		this->paZoneList = paSelectorAnother.paZoneList;
	};

	~PaSelector() {};

	/**
	* Selector Type
	*/
	enum Type
	{
		SELECTOR_TYPE_UNDEFINED = 0,			// Third party sink selector
		DIN_NORMALLY_OPEN,						// Digital Input which is normally open
		DIN_NORMALLY_CLOSED,					// Digital Input which is normally closed
		KEYPAD_KEY,								// KEYPAD_KEY
		TOUCHPAD_REGION,						// TOUCHPAD_REGION
		TELEPHONY_INPUT,						// TELEPHONY_INPUT
		TELEPHONY_DID,							// TELEPHONY_DID
		USER_DEFINED							// Software Selector
	};

	/**
	* Selector Protection
	*/
	enum Protection
	{
		FIXED_CONFIGURATION = 0,				// Selector configuration cannot be altered/changed
		VOLATILE_SOFTWARE_CONFIGURABLE,			// Selector configuration can be updated using software, however changes will not be saved
		NONVOLATILE_SOFTWARE_CONFIGURABLE		// Selector configuration can be updated using software and changes will be persistent
	};

	/**
	* Selector Mode
	*/
	enum Mode
	{
		ADD_TO_EXISTING_SET = 0,				// Sink(s) can be added to existing set during announcement
		REPLACE_EXISTING_SET					// Sink(s) will replace existing set during announcement
	};

	/**
	* Selector DynamicRestriction
	*/
	enum DynamicRestriction
	{
		UPDATES_NOT_ALLOWED = 0,				// Addition/Deletion of sinks not allowed
		ADD_SINKS_TO_SET_ALLOWED,				// New sink(s) can be added to the selector set, however, sinks cannot be removed
		ADD_REMOVE_SINKS_TO_SET_ALLOWED			// Sink(s) can be added or removed from the selector set
	};

	/**
	* Selector State
	**/
	enum State
	{
		ENABLED_AND_UNKNOWN = 0,				// Selector is enabled however it's state is not known at time of query
		ENABLED_AND_INACTIVE,					// Selector is enabled and is currently inactive
		ENABLED_AND_ACTIVE,						// Selector is enabled and is currently active
		DISABLED								// Selector is disabled
	};

	// Id of the PA Selector. Application can use this to enquire/modify the selector. For Example: 8
	int id;

	// Id of the PA Source to which the selector is bound to
	int associatedSourceId;

	// Textual description (if available) of the selector. For Example: "keypad Button 1"
	std::string label;

	// Selector Type. Possible values are listed in the enumeration Type
	Type type;

	/** Indicates device specific details for the selector. For Example:
	*  <p><li>|---------------------------------------|</li></p>
	*  <p><li>|Digital Input:            Input Number |</li></p>
	*  <p><li>|Keypad key:               Key Number   |</li></p>
	*  <p><li>|Touch Pad Region:         Region Number|</li></p>
	*  <p><li>|Telephony Input:          DTMF         |<li></p>
	*  <p><li>|Telephony DID:            Indial Number|</li></p>
	*  <p><li>|---------------------------------------|</li></p>
	**/
	std::string number;

	// Selector Protection. Possible values are listed in the enumeration Protection
	Protection protection;

	// Selector Mode. Possible values are listed in the enumeration Mode
	Mode mode;

	// Selector Dynamic Restriction. Possible values are listed in the enumeration DynamicRestriction
	DynamicRestriction dynamicRestriction;

	// Indicates the current state of the selector
	State state;

	// Indicates the list of PaSinks
	//std::map<int, int> paSinkList;

	// Indicates the list of PaZones
	std::map<int, string> paZoneList;
};

#endif // _PAINFO_HPP_
