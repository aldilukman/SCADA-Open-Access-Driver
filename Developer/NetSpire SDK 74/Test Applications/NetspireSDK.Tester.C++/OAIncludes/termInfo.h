#ifndef	__TERMINFO_H__
#define	__TERMINFO_H__

#ifdef __cplusplus
#include <string>

using namespace std;

namespace termInfo
{
	enum Type
	{
		T_SIP = 2,
		T_IAX = 1,
		T_UNKNOWN = 0
	};
	
	enum NetSpireDeviceType
	{
		NS_DUMMY_NONE = 0,
		NS_DEV_CXS = 7,        // 7:Communication Exchange Server
		NS_DEV_TCX = 20,       // 20:Train Communications Exchange
		NS_DEV_TGU = 21,       // 21:Train Gateway Unit
		NS_DEV_CAC = 22,       // 22:Cabin Audio Controller
		NS_DEV_PEI = 23,       // 23:Passenger Emergency Intercom (PUHTC01 PUHTS01)
		NS_DEV_CI = 24,        // 24:Crew Intercom
		NS_DEV_CP = 25,        // 25:Crew Panel
		NS_DEV_NAC = 26,       // 26:Network Audio Controller (NAC)
		NS_DEV_VCU3 = 27,      // VCU3
		NS_DEV_IPS = 28,       // 28:IP Paging Station (IPPA)
		NS_DEV_NAM2I = 29,     // 29:Network Amplifier Module (NAM2i NAM2i-loop)
		NS_DEV_NAR4C = 30,     // 30:Network Audio Router (NAR4c)
		NS_DEV_IPHP = 31,      // 31:IP Help Phone (PUAET02/03/04)
		NS_DEV_IPS_LCD = 32,   // 32:IP Paging Station with LCD (IPS_LCD)
		NS_DUMMY_IDI = 201,    // Internal Display Indicator
		NS_DUMMY_EDI = 202,    // External Display Indicator
		NS_DUMMY_TR = 203      // Train Radio
	};

	enum HealthState
	{
		NOT_HEALTHY = 0,
		HEALTHY,
		HS_FAULTY,
		HS_ISOLATED
	};
			
	enum State
	{
		S_UNKNOWN = 0,
		S_FAULTY,
		S_IDLE,
		S_RINGING,
		S_CONNECTED,
		S_HELD,
		S_ISOLATED
	};

	enum Role
	{
		R_ANY = 0,		// Any Role, including Others
		R_PEI,		// PEI
		R_GUARD,		// Guard
		R_DRIVER,		// Driver
		R_CAC_G,		// CAC for guard
		R_CAC_D,		// CAC for driver		
		R_OTHER,		// All Others
		R_NONE
	};

	enum Location
	{
		L_ANYWHERE = 0,	// Any where, incuding Other Place
		L_DRIVER_CABIN,
		L_GUARD_CABIN,
		L_OTHER_PLACE
	};

	enum DeviceRole
		{
			DEV_ROLE_NONE = 0,
			DEV_ROLE_GUARD,
			DEV_ROLE_DRIVER,
			DEV_ROLE_NOT_USED,
			DEV_INVALID,
			DEV_LIMITED_ICOM
		};
        
        static string deviceRoleName[] = {
            "NONE", "GUARD", "DRIVER", "NOT_USED",
            "INVALID", "LIMITED_ICOM_ONLY"};

        enum PeiEscalationMode
        {
            PEI_ESC_NONE = 0,
            PEI_ESC_INSTANT,
            PEI_ESC_DELAYED
        };
        
        static string peiEscalationModeName[] = {
            "NONE", "INSTANT", "DELAYED"};

        enum CommercialRadioMode
        {
            CMR_MUTED = 0,
            CMR_UNMUTED
        };

        static string commercialRadioModeName[] = {
            "MUTED", "UNMUTED"};

        enum TrainRadioItfMode
        {
            TR_ITF_NONE = 0,
            TR_ITF_PEI_ESC,
            TR_ITF_ICOM_PA
        };

        static string trainRadioItfModeName[] = {
            "NONE", "PEI_ESCALATION", "ICOM_PA"};

	enum CallPresentation
		{
			CALL_PRESENT_NONE = 0,
			CALL_PRESENT_IMMEDIATELY,
			CALL_PRESENT_DELAYED
		};

	enum CommRadioControl
		{
			COMM_RADIO_DISABLED = 0,
			COMM_RADIO_ENABLED
		};
	
	enum TrainRadioMode
		{
			TRAIN_RADIO_NONE = 0,
			TRAIN_RADIO_PEI,
			TRAIN_RADIO_ICOMPA,
			TRAIN_RADIO_ALL
		};

	enum LedEventCode
	{
		LED_NONE = 0,
		LED_TF_PEI_REPLY,  /* Triple flash PEI_REPLY LED of the CI*/
		LED_TF_TR,         /* Triple flash TRAIN RADIO LED of the CI*/
		LED_MAX
	};

	enum LedStatusCode
	{
		LED_STATUS_NONE = 0,
		LED_TR_ESC_PEICALL_IN_LOCAL_CAB,  /* There's escalated-to-TR PEI call in the same cabin*/
		LED_STATUS_MAX
	};

	class TerminalParamName
	{
		public:
			static const char *typeName( int id )
			{
				switch( id )
				{
					case T_SIP:	return "SIP";
					case T_IAX:	return "IAX";
					default:		return "UNKNOWN";
				}
			}

			static const char *healthStateName( int id )
			{
				switch( id )
				{
					case HEALTHY:	return "HEALTHY";
					default:		return "NOT HEALTHY";
				}
			}

			static const char *stateName( int id )
			{
				switch( id )
				{
					case S_FAULTY:	return "FAULTY";
					case S_IDLE:		return "IDLE";
					case S_RINGING:	return "RINGING";
					case S_CONNECTED:	return "CONNECTED";
					case S_HELD:		return "HELD";
					case S_ISOLATED:	return "ISOLATED";
					default:		return "UNKNOWN";
				}
			}

			static const char *roleName( int id )
			{
				switch( id )
				{
					case R_PEI:	return "PEI";
					case R_GUARD:	return "GUARD";
					case R_DRIVER:	return "DRIVER";
					case R_CAC_G:	return "R_CAC_G";
					case R_CAC_D:	return "R_CAC_D";
					case R_OTHER:	return "R_OTHER";
					default:		return "OTHER";
				}
			}

			static const char *CallPresentationName( enum CallPresentation codeVal)
			{
				switch(codeVal)
				{
					case CALL_PRESENT_NONE:			return "CALL_PRESENT_NONE";
					case CALL_PRESENT_IMMEDIATELY:	return "CALL_PRESENT_IMMEDIATELY";
					case CALL_PRESENT_DELAYED:		return "CALL_PRESENT_DELAYED";
					default:						return "OTHER";
				}
			}

			static const char *locationName( int id )
			{
				switch( id )
				{
					case L_DRIVER_CABIN:	return "DRIVER CABIN";
					case L_GUARD_CABIN:	return "GUARD CABIN";
					default:			return "OTHER PLACE";
				}
			}
	};
}
#endif

// Note:
// These defines need to be the same with the resDirectory directory.h, to be improved.
/* terminal types */
#define TT_UNKNOWN   0
#define TT_IAX_HP    1
#define TT_SIP_AGENT 2
#define TT_SIP_HP    3
#define TT_IPMIC     4
#define TT_3RDPTY    5

/* terminal states */
#define TS_UNKNOWN      0
#define TS_NOT_READY    1
#define TS_READY        2
#define TS_ACTIVE       3
#define TS_ERROR        4
#define TS_DISABLED     5
#define TS_ISOLATED     6

/* available switches : should be in call */
#define TC_UNKNOWN        0
#define TC_SOFT_SWITCH_V1 1
#define TC_SOFT_SWITCH_OA 2


#define HC_PING		1
#define HC_AUDIO	2

#define STS_HC_UNKNOWN      0
#define STS_HC_OK	    1
#define STS_HC_PING_FAILED  2
#define STS_HC_AUDIO_FAILED 3
#define STS_HC_TIMEOUT      4
#define	STS_HC_ISOLATED     5
#define STS_HC_BYPASSED     6

#endif	//__TERMINFO_H__
