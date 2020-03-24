#ifndef _PRIORITYINFO_H_
#define _PRIORITYINFO_H_

#include <string.h>

#define MINIMUM_PRIORITY_LEVEL				1
#define MAXIMUM_PRIORITY_LEVEL				1000
#define DEFAULT_PRIORITY_LEVEL				500
#define DEFAULT_RELATIVE_PRIORITY_LOW		-200
#define DEFAULT_RELATIVE_PRIORITY_NORMAL	0
#define DEFAULT_RELATIVE_PRIORITY_HIGH		200
#define DEFAULT_RELATIVE_PRIORITY_EMERGENCY	400

typedef enum
{
	RPL_LOW = 10001,
	RPL_NORMAL = 10002,
	RPL_HIGH = 10003,
	RPL_EMERGENCY = 10004
} RELATIVE_PRIORITY_LEVELS;

typedef enum
{
	CODEC_L16_48K = 0,
	CODEC_L16_16K,
	CODEC_PCMA_8K,
	CODEC_PCMU_8K,
	CODEC_CELT,
	CODEC_UNKNOWN
} CODEC;

inline const char *getCodecString(CODEC codecId)
{
	switch (codecId)
	{
	case CODEC_L16_48K:		return "L16_48K";
	case CODEC_L16_16K:		return "L16_16K";
	case CODEC_PCMA_8K:		return "PCMA_8K";
	case CODEC_PCMU_8K:		return "PCMU_8K";
	case CODEC_CELT:		return "CELT";
	default:				return "UNKNOWN_CODEC";
	}
}

inline CODEC getCodecId(const char *codecString)
{
	if (!strcmp(codecString, "L16_48K") || !strcmp(codecString, "L16_48000"))		return CODEC_L16_48K;
	else if (!strcmp(codecString, "L16_16K") || !strcmp(codecString, "L16_16000"))	return CODEC_L16_16K;
	else if (!strcmp(codecString, "PCMA_8K") || !strcmp(codecString, "PCMA_8000"))	return CODEC_PCMA_8K;
	else if (!strcmp(codecString, "PCMU_8K") || !strcmp(codecString, "PCMU_8000"))	return CODEC_PCMU_8K;
	else if (!strcmp(codecString, "CELT") || !strcmp(codecString, "CELT_48000"))	return CODEC_CELT;
	else																			return CODEC_UNKNOWN;
}

typedef enum
{
	APM_ABSOLUTE_PRIORITY = 0,		// Use the priority level in the request as the announcement priority
	APM_RELATIVE_PRIORITY			// Use the priority level of the audio source and add the Request Priority Level.
} AnnouncementPriorityMode;

typedef enum
{
	AT_LIVE = 0,				// PA
	AT_STORED_DATA,				// Pre-recorded audio (Live PA Preview / TTS Preview / Streamed DVA, etc)
	AT_DVA,						// WAV files. Referred as DICTIONARY in API (Message::Type)
	AT_DISPLAY_v0,				// LED/LCD update. Waratah/HK systems where the zones in the opPlayAnnouncement are treated as display IDs
	AT_TTS,						// TTS Streamed. TTS generated audio is streamed using multicast/unicast.
	AT_BGM,						// BGM (same concept as AT_LIVE)
	AT_MIXED,					// Announcement type is composed of mixbag of above
	AT_NONE,					// Port is idle
	AT_TEMPLATE,				// 
	AT_DISPLAY_TEMPLATE,		// LED/LCD update
	AT_TTS_SAF,					// TTS Store & Forward. Used when network is not stable. TTS messges are transferred to end clients prior to playback.
	AT_STORED_DATA_SAF,			// Pre-recorded audio (Live PA Preview / TTS Preview / Streamed DVA, etc) Store & Forward. Used when network is not stable. User Recorded messges are transferred to end clients prior to playback.

	AT_MAX_ANNOUNCEMENT_TYPES	// used to filter announcement requests with invalid announcement types
} AnnouncementType;

typedef enum
{
	ASM_INSTANT = 0,				// unsynchronized. message is processed indiviually on each sink
	ASM_HARD,						// synchronized. dispatcher sends a multicast beacon to start playback
	ASM_SOFT						// parser will wait until dispatcher indicates the announcement is ready to play by changing state to READY
} AnnouncementSyncMode;

typedef enum
{
	AIM_INTERRUPT = 0,				// higher priority announcement will interrupt active announcement
	AIM_WAIT						// higher priority announcement will wait for current announcement to finish
} AnnouncementInterruptMode;

typedef enum
{
	ARM_DISCARD = 0,				// do not replay file
	ARM_RESTART,					// restart playback of file from start - not yet supported
	ARM_RESUME						// resume after interruption (only supported for Live PA + BGM types)
} AnnouncementResumeMode;

typedef enum
{
	MTS_UNKNOWN = 0,
	MTS_NOTACTIVE,
	MTS_ACTIVE1
} MessageTriggerStatus;

typedef enum
{
	AS_PENDING = 0,					// used by requester and parser to indicate that is waiting to be played (init and queue state)
	AS_WAITING_TO_ANNOUNCE,			// 
	AS_ACTIVE,						// all/some target zones are playing requested announcement
	AS_COMPLETED,					// from dispatcher point of view all target zones have played the announcement
	AS_CANCELLED,					// when requester cancels the announcement request
	AS_WAITING_TO_STOP,				//
	AS_INTERRUPTED,					// from dispatcher point of view some/all target zones have started playing another higher priority request
	AS_FAILED,
	AS_PENDING_FILE_CREATE,			// used by AT_TTS when request is sent to resTTS to generate file
	AS_WAITING_PLAYER,				// used by AT_LIVE, AT_STORED_DATA, AT_TTS, when streaming resource is not available for playback
	AS_SYNC_COMPLETE,				// used by AT_TTS_SAF & AT_STORED_DATA_SAF to indicate to clients to start sending multicast packets for sync playback
	AS_PENDING_FILE_TRANSFER		// used by AT_TTS_SAF & AT_STORED_DATA_SAF when request is sent to sending agent to transfer file to end clients
} AnnouncementState;

typedef enum
{
	ACC_UNKNOWN = 0,
	ACC_SUCCESS = 1000,
	ACC_INTERRUPTED = 2000,
	ACC_PARTIALLY_INTERRUPTED,
	ACC_USER_STOPPED,
	ACC_SOFTWARE_CANCELLED,
	ACC_FAILED_INVALID_DICTIONARY_ITEMS,
	ACC_FAILED_MEMORY_ALLOCATION,
	ACC_FAILED_SORTING_MESSAGE_QUEUE,
	ACC_FAILED_NO_TARGET_PASINK,
	ACC_FAILED_TTS_ENGINE_NOT_AVAILABLE,
	ACC_FAILED_EXCEED_ANNOUNCEMENT_MAX_TIME,
	ACC_FAILED_MEDIA_TRANSFER
} AnnouncementCompletionCode;

typedef enum
{
	ZA_UNKNOWN = 0,
	ZA_IDLE,
	ZA_ACTIVE,
	ZA_PARTIAL_ACTIVE
} ZoneActivityStatus;

typedef enum
{
	ZH_UNKNOWN = 0,
	ZH_HEALTHY,
	ZH_FAULTY,
	ZH_PARTIAL_HEALTHY
} ZoneHealthStatus;

#endif // _PRIORITYINFO_H_
