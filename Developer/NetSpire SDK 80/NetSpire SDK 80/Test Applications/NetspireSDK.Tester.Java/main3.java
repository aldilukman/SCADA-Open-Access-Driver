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

public class main3
{
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
        
        // setup Audio Server addresses
        StringArray serverAddresses = new StringArray();
        serverAddresses.add("10.0.7.179");
        serverAddresses.add("10.0.231.179");
        
        // setup connection parameters
        KeyValueMap connectionParametersKeyValuePair = new KeyValueMap();
        connectionParametersKeyValuePair.set("NETSPIRE_SDK_MAX_EVENTITEMS", "0");
        connectionParametersKeyValuePair.set("NETSPIRE_SDK_SOCKET_PORT", "20775");             // change API binding port number (default port number is 20770)
        connectionParametersKeyValuePair.set("NETSPIRE_SDK_SET_LOG_LEVEL", "0");
        //connectionParametersKeyValuePair.set("NETSPIRE_SDK_SET_DEBUG_FILE", "./sdkDebug.log");
        //connectionParametersKeyValuePair.set("NETSPIRE_SDK_SET_ERROR_FILE", "./sdkError.log");
        
        // send connection request
        gAudioServer.connect(serverAddresses, connectionParametersKeyValuePair);
        serverAddresses.delete();    
        connectionParametersKeyValuePair.delete();
        
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
        
        // Upload dynamic configuration
        KeyValueMap dynamicConfiguration = new KeyValueMap();
        //gAudioServer.setDynamicConfiguration(configuration);
        dynamicConfiguration.delete();
        
        //===============================================================================================
        // connection to audio server is established. idle forever and get device states every x seconds
        //===============================================================================================
        int cntLoop = 0;
        boolean uploadDeviceList = true, getAllDevicesFromTCX = false;
		boolean doDeviceTest = false;
		boolean play = false;
		boolean play2 = false;
        oa.as.DeviceClassMap deviceListForAudioServer = new oa.as.DeviceClassMap();
        for (;;)
        {
            if (uploadDeviceList)
            {
                deviceListForAudioServer.clear();
                if (!getAllDevicesFromTCX)
                {
                    deviceListForAudioServer.set("TCXC1", oa.as.Device.DeviceClass.COMMUNICATIONS_EXCHANGE);
                    deviceListForAudioServer.set("TCXC2", oa.as.Device.DeviceClass.COMMUNICATIONS_EXCHANGE);
                    deviceListForAudioServer.set("TCXC3", oa.as.Device.DeviceClass.COMMUNICATIONS_EXCHANGE);
                    deviceListForAudioServer.set("TCXC4", oa.as.Device.DeviceClass.COMMUNICATIONS_EXCHANGE);
                    deviceListForAudioServer.set("TCXC5", oa.as.Device.DeviceClass.COMMUNICATIONS_EXCHANGE);
                    deviceListForAudioServer.set("TCXC6", oa.as.Device.DeviceClass.COMMUNICATIONS_EXCHANGE);
                    deviceListForAudioServer.set("TCXC7", oa.as.Device.DeviceClass.COMMUNICATIONS_EXCHANGE);
                    deviceListForAudioServer.set("TCXC8", oa.as.Device.DeviceClass.COMMUNICATIONS_EXCHANGE);
                    deviceListForAudioServer.set("CSPKR1", oa.as.Device.DeviceClass.MONITOR_SPEAKER);
                    deviceListForAudioServer.set("CSPKR8", oa.as.Device.DeviceClass.MONITOR_SPEAKER);
                    deviceListForAudioServer.set("TGUC1", oa.as.Device.DeviceClass.NETWORK_AUDIO_CONTROLLER);
                    deviceListForAudioServer.set("TGUC2", oa.as.Device.DeviceClass.NETWORK_AUDIO_CONTROLLER);
                    deviceListForAudioServer.set("TGUC3", oa.as.Device.DeviceClass.NETWORK_AUDIO_CONTROLLER);
                    deviceListForAudioServer.set("TGUC4", oa.as.Device.DeviceClass.NETWORK_AUDIO_CONTROLLER);
                    deviceListForAudioServer.set("TGUC5", oa.as.Device.DeviceClass.NETWORK_AUDIO_CONTROLLER);
                    deviceListForAudioServer.set("TGUC6", oa.as.Device.DeviceClass.NETWORK_AUDIO_CONTROLLER);
                    deviceListForAudioServer.set("TGUC7", oa.as.Device.DeviceClass.NETWORK_AUDIO_CONTROLLER);
                    deviceListForAudioServer.set("TGUC8", oa.as.Device.DeviceClass.NETWORK_AUDIO_CONTROLLER);
                    deviceListForAudioServer.set("TRC1", oa.as.Device.DeviceClass.TRAIN_RADIO);
                    deviceListForAudioServer.set("TRC2", oa.as.Device.DeviceClass.TRAIN_RADIO);
                    deviceListForAudioServer.set("EDIC1", oa.as.Device.DeviceClass.PASSENGER_INFORMATION_DISPLAY);
                    deviceListForAudioServer.set("EDIC8", oa.as.Device.DeviceClass.PASSENGER_INFORMATION_DISPLAY);
                    deviceListForAudioServer.set("IDIC1N1", oa.as.Device.DeviceClass.PASSENGER_INFORMATION_DISPLAY);
                }
                gAudioServer.setDeviceList(deviceListForAudioServer);
                getAllDevicesFromTCX = !getAllDevicesFromTCX;
                uploadDeviceList = false;
            }
            
            if (cntLoop % 10 == 00)
            {
                uploadDeviceList = true;
            }
            
			if (cntLoop == 200)
			{
				//gAudioServer.updateDictionary("/home/openaccess/java/ktmbDictionary.zip");
			}
            
            if (cntLoop % 1000 == 00)
            {
                doDeviceTest = true;
            }
            
            if (cntLoop % 100 == 00)
            {
                play = true;
            }
            
            if (cntLoop % 200 == 00)
            {
                play2 = true;
            }

            System.out.println("");
            System.out.println("-------------------------------------------------------------------------------------------------------------------------------");
            DeviceStateArray states = new DeviceStateArray();
            states = gAudioServer.getDeviceStates();
            PaSinkArray availablePaSinks = gAudioServer.getPAController().getPaSinks();
            PaZoneArray availableZones = gAudioServer.getPAController().getPaZones();
            
            System.out.println("Cnt: " + cntLoop++ + ", Is Audio Connected: " + gAudioServer.isAudioConnected() + ", Total Devices: " + states.size() + ", Total Pasinks: " + availablePaSinks.size() + ", Total PaZones: " + availableZones.size());
            
            String outputMessage = "";
            for (int i = 0; i < states.size(); i++)
            {
                //Device device  = new Device();
                //device = states.get(i);
                Device device  = states.get(i);
                
                // ignore EDI/IDI devices
                if (device.getDeviceClass().toString() == "PASSENGER_INFORMATION_DISPLAY")
                {
                    continue;
                }
                
                outputMessage += device.getName() + "\t\t";
                outputMessage += device.getDeviceClass() + (device.getDeviceClass().toString() == "HELP_POINT" || device.getDeviceClass().toString() == "MONITOR_SPEAKER" ? "\t\t\t" : (device.getDeviceClass().toString() == "NETWORK_AUDIO_CONTROLLER" ? "\t" : "\t\t")); 
                outputMessage += device.getDstNo() + "\t";
                outputMessage += device.getIP() + "\t";
                outputMessage += device.getState() + "\t";
                outputMessage += device.getStateText() + "\t";
                outputMessage += device.getSoftwareRevision() + "\t";
                outputMessage += device.getDictionarySupport() + "\t";
                outputMessage += device.getDictionaryUpdateStatus() + "\t";
                outputMessage += device.getDictionaryVersion() + "\n";

				if( doDeviceTest )
				{
					System.out.println("Starting device test for " + device.getName() );
					gAudioServer.initiateDeviceHealthTest( device.getName() );
				}
            }
            System.out.println(outputMessage);
			doDeviceTest = false;
			if( play )
			{
				StringArray targetZones = new StringArray();
				targetZones.clear();
				targetZones.add("Vestibules");
				targetZones.add("Decks");
				targetZones.add("Side 1");
				targetZones.add("Side 2");
				targetZones.add("Crew Cab");
				
				StringArray visualDevices = new StringArray();
				//visualDevices.clear();
				visualDevices.add("EDIC1");
				visualDevices.add("EDIC8");
				visualDevices.add("IDIC1N1");
				
				Gain gain = new Gain();
				
				NumberArray dictItems = new NumberArray();
				dictItems.clear();
				dictItems.add(99200);
				dictItems.add(99001);

				String content = "";
				if( play2 )
				{
					content = "Announcement in Progress";
				}

				String text = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><screen x='0' y='0' w='96' h='17'><region id='Main' x='0' y='0' w='96' h='17'><control id='id' x='0' y='0' h='16'><text fontid='FONT1'>" + content + "</text></control></region></screen>";
				
				gAudioServer.playMessage(targetZones, visualDevices, gain, dictItems, text, false, false, 0, 0, 0);
				play = false;
				play2 = false;
			}
            System.out.println("");

            
            // sleep
            try { Thread.sleep(80); }
            catch (InterruptedException e) { }
          
        }
        
        // close connection to AudioServer
        //gAudioServer.initiateShutdown();
        //gAudioServer.disconnect();
        //gAudioServer.delete();
        //deviceListForAudioServer.delete();
    }
}

