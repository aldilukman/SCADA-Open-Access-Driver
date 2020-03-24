// Standard Includes
#include <stdio.h>
#include <string>
#include <stdlib.h>
#include <iostream>

// Open Access Includes
#include "StandardIncludes.hpp"
#include "AudioServer.hpp"
#include "AudioServerObserver.hpp"

// Global Variables
AudioServer gAudioServer;
extern void UpdatePIController(PassengerInformationServer* piServer);

/***************************************************************************************************/
class AsObserverSample : public AudioServerObserver
{
	public:
		AsObserverSample()
		{
			std::cout << "AsObserverSample::AsObserverSample()" << std::endl;
		}
		void onCommsLinkUp()
		{
			std::cout << "AsObserverSample::onCommsLinkUp()" << std::endl;
		}
		void onCommsLinkDown()
		{
			std::cout << "AsObserverSample::onCommsLinkDown()" << std::endl;
		}
		void onAudioConnected()
		{
			std::cout << "AsObserverSample::onAudioConnected()" << std::endl;
		}
		void onAudioDisconnected()
		{
			std::cout << "AsObserverSample::onAudioDisconnected()" << std::endl;
		}
		void onDeviceStateChange(Device device)
		{
			std::cout << "AsObserverSample::onDeviceStateChange(): " << "deviceId >" << device.getName() << "< deviceState >" << device.getStateText() << "<" << std::endl;
		}
		void onDeviceDelete(Device device)
		{
			std::cout << "AsObserverSample::onDeviceDelete(): deviceId >" << device.getName() << "<" << std::endl;
		}
		void onAlarmUpdate(Alarm alarm)
		{
			std::cout << "AudioServerObserver::onAlarmUpdate(): key=" << alarm.key << std::endl;
		}
		void onAlarmDelete(Alarm alarm)
		{
			std::cout << "AudioServerObserver::onAlarmDelete(): key=" << alarm.key << std::endl;
		}
};

/***************************************************************************************************/
class PaObserverSample : public PAControllerObserver
{
	public:
		PaObserverSample()
		{
			std::cout << "PaObserverSample::PaObserverSample()" << std::endl;
		}
		void onPaSourceUpdate(PaSource source)
		{
			std::cout << "PaObserverSample::onPaSourceUpdate(): PaSourceId >" << source.id << "<" << std::endl;
		}
		void onPaSourceDelete(int sourceId)
		{
			std::cout << "PaObserverSample::onPaSourceDelete(): PaSourceId >" << sourceId << "<" << std::endl;
		}
		void onPaSinkUpdate(PaSink sink)
		{
			std::cout << "PaObserverSample::onPaSinkUpdate(): PaSinkId >" << sink.id << "<" << std::endl;
		}
		void onPaSinkDelete(int sinkId)
		{
			std::cout << "PaObserverSample::onPaSinkDelete(): PaSinkId >" << sinkId << "<" << std::endl;
		}
		void onPaTriggerUpdate(PaTrigger trigger)
		{
			std::cout << "PaObserverSample::onPaTriggerUpdate(): PaTriggerId >" << trigger.id << "<" << std::endl;
		}
		void onPaTriggerDelete(int triggerId)
		{
			std::cout << "PaObserverSample::onPaTriggerDelete(): PaTriggerId >" << triggerId << "<" << std::endl;
		}
		void onPaSelectorUpdate(PaSelector selector)
		{
			std::cout << "PaObserverSample::onPaSelectorUpdate(): PaSelectorId >" << selector.id << "<" << std::endl;
		}
		void onPaSelectorDelete(int selectorId)
		{
			std::cout << "PaObserverSample::onPaSelectorDelete(): PaSelectorId >" << selectorId << "<" << std::endl;
		}
		void onPaZoneUpdate(PaZone zone)
		{
			std::cout << "PaObserverSample::onPaZoneUpdate(): PaZoneId >" << zone.id << "<" << std::endl;
		}
		void onPaZoneDelete(std::string zoneId)
		{
			std::cout << "PaObserverSample::onPaZoneDelete(): PaZoneId >" << zoneId << std::endl;
		}
		void onAudioMessageRetrievalComplete(const std::string& uuid, bool success, MessageRetrievalError errorCode, const std::string& uri)
		{
			std::cout << "PaObserverSample::onAudioMessageRetrievalComplete()" << std::endl;
		}
		void onMessageUpdate(AnnouncementRequest req)
		{
			std::cout << "PaObserverSample::onMessageUpdate() UUID=" << req.getRequestID()
				<< " State=" << req.stateText
				<< " Priority=" << req.priorityText
				<< " Date/Time=" << req.startDateTime
				<< " Completion Code=" << req.completionCode
				<< " Completion Msg=" << req.completionMsg
				<< std::endl;
		}
		void onMessageDelete(AnnouncementRequest req)
		{
			std::cout << "PaObserverSample::onMessageDelete() UUID=" << req.getRequestID() << std::endl;
		}
};

/***************************************************************************************************/
class CallObserverSample : public CallControllerObserver
{
	public:
		CallObserverSample()
		{
			std::cout << "CallObserverSample::CallObserverSample()" << std::endl;
		}
		void onCallUpdate(CallInfo callInfo)
		{
			std::cout << "CallObserverSample::onCallUpdate(): callId >" << callInfo.callId << "<" << std::endl;
		}
		void onCallDelete(int callId)
		{
			std::cout << "CallObserverSample::onCallDelete(): callId >" << callId << "<" << std::endl;
		}
		void onCDRMessageUpdate(CDRInfo cdrInfo)
		{
			std::cout << "CallObserverSample::onCDRMessageUpdate(): cdrId >" << cdrInfo.id << "<" << std::endl;
		}
		void onCDRMessageDelete(int cdrId)
		{
			std::cout << "CallObserverSample::onCDRMessageDelete(): cdrId >" << cdrId << "<" << std::endl;
		}
		void onTerminalUpdate(TerminalInfo termInfo)
		{
			std::cout << "CallObserverSample::onTerminalUpdate(): TermId >" << termInfo.termId << "<" << std::endl;
		}
		void onTerminalDelete(int termId)
		{
			std::cout << "CallObserverSample::onTerminalDelete(): TermId >" << termId << "<" << std::endl;
		}
		void onSIPTrunkUpdate(SIPTrunk sipTrunk)
		{
			std::cout << "CallObserverSample::onSIPTrunkUpdate(): trunkName >" << sipTrunk.getName() << "< updated in deviceId >" << sipTrunk.getDeviceId() << "<" << std::endl;
		}
		void onSIPTrunkDelete(SIPTrunk sipTrunk)
		{
			std::cout << "CallObserverSample::onSIPTrunkDelete(): trunkName >" << sipTrunk.getName() << "< deleted from deviceId >" << sipTrunk.getDeviceId() << "<" << std::endl;
		}
		void onISDNTrunkUpdate(ISDNTrunk isdnTrunk)
		{
			std::cout << "CallObserverSample::onISDNTrunkUpdate(): trunkName >" << isdnTrunk.getName() << "< updated in deviceId >" << isdnTrunk.getDeviceId() << "<" << std::endl;
		}
		void onISDNTrunkDelete(ISDNTrunk isdnTrunk)
		{
			std::cout << "CallObserverSample::onISDNTrunkDelete(): trunkName >" << isdnTrunk.getName() << "< deleted from deviceId >" << isdnTrunk.getDeviceId() << "<" << std::endl;
		}
};

/***************************************************************************************************/
class PisObserverSample : public PassengerInformationObserver
{
	public:
		PisObserverSample()
		{
			std::cout << "PisObserverSample::PisObserverSample()" << std::endl;
		}
		void onServerStatusUpdated(bool status)
		{
			std::cout << "PisObserverSample::onServerStatusUpdated(): serverStatus >" << status << "<" << std::endl;
		}
		void onLineUpdated(Line line)
		{
			std::cout << "PisObserverSample::onLineUpdated(): lineId >" << line.getId() << "<" << std::endl;
		}
		void onLineRemoved(Line line)
		{
			std::cout << "PisObserverSample::onLineRemoved(): lineId >" << line.getId() << "<" << std::endl;
		}
		void onVehicleUpdated(Vehicle vehicle)
		{
			std::cout << "PisObserverSample::onVehicleUpdated(): vehicleId >" << vehicle.getId() << "<" <<
				"Name >" << vehicle.getName() << "< " <<
				"Abbreviation >" << vehicle.getAbrv() << "< " <<
				"Num Cars >" << vehicle.getNumCars() << "< " <<
				"Services >" << vehicle.getServicesListAsString() << "< " <<
				"Is Ascending >" << vehicle.getDirection() << "< " <<
				"Location >" << vehicle.getCurrentLocation() << "< " <<
				"State-Text >" << vehicle.getStateText() << "< " <<
				"Current Service >" << vehicle.getCurrentServiceId() << "< " <<
				"Opening Doors Text >" << vehicle.getOpeningDoorsText() << "< " <<
				"Opened Doors Text >" << vehicle.getOpenedDoorsText() << "<" <<
				std::endl;
		}
		void onVehicleRemoved(Vehicle vehicle)
		{
			std::cout << "PisObserverSample::onVehicleRemoved(): vehicleId >" << vehicle.getId() << "<" << std::endl;
		}
		void onStationUpdated(Station station)
		{
			std::cout << "PisObserverSample::onStationUpdated(): stationId >" << station.getId() << "<" << std::endl;
		}
		void onStationRemoved(Station station)
		{
			std::cout << "PisObserverSample::onStationRemoved(): stationId >" << station.getId() << "<" << std::endl;
		}
		void onPlatformInfoUpdated(PlatformInfo platformInfo)
		{
			std::cout << "PisObserverSample::onPlatformInfoUpdated(): platformId >" << platformInfo.getId() << "< " <<
				"Name >" << platformInfo.getName() << "< " <<
				"Station Id >" << platformInfo.getStationId() << "< " <<
				"Location >" << platformInfo.getLocation() << "< " <<
				"State >" << platformInfo.getState() << "< " <<
				"State-Text >" << platformInfo.getStateText() << "< " <<
				"Passers-List >" << platformInfo.getPassersListAsString() << "<" <<
				std::endl;
		}
		void onPlatformInfoRemoved(PlatformInfo platformInfo)
		{
			std::cout << "PisObserverSample::onPlatformInfoRemoved(): platformId >" << platformInfo.getId() << "<" << std::endl;
		}
		void onServiceUpdated(Service service)
		{
			std::cout << "PisObserverSample::onServiceUpdated(): serviceId >" << service.getId() << "< " << 
				"Trip Id >" << service.getTripId() << "< " <<
				"Vehicle Id >" << service.getVehicleId() << "< " <<
				"Following Stops >" << service.getFollowingStopsListAsString() << "< " <<
				"State-Text >" << service.getStateText() << "<" <<
				std::endl;
		}
		void onServiceRemoved(Service service)
		{
			std::cout << "PisObserverSample::onServiceRemoved(): serviceId >" << service.getId() << "<" << std::endl;
		}
		~PisObserverSample()
		{
			//std::cout << "pisObserverClass::~pisObserverClass()" << std::endl;
		}
};

/***************************************************************************************************/
int main (int argc, char *argv[])
{
	// setup connection parameters
	KeyValueMap connectionParametersKeyValuePair;
	connectionParametersKeyValuePair.set("NETSPIRE_SDK_SOCKET_PORT", "20772");  // change API binding port number (default port number is 20770)
	//connectionParametersKeyValuePair.set("NETSPIRE_SDK_SET_LOG_LEVEL", "0");
	//connectionParametersKeyValuePair.set("NETSPIRE_SDK_SET_DEBUG_FILE", "/home/openaccess/tmp/sdkDebug.log");
	//connectionParametersKeyValuePair.set("NETSPIRE_SDK_SET_ERROR_FILE", "/home/openaccess/tmp/sdkError.log");

	AudioServer::StringArray audioserverAddresses;
	audioserverAddresses.push_back("192.168.110.60");

	// register observers
	AsObserverSample asObserver;
	PaObserverSample paObserver;
	CallObserverSample callObserver;
	PisObserverSample pisObserver;
	gAudioServer.registerObserver(&asObserver);
	gAudioServer.getPAController()->registerObserver(&paObserver);
	gAudioServer.getCallController()->registerObserver(&callObserver);
	gAudioServer.getPassengerInformationServer()->registerObserver(&pisObserver);

	// connect to audio server
	gAudioServer.connect (audioserverAddresses, connectionParametersKeyValuePair);
	// wait until connection is established on the AudioServer. Alternatively do something untill 'onAudioConnected()' is not received.
	for (unsigned int i = 0; i < 1000; i++)
	{
		if (gAudioServer.isAudioConnected())
		{
			break;
		}
#ifdef WIN32
		Sleep(500);
#else
		sleep(1);
#endif
	}

#ifdef WIN32
		Sleep(500);
#else
		sleep(1);
#endif
	if (!gAudioServer.isAudioConnected())
	{
		std::cerr << "Cannot connect to the Audio Server." << std::endl;
		return EXIT_FAILURE;
	}

	//===============================================================================================
	// Connection to audio server is established. Update Service, Vehicle, Station and 
	// Platform information stored in the Passenger Information Server system.
	//===============================================================================================
	UpdatePIController(gAudioServer.getPassengerInformationServer());

	//===============================================================================================
	// Idle forever and get device states every 2 seconds
	//===============================================================================================
	for (;;)
	{
		std::cout << "NetSpire SDK Build: " << gAudioServer.getSDKRevision() << ", Software Build: " << gAudioServer.getSystemRevision() << ", Is Software Build Consistent? " << gAudioServer.isSystemRevisionConsistent() << std::endl;
		AudioServer::DeviceStateArray devState = gAudioServer.getDeviceStates();
		std::cout << "Found >" << devState.size() << "< devices in the AudioServer" << std::endl;
		for (std::vector<Device>::iterator i = devState.begin(); i != devState.end(); i++)
		{
			Device tmp = (*i);
			std::ostringstream oss;
			oss << tmp.getName();
			oss << " dstNo: " << tmp.getDstNo();
			oss << " locName: " << tmp.getLocationName();
			oss << " locId: " << tmp.getLocationId();
			oss << " IP: " << tmp.getIP();
			oss << " dictSupported: " << tmp.getDictionarySupport();
			oss << " dictVersion: " << tmp.getDictionaryVersion();
			oss << " dictStatus: " << tmp.getDictionaryUpdateStatus();
			oss << " state: " << tmp.getStateText();
			oss << " softwareRevision: " << tmp.getSoftwareRevision();
			oss << " devClass: " << tmp.getDeviceClass();
			oss << " devModelName: " << tmp.getDeviceModel().getName();
			std::cout << oss.str() << std::endl;
		}
		std::cout << std::endl;
#ifdef WIN32
		Sleep(2000);
#else
		sleep(2);
#endif
	}
	gAudioServer.disconnect();
	return EXIT_SUCCESS;
}
