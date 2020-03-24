#ifndef CALL_INFO_H
#define CALL_INFO_H


namespace callInfo
{
	typedef enum _callState
	{
		PROGRESS = 0,
		CONNECTED,
		HELD,
		DISCONNECTED,
		UNKNOWN
	} CALLSTATE;

	typedef enum _callType
	{
		PEI = 0,
		ICOM,
		PA,
		TR,
		TR_PA,
		TR_EMERGENCY,
		BGM,
		PEI_EDR,
//#ifdef RES_CALL_CXS
		APP,          /* User Application origniated call*/
//#endif 
//#ifdef RES_CALL_TCX
		CP2TR,
//#endif
		CT_UNKNOWN,
		CT_PEI_TYPE1 = PEI,
		CT_PEI_TYPE2 = PEI_EDR,
		PA_TYPE1 = 11
	} CALLTYPE;

	enum AUDIO_EVT
	{
		AUDIO_ALERT_NONE = 0,
		AUDIO_ALERT_PEI_ESCLATION,
		MAX_AUDIO_EVT
	}; /* Audio event indicator, e.g. PEI esclation alert on CACs at 30th seconds after call made */


	enum TRUNK_STATUS
	{
		TRUNK_STATUS_NONE = 0,
		TRUNK_STATUS_UP,
		TRUNK_STATUS_DOWN,
		TRUNK_STATUS_FAIL
	};

	enum TRUNK_TYPE
	{
		TRUNK_TYPE_SIP = 0,
		TRUNK_TYPE_E1
	};

	inline const char *stateName( int id )
	{
		switch( id )
		{
			case callInfo::PROGRESS:	return "PROGRESS";
			case callInfo::CONNECTED:	return "CONNECTED";
			case callInfo::HELD:		return "HELD";
			case callInfo::DISCONNECTED:	return "DISCONNECTED";
			default:			return "UNKNOWN";
		}
	}

	inline const char *typeName( int id )
	{
		switch( id )
		{
			//case callInfo::PEI:
			case callInfo::CT_PEI_TYPE1:    return "PEI_TYPE1";
			case callInfo::ICOM:            return "ICOM";
			case callInfo::PA:              return "PA";
			case callInfo::TR:              return "TR";
			case callInfo::TR_PA:           return "TR_PA";
			case callInfo::TR_EMERGENCY:    return "TR_EMERGENCY";
			case callInfo::BGM:             return "BGM";
			//case callInfo::PEI_EDR:         
			case callInfo::CT_PEI_TYPE2:    return "PEI_TYPE2";
#if defined(RES_CALL_CXS) || defined(RESCALL_FOR_KTMB)
			case callInfo::APP:             return "APP";
#endif 
#if defined(RES_CALL_TCX) || defined(RESCALL_KLM)
			case callInfo::CP2TR:           return "CP2TR";
#endif 
			default:                        return "UNKNOWN";
		}
	}

	inline const char *trunkStatusName( int id , callInfo::TRUNK_TYPE type )
	{
		if( type == callInfo::TRUNK_TYPE_SIP )
		{
			switch( id )
			{
				case callInfo::TRUNK_STATUS_UP:		return "Registration Ok";
				case callInfo::TRUNK_STATUS_DOWN:	return "Not Registered";
				case callInfo::TRUNK_STATUS_FAIL:	return "Registration Failed";
				default:				return "Unknown";
			}
		}
		else if ( type == callInfo::TRUNK_TYPE_E1 )
		{
			switch( id )
			{
				case callInfo::TRUNK_STATUS_UP:         return "Up";
				case callInfo::TRUNK_STATUS_DOWN:       return "Down";
				default:                                return "Unknown";
			}
		}
		else
			return "Unknown";
	}
}


#endif  // CALL_INFO_H

