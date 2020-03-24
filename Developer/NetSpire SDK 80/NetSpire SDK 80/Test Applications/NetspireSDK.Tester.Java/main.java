/********************************************************************************
 * Instructions for compiling and running in Windows:
 * "c:\Program Files (x86)\Java\jdk1.7.0_17\bin\javac.exe" -cp as.jar main.java
 * "c:\Program Files (x86)\Java\jdk1.7.0_17\bin\java.exe" -cp as.jar;. main
 *******************************************************************************/
import oa.as.AudioServer;
import oa.as.AudioServerObserver;
import oa.as.PAController;
import oa.as.PAControllerObserver;
import oa.as.CallControllerObserver;
import oa.as.PassengerInformationObserver;
import oa.as.CDRInfo;
import oa.as.CallInfo;
import oa.as.Device;
import oa.as.DeviceStateArray;
import oa.as.Gain;
import oa.as.KeyValueMap;
import oa.as.NumberArray;
import oa.as.PaSelector;
import oa.as.PaSink;
import oa.as.PaSinkArray;
import oa.as.PaZoneArray;
import oa.as.PaSource;
import oa.as.PaTrigger;
import oa.as.PaZone;
import oa.as.StringArray;
import oa.as.TerminalInfo;
import oa.as.NamedId;
import oa.as.Vehicle;
import oa.as.Station;
import oa.as.PlatformInfo;
import oa.as.Service;

public class main
{
	/**************************************************************************/
	private static class audioserverObserver extends AudioServerObserver
	{
		@Override
		public void onCommsLinkUp()
		{
			System.out.println("audioserverObserver::onCommsLinkUp()");
		}
		@Override
		public void onCommsLinkDown()
		{
			System.out.println("audioserverObserver::onCommsLinkDown()");
		}
		@Override
		public void onAudioConnected()
		{
			System.out.println("audioserverObserver::onAudioConnected()");
		}
		@Override
		public void onAudioDisconnected()
		{
			System.out.println("audioserverObserver::onAudioDisconnected()");
		}
		@Override
		public void onDeviceStateChange(Device device)
		{
			System.out.println("audioserverObserver::onDeviceStateChange() DeviceId=" + device.getName() + " State=" + device.getState());
		}
		@Override
		public void onDeviceDelete(Device device)
		{
			System.out.println("audioserverObserver::onDeviceDelete() DeviceId=" + device.getName());
		}
	}
	
	/**************************************************************************/
	private static class paControllerObserver extends PAControllerObserver
	{
		@Override
		public void onPaSourceUpdate(PaSource source)
		{
			System.out.println("paControllerObserver::onPaSourceUpdate() sourceId=" + source.getId());
		}
		@Override
		public void onPaSourceDelete(int sourceId)
		{
			System.out.println("paControllerObserver::onPaSourceDelete() sourceId=" + sourceId);
		}
		@Override
		public void onPaSinkUpdate(PaSink sink)
		{
			System.out.println("paControllerObserver::onPaSinkUpdate() sinkId=" + sink.getId());
		}
		@Override
		public void onPaSinkDelete(int sinkId)
		{
			System.out.println("paControllerObserver::onPaSinkDelete() sinkId=" + sinkId);
		}
		@Override
		public void onPaTriggerUpdate(PaTrigger trigger)
		{
			System.out.println("paControllerObserver::onPaTriggerUpdate() triggerId=" + trigger.getId());
		}
		@Override
		public void onPaTriggerDelete(int triggerId)
		{
			System.out.println("paControllerObserver::onPaTriggerDelete() triggerId=" + triggerId);
		}
		@Override
		public void onPaSelectorUpdate(PaSelector selector)
		{
			System.out.println("paControllerObserver::onPaSelectorUpdate() selectorId=" + selector.getId());
		}
		@Override
		public void onPaSelectorDelete(int selectorId)
		{
			System.out.println("paControllerObserver::onPaSelectorDelete() selectorId=" + selectorId);
		}
		@Override
		public void onPaZoneUpdate(PaZone zone)
		{
			System.out.println("paControllerObserver::onPaZoneUpdate() zoneId=" + zone.getId());
		}
		@Override
		public void onPaZoneDelete(String zoneId)
		{
			System.out.println("paControllerObserver::onPaZoneDelete() zoneId=" + zoneId);
		}
	}
	
	/**************************************************************************/
	/*private static class callControllerObserver extends CallControllerObserver
	{
		@Override
		public void onCallUpdate(CallInfo callInfo)
		{
			System.out.println("callControllerObserver::onCallUpdate() callId=" + callInfo.getCallId());
		}
		@Override
		public void onCallDelete(int callId)
		{
			System.out.println("callControllerObserver::onCallDelete() callId=" + callId);
		}
		@Override
		public void onCDRMessageUpdate(CDRInfo cdrInfo)
		{
			System.out.println("callControllerObserver::onCDRMessageUpdate() cdrId=" + cdrInfo.getId());
		}
		@Override
		public void onCDRMessageDelete(int cdrId)
		{
			System.out.println("callControllerObserver::onCDRMessageDelete() cdrId=" + cdrId);
		}
		@Override
		public void onTerminalUpdate(TerminalInfo termInfo)
		{
			System.out.println("callControllerObserver::onTerminalUpdate() termId=" + termInfo.getTermId());
		}
		@Override
		public void onTerminalDelete(int termId)
		{
			System.out.println("callControllerObserver::onTerminalDelete() termId=" + termId);
		}
	}
	
	/**************************************************************************/
	/*private static class passengerInformationObserver extends PassengerInformationObserver
	{
		@Override
		public void onServerStatusUpdated(boolean status)
		{
			System.out.println("passengerInformationObserver::onServerStatusUpdated() serverStatus=" + status);
		}
		@Override
		public void onLineUpdated(NamedId line)
		{
			System.out.println("passengerInformationObserver:onLineUpdated() lineId=" + line.getId());
		}
		@Override
		public void onLineRemoved(NamedId line)
		{
			System.out.println("passengerInformationObserver::onLineRemoved() lineId=" + line.getId());
		}
		@Override
		public void onVehicleUpdated(Vehicle vehicle)
		{
			System.out.println("passengerInformationObserver::onVehicleUpdated(): vehicleId=" + vehicle.getId() +
				" Name=" + vehicle.getName() +
				" Abbreviation=" + vehicle.getAbrv() +
				" Num Cars=" + vehicle.getNumCars() +
				" Services=" + vehicle.getServicesListAsString() +
				" Is Ascending=" + vehicle.getDirection() +
				" Location=" + vehicle.getCurrentLocation() +
				" State-Text=" + vehicle.getStateText() +
				" Current Service=" + vehicle.getCurrentServiceId() +
				" Opening Doors Text=" + vehicle.getOpeningDoorsText() +
				" Opened Doors Text=" + vehicle.getOpenedDoorsText());
		}
		@Override
		public void onVehicleRemoved(Vehicle vehicle)
		{
			System.out.println("passengerInformationObserver::onVehicleRemoved() vehicleId=" + vehicle.getId());
		}
		@Override
		public void onStationUpdated(Station station)
		{
			System.out.println("passengerInformationObserver::onStationUpdated() stationId=" + station.getId());
		}
		@Override
		public void onStationRemoved(Station station)
		{
			System.out.println("passengerInformationObserver::onStationRemoved() stationId=" + station.getId());
		}
		@Override
		public void onPlatformInfoUpdated(PlatformInfo platformInfo)
		{
			System.out.println("passengerInformationObserver::onPlatformInfoUpdated() platformId=" + platformInfo.getId() +
			" Name=" + platformInfo.getName() +
			" Station Id=" + platformInfo.getStationId() +
			" Location=" + platformInfo.getLocation() +
			" State=" + platformInfo.getState() +
			" State-Text=" + platformInfo.getStateText() +
			" Passers-List=" + platformInfo.getPassersListAsString());
		}
		@Override
		public void onPlatformInfoRemoved(PlatformInfo platformInfo)
		{
			System.out.println("passengerInformationObserver::onPlatformInfoRemoved() platformId=" + platformInfo.getId());
		}
		@Override
		public void onServiceUpdated(Service service)
		{
			System.out.println("passengerInformationObserver::onServiceUpdated() serviceId=" + service.getId() +
				" Trip Id=" + service.getTripId() +
				" Vehicle Id=" + service.getVehicleId() +
				" Following Stops=" + service.getFollowingStopsListAsString() +
				" State-Text=" + service.getStateText());
		}
		@Override
		public void onServiceRemoved(Service service)
		{
			System.out.println("passengerInformationObserver::onServiceRemoved() serviceId=" + service.getId());
		}
	}
	
	/**************************************************************************/
	public static void main (String[] argv)
	{
		// Load libraries if running in Windows
		System.loadLibrary("FTPlib");
		System.loadLibrary("netspireSDK");

		// Load library if running in Linux
		//String libraryAbsolutePath = System.getProperty("user.dir") + "/libaudioserver.so";
		//System.load(libraryAbsolutePath);

		// Create new instance of AudioServer
		AudioServer gAudioServer = new AudioServer();	  // an instance of oa.as.AudioServer
		
		// setup Audio Server addresses
		StringArray serverAddresses = new StringArray();
		serverAddresses.add("192.168.101.10");
		
		// setup connection parameters
		KeyValueMap connectionParametersKeyValuePair = new KeyValueMap();
		//connectionParametersKeyValuePair.set("NETSPIRE_SDK_SOCKET_PORT", "20772");			 // change API binding port number (default port number is 20770)
		//connectionParametersKeyValuePair.set("NETSPIRE_SDK_SET_LOG_LEVEL", "0");
		//connectionParametersKeyValuePair.set("NETSPIRE_SDK_SET_DEBUG_FILE", "c:\\share\\sdkDebug.log");
		//connectionParametersKeyValuePair.set("NETSPIRE_SDK_SET_ERROR_FILE", "c:\\share\\sdkError.log");
		
		// register observers if using event based communication
		audioserverObserver asObserver = new audioserverObserver();
		gAudioServer.registerObserver(asObserver);
		paControllerObserver paObserver = new paControllerObserver();
		gAudioServer.getPAController().registerObserver(paObserver);
		//callControllerObserver callObserver = new callControllerObserver();
		//gAudioServer.getCallController().registerObserver(callObserver);
		//passengerInformationObserver pisObserver = new passengerInformationObserver();
		//gAudioServer.getPassengerInformationServer().registerObserver(pisObserver);
		
		// send connection request
		gAudioServer.connect(serverAddresses, connectionParametersKeyValuePair);
		
		// wait until connection is established on the AudioServer. Alternatively do something untill 'onAudioConnected()' is not received.
		for (int i = 0; i < 1000; i++)
		{
			// check if connection is established
			if (gAudioServer.isAudioConnected())
			{
				break;
			}
			// sleep and then check again is connection was success
			try { Thread.sleep(500); }
			catch (InterruptedException e) { }
		}
		if (!gAudioServer.isAudioConnected())
		{
			System.err.println("Cannot connect to the Audio Server.");
			return;
		}
		
		//===============================================================================================
		// connection to audio server is established. idle forever and get device states every x seconds
		//===============================================================================================
		for (;;)
		{
			System.out.println("");
			System.out.println("------------------------------------------------------------------");
			System.out.println("NetSpire SDK Build=" + gAudioServer.getSDKRevision() + " Software Build=" + gAudioServer.getSystemRevision() + " Software Build Consistent=" + gAudioServer.isSystemRevisionConsistent());

			// Get list of all devices and their current states
			DeviceStateArray states = gAudioServer.getDeviceStates();
			System.out.println("");
			System.out.println("Total Devices in system: " + states.size());
			for (int i = 0; i < states.size(); i++)
			{
				Device device = states.get(i);
				System.out.println("Name: " + device.getName() + " State: " + device.getState());
			}
			
			// Get list of all PaSinks
			//PaSinkArray availablePaSinks = gAudioServer.getPAController().getPaSinks();
			//System.out.println("");
			//System.out.println("Total PASinks in system: " + availablePaSinks.size());
			
			// Get list of all Zones
			//PaZoneArray availableZones = gAudioServer.getPAController().getPaZones();
			//System.out.println("");
			//System.out.println("Total zones in system: " + availableZones.size());
			
			System.out.println("------------------------------------------------------------------");
			System.out.println("");
			
			// sleep
			try { Thread.sleep(10000); }
			catch (InterruptedException e) { }
		}
		// close connection to AudioServer
		//gAudioServer.initiateShutdown ();
		//gAudioServer.disconnect ();
		//gAudioServer.delete ();
	}
}

