// Standard Includes
#include <unistd.h>
#include <iostream>

// Open Access Includes
#include "StandardIncludes.hpp"
#include "PAController.hpp"
#include "AudioServer.hpp"
#include "AudioServerObserver.hpp"
#include "DeviceTypes.hpp"
#include "Device.hpp"
#include "CallController.hpp"


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

class STATION_INFO
{
public:
	std::string id;
	std::string name;
	STATION_INFO(std::string _id, std::string _name)
	{
		this->id = _id;
		this->name = _name;
	}
};

/***************************************************************************************************/
int main(int argc, char *argv[])
{
	// setup connection parameters
	AudioServer::KeyValueMap connectionParametersKeyValuePair;
	//connectionParametersKeyValuePair.insert(connectionParametersKeyValuePair.end(), std::pair<std::string, std::string>("NETSPIRE_SDK_SOCKET_PORT", "20772"));  // change API binding port number (default port number is 20770)
	//connectionParametersKeyValuePair.insert(connectionParametersKeyValuePair.end(), std::pair<std::string, std::string> ("NETSPIRE_SDK_SET_LOG_LEVEL", "4"));
	//connectionParametersKeyValuePair.insert(connectionParametersKeyValuePair.end(), std::pair<std::string, std::string> ("NETSPIRE_SDK_SET_DEBUG_FILE", "/home/openaccess/tmp/sdkDebug.log"));
	//connectionParametersKeyValuePair.insert(connectionParametersKeyValuePair.end(), std::pair<std::string, std::string> ("NETSPIRE_SDK_SET_ERROR_FILE", "/home/openaccess/tmp/sdkError.log"));

	AudioServer::StringArray audioserverAddresses;
	//audioserverAddresses.push_back("10.1.40.100");
	//audioserverAddresses.push_back("10.1.40.101");
	audioserverAddresses.push_back("192.168.101.10");

	// register observers
	AsObserverSample asObserver;
	PaObserverSample paObserver;
	CallObserverSample callObserver;
	//PisObserverSample pisObserver;
	gAudioServer.registerObserver(&asObserver);
	gAudioServer.getPAController()->registerObserver(&paObserver);
	gAudioServer.getCallController()->registerObserver(&callObserver);
	//gAudioServer.getPassengerInformationServer()->registerObserver(&pisObserver);

	// connect to audio server
	std::cout << "about to send connect" << std::endl;
	fflush(NULL);
	gAudioServer.connect(audioserverAddresses, connectionParametersKeyValuePair);
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
	//UpdatePIController(gAudioServer.getPassengerInformationServer());

	//===============================================================================================
	// Idle forever and get device states every 2 seconds
	//===============================================================================================
	int cnt = 0;
	
	STATION_INFO Toorak("1", "Toorak");
	STATION_INFO Armadale("2", "Armadale");
	STATION_INFO Malvern("3", "Malvern");
	STATION_INFO Caulfield("4", "Caulfield");
	STATION_INFO Carnegie("5", "Carnegie");
	STATION_INFO Murrumbeena("6", "Murrumbeena");
	STATION_INFO Hughesdale("7", "Hughesdale");
	STATION_INFO Oakleigh("8", "Oakleigh");
	STATION_INFO Huntingdale("9", "Huntingdale");
	STATION_INFO Clayton("10", "Clayton");
	STATION_INFO Westall("11", "Westall");
	STATION_INFO Springvale("12", "Springvale");
	STATION_INFO SandownPark("13", "Sandown Park");
	STATION_INFO NoblePark("14", "Noble Park");
	STATION_INFO Yarraman("15", "Yarraman");
	STATION_INFO Dandenong("16", "Dandenong");
	STATION_INFO Hallam("17", "Hallam");
	STATION_INFO NarreWarren("18", "Narre Warren");
	STATION_INFO Berwick("19", "Berwick");
	STATION_INFO Beaconsfield("20", "Beaconsfield");
	STATION_INFO Officer("21", "Officer");
	STATION_INFO CardiniaRd("22", "Cardinia Rd");
	STATION_INFO Pakenham("23", "Pakenham");

	std::vector<STATION_INFO> stationNames;
	stationNames.push_back(Toorak);
	stationNames.push_back(Armadale);
	stationNames.push_back(Malvern);
	stationNames.push_back(Caulfield);
	stationNames.push_back(Carnegie);
	stationNames.push_back(Murrumbeena);
	stationNames.push_back(Hughesdale);
	stationNames.push_back(Oakleigh);
	stationNames.push_back(Huntingdale);
	stationNames.push_back(Clayton);
	stationNames.push_back(Westall);
	stationNames.push_back(Springvale);
	stationNames.push_back(SandownPark);
	stationNames.push_back(NoblePark);
	stationNames.push_back(Yarraman);
	stationNames.push_back(Dandenong);
	stationNames.push_back(Hallam);
	stationNames.push_back(NarreWarren);
	stationNames.push_back(Berwick);
	stationNames.push_back(Beaconsfield);
	stationNames.push_back(Officer);
	stationNames.push_back(CardiniaRd);
	stationNames.push_back(Pakenham);
	std::vector<STATION_INFO>::iterator stationNamesIT = stationNames.begin();

	std::vector<std::string> zonesDRM;
	zonesDRM.push_back("DRM");

	std::vector<std::string> zonesSaloon;
	zonesSaloon.push_back("Saloon");

	for (;;)
	{
		/*
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
		*/

		std::cout << "-> Sending Request: DRMId=" << stationNamesIT->id << " StationName=" << stationNamesIT->name << std::endl;
		// update DRM
		Message drmMessage;
		drmMessage.setVisualMessageType(Message::DISPLAY_TEMPLATE);
		std::map<std::string, std::vector<DISPLAY_VALUE_INFO> > keyValueListForDRM;
		std::vector<DISPLAY_VALUE_INFO> valueListForDRM;
		DISPLAY_VALUE_INFO displayValueForDRM;
		displayValueForDRM.text = stationNamesIT->id;
		valueListForDRM.push_back(displayValueForDRM);
		keyValueListForDRM["DRM^station_index"] = valueListForDRM;
		drmMessage.setVisualMessage("", keyValueListForDRM, 0);
		drmMessage.playAnnouncement(zonesDRM);

		// update IDU
		Message iduMessage;
		iduMessage.setVisualMessageType(Message::DISPLAY_TEMPLATE);
		std::map<std::string, std::vector<DISPLAY_VALUE_INFO> > keyValueListForIDU;
		
		std::vector<DISPLAY_VALUE_INFO> valueListForTitle;
		DISPLAY_VALUE_INFO displayValueForTitle;
		displayValueForTitle.text = "Next Station";
		valueListForTitle.push_back(displayValueForTitle);
		keyValueListForIDU["IDUTemplate^title"] = valueListForTitle;

		std::vector<DISPLAY_VALUE_INFO> valueListForStationName;
		DISPLAY_VALUE_INFO displayValueForStationName;
		displayValueForStationName.text = stationNamesIT->name;
		valueListForStationName.push_back(displayValueForStationName);
		keyValueListForIDU["IDUTemplate^station_name"] = valueListForStationName;

		iduMessage.setVisualMessage("", keyValueListForIDU, 0);
		iduMessage.playAnnouncement(zonesSaloon);

		// move to next station
		stationNamesIT++;
		if (stationNamesIT == stationNames.end())
		{
			std::cout << "-> Sending Train Terminate Request" << std::endl << std::endl;
			// play DVA
			Message terminateMessage;
			terminateMessage.setAudioMessageType(Message::DICTIONARY);
			std::vector<unsigned int> dvaList;
			dvaList.push_back(99200);
			dvaList.push_back(100004);
			terminateMessage.setAudioMessage(dvaList);
			terminateMessage.setVisualMessageType(Message::DISPLAY_TEMPLATE);
			std::map<std::string, std::vector<DISPLAY_VALUE_INFO> > keyValueListForIDU;
			std::vector<DISPLAY_VALUE_INFO> valueListForTitle;
			DISPLAY_VALUE_INFO displayValueForTitle;
			displayValueForTitle.text = "Train Terminates Here";
			valueListForTitle.push_back(displayValueForTitle);
			keyValueListForIDU["IDUTemplate^title"] = valueListForTitle;

			terminateMessage.setVisualMessage("", keyValueListForIDU, 0);
			terminateMessage.playAnnouncement(zonesSaloon);
#ifdef WIN32
			Sleep(45000);
#else
			sleep(45);
#endif

			// reset
			stationNamesIT = stationNames.begin();

		}
		else
		{

#ifdef WIN32
			Sleep(7000);
#else
			sleep(7);
#endif
		}
		cnt++;
	}
	gAudioServer.disconnect();
	return EXIT_SUCCESS;
}
