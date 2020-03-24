#ifndef _CALL_CONTROLLER_HPP_
#define _CALL_CONTROLLER_HPP_

#include "StandardIncludes.hpp"

// Forward Declaration
class AudioServer;

class CallPartyInfo
{
public:
	CallPartyInfo()
	{
		this->streamNo = 0;
		this->srcDst = 0;
		this->callType = 0;
		this->callTypeStr.clear();
		this->callFromDst.clear();
		this->callToDst.clear();
		this->callAlertingDestinations.clear();
		this->callState = 0;
		this->callStateStr.clear();
		this->broadcastAddr.clear();
		this->broadcastPort = 0;
		this->isMulticast = true;
	}

	CallPartyInfo(const CallPartyInfo &callPartyInfoAnother)
	{
		this->streamNo = callPartyInfoAnother.streamNo;
		this->srcDst = callPartyInfoAnother.srcDst;
		this->callType = callPartyInfoAnother.callType;
		this->callTypeStr = callPartyInfoAnother.callTypeStr;
		this->callFromDst = callPartyInfoAnother.callFromDst;
		this->callToDst = callPartyInfoAnother.callToDst;
		this->callAlertingDestinations = callPartyInfoAnother.callAlertingDestinations;
		this->callState = callPartyInfoAnother.callState;
		this->callStateStr = callPartyInfoAnother.callStateStr;
		this->broadcastAddr = callPartyInfoAnother.broadcastAddr;
		this->broadcastPort = callPartyInfoAnother.broadcastPort;
		this->isMulticast = callPartyInfoAnother.isMulticast;
	}

	~CallPartyInfo() { }

	int streamNo;
	int srcDst;
	int callType;
	std::string callTypeStr;
	std::string callFromDst;
	std::string callToDst;
	std::string callAlertingDestinations;
	int callState;
	std::string callStateStr;
	std::string broadcastAddr;
	int broadcastPort;
	bool isMulticast;
};

/******************************************************************************************************
* CallInfo objects are used to provide information on active calls managed by the system.
*****************************************************************************************************/
class CallInfo
{
public:
	CallInfo()
	{
		this->callId = 0;
		this->callStartTime.clear();
		this->callDuration = 0;
		this->callAnswerTime = 0;
		this->callAPartyId.clear();
		this->callBPartyId.clear();
		this->callTargetIds.clear();
		this->callState = UNKNOWN;
		this->callStateText = "UNKNOWN";
		this->callType = 0;
		this->callTypeText = "UNKNOWN";
		this->callReleaseCause = 0;
		this->callTransferredFlag = false;
		this->callPartyList.clear();
	}

	CallInfo(const CallInfo &callInfoAnother)
	{
		this->callId = callInfoAnother.callId;
		this->callStartTime = callInfoAnother.callStartTime;
		this->callDuration = callInfoAnother.callDuration;
		this->callAnswerTime = callInfoAnother.callAnswerTime;
		this->callAPartyId = callInfoAnother.callAPartyId;
		this->callBPartyId = callInfoAnother.callBPartyId;
		this->callTargetIds = callInfoAnother.callTargetIds;
		this->callState = callInfoAnother.callState;
		this->callReleaseCause = callInfoAnother.callReleaseCause;
		this->callStateText = callInfoAnother.callStateText;
		this->callType = callInfoAnother.callType;
		this->callTypeText = callInfoAnother.callTypeText;
		this->callReleaseCause = callInfoAnother.callReleaseCause;
		this->callTransferredFlag = callInfoAnother.callTransferredFlag;
		this->callPartyList.clear();
		this->callPartyList = callInfoAnother.callPartyList;
	};

	~CallInfo() { };

	/**
	* Defines call states reported by the system.
	*/
	enum State
	{
		PROGRESS,		// Call in progress
		CONNECTED,		// Call connected to B-party
		HELD,			// Call held
		DISCONNECTED,	// Call disconnected
		UNKNOWN			// Call state unknown
	};

	/// Unique ID for the call
	unsigned long callId;

	/// Call origination time
	std::string callStartTime;

	/// Call duration
	unsigned long callDuration;

	/// Time elapsed before call is accepted by B-party
	unsigned long callAnswerTime;

	/// A-party (call originator) address
	std::string callAPartyId;

	/// B-party (recipient) address
	std::string callBPartyId;

	/// List of addresses currently being alerted in relation to this call
	std::string callTargetIds;

	/// Call State
	State callState;

	// Call State Text
	std::string callStateText;

	// Call Type
	int callType;

	// Call Type Text
	std::string callTypeText;

	/** Call Release Cause as specified in ITU-T Q.850. Common release causes are:<br>
	* 16: Normal call clearing<br>
	* 17: User busy<br>
	* 18: No user responding<br>
	* 19: No answer from user (user alerted)<br>
	* 31: Normal, unspecified<br>
	**/ 
	int callReleaseCause;

	// Indicates if the A-party of the call is transferred.
	bool callTransferredFlag;

	// Indicates information for each party associated in the call
	std::vector<CallPartyInfo> callPartyList;
};

/******************************************************************************************************
* CDRInfo objects are used to provide information on call logs managed by the system.
*****************************************************************************************************/
class CDRInfo
{
public:
	CDRInfo()
	{
		this->key = 0;
		this->id = 0;
		this->startDateYear = 1970;
		this->startDateMonth = 1;
		this->startDateDay = 1;
		this->startTimeHour = 0;
		this->startTimeMinute = 0;
		this->startTimeSecond = 0;
		this->callDuration = 0;
		this->answerDuration = 0;
		this->callFromTermId.clear();
		this->callToTermIdList.clear();
		this->callCompletionStatus = 0;
	}

	CDRInfo(const CDRInfo &cdrInfoAnother)
	{
		this->key = cdrInfoAnother.key;
		this->id = cdrInfoAnother.id;
		this->startDateYear = cdrInfoAnother.startDateYear;
		this->startDateMonth = cdrInfoAnother.startDateMonth;
		this->startDateDay = cdrInfoAnother.startDateDay;
		this->startTimeHour = cdrInfoAnother.startTimeHour;
		this->startTimeMinute = cdrInfoAnother.startTimeMinute;
		this->startTimeSecond = cdrInfoAnother.startTimeSecond;
		this->callDuration = cdrInfoAnother.callDuration;
		this->answerDuration = cdrInfoAnother.answerDuration;
		this->callFromTermId = cdrInfoAnother.callFromTermId;
		this->callToTermIdList = cdrInfoAnother.callToTermIdList;
		this->callCompletionStatus = cdrInfoAnother.callCompletionStatus;
	};

	~CDRInfo() {}

	int id;
	int startDateYear;
	int startDateMonth;
	int startDateDay;
	int startTimeHour;
	int startTimeMinute;
	int startTimeSecond;
	int callDuration;
	int answerDuration;
	std::string callFromTermId;
	std::string callToTermIdList;
	int callCompletionStatus;

	// HELPER-FUNCTIONS
	// Following functions will be filtered by SWIG. These need to be explictly
	// declared in the csharp.i and java.i.
	// We need them to be public for ease of implementation but don't wish to 
	// expose them to the client/end-user.
	void setCDRKey(int key)		{ this->key = key;  }
	int getCDRKey()				{ return this->key; }

private:
	int key;
};

/******************************************************************************************************
* TerminalInfo objects are used to provide information about the terminals configured in the system.
*****************************************************************************************************/
class TerminalInfo
{
public:
	TerminalInfo()
	{
		this->key = 0;
		this->termId = 0;
		this->termInfo.clear();
		this->termAddress.clear();
		this->termType.clear();
		this->termLocation.clear();
		this->termDescription.clear();
		this->termState = termInfo::S_UNKNOWN;
		this->termStateText = "UNKNOWN";
		this->termConnectedToPartyId = 0;
		this->termOnHoldByPartyId = 0;
		this->termCameraFeedURL.clear();
	}

	TerminalInfo(const TerminalInfo &terminalInfoAnother)
	{
		this->key = terminalInfoAnother.key;
		this->termId = terminalInfoAnother.termId;
		this->termInfo = terminalInfoAnother.termInfo;
		this->termAddress = terminalInfoAnother.termAddress;
		this->termType = terminalInfoAnother.termType;
		this->termLocation = terminalInfoAnother.termLocation;
		this->termDescription = terminalInfoAnother.termDescription;
		this->termState = terminalInfoAnother.termState;
		this->termStateText = terminalInfoAnother.termStateText;
		this->termConnectedToPartyId = terminalInfoAnother.termConnectedToPartyId;
		this->termOnHoldByPartyId = terminalInfoAnother.termOnHoldByPartyId;
		this->termCameraFeedURL = terminalInfoAnother.termCameraFeedURL;
	};

	~TerminalInfo() { }

	int termId;
	std::string termInfo;
	std::string termAddress;
	std::string termType;
	std::string termLocation;
	std::string termDescription;
	int termState;
	std::string termStateText;
	int termConnectedToPartyId;
	int termOnHoldByPartyId;
	std::string termCameraFeedURL;

	// HELPER-FUNCTIONS
	// Following functions will be filtered by SWIG. These need to be explictly
	// declared in the csharp.i and java.i.
	// We need them to be public for ease of implementation but don't wish to 
	// expose them to the client/end-user.
	void setTerminalKey(int key)	{ this->key = key;  }
	int getTerminalKey()			{ return this->key; }

private:
	int key;
};

/******************************************************************************************************
* The Trunk class is the base class and is inherited by SIPTrunk and ISDNTrunk classes
*****************************************************************************************************/
class Trunk
{
public:
	Trunk(int dstNo = 0, int elemNo = 0, const std::string &trunkDevId = "", callInfo::TRUNK_TYPE trunkType = callInfo::TRUNK_TYPE_SIP, const std::string &trunkName = "")
	{
		this->dstNo = dstNo;
		this->elemNo = elemNo;
		this->trunkDeviceId = trunkDevId;
		this->trunkType = trunkType;
		this->trunkName = trunkName;
	}
	~Trunk() {};
	void setDstNo(int dstNo)					{ this->dstNo = dstNo;         }
	int getDstNo()								{ return this->dstNo;          }
	void setElemNo(int elemNo)					{ this->elemNo = elemNo;       }
	int getElemNo()								{ return this->elemNo;         }
	void setType(callInfo::TRUNK_TYPE type)		{ this->trunkType = type;      }
	int getType()								{ return this->trunkType;      }
	void setName(const std::string &name)		{ this->trunkName = name;      }
	std::string getName() const					{ return this->trunkName;      }
	void setDeviceId(const std::string &devId)	{ this->trunkDeviceId = devId; }
	std::string getDeviceId() const				{ return this->trunkDeviceId;  }

private:
	int dstNo;
	int elemNo;
	callInfo::TRUNK_TYPE trunkType;
	std::string trunkName;
	std::string trunkDeviceId;
};

// An instance of SIP Trunk
class SIPTrunk : public Trunk
{
public:
	SIPTrunk() {};
	SIPTrunk(int dstNo, int elemNo, const std::string &trunkDevId, const std::string &trunkName) : Trunk(dstNo, elemNo, trunkDevId, callInfo::TRUNK_TYPE_SIP, trunkName) {};
	~SIPTrunk() {};
	void setStatus(callInfo::TRUNK_STATUS status)	{ this->sipStatus = status; }
	int getStatus()									{ return this->sipStatus;   }

private:
	callInfo::TRUNK_STATUS sipStatus;
};

// An instance of ISDN Trunk 
class ISDNTrunk : public Trunk
{
public:
	ISDNTrunk() {};
	ISDNTrunk(int dstNo, int elemNo, const std::string &trunkDevId, const std::string &trunkName) : Trunk(dstNo, elemNo, trunkDevId, callInfo::TRUNK_TYPE_E1, trunkName) {};
	~ISDNTrunk() {};
	void setLayer1Status(callInfo::TRUNK_STATUS status)		{ this->e1Layer1Status = status;   }
	int getLayer1Status()									{ return this->e1Layer1Status;     }
	void setLayer2_3Status(callInfo::TRUNK_STATUS status)	{ this->e1Layer2_3Status = status; }
	int getLayer2_3Status()									{ return this->e1Layer2_3Status;   }

private:
	callInfo::TRUNK_STATUS e1Layer1Status;
	callInfo::TRUNK_STATUS e1Layer2_3Status;
};

/******************************************************************************************************
* Call Observer to send real time events to the end user
*****************************************************************************************************/
class CallControllerObserver
{
public:
	/// Default Constructor
	CallControllerObserver()
	{
	}

	/**
	* Callback function - called when a new call is created or existing call parameters updated
	* This method will be executed in a separate thread created by
	* the SDK library.
	* @param reference to the new call added or exisitng call parameters updated
	**/
	virtual void onCallUpdate(CallInfo callInfo)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "CallControllerObserver::onCallUpdate(): callId >%ld< updated", callInfo.callId);
#else
		std::cout << "CallControllerObserver::onCallUpdate(): callId >" << callInfo.callId << "<" << std::endl;
#endif
	}

	/**
	* Callback function - called when a call is deleted from the system
	* This method will be executed in a separate thread created by
	* the SDK library.
	* @param Id of the call deleted from the system
	**/
	virtual void onCallDelete(int callId)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "CallControllerObserver::onCallDelete(): callId >%d deleted", callId);
#else
		std::cout << "CallControllerObserver::onCallDelete(): callId >" << callId << "<" << std::endl;
#endif
	}

	/**
	* Callback function - called when a new Call Detail Record (CDR) is created
	* or existing call parameters updated
	* This method will be executed in a separate thread created by
	* the SDK library.
	**/
	virtual void onCDRMessageUpdate(CDRInfo cdrInfo)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "CallControllerObserver::onCDRMessageUpdate(): cdrId >%d< updated", cdrInfo.id);
#else
		std::cout << "CallControllerObserver::onCDRMessageUpdate(): cdrId >" << cdrInfo.id << "< updated" << std::endl;
#endif
	}

	/**
	* Callback function - called when a CDR is deleted from the system
	* This method will be executed in a separate thread created by
	* the SDK library.
	**/
	virtual void onCDRMessageDelete(int cdrId)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "CallControllerObserver::onCDRMessageDelete(): cdrId >%d< deleted", cdrId);
#else
		std::cout << "CallControllerObserver::onCDRMessageDelete(): cdrId >" << cdrId << "< deleted" << std::endl;
#endif
	}

	/**
	* Callback function - called when a new Terminal is created or 
	* it's parameters updated
	* This method will be executed in a separate thread created by
	* the SDK library.
	**/
	virtual void onTerminalUpdate(TerminalInfo termInfo)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "CallControllerObserver::onTerminalUpdate(): termId >%d< updated", termInfo.termId);
#else
		std::cout << "CallControllerObserver::onTerminalUpdate(): termId >" << termInfo.termId << "< updated" << std::endl;
#endif
	}

	/**
	* Callback function - called when a Terminal is deleted from the system
	* This method will be executed in a separate thread created by
	* the SDK library.
	**/
	virtual void onTerminalDelete(int termId)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "CallControllerObserver::onTerminalDelete(): termId >%d< deleted", termId);
#else
		std::cout << "CallControllerObserver::onTerminalDelete(): termId >" << termId << "< deleted" << std::endl;
#endif
	}

	/**
	* Callback function - called when a SIP trunk is added/updated in the system
	**/
	virtual void onSIPTrunkUpdate(SIPTrunk sipTrunk)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "CallControllerObserver::onSIPTrunkUpdate(): trunkName >%s< updated in deviceId >%s<", sipTrunk.getName().c_str(), sipTrunk.getDeviceId().c_str());
#else
		std::cout << "CallControllerObserver::onSIPTrunkUpdate(): trunkName >" << sipTrunk.getName() << "< updated in deviceId >" << sipTrunk.getDeviceId() << "<" << std::endl;
#endif
	}

	/**
	* Callback function - called when a SIP trunk is deleted from the system
	**/
	virtual void onSIPTrunkDelete(SIPTrunk sipTrunk)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "CallControllerObserver::onSIPTrunkDelete(): trunkName >%s< deleted from deviceId >%s<", sipTrunk.getName().c_str(), sipTrunk.getDeviceId().c_str());
#else
		std::cout << "CallControllerObserver::onSIPTrunkDelete(): trunkName >" << sipTrunk.getName() << "< deleted from deviceId >" << sipTrunk.getDeviceId() << "<" << std::endl;
#endif
	}

	/**
	* Callback function - called when a ISDN trunk is added/updated in the system
	**/
	virtual void onISDNTrunkUpdate(ISDNTrunk isdnTrunk)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "CallControllerObserver::onISDNTrunkUpdate(): trunkName >%s< updated in deviceId >%s<", isdnTrunk.getName().c_str(), isdnTrunk.getDeviceId().c_str());
#else
		std::cout << "CallControllerObserver::onISDNTrunkUpdate(): trunkName >" << isdnTrunk.getName() << "< updated in deviceId >" << isdnTrunk.getDeviceId() << "<" << std::endl;
#endif
	}

	/**
	* Callback function - called when an ISDN trunk is deleted from the system
	**/
	virtual void onISDNTrunkDelete(ISDNTrunk isdnTrunk)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "CallControllerObserver::onISDNTrunkDelete(): trunkName >%s< deleted from deviceId >%s<", isdnTrunk.getName().c_str(), isdnTrunk.getDeviceId().c_str());
#else
		std::cout << "CallControllerObserver::onISDNTrunkDelete(): trunkName >" << isdnTrunk.getName() << "< deleted from deviceId >" << isdnTrunk.getDeviceId() << "<" << std::endl;
#endif
	}

	/**
	* Callback function - called when deleting the observer object
	*/
	virtual ~CallControllerObserver()
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "CallControllerObserver::~CallControllerObserver()");
#else
		std::cout << "CallControllerObserver::~CallControllerObserver()" << std::endl;
#endif
	}
};

/******************************************************************************************************
* An instance of this class can be used to control call flow and handle incoming and outgoing calls
* in a system.
*****************************************************************************************************/
class CallController
{
public:
	CallController();

	~CallController();

	/**
	* A collection of CallInfo objects. 
	*/
	typedef std::vector<CallInfo> CallInfoArray;

	/**
	* A collection of CallInfo objects.
	*/
	typedef std::vector<CallPartyInfo> CallPartyInfoArray;

	/**
	* A collection of CDRMessageInfo objects.
	*/
	typedef std::vector<CDRInfo> CDRInfoArray;

	/**
	* A collection of Terminal objects.
	*/
	typedef std::vector<TerminalInfo> TerminalInfoArray;

	/** A collection of strings. Note: The "clear" method is required to be called when the StringArray is declared
	* with size specified, for example, new StringArray(8). The "clear" method needs to be called once, before
	* any "add" actions.
	*/
	typedef std::vector<std::string> StringArray;

	/**
	* A collection of SIP Trunks
	*/
	typedef std::vector<SIPTrunk> SIPTrunkList;

	/**
	* A collection of ISDN Trunks
	*/
	typedef std::vector<ISDNTrunk> ISDNTrunkList;

	/**
	* Creates a destination map that allows each CXS (Communication Exchange Server)
	* to redirect incoming calls to a group destination in a PABX. The groups are required to be
	* programmed into the PABX.
	*
	* @param bPartyExtList List of Group Numbers to redirect incoming calls. The list should always contain at least one entry. The system will initiate a call to the first entry in the list. If the call is not answered within a predefined timeout period it will be escalated to the next entry in the list.
	* @param CLI Caller Line Identification of the incoming call as received by one of the call control servers (normally CXS1 and CXS2). If dynamic routing is enabled for a phone (e.g. as determined by an associated discrete signal), the CLI will be the phone ID followed by a suffix. The suffix will be set to 0 if the Digital Input signal associated with the phone is not asserted or is unknown. The suffix will be set to 1 if the Digital Input signal associated with the phone is asserted.
	* @param deviceID Device ID of the CXS where the call will be received. The only valid device type that can be specified for this field is CXS.
	* @throw std::invalid_argument This exception is thrown when an invalid ID number or device id is specified.
	*
	*/
	void createDestination(const StringArray& bPartyExtList, const std::string& CLI, const std::string& deviceID);

	/***
	* Resets and clears existing destination maps previously created by calling createDestinationMap().
	*
	* @param deviceID Device ID of the CXS where the call will be received. The only valid device type that can be specified for this field is CXS.
	*/
	void resetDestinationMap(const std::string& deviceID);

	/**
	* Allows a new call to be created from A-party to B-party.
	* 
	* @param aPartyCLI Caller Line Identification of A-Party
	* @param bPartyCLI Caller Line Identification of B-Party
	* @return Call reference ID which uniquely identifies this call
	* @throw std::invalid_argument This exception is thrown when an invalid CLI is specified.
	*/
	int createCall(const std::string& aPartyCLI, const std::string& bPartyCLI);

	/**
	* Allows a terminal to answer a call/caller. User MUST specify at least one of these two optional arguments: callReference and aPartyCLI
	* 
	* @param callReference unique identifier of a call. Is conditonally optional and can be set to 0 if not present.
	* @param bPartyCLI Caller Line Identification of B-Party. Is mandatory.
	* @param aPartyCLI Caller Line Identification of A-Party Is conditionally optional and can be left as empty if not present.
	* @return Call reference ID which uniquely identifies this call
	* @throw std::invalid_argument This exception is thrown when an invalid CLI is specified.
	*/
	void answerCallOnTerminal(int callReference, const std::string& bPartyCLI, const std::string& aPartyCLI);

	/**
	* Allows active call to be resumed.
	*
	* @param callReference - call ID as specified in CallInfo. call ID is optional and can be set to 0. 
	* @param bPartyCLI - B-party address to be resumed. B-party address is mandatory.
	* @throw std::invalid_argument This exception is thrown when an invalid call reference or CLI is specified.
	*/
	void resumeCall(int callReference, const std::string& bPartyCLI);

	/**
	* Allows a terminal to offhold a call/caller (which can be different from where the call was previously held)
	* 
	* @param callReference unique identifier of a call. Is conditonally optional and can be set to 0 if not present.
	* @param bPartyCLI Caller Line Identification of B-Party. Is mandatory.
	* @param aPartyCLI Caller Line Identification of A-Party Is conditionally optional and can be left as empty if not present.
	* @return Call reference ID which uniquely identifies this call
	* @throw std::invalid_argument This exception is thrown when an invalid CLI is specified.
	*/
	void resumeCallOnTerminal(int callReference, const std::string& bPartyCLI, const std::string& aPartyCLI);

	/**
	* Allows active call (or a call that has had one leg terminated) to be
	* reconnected to a new B-party. Note call transfer support is dependent on system architecture.
	*
	* @param callReference - call ID as specified in CallInfo
	* @param bPartyCLI - B-party address where the call will be redirected
	* @throw std::invalid_argument This exception is thrown when an invalid call reference or CLI is specified.
	*/
	void transferCall(int callReference, const std::string& bPartyCLI);

	/**
	* Allows active call to be put on hold.
	*
	* @param callReference - call ID as specified in CallInfo. call ID is optional and can be set to 0. 
	* @param bPartyCLI - B-party address to be put on hold. B-party address is mandatory.
	* @throw std::invalid_argument This exception is thrown when an invalid call reference or CLI is specified.
	*/
	void holdCall(int callReference, const std::string& bPartyCLI);

	/**
	* Allows call to be terminated with the ability to restrict this to only
	* one leg of the call.
	*
	* @param callReference - call ID as specified in CallInfo
	* @param callConnection - send empty string to release all connections. To release a specific connection (e.g. current B-party) but leave the call active in the system specify the address of the connection to be released. Note support for releasing a specific connection is dependent on system architecture.
	* @throw std::invalid_argument This exception is thrown when an invalid call reference or call leg is specified.
	*/
	void terminateCall(int callReference, const std::string& callConnection);

	/**
	* Allows call to be rejected with the ability to provide a clear down cause code.
	* ftp://ftp.sangoma.com/vega/docs/IN_18-Q850_cleardown_cause_codes_10.pdf
	* When calls clear in SIP, H.323 and ISDN a "Cleardown Cause Code" is provided
	* to indicate the reason why the call cleared down. The specification which defines
	* the values and meanings of the Cleardown Cause Codes is the ITU (International
	* Telecommunication Union) specification Q.850.
	*
	* @param callReference - call ID as specified in CallInfo
	* @param cleardownCauseCode - cleardown cause code as defiend in the ITU-Q.850
	* @throw std::invalid_argument This exception is thrown when an invalid call reference
	*/
	void terminateCall(int callReference, int cleardownCauseCode);

	/**
	* List all active calls in the system.
	* @return Current active calls.
	**/
	CallInfoArray getCalls();

	/**
	* List all active call logs in the system.
	* @return Current call logs
	**/
	CDRInfoArray getCDRMessages();

	/**
	* List all active calls in the system.
	* @return Current active calls.
	**/
	TerminalInfoArray getTerminalList();

	/** Register observer to get information on events as they come */
	void registerObserver(CallControllerObserver *callObserver);

	/* Return a list of all available SIP Trunks configured in the system */
	SIPTrunkList getSIPTrunks();

	/* Return a list of all available ISDN Trunks configured in the system */
	ISDNTrunkList getISDNTrunks();

	// Enables detailed party information for call.
	void enableCallPartyRecord();

	// Disables detailed party information for call.
	void disableCallPartyRecord();

	// Indicates if detailed party information is enabled/disabled
	bool isCallPartyInfoEnabled();

	// HELPER-FUNCTIONS
	// Following functions will be filtered by SWIG. These need to be explictly
	// declared in the csharp.i and java.i.
	// We need them to be public for ease of implementation but don't wish to 
	// expose them to the client/end-user.
	void initCallResources();
	void addUpdateCallInfo(DVAlist *elemArgList);
	void deleteCallInfo(int elemNo);
	void addUpdateCDRInfo(int elemNo, DVAlist *elemArgList);
	void deleteCDRInfo(int elemNo);
	void addUpdateTerminalInfoFromResDirectory(int elemNo, DVAlist *elemArgList);
	void addUpdateTerminalInfoFromResCall(int elemNo, DVAlist *elemArgList);
	void deleteTerminalInfo(int elemNo);
	void addUpdateTrunkStatus(int dstNo, int elemNo, DVAlist *elemArgList);
	void deleteTrunkStatus(int dstNo, int elemNo);
	void updatePartyInfoForCall_From_StreamingCoordinator(int elemNo, DVAlist *elemArgList);

private:
	CallInfoArray callList;
	oaLocalMutex callControllerMtx;
	CDRInfoArray cdrList;
	TerminalInfoArray terminalList;
	std::vector<CallControllerObserver *> callControllerObservers;
	SIPTrunkList sipTrunkList;
	ISDNTrunkList isdnTrunkList;
	bool callPartyInfoEnabled;
};

#endif // _CALL_CONTROLLER_HPP_
