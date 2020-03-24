#ifndef _PA_CONTROLLER_HPP_
#define _PA_CONTROLLER_HPP_

#include "StandardIncludes.hpp"
#include "paInfo.hpp"
#include "priorityInfo.h"

// Forward Declaration
class AudioServer;
class TTSVocalizer;

struct PaSourceLockStatus
{
	int sourceId;
	std::string lockedUUID;
};

struct PaSWTrigger
{
	PaTrigger trigger;
	int associatedSourceId;
	std::string preChime;
	std::string displayParameters;
};

class ANNOUNCEMENT_REQUEST
{
public:
	int id;
	std::string uuid;
	std::string stateText;
	std::string startDateTime;
	std::string message;
	std::string priorityText;

	ANNOUNCEMENT_REQUEST()
	{
		this->id = 0;
		this->uuid.clear();
		this->stateText.clear();
		this->startDateTime.clear();
		this->message.clear();
		this->priorityText.clear();
	}

	ANNOUNCEMENT_REQUEST(const ANNOUNCEMENT_REQUEST &reqAnother)
	{
		this->id = reqAnother.id;
		this->uuid = reqAnother.uuid;
		this->stateText = reqAnother.stateText;
		this->startDateTime = reqAnother.startDateTime;
		this->message = reqAnother.message;
		this->priorityText = reqAnother.priorityText;
	}
};

class PaZone
{
public:
	unsigned int elemNo;
	std::string id;
	int activity;
	std::string activityText;
	int announcementType;
	std::string announcementTypeText;
	int health;
	std::string healthText;
	std::vector<PaSink> getMembers() const;

	PaZone()
	{
		this->elemNo = 0;
		this->id.clear();
		this->activity = 0;
		this->activityText.clear();
		this->announcementType = 0;
		this->announcementTypeText.clear();
		this->health = 0;
		this->healthText.clear();
	}

	PaZone(const PaZone &paZoneAnother)
	{
		// Deep copy all attributes
		this->elemNo = paZoneAnother.elemNo;
		this->id = paZoneAnother.id;
		this->activity = paZoneAnother.activity;
		this->activityText = paZoneAnother.activityText;
		this->announcementType = paZoneAnother.announcementType;
		this->announcementTypeText = paZoneAnother.announcementTypeText;
		this->health = paZoneAnother.health;
		this->healthText = paZoneAnother.healthText;
	};
};

/**
* ScheduleDefinition is used to schedule message within the Audio Server system.
**/
class ScheduleDefinition
{
public:
	ScheduleDefinition()
	{
		this->Id = 0;
		this->descrption.clear();
		this->startYear = 0;
		this->startMonth = 0;
		this->startDay = 0;
		this->startHour = 0;
		this->startMinute = 0;
		this->endYear = 0;
		this->endMonth = 0;
		this->endDay = 0;
		this->endHour = 0;
		this->endMinute = 0;
		this->dayMask = 0;
		this->frequency = 0;
		this->paZones.clear();
		this->priorityMode = AnnouncementPriorityMode(APM_ABSOLUTE_PRIORITY);
		this->priorityLevel = DEFAULT_PRIORITY_LEVEL;
		this->announcementType = AnnouncementType(AT_NONE);
		this->visualAnnouncementType = AnnouncementType(AT_NONE);
		this->dictionaryItems.clear();
		this->repeatCnt = 1;
		this->ttsList.clear();
		this->storedDataSourceId = 0;
		this->storedDataFilePath.clear();
		this->gain = 0;
		this->chime.clear();
		this->displayTemplateName.clear();
		this->displayTemplateKeyValueList.clear();
		this->displayTemplateValidityPeriodSeconds = 0;
	}

	ScheduleDefinition(const ScheduleDefinition &scheduleDefinitionAnother)
	{
		this->Id = scheduleDefinitionAnother.Id;
		this->descrption = scheduleDefinitionAnother.descrption;
		this->startYear = scheduleDefinitionAnother.startYear;
		this->startMonth = scheduleDefinitionAnother.startMonth;
		this->startDay = scheduleDefinitionAnother.startDay;
		this->startHour = scheduleDefinitionAnother.startHour;
		this->startMinute = scheduleDefinitionAnother.startMinute;
		this->endYear = scheduleDefinitionAnother.endYear;
		this->endMonth = scheduleDefinitionAnother.endMonth;
		this->endDay = scheduleDefinitionAnother.endDay;
		this->endHour = scheduleDefinitionAnother.endHour;
		this->endMinute = scheduleDefinitionAnother.endMinute;
		this->dayMask = scheduleDefinitionAnother.dayMask;
		this->frequency = scheduleDefinitionAnother.frequency;
		this->paZones.clear();
		this->paZones = scheduleDefinitionAnother.paZones;
		this->priorityMode = scheduleDefinitionAnother.priorityMode;
		this->priorityLevel = scheduleDefinitionAnother.priorityLevel;
		this->announcementType = scheduleDefinitionAnother.announcementType;
		this->visualAnnouncementType = scheduleDefinitionAnother.visualAnnouncementType;
		this->dictionaryItems.clear();
		this->dictionaryItems = scheduleDefinitionAnother.dictionaryItems;
		this->repeatCnt = scheduleDefinitionAnother.repeatCnt;
		this->ttsList.clear();
		this->ttsList = scheduleDefinitionAnother.ttsList;
		this->storedDataSourceId = scheduleDefinitionAnother.storedDataSourceId;
		this->storedDataFilePath = scheduleDefinitionAnother.storedDataFilePath;
		this->gain = scheduleDefinitionAnother.gain;
		this->chime = scheduleDefinitionAnother.chime;
		this->displayTemplateName = scheduleDefinitionAnother.displayTemplateName;
		this->displayTemplateKeyValueList.clear();
		this->displayTemplateKeyValueList = scheduleDefinitionAnother.displayTemplateKeyValueList;
		this->displayTemplateValidityPeriodSeconds = scheduleDefinitionAnother.displayTemplateValidityPeriodSeconds;
	}

	~ScheduleDefinition() { };

	// Id of the schedule message
	int Id;

	// Description about this schedule
	std::string descrption;

	// Year in which the schedule messages are to start
	int startYear;

	// Month in which the schedule messages are to start
	int startMonth;

	// Day in which the schedule messages are to start
	int startDay;

	// Time in Hours when the schedule messages are to start on each day
	int startHour;

	// Time in Minutes when the schedule messages are to start on each day
	int startMinute;

	// Year in which the schedule messages are to finish
	int endYear;

	// Month in which the schedule messages are to finish
	int endMonth;

	// Day in which the schedule messages are to finish
	int endDay;

	// Time in Hours when the schedule messages are to finish on each day
	int endHour;

	// Time in Minutes when the schedule messages are to start on each day
	int endMinute;

	/** Days of the week on which the schedule messages are to occur. This
	field also allows Public Holiday days to be specified.
	If field null, Day Mask will default to all days.
	<p><li>|-----------------------------------------|</li></p>
	<p><li>| Type              Hex           Decimal |</li></p>
	<p><li>|-----------------------------------------|</li></p>
	<p><li>| none              0               0     |</li></p>
	<p><li>| Sunday            0x1             1     |</li></p>
	<p><li>| Monday            0x2             2     |</li></p>
	<p><li>| Tuesday           0x4             4     |</li></p>
	<p><li>| Wednesday         0x8             8     |</li></p>
	<p><li>| Thursday          0x10            16    |</li></p>
	<p><li>| Friday            0x20            32    |</li></p>
	<p><li>| Saturday          0x40            64    |</li></p>
	<p><li>| Public Holiday    0x80            128   |</li></p>
	<p><li>|-----------------------------------------|</li></p>
	**/
	int dayMask;

	// Frequency in seconds to play the message. If field null, frequency will be sent to once per day (i.e. 24 hours)
	int frequency;

	// List of target destinations where the message is to be played.
	std::vector<std::string> paZones;

	// The priority mode to use when playing the announcement (0=absolute, 1=relative).
	AnnouncementPriorityMode priorityMode;

	// The priority level (absolute or relative depending on mode) to use when playing the announcement.
	int priorityLevel;

	// AT_DVA/AT_TTS/AT_TTS_SAF/AT_STORED_DATA/AT_STORED_DATA_SAF
	AnnouncementType announcementType;

	// AT_DISPLAY_TEMPLATE
	AnnouncementType visualAnnouncementType;

	// List of dictionary items to be played if AnnouncementType is AT_DVA
	std::vector<unsigned int> dictionaryItems;

	// Number of times the DVA announcement. Not applicable to other types
	int repeatCnt;

	// TTS entries if AnnouncementyType is AT_TTS/AT_TTS_SAF
	std::vector<TTSVocalizer> ttsList;

	// Stored Data pasourceId if AnnouncementType is AT_STORED_DATA/AT_STORED_DATA_SAF
	int storedDataSourceId;

	// Stored Data file path if AnnouncementType is AT_STORED_DATA/AT_STORED_DATA_SAF
	std::string storedDataFilePath;

	// template name if AnnouncementType is AT_DISPLAY_TEMPLATE
	std::string displayTemplateName;

	// key-value pair of variables if AnnouncementType is AT_DISPLAY_TEMPLATE
	std::map<std::string, std::string> displayTemplateKeyValueList;

	// validity period if AnnouncementType is AT_DISPLAY_TEMPLATE.
	//		-1: valid forever(never gets deleted from the announcement queue
	//		0 - maxint: Validity period in seconds.The message will be attempted to be displayed as soon as the message becomes the highest priority item.The message will stay in the announcement queue for the specified duration.
	//		Items to consider:
	//		If 0: This indicates the message is "one-shot".If it can be displayed now, will be displayed, if not will be discarded.
	//		Message Stale timeout: This is normally set to 5 minutes and if a message is not displayed within this time for the first time, it is removed.If the Message Validity period is specified, the Stale timeout should be ignored.
	int displayTemplateValidityPeriodSeconds;

	// gain adjustment
	double gain;

	// chime segment to be played before start of announcement
	std::string chime;
};

// VisualDisplay
class VisualDisplay
{
private:
	std::string displayName;
	std::string commsType;
	std::string ipAddress;
	std::string healthState;

public:
	// Returns the device name assigned to the display device
	void setName(const std::string &name)				{ this->displayName = name;		}
	std::string getName()								{ return (this->displayName);	}

	// Retrieves the method used for communications with the device
	void setCommsType(const std::string &commsType)		{ this->commsType = commsType;	}
	std::string getCommsType()							{ return (this->commsType);		}

	// Returns the address used for communications to the display device
	void setAddress(const std::string &address)			{ this->ipAddress = address;	}
	std::string getAddress()							{ return (this->ipAddress);		}

	// Returns the current health test state for the VisualDisplay
	void setHealthState(const std::string &state)		{ this->healthState = state;	}
	std::string getHealthState()						{ return (this->healthState);	}
};

/**
 * PlayMessageParams.
 **/
class PlayMessageParams
{
public:
	bool resumeOnInterruptFlag;
	bool overrideExisting;
	unsigned int validityPeriod;
	unsigned int priority;
	unsigned int mode;

	PlayMessageParams()
	{
		this->resumeOnInterruptFlag = false;
		this->overrideExisting = false;
		this->validityPeriod = 0;
		this->priority = 0;
		this->mode = 0;
	}
};

class MessagePriority
{
public:
	MessagePriority()
	{
		this->mode = AnnouncementPriorityMode(APM_ABSOLUTE_PRIORITY);
		this->level = 500;
	}

	MessagePriority(const MessagePriority &messagePriorityAnother)
	{
		this->mode = messagePriorityAnother.mode;
		this->level = messagePriorityAnother.level;
	}

	MessagePriority (AnnouncementPriorityMode mode, int level)
	{
		this->mode = mode;
		this->level = level;
	}

	void setPriorityMode(AnnouncementPriorityMode mode) { this->mode = mode; }
	AnnouncementPriorityMode getPriorityMode() { return (this->mode); }

	void setPrioritylevel(int level) { this->level = level; }
	int getPriorityLevel() { return (this->level); }

private:
	AnnouncementPriorityMode mode;
	int level;
};

class DISPLAY_VALUE_INFO
{
public:
	std::string text;
	int timeout;

	DISPLAY_VALUE_INFO()
	{
		this->text.clear();
		this->timeout = 0;
	}

	DISPLAY_VALUE_INFO(const std::string &_text, const int &_timeout)
	{
		this->text = _text;
		this->timeout = _timeout;
	}
};

class Message
{
public:
	// this should match AnnouncementType in priorityInfo.h
	enum Type
	{
		LIVE_STREAM = 0,			// equals AT_LIVE
		RECORDED_STREAM = 1,		// equals AT_STORED_DATA
		DICTIONARY = 2,				// equals AT_DVA
		DISPLAY = 3,				// equals AT_DISPLAY_v0
		TTS = 4,					// equals AT_TTS
		BGM = 5,					// equals AT_BGM
		MIXED = 6,					// equals AT_MIXED
		NONE = 7,					// equals AT_NONE
		TEMPLATE = 8,				// equals AT_TEMPLATE
		DISPLAY_TEMPLATE = 9,		// equals AT_DISPLAY_TEMPLATE
		TTS_SAF = 10,				// equals AT_TTS_SAF
		RECORDED_STREAM_SAF = 11	// equals AT_STORED_DATA_SAF
	};

	enum TextEncoding
	{
		ISO_8859_1 = 0,
		UTF_8
	};

	Message()
	{
		this->requestId.clear();
		//this->priority;
		this->preChime = 0;
		this->postChime = 0;
		this->gain = 0;
		this->overrideDefaultGain = false;
		this->audioMessageType = Message::NONE;
		this->visualMessageType = Message::NONE;
		this->audioDictionaryItems.clear();
		this->ttsList.clear();
		this->recordedStreamAbsoluteFilePathWithName.clear();
		this->displayText.clear();
		this->displayDictionaryItems.clear();
		this->playedOnce = false;
		this->repeatCnt = 1;				// 0 = loop until stopped, else play for x times
		this->displayKeyValueList.clear();
		this->displayValidityPeriod = -1;	// -1 = indefinitely, -2 = stop when correcponding audio finishes, 0 or more = finite time
	}

	Message(const Message &messageAnother)
	{
		this->requestId = messageAnother.requestId;
		this->priority = messageAnother.priority;
		this->preChime = messageAnother.preChime;
		this->postChime = messageAnother.postChime;
		this->gain = messageAnother.gain;
		this->overrideDefaultGain = messageAnother.overrideDefaultGain;
		this->audioMessageType = messageAnother.audioMessageType;
		this->visualMessageType = messageAnother.visualMessageType;
		this->audioDictionaryItems = messageAnother.audioDictionaryItems;
		this->ttsList.clear();
		this->ttsList = messageAnother.ttsList;
		this->recordedStreamAbsoluteFilePathWithName = messageAnother.recordedStreamAbsoluteFilePathWithName;
		this->displayText = messageAnother.displayText;
		this->displayDictionaryItems = messageAnother.displayDictionaryItems;
		this->playedOnce = messageAnother.playedOnce;
		this->repeatCnt = messageAnother.repeatCnt;
		this->displayKeyValueList.clear();
		this->displayKeyValueList = messageAnother.displayKeyValueList;
		this->displayValidityPeriod = messageAnother.displayValidityPeriod;
	}

	void setRequestID(const std::string &id);
	std::string getRequestID();
	void setPriority(const MessagePriority &priority);
	MessagePriority getPriority();
	void setPreChime(int chime);
	int getPreChime();
	void setPostChime(int chime);
	int getPostChime();
	void setGain(double gain);
	double getGain();
	bool getOverrideDefaultGain();
	void setAudioMessageType(Type type);
	Type getAudioMessageType();
	void setVisualMessageType(Type type);
	Type getVisualMessageType();
	void setAudioMessage(const std::vector<unsigned int> &dictionaryItems) throw (std::invalid_argument, std::out_of_range);
	std::vector<unsigned int> getAudioMessageDictionaryItems();
	void setAudioMessage(const std::string &text, const std::string &language, const std::string &voice, TextEncoding encoding = UTF_8) throw (std::invalid_argument, std::out_of_range);
	void setAudioMessage(const std::vector<TTSVocalizer> &ttsList) throw (std::invalid_argument, std::out_of_range);
	std::string getAudioMessageTTSText();
	std::string getAudioMessageTTSLanguage();
	std::string getAudioMessageTTSVoice();
	TextEncoding getAudioMessageTTSEncoding();
	void setAudioMessage(const std::string &absolutePathWithFileName);
	std::string getAudioMessageAbsoluteFilePath();
	int getAudioMessageTTSRate();
	void setAudioMessage(const std::string &templateName, const std::map<std::string, std::string> &arguments) throw (std::invalid_argument, std::out_of_range);
	void setVisualMessage(const std::vector<unsigned int> &dictionaryItems) throw (std::invalid_argument, std::out_of_range);
	std::vector<unsigned int> getVisualMessageDictionaryItems();
	void setVisualMessage(const std::string &text) throw (std::invalid_argument, std::out_of_range);
	std::string getVisualMessageText();
	void setVisualMessage(const std::string &_templateName, const std::map <std::string, std::vector<DISPLAY_VALUE_INFO> > &_keyValueList, int validity = -1) throw (std::invalid_argument, std::out_of_range);
	std::string playAnnouncement(const std::vector<std::string>& zones) throw (std::invalid_argument);
	std::string retrieveAudioMessage() throw (std::invalid_argument, std::out_of_range);
	void setRepeatCnt(int);
	int getRepeatCnt();

private:
	static const unsigned int minRequestIdLength_ = 6;
	static const unsigned int maxRequestIdLength_ = 63;

	std::string requestId;
	MessagePriority priority;
	int preChime;
	int postChime;
	double gain;
	bool overrideDefaultGain;
	Type audioMessageType;
	Type visualMessageType;
	std::vector<unsigned int> audioDictionaryItems;
	std::vector<TTSVocalizer> ttsList;
	std::string recordedStreamAbsoluteFilePathWithName;
	std::string displayText;
	std::vector<unsigned int> displayDictionaryItems;
	bool playedOnce;
	int repeatCnt;
	std::map<std::string, std::vector<DISPLAY_VALUE_INFO> > displayKeyValueList;
	int displayValidityPeriod;
};

class TTSVocalizer
{
public:
	int portNo;
	std::string text;
	std::string language;
	std::string voice;
	int rate;
	Message::TextEncoding encoding;

	TTSVocalizer()
	{
		this->portNo = 0;
		this->text.clear();
		this->language.clear();
		this->voice.clear();
		this->rate = 48000;
		this->encoding = Message::UTF_8;
	}
};

class TTSDictionary
{
public:
	int key;
	int portNo;
	std::string text;
	std::string fullName;
	std::string size;
	std::string checksum;
	std::string uuid;
	std::string uri;

	TTSDictionary()
	{
		this->key = 0;
		this->portNo = 0;
		this->text.clear();
		this->fullName.clear();
		this->size.clear();
		this->checksum.clear();
		this->uuid.clear();
		this->uri.clear();
	}
};

class TTSPendingInfo
{
public:
	int portNo;
	std::string language;
	std::string voice;
	int rate;
	std::string text;
	time_t startTime;

	TTSPendingInfo()
	{
		this->portNo = 0;
		this->language.clear();
		this->voice.clear();
		this->rate = 0;
		this->text.clear();
		this->startTime = 0;
	}

	TTSPendingInfo(const int portNo, const std::string &language, const std::string &voice, const int rate, const std::string &text, const time_t startTime)
	{
		this->portNo = portNo;
		this->language = language;
		this->voice = voice;
		this->rate = rate;
		this->text = text;
		this->startTime = startTime;
	}
};

class PAControllerObserver
{
public:
	enum MessageRetrievalError
	{
		MRE_NO_ERROR = 0,
		MRE_TIMED_OUT,
		MRE_INVALID_PARAMS
	};

	/// Default Constructor
	PAControllerObserver()
	{
	}

	/**
	* Callback function - called when a PA Source is updated.
	* This method will be executed in a separate thread created by
	* the SDK library.
	* @param device Reference to the PA Source that has changed state
	**/
	virtual void onPaSourceUpdate(PaSource source)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "PAControllerObserver::onPaSourceUpdate(): PaSourceId >%d< updated", source.id);
#else
		std::cout << "PAControllerObserver::onPaSourceUpdate(): PaSourceId >" << source.id << "< updated" << std::endl;
#endif
	}

	/**
	* Callback function - called when a PA Source is deleted.
	* This method will be executed in a separate thread created by
	* the SDK library.
	* @param device Id of the PA Source removed from the list
	**/
	virtual void onPaSourceDelete(int sourceId)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "PAControllerObserver::onPaSourceDelete(): PaSourceId >%d< deleted", sourceId);
#else
		std::cout << "PAControllerObserver::onPaSourceDelete(): PaSourceId >" << sourceId << "< deleted" << std::endl;
#endif
	}

	/**
	* Callback function - called when a PA Sink is updated.
	* This method will be executed in a separate thread created by
	* the SDK library.
	* @param device Reference to the PA Sink that has changed state
	**/
	virtual void onPaSinkUpdate(PaSink sink)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "PAControllerObserver::onPaSinkUpdate(): PaSinkId >%d< updated", sink.id);
#else
		std::cout << "PAControllerObserver::onPaSinkUpdate(): PaSinkId >" << sink.id << "< updated" << std::endl;
#endif
	}

	/**
	* Callback function - called when a PA Sink is deleted.
	* This method will be executed in a separate thread created by
	* the SDK library.
	* @param device Id of the PA Sink removed from the list
	**/
	virtual void onPaSinkDelete(int sinkId)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "PAControllerObserver::onPaSinkDelete(): PaSinkId >%d< deleted", sinkId);
#else
		std::cout << "PAControllerObserver::onPaSinkDelete(): PaSinkId >" << sinkId << "< deleted" << std::endl;
#endif
	}

	/**
	* Callback function - called when a PA Trigger is updated.
	* This method will be executed in a separate thread created by
	* the SDK library.
	* @param device Reference to the PA Trigger that has changed state
	**/
	virtual void onPaTriggerUpdate(PaTrigger trigger)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "PAControllerObserver::onPaTriggerUpdate(): PaTriggerId >%d< updated", trigger.id);
#else
		std::cout << "PAControllerObserver::onPaTriggerUpdate(): PaTriggerId >" << trigger.id << "< updated" << std::endl;
#endif
	}

	/**
	* Callback function - called when a PA Trigger is deleted.
	* This method will be executed in a separate thread created by
	* the SDK library.
	* @param device Id of the PA Trigger removed from the list
	**/
	virtual void onPaTriggerDelete(int triggerId)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "PAControllerObserver::onPaTriggerDelete(): PaTriggerId >%d< deleted", triggerId);
#else
		std::cout << "PAControllerObserver::onPaTriggerDelete(): PaTriggerId >" << triggerId << "< deleted" << std::endl;
#endif
	}

	/**
	* Callback function - called when a PA Selector is updated.
	* This method will be executed in a separate thread created by
	* the SDK library.
	* @param device Reference to the PA Selector that has changed state
	**/
	virtual void onPaSelectorUpdate(PaSelector selector)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "PAControllerObserver::onPaSelectorUpdate(): PaSelectorId >%d< updated", selector.id);
#else
		std::cout << "PAControllerObserver::onPaSelectorUpdate(): PaSelectorId >" << selector.id << "< updated" << std::endl;
#endif
	}

	/**
	* Callback function - called when a PA Selector is deleted.
	* This method will be executed in a separate thread created by
	* the SDK library.
	* @param device Id of the PA Selector removed from the list
	**/
	virtual void onPaSelectorDelete(int selectorId)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "PAControllerObserver::onPaSelectorDelete(): PaSelectorId >%d< deleted", selectorId);
#else
		std::cout << "PAControllerObserver::onPaSelectorDelete(): PaSelectorId >" << selectorId << "< deleted" << std::endl;
#endif
	}

	// Callback function - This method will be executed in a separate thread created by the SDK library.
	virtual void onScheduleUpdate(ScheduleDefinition schedule)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "PAControllerObserver::onScheduleUpdate(): scheduleId=%d", schedule.Id);
#else
		std::cout << "PAControllerObserver::onScheduleUpdate(): scheduleId=" << schedule.Id << std::endl;
#endif
	}

	// Callback function - This method will be executed in a separate thread created by the SDK library.
	virtual void onScheduleDelete(ScheduleDefinition schedule)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "PAControllerObserver::onScheduleDelete(): scheduleId=%d", schedule.Id);
#else
		std::cout << "PAControllerObserver::onScheduleDelete(): scheduleId=" << schedule.Id << std::endl;
#endif
	}

	// Callback function - This method will be executed in a separate thread created by the SDK library.
	virtual void onPaZoneUpdate(PaZone paZone)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "PAControllerObserver::onPaZoneUpdate()");
#else
		std::cout << "PAControllerObserver::onPaZoneUpdate()" << std::endl;
#endif
	}

	// Callback function - This method will be executed in a separate thread created by the SDK library.
	virtual void onPaZoneDelete(std::string zoneId)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "PAControllerObserver::onPaZoneDelete()");
#else
		std::cout << "PAControllerObserver::onPaZoneDelete()" << std::endl;
#endif
	}

	// Callback function - This method will be executed in a separate thread created by the SDK library.
	//						It is called to retrieve the audio message due to a previous call to Message::retrieveAudioMessage is complete.
	// PARAMETERS:
	//	uuid Identifier assigned to the audio content retrieval request by the previous invocation to Message::retrieveAudioMessage
	//	success Indicates if audio message could be retrieved.True indicates success and false indicates failure.
	//	errorCode Set to one of the supported enumerated error codes on failure.
	//	uri HTTP URI that can be used to access the audio message contents.
	virtual void onAudioMessageRetrievalComplete(const std::string& uuid, bool success, MessageRetrievalError errorCode, const std::string& uri)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "PAControllerObserver::onAudioMessageRetrievalComplete()");
#else
		std::cout << "PAControllerObserver::onAudioMessageRetrievalComplete()" << std::endl;
#endif
	}

	virtual void onMessageUpdate(ANNOUNCEMENT_REQUEST req)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "PAControllerObserver::onMessageUpdate()");
#else
		std::cout << "PAControllerObserver::onMessageUpdate()" << std::endl;
#endif
	}

	virtual void onMessageDelete(ANNOUNCEMENT_REQUEST req)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "PAControllerObserver::onMessageDelete()");
#else
		std::cout << "PAControllerObserver::onMessageDelete()" << std::endl;
#endif
	}

	// Callback function - called when deleting the observer object
	virtual ~PAControllerObserver()
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "PAControllerObserver::~PAControllerObserver()");
#else
		std::cout << "PAControllerObserver::~PAControllerObserver()" << std::endl;
#endif
	}
};

/**
* Provides an interface to control Public Address functionality.
*/
class PAController : public threadClass
{
public:
	PAController();

	~PAController();

	// A collection of PA Sources
	typedef std::vector<PaSource> PaSourceArray;

	// A collection of PA Sinks
	typedef std::vector<PaSink> PaSinkArray;

	// A collection of PA Zones
	typedef std::vector<PaZone> PaZoneArray ;

	// A collection of PA Triggers (both hardware and software)
	typedef std::vector<PaTrigger> PaTriggerArray;

	// A collection of PA Selectors
	typedef std::vector<PaSelector> PaSelectorArray;

	// A collection of schedule messages that are currently stored in the server
	typedef std::vector<ScheduleDefinition> ScheduleDefinitionArray;

	// A collection of TTS ports
	typedef std::vector<TTSVocalizer> TTSVocalizerArray;

	/**
	* Returns the list of available PA sources (accessible to this user). This list may be cross checked
	* against a pre-configured source which the program has configured or alternatively provides a list
	* for the user to select one.
	*
	* @return List of available audio sources. The returned list will be empty if not connected to the NetSpire server.
	*/
	PaSourceArray getPaSources();

	/**
	* Allows an application to bind to a source. Only one application can bind to a source at a time.
	*
	* @param sourceId Specifies the selected PA Source Id
	* @return true if binding to source was successful else false
	* @throw std::invalid_argument (IllegalArgumentException in Java) if audio source is not available
	*        (due to comms status or is already associated with another application)
	*        std::out_of_range (IndexOutOfBoundsException in Java) if audio source Id does not exist.
	*/
	bool attachPaSource(int sourceId)
		throw (std::invalid_argument, std::out_of_range);

	void enablePaSourceVUMeter(int sourceId);

	void disablePaSourceVUMeter(int sourceId);

	/**
	* Allows an application to unbind from a source
	* 
	* @param sourceId Specifies the selected PA Source Id
	* @return true if unbinding to source was successful else false
	* @throw std::invalid_argument (IllegalArgumentException in Java) if audio source is not available
	*        (due to comms status or is already associated with another application)
	*        std::out_of_range (IndexOutOfBoundsException in Java) if audio source Id does not exist.
	*/
	bool detachPaSource(int sourceId)
		throw (std::invalid_argument, std::out_of_range);

	/** 
	* This function returns a list of all audio sinks that are available to make announcements to from this
	* source. Note that a specific sink can be a fine a granularity as a single speaker and not necessarily
	* refer to a larger area (eg. Factory floor or concourse ).
	* The sinkID and descriptions are non-volatile and will be consistent from one application invocation to
	* the next as long as the configuration of the system has not changed.
	* This list will contain all available sinks including those that are not attached to the specified source.
	*
	* @param sourceId Specifies the PA Source Id for which available sinks are being queried
	* @return List of available audio sinks. The returned list will be empty if not connected to the NetSpire server.
	* @throw std::out_of_range (IndexOutOfBoundsException in Java) if sourceId does not exist.
	*/ 
	PaSinkArray getPaSinks(int sourceId)
		throw (std::out_of_range);

	// This function returns a list of all audio sinks that can be used to direct announcements.
	// All PaSinks will be listed irrespective of PaSources.
	PaSinkArray getPaSinks();

	// This function returns a list of all PaZones defined in the system.
	// Zones are part of the system configuration and can be customised by users to suit system zoning requirements.
	PaZoneArray getPaZones();

	/**
	* Returns a list of availale hardware triggers for a particular source Id. A PA announcement will start
	* when a trigger is activated and end when the trigger is de-activated.
	*
	* @param sourceId Specifies the selected PA Source Id
	* @return Returns a list of available hardware triggers for the selected PA Source Id
	* @throw std::invalid_argument (IllegalArgumentException in Java) if audio source is not available
	*        (due to comms status or is already associated with another application)
	*        std::out_of_range (IndexOutOfBoundsException in Java) if audio source Id does not exist.
	*/
	PaTriggerArray getHwPaTriggers(int sourceId)
		throw (std::out_of_range, std::invalid_argument);

	/**
	* Allows an application to get the current state of the hardware trigger.
	*
	* @param triggerId Specifies the selected PA Trigger Id
	* @return Returns the current state of the selected PA Trigger Id
	* @throw std::out_of_range (IndexOutOfBoundsException in Java) if trigger Id does not exist.
	*/
	PaTrigger::State getHwPaTriggerState(int triggerId)
		throw (std::out_of_range);

	/**
	* Allows a software trigger to be created by the application. This allows a GUI application
	* or third party interface or hardware device to make announcements in the system.
	*
	* @param sourceId Specifies the selected PA Source Id
	* @param triggerPriority The priority level for the new SW trigger. Value is in the range of 0 being lowest and 254 being highest. 
	* @return triggerId Returns the newly created Trigger Id
	* @throw std::invalid_argument (IllegalArgumentException in Java) if audio source Id is not available
	*        (due to comms status or is already associated with another application)
	*        std::out_of_range (IndexOutOfBoundsException in Java) if audio source Id does not exist.
	*        std::out_of_range (IndexOutOfBoundsException in Java) if triggerPriority is outside 0-254
	*/
	int createSwPaTrigger(int sourceId, int triggerPriority)
		throw (std::out_of_range, std::invalid_argument);

	int createSwPaTrigger(	int sourceId, int triggerPriority,
		const std::string& preChime, const std::string& displayParameters)
		throw (std::out_of_range, std::invalid_argument);

	/**
	* Allows a software trigger that has been previously created by application to be deleted.
	*
	* @param triggerId Specifies the selected Trigger Id
	* @throw std::out_of_range (IndexOutOfBoundsException in Java) if trigger Id does not exist.
	*/
	void deleteSwPaTrigger(int triggerId)
		throw (std::out_of_range);

	/**
	* Allows a software trigger that has been previously created to be activated. Note that
	* hardware triggers cannot be triggered via this function call. A typical use for this
	* function would be to bind it to the button down event for a PTT button on a GUI console.
	* 
	* @param triggerId Specifies the selected Trigger Id
	* @throw std::out_of_range (IndexOutOfBoundsException in Java) if trigger Id does not exist.
	*/
	void activateSwPaTrigger(int triggerId)
		throw (std::out_of_range);

	/**
	* Allows a software trigger that has been previously activated to be de-activated. Note that
	* hardware triggers cannot be de-activated via this function call. A typical use for this
	* function would be to bind it to the button up event for a PTT button on a GUI console.
	*
	* @param triggerId Specifies the selected Trigger Id
	* @throw std::out_of_range (IndexOutOfBoundsException in Java) if trigger Id does not exist.
	*/
	void deactivateSwPaTrigger(int triggerId)
		throw (std::out_of_range);

	/**
	* Returns a list of all available sink selectors for a particular source.
	* Sink selectors is a mechanism via which hardware inputs can be configured to associated and
	* activate sinks on a source. Sink selectors can be configured in the system to be non-volatile
	* and non-modifiable or can be configured to be under software control.
	* 
	* @param sourceId Id of the PA source for which to list the sink selectors
	* @return List of available sink selectors. The returned list will be empty if not connected to the NetSpire server.
	* @throw std::out_of_range (IndexOutOfBoundsException in Java) if source Id does not exist.
	*/
	PaSelectorArray getPaSelectors(int sourceId)
		throw (std::out_of_range);

	/**
	* Allows sinks associated with a sink selector to be retrieved.
	*
	* @param selectorId Id of the sink selector whose associated sinks need to be listed
	* @return List of available sinks. The returned list will be empty if not connected to the Netspire server.
	* @throw std::out_of_range (IndexOutOfBoundsException in Java) if selecotrId does not exist
	*/
	PaSinkArray getPaSinksForSelector(int selectorId)
		throw (std::out_of_range);

	/**
	* Adds a sink to a software controllable so thta when this selector is activated, the sink
	* will be included in the announcement area.
	*
	* @param selectorId Id of the sink selector to which the sink will be added
	* @param sinkId Id of the sink to be included in the announcement area
	* @throw std::out_of_range (IndexOutOfBounds in Java) if selectorId or sinkId does not exist
	*/
	void addPaSinkToSelector(int selectorId, int sinkId)
		throw (std::out_of_range);

	/**
	* Removes a specific sink form a software controllable sink selector or for all sinks to be deleted
	* from the software controllable sink selector.
	*
	* @param selectorId Id of the sink selector from which sink(s) will be removed
	* @param sinkId Id of the sink to be removed from the sink selector. -1 indicates all sinks to be removed.
	* @throw std::out_of_range (IndexOutOfBounds in Java) if selectorId or sinkId does not exist
	*/
	void deletePaSinkFromSelector(int selectorId, int sinkId)
		throw (std::out_of_range);

	/**
	* Allows the software to enable a sink selector
	*
	* @param selectorId Id of the sink selector to be enabled
	* @throw std::out_of_range (IndexOutOfBounds in Java) if selectorId does not exist
	*/
	void enablePaSelector(int selectorId)
		throw (std::out_of_range);

	/**
	* Allows the software to disable a sink selector
	*
	* @param selectorId Id of the sink selector to be disabled
	* @throw std::out_of_range (IndexOutOfBounds in Java) if selectorId does not exist
	*/
	void disablePaSelector(int selectorId)
		throw (std::out_of_range);

	/**
	* Returns the current state of the sink selector.
	*
	* @param selectorId Id of the sink selector whose state needs to be queried
	* @return current state of the sink selector as stated in the enumeration PaSelector state
	* @throw std::out_of_range (IndexOutOfBounds in Java) if selectorId does not exist
	*/
	PaSelector::State getPaSelectorState(int selectorId)
		throw (std::out_of_range);

	void registerObserver(PAControllerObserver *paObserver);

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
	unsigned int playMessage ( const std::vector<std::string> &zones, const std::vector<std::string> &visualDevices, const Gain &gain,
		const std::vector<unsigned int> &dictionaryItems, const char *visualText,
		bool resumeOnInterruptFlag, bool overrideExisting, unsigned int validityPeriod, unsigned int priority, unsigned int mode)
		throw (std::invalid_argument, std::out_of_range);

	///**
	// * Initiate message on Audio Zones and or Visual Displays.  A single function is
	// * used to allow queuing and synchronisation of audio and visual information.
	// * @param params List of audio sinks (audio zones) in the system to be used for audio output.
	// *   IllegalArgumentException or IndexOutOfBoundsException respectively)
	// **/
	unsigned int playMessage ( const std::vector<std::string> &zones, const std::vector<std::string> &visualDevices, const Gain &gain,
		const std::vector<unsigned int> &dictionaryItems, const char *visualText, const PlayMessageParams &params)
		throw (std::invalid_argument, std::out_of_range);

	std::string playMessage(const std::vector<std::string> &zones, Message& message)
		throw (std::invalid_argument, std::out_of_range);

	/**
	* Enables message that has been previously initiated to be cancelled by
	* using the messageId returned by playMessage().
	* @param messageId Identifier for message as returned by playMessage(). Set it to 0 to cancel all messages.
	**/
	void cancelMessage (unsigned int messageId)
		throw (std::invalid_argument);

	void cancelMessage(const std::string &requestId)
		throw (std::invalid_argument);

	/**
	* List all schedule message present in the AS.
	* @return a list of schedules.
	**/
	ScheduleDefinitionArray listSchedules();

	/**
	* Schedule message on Audio Zones and or Visual Displays.  A single function is
	* used to allow queuing and synchronisation of audio and visual information.
	* @param schduleInfo contains all the information to schedule the message.
	* @throw std::invalid_argument (IllegalArgumentException in Java) if no audio sinks specified
	*        std::out_of_range (IndexOutOfBoundsException in Java) if any of the schduleInfo is incorrect
	**/
	void createSchedule(const ScheduleDefinition &scheduleDef)
		throw (std::invalid_argument, std::out_of_range);

	/**
	* Schedule message on Audio Zones and or Visual Displays.  A single function is
	* used to allow queuing and synchronisation of audio and visual information.
	* This is a blocking function and will return the schedule ID upon creation of the schedule.
	* @param schduleInfo contains all the information to schedule the message.
	* @throw std::invalid_argument (IllegalArgumentException in Java) if no audio sinks specified
	*        std::out_of_range (IndexOutOfBoundsException in Java) if any of the schduleInfo is incorrect
	**/
	int createScheduleSync(const ScheduleDefinition &scheduleDef)
		throw (std::invalid_argument, std::out_of_range);

	/**
	* Allows an already created schedule to be modified. The scheduleId must be part of the
	* object.
	* @param schduleInfo contains all the information to schedule the message including scheduleId
	* @throw std::invalid_argument (IllegalArgumentException in Java) if no audio sinks specified
	*        std::out_of_range (IndexOutOfBoundsException in Java) if any of the schduleInfo is incorrect
	**/
	void modifySchedule(const ScheduleDefinition &scheduleDef)
		throw (std::invalid_argument, std::out_of_range);

	/**
	* Delete an existing schduled message from the AS.
	* @param scheduleId id of the schdule to be deleted from the AS
	* @throw std::invalid_argument (IllegalArgumentException in Java) if schdule Id is invalid
	*        std::out_of_range (IndexOutOfBoundsException in Java) if audio schdule Id is not in range
	**/
	void deleteSchedule(int scheduleId)
		throw (std::invalid_argument, std::out_of_range);

	TTSVocalizerArray getTTSVocalizerList();

	// DEVELOPMENT FUNCTIONS
	// Following functions are in development phase and needs to be cleaned at a later date
	void startUserRecording(const std::string &absolutePathWithFileName);
	void stopUserRecording();
	void previewUserRecording(const std::string &absolutePathWithFileName);
	void deleteUserRecording(const std::string &absolutePathWithFileName);

	// HELPER-FUNCTIONS
	// Following functions will be filtered by SWIG. These need to be explictly
	// declared in the csharp.i and java.i.
	// We need them to be public for ease of implementation but don't wish to 
	// expose them to the client/end-user.
	void initPaResources();
	void addUpdatePaSource(int elemNo, DVAlist *elemArgList);
	void addUpdatePaSink(int elemNo, DVAlist *elemArgList);
	void addUpdatePaTrigger(int elemNo, DVAlist *elemArgList);
	void addUpdatePaSelector(int elemNo, DVAlist *elemArgList);
	void addUpdateAttachedPaZone(int elemNo, DVAlist *elemArgList);
	void addUpdateAnnouncementRequest(int elemNo, DVAlist *elemArgList);
	void getAttachedPaZoneList(int sourceId, std::vector<std::string> *attachedPaZoneList);
	void addUpdatePaZone(int dstNo, int elemNo, DVAlist *elemArgList);
	void deleteAudioVideoControllerInfo(int dstNo, int elemId, int elemNo);
	std::string createScheduleHelper(bool scheduleId_HasToBe_Zero, const ScheduleDefinition &schedule);
	void updateScheduleList(int elemNo, DVAlist *elemArgList);
	void updateFromTTS_Ports(int elemNo, DVAlist *elemArgList);
	void deleteFromTTS_Ports(int elemNo);
	void updateFromTTS_Dictionary(int elemNo, DVAlist *elemArgList);
	void deleteFromTTS_Dictionary(int elemNo);
	void addToTTSPreviewPendingList(const std::string &uuid, TTSPendingInfo &ttsPendingInfo);

private:
	static int const workerThreadSleepTimeMS = 9000;
	std::map<int, PaSource> availablePaSources;
	std::vector<PaSourceLockStatus> availablePaSourcesLockStatus;
	std::map<int, PaSink> availablePaSinks;
	std::map<int, PaTrigger> availablePaTriggersHW;
	std::vector<PaSWTrigger> availablePaTriggersSW;
	std::map<int, PaSelector> availablePaSelectors;
	std::map<int, std::set<std::string> > attachedPaZonesToPaSourceList;
	oaLocalMutex paControllerMtx;
	int threadFunction(void *param);
	std::vector<PAControllerObserver *> paControllerObservers;
	std::map<int, PaZone> availablePaZonesPrimaryServer;
	std::map<int, PaZone> availablePaZonesLocalServer;
	ScheduleDefinitionArray scheduleList;
	std::map<int, TTSVocalizer> ttsVocalizertList;
	std::map<int, TTSDictionary> ttsDictionaryList;
	std::map<std::string, TTSPendingInfo> ttsPendingPreviewList;
	std::map<int, ANNOUNCEMENT_REQUEST> activeAnnouncementRequestList;
};

#endif // _PACONTROLLER_HPP_
