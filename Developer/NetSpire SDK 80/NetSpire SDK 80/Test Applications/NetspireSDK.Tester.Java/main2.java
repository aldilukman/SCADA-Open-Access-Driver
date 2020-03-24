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
            System.out.println("main::onCommsLinkUp()");
        }
        
        @Override
        public void onCommsLinkDown()
        {
            System.out.println("main::onCommsLinkDown()");
        }
        
        @Override
        public void onAudioConnected()
        {
            System.out.println("main::onAudioConnected()");
        }
        
        @Override
        public void onAudioDisconnected()
        {
            System.out.println("main::onAudioDisconnected()");
        }
        
        @Override
        public void onDeviceStateChange(Device device)
        {
            System.out.println("main::onDeviceStateChange() - State of " + device.getName() + " is now " + device.getState());
        }
        
        @Override
        public void onDeviceDelete(Device device)
        {
            System.out.println("main::onDeviceDelete() - " + device.getName());
        }
    }
    
    /**************************************************************************/
    private static class paControllerObserver extends PAControllerObserver
    {
        @Override
        public void onPaSourceUpdate(final PaSource nativeSourceObject)
        {
            System.out.println("main::onPaSourceUpdate()");
            PaSource source = new PaSource(nativeSourceObject);
            int sourceId = source.getId();
            System.out.println("Received PA Source update: " + sourceId + " (source hash = " + source.hashCode() + ")");
        }
        
        @Override
        public void onPaSourceDelete(int sourceId)
        {
            System.out.println("main::onPaSourceDelete()");
        }
        
        @Override
        public void onPaSinkUpdate(final PaSink nativeSinkObject)
        {
            System.out.println("main::onPaSinkUpdate()");
            PaSink sink = new PaSink(nativeSinkObject);
            int sinkId = sink.getId();
            System.out.println("Received PA sink update: " + sinkId + " (sink hash = " + sink.hashCode() + ")");
        }
        
        @Override
        public void onPaSinkDelete(int sinkId)
        {
            System.out.println("main::onPaSinkDelete()");
        }
        
        @Override
        public void onPaTriggerUpdate(final PaTrigger nativeTriggerObject)
        {
            System.out.println("main::onPaTriggerUpdate()");
        }
        
        @Override
        public void onPaTriggerDelete(int triggerId)
        {
            System.out.println("main::onPaTriggerDelete()");
        }
        
        @Override
        public void onPaSelectorUpdate(final PaSelector nativeSelectorObject)
        {
            System.out.println("main::onPaSelectorUpdate()");
        }
        
        @Override
        public void onPaSelectorDelete(int selectorId)
        {
            System.out.println("main::onPaSelectorDelete()");
        }
    }
    
    /**************************************************************************/
    /*private static class callControllerObserver extends CallControllerObserver
    {
        @Override
        public void onCallUpdate(final CallInfo callInfo)
        {
            System.out.println("main::onCallUpdate()");
        }
        
        @Override
        public void onCallDelete(int callId)
        {
            System.out.println("main::onCallDelete()");
        }
        
        @Override
        public void onCDRMessageUpdate(final CDRInfo cdrInfo)
        {
            System.out.println("main::onCDRMessageUpdate()");
        }
        
        @Override
        public void onCDRMessageDelete(int cdrId)
        {
            System.out.println("main::onCDRMessageDelete()");
        }
        
        @Override
        public void onTerminalUpdate(final TerminalInfo termInfo)
        {
            System.out.println("main::onTerminalUpdate()");
        }
        
        @Override
        public void onTerminalDelete(int termId)
        {
            System.out.println("main::onTerminalDelete()");
        }
    }
    
    /**************************************************************************/
    /*private static class passengerInformationObserver extends PassengerInformationObserver
    {
        @Override
        public void onServerStatusUpdated(boolean status)
        {
            System.out.println("main::onServerStatusUpdated()");
        }
        
        @Override
        public void onLineUpdated(final NamedId line)
        {
            System.out.println("main:onLineUpdated(): lineId >" + line.getId() + "<");
        }
        
        @Override
        public void onLineRemoved(final NamedId line)
        {
            System.out.println("main::onLineRemoved(): lineId >" + line.getId() + "<");
        }
        
        @Override
        public void onVehicleUpdated(final Vehicle vehicle)
        {
            System.out.println("main::onVehicleUpdated(): vehicleId >" + vehicle.getId() + "<");
        }
        
        @Override
        public void onVehicleRemoved(final Vehicle vehicle)
        {
            System.out.println("main::onVehicleRemoved(): vehicleId >" + vehicle.getId() + "<");
        }
        
        @Override
        public void onStationUpdated(final Station station)
        {
            System.out.println("main::onStationUpdated(): stationId >" + station.getId() + "<");
        }
        
        @Override
        public void onStationRemoved(final Station station)
        {
            System.out.println("main::onStationRemoved(): stationId >" + station.getId() + "<");
        }
        
        @Override
        public void onPlatformInfoUpdated(final PlatformInfo platformInfo)
        {
            System.out.println("main::onPlatformInfoUpdated(): platformId >" + platformInfo.getId() + "<");
        }
        
        @Override
        public void onPlatformInfoRemoved(final PlatformInfo platformInfo)
        {
            System.out.println("main::onPlatformInfoRemoved(): platformId >" + platformInfo.getId() + "<");
        }
        
        @Override
        public void onServiceUpdated(final Service service)
        {
            System.out.println("main::onServiceUpdated(): serviceId >" + service.getId() + "<");
        }
        
        @Override
        public void onServiceRemoved(final Service service)
        {
            System.out.println("main::onServiceRemoved(): serviceId >" + service.getId() + "<");
        }
    }
    
    /**************************************************************************/
    public static void main (String[] argv)
    {
        // Load libraries if running in Windows
        //System.loadLibrary("FTPlib");
        //System.loadLibrary("netspireSDK");

        // Load library if running in Linux
        String libraryAbsolutePath = System.getProperty("user.dir") + "/libaudioserver.so";
        System.load(libraryAbsolutePath);

        // Create new instance of AudioServer
        AudioServer gAudioServer = new AudioServer();      // an instance of oa.as.AudioServer
        //boolean gUsePolling = true;                                     // use polling or event-driven
        
        // setup Audio Server addresses
        StringArray serverAddresses = new StringArray();
        serverAddresses.add("10.0.7.179");
        serverAddresses.add("10.0.231.179");
        
        // setup connection parameters
        KeyValueMap connectionParametersKeyValuePair = new KeyValueMap();
        //connectionParametersKeyValuePair.set("NETSPIRE_SDK_SOCKET_PORT", "20772");             // change API binding port number (default port number is 20770)
        //connectionParametersKeyValuePair.set("NETSPIRE_SDK_SET_LOG_LEVEL", "0");
        //connectionParametersKeyValuePair.set("NETSPIRE_SDK_SET_DEBUG_FILE", "c:\\share\\sdkDebug.log");
        //connectionParametersKeyValuePair.set("NETSPIRE_SDK_SET_ERROR_FILE", "c:\\share\\sdkError.log");
        
        // register observers if using event based communication
        audioserverObserver asObserver = new audioserverObserver();
        paControllerObserver paObserver = new paControllerObserver();
        //callControllerObserver callObserver = new callControllerObserver();
        //passengerInformationObserver pisObserver = new passengerInformationObserver();
        
        //if (!gUsePolling)
        {
            gAudioServer.registerObserver(asObserver);
            gAudioServer.getPAController().registerObserver(paObserver);
            //gAudioServer.getCallController().registerObserver(callObserver);
            //gAudioServer.getPassengerInformationServer().registerObserver(pisObserver);
        }
        
        // send connection request
        gAudioServer.connect(serverAddresses, connectionParametersKeyValuePair);
        serverAddresses.delete();
        connectionParametersKeyValuePair.delete();
        
        //if (gUsePolling)
        {
            // wait until connection is established on the AudioServer. Alternatively do something untill 'onAudioConnected()' is not received.
            for (int i = 0; i < 10; i++)
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
        }
        
        //===============================================================================================
        // connection to audio server is established. idle forever and get device states every x seconds
        //===============================================================================================
        for (;;)
        {
            //if (gUsePolling)
            {
                // Get list of all devices and their current states
                DeviceStateArray states = gAudioServer.getDeviceStates();
                for (int i = 0; i < states.size(); i++)
                {
                    Device device = states.get(i);
                    System.out.println("Name: " + device.getName() + " State: " + device.getState());
                }
                states.delete();
                
                // Get list of all PaSinks
                PaSinkArray availablePaSinks = gAudioServer.getPAController().getPaSinks();
                System.out.println("Total PASinks in system: " + availablePaSinks.size());
                
                // Get list of all Zones
                PaZoneArray availableZones = gAudioServer.getPAController().getPaZones();
                System.out.println("Total zones in system: " + availableZones.size());

                // Test enable/disable PAMonitoring
                PaSink tmpSink = new PaSink();
                tmpSink.setPAMonitoring(true);
                boolean retVal = tmpSink.getPAMonitoring();
                System.out.println("Monitoring is: " + retVal);
                tmpSink.setPAMonitoring(false);
                retVal = tmpSink.getPAMonitoring();
                System.out.println("Monitoring is: " + retVal);

                System.out.println("");
            }
            
            // sleep
            try { Thread.sleep(5000); }
            catch (InterruptedException e) { }
          
        }
        
        // close connection to AudioServer
        //gAudioServer.initiateShutdown ();
        //gAudioServer.disconnect ();
        //gAudioServer.delete ();
    }
}

