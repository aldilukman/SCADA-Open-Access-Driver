using System;
using netspire;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
using Len.Jakpro.Database.Rendundancy;
using SimpleTCP;
using System.Data.SqlClient;
using System.Data;
using NLog;
using Len.Jakpro.OpenAccess.Logging;
using Len.Jakpro.ClassOpenAccess.Model_Train;
using System.Globalization;
using System.Net.Sockets;

namespace Len.Jakpro.ClassOpenAccess
{
    /// <summary>
    /// OA Driver Monitoring and Control
    /// </summary>
    public class OpenAccessDriver
    {
        public DisplayVariableValueMap PIDDisplay { get; set; }
        // IPPA,NAC,CXS,PHP,PID
        private List<string> _ListHadwareIPPAID = new List<string>();
        private List<string> _ListHadwareNACID = new List<string>();
        private List<string> _ListHadwareCXSServerID = new List<string>();
        private List<string> _ListHadwarePHPID = new List<string>();
        private List<string> _ListHadwarePIDID = new List<string>();
        // PA Source and Sink
        private List<string> _ListHadwareSinkID = new List<string>();
        private List<string> _ListHadwareSourceID = new List<string>();
        // Netspire AudioServer and PA server
        private AudioServer _AudioServer = null;
        private AudioObserver _AudioServerObserver = null;
        private PAObserverClass _PAControllerObserver = null;
        private CallObserverClass _CallControllerObserver = null;
        // Database Connection
        private Connection _Con;
        // Variable for Activate Annoucements
        private int _mySourceId = 0, _mySWTriggerId = 0;
        // Send to HMI Server
        private string _IPServer;
        private int _Port;
        // Log Data
        private Logger _logger;
        // Construct OA Driver
        public OpenAccessDriver()
        {
            try
            {
                _logger = LogManager.GetCurrentClassLogger();
                GetTblDynamic("OA_GetDeviceCXS", _ListHadwareCXSServerID);
                GetTblDynamic("OA_GetDeviceIPPA", _ListHadwareIPPAID);
                GetTblDynamic("OA_GetDeviceNAC", _ListHadwareNACID);
                GetTblDynamic("OA_GetDevicePHP", _ListHadwarePHPID);
                GetTblDynamic("OA_GetDevicePID", _ListHadwarePIDID);

                GetTblDynamic("OA_GetDevicePASink", _ListHadwareSinkID);
                GetTblDynamic("OA_GetDevicePASource", _ListHadwareSourceID);
                var _logging = new NLogConfigurator();
                _logging.Configure();
            }
            catch (Exception e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }            
        }
     
        #region Communication Driver
        public void StartSend(string IP, int Port)
        {
            try
            {
                _IPServer = IP;
                _Port = Port;
            }
            catch (Exception e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }
        }
        // Connect To Audio Server CXS
        public void ConnectAudioServer()
        {
            try
            {
                StringArray ServerAddress = new StringArray
                {
                    //Primary Server
                    _ListHadwareCXSServerID[0],
                    //Secondary Server
                    _ListHadwareCXSServerID[1]
                };
                // set config items
                KeyValueMap _ConfigurationItems = new KeyValueMap
                {
                    { "NETSPIRE_SDK_SOCKET_PORT", "20770" },
                    { "NETSPIRE_SDK_SET_LOG_LEVEL", "1" },
                    { "NETSPIRE_SDK_SET_DEBUG_FILE", "Open Access Server.log" },
                    { "NETSPIRE_SDK_SET_ERROR_FILE", "Open Access Server Error.log" }
                };
                // connect to AudioServer
                ConnectToAudioServer(ServerAddress, _ConfigurationItems);
            }
            catch (Exception e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }
              
        }
        public void ConnectToAudioServer(StringArray serverAddresses, KeyValueMap configItems)
        {
            try
            {
                if (_AudioServer == null)
                {
                    _AudioServer = new AudioServer();
                }
                _AudioServer.connect(serverAddresses, configItems);

                Thread.Sleep(3000);
                while (!_AudioServer.isAudioConnected())
                {
                    serverAddresses.Dispose();
                    configItems.Dispose();
                    Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "Trying to connect CXS....");
                    Thread.Sleep(1000);
                }
                if (_AudioServer.isAudioConnected())
                {
                    // Connect To AudioServer
                    Console.WriteLine("=================================================SERVICE INFO========================================================");
                    _AudioServerObserver = new AudioObserver(_IPServer, _Port);
                    _AudioServer.registerObserver(_AudioServerObserver);
                    Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "Audio CXS Service Started.....");
                    // Connect To VOIP Call
                    _CallControllerObserver = new CallObserverClass(_IPServer, _Port);
                    _AudioServer.getCallController().registerObserver(_CallControllerObserver);
                    Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "Call Controller Service Started.....");
                    // Connect To PAServer
                    _PAControllerObserver = new PAObserverClass(_IPServer, _Port);
                    _AudioServer.getPAController().registerObserver(_PAControllerObserver);
                    Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "Public Announcements Controller Service Started.....");
                    Console.WriteLine("=================================================MESSAGE INFO========================================================");
                    SendAllCurrentDevicesState();
                    string timeStation = GetScheduledArrival(1006, 2);
                    Console.WriteLine("Time Station :" + timeStation);
                    string timeStationNext = GetScheduledArrival(1004, 2);
                    Console.WriteLine("Time Next Station  :" + timeStationNext);
                    DateTime dateTimeStation = DateTime.ParseExact(timeStation, "HH:mm:ss", CultureInfo.InvariantCulture);
                    DateTime dateTimeStationNext = DateTime.ParseExact(timeStationNext, "HH:mm:ss", CultureInfo.InvariantCulture);
                    double differentTime = dateTimeStation.Subtract(dateTimeStationNext).TotalMinutes;
                }
            }
            catch (Exception e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
                return;
            }            
        }
        public void DisconnectAudioServer()
        {
            try
            {
                if (_AudioServer != null)
                {
                    if (_mySourceId > 0)
                    {
                        _AudioServer.getPAController().detachPaSource(_mySourceId);
                        _mySourceId = 0;
                    }
                    _AudioServer.disconnect();
                    _AudioServer.Dispose();
                    _AudioServer = null;
                    _AudioServerObserver = null;
                }
            }
            catch (Exception e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }
        }
        // Get Current State PA and Mic for conversion from string value to integer.
        public int GetDeviceState(string State)
        {
                         
                    if (State.Equals("IDLE"))
                    {
                        return 1;
                    }
                    else if (State.Equals("ALERTING"))
                    {
                        return 2;
                    }
                    else if (State.Equals("ACTIVE"))
                    {
                        return 3;
                    }
                    else if (State.Equals("FAULTY"))
                    {
                        return 4;
                    }
                    else if (State.Equals("COMMSFAULT"))
                    {
                        return 5;
                    }
                    else if (State.Equals("HELD"))
                    {
                        return 6;
                    }
                    else
                    {
                        return 10;
                    }           
          
        }
        public int GetCurrentStatePA(string State)
        {
            if (State.Equals("HEALTHY"))
            {
                return 1;
            }
            else if (State.Equals("UNKNOWN_HEALTHSTATE"))
            {
                return 2;
            }
            else if (State.Equals("MINOR_FAULT"))
            {
                return 3;
            }
            else if (State.Equals("MAJOR_FAULT"))
            {
                return 5;
            }
            else
            {
                return 10;
            }
        }
        public int GetCurrentStateMIC(string State)
        {
            if (State.Equals("HEALTHY"))
            {
                return 1;
            }
            else if (State.Equals("UNKNOWN_HEALTHSTATE"))
            {
                return 4;
            }
            else if (State.Equals("FAULTY"))
            {
                return 5;
            }
            else
            {
                return 10;
            }
        }
        public void SendToTCPCurrentDevice(string variableName, int Index, int Value)
        {            
            try
            {
                SimpleTcpClient _SendCurrentDeviceState;
                _SendCurrentDeviceState = new SimpleTcpClient().Connect(_IPServer, _Port);
                _SendCurrentDeviceState.Write("" + variableName + "," +
                    Index + "," +
                    Value + ""
                    );
                _SendCurrentDeviceState.Dispose();
            }
            catch (SocketException e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }
        }
        public void GetTblDynamicPASink(string QueryProcedure, List<string> _ListHadwareID, List<string> _ListCOMM)
        {           
            try
            {
                _Con = new Connection();
                _Con.Connected();
                _Con.SqlCmd = new SqlCommand
                {
                    Connection = _Con.SqlCon,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = QueryProcedure
                };
                _Con.SqlRead = _Con.SqlCmd.ExecuteReader();
                while (_Con.SqlRead.Read())
                {
                    _ListHadwareID.Add(_Con.SqlRead.GetValue(0).ToString());
                    _ListCOMM.Add(_Con.SqlRead.GetValue(1).ToString());
                }
                _Con.Disconnected();
            }
            catch (SqlException e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }
        }
        public void SendCurrentDeviceStates(string Communication)
        {
            try
            {
                SimpleTcpClient _VoipClient;
                _VoipClient = new SimpleTcpClient().Connect(_IPServer, _Port);
                _VoipClient.Write("" + Communication + ""
                    );
                _VoipClient.Dispose();
            }
            catch (SocketException e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }
        }
        public void GetTblDynamic(string QueryProcedure, List<string> _ListHadwareID)
        {           
            try
            {
                _Con = new Connection();
                _Con.Connected();
                _Con.SqlCmd = new SqlCommand
                {
                    Connection = _Con.SqlCon,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = QueryProcedure
                };
                _Con.SqlRead = _Con.SqlCmd.ExecuteReader();
                while (_Con.SqlRead.Read())
                {
                    _ListHadwareID.Add(_Con.SqlRead.GetValue(0).ToString());
                }
                _Con.Disconnected();
            }
            catch (SqlException e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }
        }
        // Send All Devices Including IPPA,NAC,PHP,PID,PA also Microphone.
        public void SendAllCurrentDevicesState()
        {
                try
                {           
                Thread.Sleep(2000);
                // Get All all Status Devices CXS IPPA NAC PHP PID
                DeviceStateArray _DeviceStateArray = _AudioServer.getDeviceStates();
                for (short i = 0; i < _DeviceStateArray.Count; i++)
                {
                    Device thisDevice = _DeviceStateArray.ElementAt(i);
                    if (_ListHadwareCXSServerID.Contains(thisDevice.getIP().ToString()))
                    {
                        try
                        {
                            _Con = new Connection();
                            _Con.Connected();
                            _Con.SqlCmd = new SqlCommand
                            {
                                Connection = _Con.SqlCon,
                                CommandType = CommandType.StoredProcedure,
                                CommandText = "OA_GetCommunicationCXS"
                            };
                            _Con.SqlCmd.Parameters.AddWithValue("@HadwareID", thisDevice.getIP());
                            _Con.SqlRead = _Con.SqlCmd.ExecuteReader();
                            while (_Con.SqlRead.Read())
                            {
                                SendCurrentDeviceStates(_Con.SqlRead.GetString(1) + "," + GetDeviceState(thisDevice.getState().ToString()));
                            }
                            _Con.Disconnected();
                        }
                        catch (SqlException e)
                        {
                            _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
                        }
                    }
                    // IPPA List          
                    if (_ListHadwareIPPAID.Contains(thisDevice.getDstNo().ToString()))
                    {
                        try
                        {
                            _Con = new Connection();
                            _Con.Connected();
                            _Con.SqlCmd = new SqlCommand
                            {
                                Connection = _Con.SqlCon,
                                CommandType = CommandType.StoredProcedure,
                                CommandText = "OA_GetCommunicationIPPA"
                            };
                            _Con.SqlCmd.Parameters.AddWithValue("@HadwareID", thisDevice.getDstNo());
                            _Con.SqlRead = _Con.SqlCmd.ExecuteReader();
                            while (_Con.SqlRead.Read())
                            {
                                SendCurrentDeviceStates(_Con.SqlRead.GetString(1) + "," + GetDeviceState(thisDevice.getState().ToString()));
                            }
                            _Con.Disconnected();
                        }
                        catch (SqlException e)
                        {
                            _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
                        }
                    }
                    // NAC List
                    if (_ListHadwareNACID.Contains(thisDevice.getDstNo().ToString()))
                    {
                        try
                        {
                            _Con = new Connection();
                            _Con.Connected();
                            _Con.SqlCmd = new SqlCommand
                            {
                                Connection = _Con.SqlCon,
                                CommandType = CommandType.StoredProcedure,
                                CommandText = "OA_GetCommunicationNAC"
                            };
                            _Con.SqlCmd.Parameters.AddWithValue("@HadwareID", thisDevice.getDstNo());
                            _Con.SqlRead = _Con.SqlCmd.ExecuteReader();
                            while (_Con.SqlRead.Read())
                            {
                                SendCurrentDeviceStates(_Con.SqlRead.GetString(1) + "," + GetDeviceState(thisDevice.getState().ToString()));
                            }
                            _Con.Disconnected();
                        }
                        catch (SqlException e)
                        {
                            _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
                        }
                    }
                    // PHP 
                    if (_ListHadwarePHPID.Contains(thisDevice.getDstNo().ToString()))
                    {
                        try
                        {
                            _Con = new Connection();
                            _Con.Connected();
                            _Con.SqlCmd = new SqlCommand
                            {
                                Connection = _Con.SqlCon,
                                CommandType = CommandType.StoredProcedure,
                                CommandText = "OA_GetCommunicationPHP"
                            };
                            _Con.SqlCmd.Parameters.AddWithValue("@HadwareID", thisDevice.getDstNo());
                            _Con.SqlRead = _Con.SqlCmd.ExecuteReader();
                            while (_Con.SqlRead.Read())
                            {
                                SendCurrentDeviceStates(_Con.SqlRead.GetString(1) + "," + GetDeviceState(thisDevice.getState().ToString()));
                            }
                            _Con.Disconnected();
                        }
                        catch (SqlException e)
                        {
                            _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
                        }
                    }
                    // PID 
                    if (_ListHadwarePIDID.Contains(thisDevice.getIP().ToString()))
                    {
                        try
                        {
                            _Con = new Connection();
                            _Con.Connected();
                            _Con.SqlCmd = new SqlCommand
                            {
                                Connection = _Con.SqlCon,
                                CommandType = CommandType.StoredProcedure,
                                CommandText = "OA_GetCommunicationPID"
                            };
                            _Con.SqlCmd.Parameters.AddWithValue("@HadwareID", thisDevice.getIP());
                            _Con.SqlRead = _Con.SqlCmd.ExecuteReader();
                            while (_Con.SqlRead.Read())
                            {
                                SendCurrentDeviceStates(_Con.SqlRead.GetString(1) + "," + GetDeviceState(thisDevice.getState().ToString()));
                            }
                            _Con.Disconnected();
                        }
                        catch (SqlException e)
                        {
                            _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
                        }
                    }
                }

                // Get All Status Devices PA Audio
                PaSinkArray _GetPASink = _AudioServer.getAudioSinks();
                for (int a = 0; a < _GetPASink.Count; a++)
                {
                    if (_ListHadwareSinkID.Contains(_GetPASink.ElementAt(a).id.ToString()))
                    {
                        try
                        {
                            _Con = new Connection();
                            _Con.Connected();
                            _Con.SqlCmd = new SqlCommand
                            {
                                Connection = _Con.SqlCon,
                                CommandType = CommandType.StoredProcedure,
                                CommandText = "OA_GetCommunicationPASink"
                            };
                            _Con.SqlCmd.Parameters.AddWithValue("@HadwareID", _GetPASink.ElementAt(a).id);
                            _Con.SqlRead = _Con.SqlCmd.ExecuteReader();
                            while (_Con.SqlRead.Read())
                            {
                                SendCurrentDeviceStates(_Con.SqlRead.GetString(1) + "," + GetCurrentStatePA(_GetPASink.ElementAt(a).healthState.ToString()));
                            }
                            _Con.Disconnected();
                        }
                        catch (SqlException e)
                        {
                            _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
                        }
                    }
                }
                //Get PA Microphone
                PAController _GetPAController = _AudioServer.getPAController();
                PaSourceArray _GetPAMic = _GetPAController.getPaSources();
                for (int i = 0; i < _GetPAMic.Count; i++)
                {
                    PaSource _PASource = _GetPAMic.ElementAt(i);
                    if (_ListHadwareSourceID.Contains(_PASource.id.ToString()))
                    {
                        try
                        {
                            _Con = new Connection();
                            _Con.Connected();
                            _Con.SqlCmd = new SqlCommand
                            {
                                Connection = _Con.SqlCon,
                                CommandType = CommandType.StoredProcedure,
                                CommandText = "OA_GetCommunicationPASource"
                            };
                            _Con.SqlCmd.Parameters.AddWithValue("@HadwareID", _PASource.id);
                            _Con.SqlRead = _Con.SqlCmd.ExecuteReader();
                            while (_Con.SqlRead.Read())
                            {
                                SendCurrentDeviceStates(_Con.SqlRead.GetString(1) + "," + GetCurrentStateMIC(_PASource.healthState.ToString()));
                            }
                            _Con.Disconnected();
                        }
                        catch (SqlException e)
                        {
                            _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
                        }
                    }
                }
                }
                catch (Exception e)
                {
                    _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
                }
        }
        #endregion

        #region List PA and PID Announcements Function
        // Play DVA Audio From HMI     
        public void GetPlayDVA(List<string> ZoneValue, List<uint> CustomMedia)
        {          
            try
            {
                // Check audio server connection
                if (_AudioServer == null || !_AudioServer.isAudioConnected())
                {
                    _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss :") + "CXS Not Connected.....!");
                    return;
                }
                //Audio configuration
                netspire.Message _NetspireMessages = new netspire.Message();
                StringArray _NetspireZone = new StringArray();
                NumberArray _NetspireMediaItems = new NumberArray();
                MessagePriority _msgPriority = new MessagePriority();
                _msgPriority.setPriorityMode(AnnouncementPriorityMode.APM_RELATIVE_PRIORITY);
                _msgPriority.setPrioritylevel(950);
                // add new zone
                foreach (string a in ZoneValue)
                {
                    //Thread.Sleep(100);
                    _NetspireZone.Add(a);
                    Console.WriteLine("Zone Added :" + a);
                }
                // filter if zone exist
                if (_NetspireZone.Count > 0)
                {
                    // add new media items
                    foreach (uint var in CustomMedia)
                    {
                        _NetspireMediaItems.Add(var);
                        Console.WriteLine("Media Added :" + var);
                    }
                    _NetspireMessages.setPriority(_msgPriority);
                    _NetspireMessages.setPreChime(99200); // first audio 
                    _NetspireMessages.setAudioMessageType(netspire.Message.Type.DICTIONARY);
                    // set new media items                       
                    _NetspireMessages.setAudioMessage(_NetspireMediaItems);
                    // play media items
                    _AudioServer.getPAController().playMessage(_NetspireZone, _NetspireMessages);
                    Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "Initiate DVA Announcements Succesfully");
                    //clear after play
                    _NetspireZone.Clear();
                    _NetspireMediaItems.Clear();
                }
            }
            catch (Exception e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }
        }
        // PID Text Information Line 3
        public void SetPIDText(List<string> ZoneList, string ObjectValue)
        {
            try
            {
                string templateName = "Display";
                string keyLine1 = "*^Line3";
                string value1 = ObjectValue;
                int timeout = 0;
                // generate list of selected paZoneIds
                StringArray PIDZone = new StringArray();
                foreach (string Zone in ZoneList)
                {
                    PIDZone.Add(Zone);
                    Console.WriteLine("Zone Added: " + Zone);
                    _logger.Debug(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "Zone PID : " + Zone + "");
                }
                // Filter new zone
                if (PIDZone.Count <= 0)
                {
                    Console.WriteLine("No Zone For PID");
                    return;
                }
                // request change template
                DisplayVariableValueMap displayVariableList = new DisplayVariableValueMap();
                DisplayValueList valueList = new DisplayValueList();
                if (keyLine1.Length > 0 && value1.Length > 0)
                {
                    DISPLAY_VALUE_INFO newValue = new DISPLAY_VALUE_INFO(value1, timeout);
                    valueList.Add(newValue);
                }
                if (valueList.Count > 0)
                {
                    displayVariableList.Add(keyLine1, valueList);
                }
                netspire.Message newMessage = new netspire.Message();
                MessagePriority msgPriority = new MessagePriority();
                msgPriority.setPriorityMode(AnnouncementPriorityMode.APM_RELATIVE_PRIORITY);
                msgPriority.setPrioritylevel(500);
                newMessage.setPriority(msgPriority);
                newMessage.setVisualMessageType(netspire.Message.Type.DISPLAY_TEMPLATE);
                int validity = 0;   // until overridden
                newMessage.setVisualMessage(templateName, displayVariableList, validity);
                string requestId = _AudioServer.getPAController().playMessage(PIDZone, newMessage);
                Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "Update PID! With RequestId : " + requestId + "");
                _logger.Debug(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "Update PID Line 3 Text :" + value1 + " ! With RequestId : " + requestId + "");
            }
            catch (Exception e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }
        }
        // Live Announcements function
        public void GetPreDVALiveAnnouncements(List<string> ZoneValue, List<uint> CustomMedia)
        {           
            try
            {
                // Check audio server connection
                if (_AudioServer == null || !_AudioServer.isAudioConnected())
                {
                    _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss :") + "CXS Not Connected.....!");
                    return;
                }
                //Audio configuration
                netspire.Message NetspireMessages = new netspire.Message();
                StringArray NetspireZone = new StringArray();
                NumberArray NetspireMediaItems = new NumberArray();
                MessagePriority msgPriority = new MessagePriority();
                msgPriority.setPriorityMode(AnnouncementPriorityMode.APM_ABSOLUTE_PRIORITY);
                msgPriority.setPrioritylevel(1000);
                // add new zone
                foreach (string a in ZoneValue)
                {
                    NetspireZone.Add(a);
                }
                // add new media items
                foreach (uint var in CustomMedia)
                {
                    NetspireMediaItems.Add(var);
                }
                // filter if zone exist
                if (NetspireZone.Count > 0)
                {
                    NetspireMessages.setPriority(msgPriority);
                    // NetspireMessages.setPreChime(99200); // first audio 
                    NetspireMessages.setAudioMessageType(netspire.Message.Type.DICTIONARY);
                    // set new media items                       
                    NetspireMessages.setAudioMessage(NetspireMediaItems);
                    // play media items
                    _AudioServer.getPAController().playMessage(NetspireZone, NetspireMessages);
                }
                //clear after play
                NetspireZone.Clear();
                NetspireMediaItems.Clear();
            }
            catch (Exception e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }

        }
        public void GetLiveAnnouncement(int SourceID, List<string> Zone)
        {          
            try
            {
                if (_AudioServer == null || !_AudioServer.isAudioConnected())
                {
                    _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss :") + "CXS Not Connected For Live Announcements.....!");
                    return;
                }
                // Attach new Source
                AttachSourceID(SourceID);
                // Adding New Zone
                AttachPAZone(SourceID, Zone);
                //CreateSWTrigger
                CreateSWTrigger(SourceID);
                //When created sw trigger then activate it         
                ActivatedPATrigger();
            }
            catch (Exception e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }
        }
        public PaSource GetPaSource(int MicId)
        {
            try
            {
                foreach (PaSource source in _AudioServer.getPAController().getPaSources())
                {
                    if (source.id == MicId)
                        return source;
                }              
            }
            catch (Exception e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }
            return null;
        }
        public void AttachSourceID(int SourceID)
        {
            try
            {
                // Attach PaSource
                _mySourceId = SourceID;
                PAController paController = _AudioServer.getPAController();
                PaSourceArray PAFromServer = paController.getPaSources();
                bool IsattachSuccess = false;
                for (int i = 0; i < PAFromServer.Count; i++)
                {
                    if (PAFromServer.ElementAt(i).id == _mySourceId)
                    {
                        if (PAFromServer.Count >= PAFromServer.Count)
                        {
                            Thread.Sleep(500);
                            bool HasAttached = paController.attachPaSource(_mySourceId);
                            if (HasAttached)
                            {
                                PAFromServer.ElementAt(i).detachAllPaZones();
                                IsattachSuccess = true;
                                Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss :") + "Attach Success with source id " + _mySourceId + "");
                                break;
                            }
                        }
                    }
                }
                Thread.Sleep(1500);
                if (!IsattachSuccess)
                {
                    Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss :") + "Attach Failed");
                    _mySourceId = 0;
                    return;
                }
            }
            catch (Exception e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }
         
        }
        public void AttachPAZone(int SourceID, List<string> Zone)
        {
            try
            {
                _mySourceId = SourceID;
                PaSourceArray _PAFromServer = _AudioServer.getPAController().getPaSources();
                // Get PA Zone
                PaSource _paSource = GetPaSource(_mySourceId);
                bool IsattachSuccess = false;
                for (int i = 0; i < _PAFromServer.Count; i++)
                {
                    if (_PAFromServer.ElementAt(i).id == _mySourceId)
                    {
                        if (_PAFromServer.Count >= _PAFromServer.Count)
                        {
                            foreach (string _MyZone in Zone)
                            {
                                // Attach All PA Zone
                                _paSource.attachPaZone(_MyZone, PaSource.AttachMode.ADD_TO_EXISTING_SET);
                            }
                            IsattachSuccess = true;
                            Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss :") + "Attach Zone Succesfuly");
                        }
                        Thread.Sleep(1000);
                    }
                }
                Thread.Sleep(2000);
                if (!IsattachSuccess)
                {
                    Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss :") + "Attach Zone Failed");
                    return;
                }
            }
            catch (Exception e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }            
        }
        public void ActivatedPATrigger()
        {
            try
            {
                if (_mySWTriggerId <= 0)
                {
                    return;
                }
                // check status attached
                bool _IsAttachedPA = false;
                PAController _paController = _AudioServer.getPAController();
                PaSourceArray _GetAllPA = _paController.getPaSources();
                // Get List Attached PA Zone
                for (int i = 0; i < _GetAllPA.Count(); i++)
                {
                    if (_GetAllPA.ElementAt(i).id == _mySourceId)
                    {
                        _IsAttachedPA = (_GetAllPA.ElementAt(i).getAttachedPaZones().Count() > 0);
                        Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss :") + "Attached to system :" + _GetAllPA.ElementAt(i).id.ToString());
                        break;
                    }
                }
                // check status if true 
                if (_IsAttachedPA)
                {
                    _paController.activateSwPaTrigger(_mySWTriggerId);
                    Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss :") + "PA Annoucements Active with triggerid " + _mySWTriggerId + "");
                }
                else
                {
                    Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss :") + "No PA Zone Attached for Source ID >" + _mySourceId + "<");
                    return;
                }
            }
            catch(Exception e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }          
        }
        public void GetDisconnectAnnoucement()
        {
            try
            {
                if (_AudioServer == null || !_AudioServer.isAudioConnected())
                {
                    return;
                }
                _AudioServer.getPAController().deactivateSwPaTrigger(_mySWTriggerId);
                Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss :") + "PA Announcements Deactivated SW Trigger Succesfully");
                _AudioServer.getPAController().detachPaSource(_mySourceId);
                Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss :") + "PA Announcements Remove SourceID Succesfully");
                _AudioServer.getPAController().deleteSwPaTrigger(_mySWTriggerId);
                Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss :") + "PA Announcements DeleteSWTrigger Succesfully");
                _mySWTriggerId = 0;
            }
            catch (Exception e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }           
        }
        public void CreateSWTrigger(int SourceID)
        {
            try
            {
                if (SourceID > 0)
                {
                    if (_mySWTriggerId == 0)
                    {
                        int swPAPriority = 100;
                        PAController paC = _AudioServer.getPAController();
                        _mySWTriggerId = paC.createSwPaTrigger(SourceID, swPAPriority);
                        if (_mySWTriggerId > 0)
                            Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss :") + "created new SW triggerId >" + _mySWTriggerId + "<");
                        else
                            Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss :") + "failed create triggerId");
                    }
                    else
                    {
                        Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss :") + "trigger aleready exist");
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }           
        }
        #endregion

        #region Automatic Signalling PA and PID By Events
        // Automatic PA Service From Signalling for Arrival,Departure,Approaching
        public void SetDVAAnnouncementsSignalling(List<string> StationZone, List<uint> CustomMedia)
        {          
            try
            {
                // Check audio server connection
                if (_AudioServer == null || !_AudioServer.isAudioConnected())
                {
                    _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss :") + "CXS Not Connected.....!");
                    return;
                }
                //Audio configuration
                netspire.Message _NetspireMessages = new netspire.Message();
                StringArray _NetspireZone = new StringArray();
                NumberArray _NetspireMediaItems = new NumberArray();
                MessagePriority _msgPriority = new MessagePriority();
                _msgPriority.setPriorityMode(AnnouncementPriorityMode.APM_RELATIVE_PRIORITY);
                _msgPriority.setPrioritylevel(950);
                // add new zone
                foreach (string ListZone in StationZone)
                {
                    //Thread.Sleep(100);
                    _NetspireZone.Add(ListZone);
                    Console.WriteLine("Zone Added :" + ListZone);
                 //   _logger.Debug(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "DVA Signalling Zone Added :" + ListZone.ToString());
                }
                // filter if zone exist
                if (_NetspireZone.Count > 0)
                {
                    // add new media items
                    foreach (uint var in CustomMedia)
                    {
                        _NetspireMediaItems.Add(var);
                        Console.WriteLine("Media Added :" + var);
                       // _logger.Debug(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "DVA Signalling Media Added :" + var.ToString());
                    }
                    _NetspireMessages.setPriority(_msgPriority);
                    _NetspireMessages.setPreChime(99200); // Chime audio 
                    _NetspireMessages.setAudioMessageType(netspire.Message.Type.DICTIONARY);
                    // set new media items                       
                    _NetspireMessages.setAudioMessage(_NetspireMediaItems);
                    // play media items
                    _AudioServer.getPAController().playMessage(_NetspireZone, _NetspireMessages);
                    Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "Initiate DVA Arrival From Signalling Succesfully");
                  //  _logger.Debug(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss :") + " Initiate DVA Arrival From Signalling Succesfully");
                    //clear after play
                    _NetspireZone.Clear();
                    _NetspireMediaItems.Clear();
                }
            }
            catch (Exception e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }

        }
        public void SetSignallingPID(int LocationStation, string Direction, string PIDLine, int PIDCountdown)
        {
            try
            {
                //int LocationStation, string Direction,string PIDLine, int PIDCountdown
                string templateName = "Display";
                string keyLine1 = "*^Line" + PIDLine + "Right";
                string keyLine2 = "*^Line" + PIDLine + "Left";
                string value1 = null;
                string value2 = null;
                int timeout = 0;
                if (PIDCountdown < 10 && PIDCountdown > 0)
                {
                    value1 = "0" + PIDCountdown.ToString() + " Min";
                }
                else if (PIDCountdown <= 0)
                {
                    value1 = "00 Min";
                }
                else if (PIDCountdown == 30)
                {
                    value1 = PIDCountdown.ToString() + " Sec";
                }
                else
                {
                    value1 = PIDCountdown.ToString() + " Min";
                }

                if (Direction.Equals("1"))
                {
                    if (LocationStation.Equals(2))
                    {
                        value2 = "Velodrome";
                    }
                    else
                    {
                        value2 = "Pegangsaan Dua";
                    }
                }
                else if (Direction.Equals("2"))
                {
                    if (LocationStation.Equals(7))
                    {
                        value2 = "Pegangsaan Dua";
                    }
                    else
                    {
                        value2 = "Velodrome";
                    }
                }
                // generate list of selected paZoneIds
                StringArray PIDZone = new StringArray();
                // Zone East
                if (LocationStation.Equals(2) && Direction.Equals("1"))
                {
                    PIDZone.Add("PID DEPOT/Concourse/Concourse 2");
                    PIDZone.Add("PID DEPOT/Platform/Platform 2");
                    Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "Depot PID Platform East and PID Concourse East " + value1 + ", Line : " + PIDLine);
                }
                else if (LocationStation.Equals(3) && Direction.Equals("1"))
                {
                    PIDZone.Add("PID KGM/Concourse/Concourse 2");
                    PIDZone.Add("PID KGM/Platform/Platform 2");
                    Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "Kelapa Gading Mall PID Platform East and PID Concourse East " + value1 + ", Line : " + PIDLine);
                }
                else if (LocationStation.Equals(4) && Direction.Equals("1"))
                {
                    PIDZone.Add("PID KGB/Concourse/Concourse 2");
                    PIDZone.Add("PID KGB/Platform/Platform 2");
                    Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "Kelapa Gading Boulevard PID Platform East and PID Concourse East " + value1 + ", Line : " + PIDLine);
                }
                else if (LocationStation.Equals(5) && Direction.Equals("1"))
                {
                    PIDZone.Add("PID PULOMAS/Concourse/Concourse 2");
                    PIDZone.Add("PID PULOMAS/Platform/Platform 2");
                    Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "Pulomas PID Platform East and PID Concourse East " + value1 + ", Line : " + PIDLine);
                }
                else if (LocationStation.Equals(6) && Direction.Equals("1"))
                {
                    PIDZone.Add("PID PACUANKUDA/Concourse/Concourse 2");
                    PIDZone.Add("PID PACUANKUDA/Platform/Platform 2");
                    Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "Equestrian PID Platform East and PID Concourse East " + value1 + ", Line : " + PIDLine);
                }
                else if (LocationStation.Equals(7) && Direction.Equals("1"))
                {
                    PIDZone.Add("PID VEL/Concourse/Concourse 2");
                    PIDZone.Add("PID VEL/Platform/Platform 2");
                    Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "Velodrome PID Platform East and PID Concourse East " + value1 + ", Line : " + PIDLine);
                }
                // Zone West
                else if (LocationStation.Equals(2) && Direction.Equals("2"))
                {
                    PIDZone.Add("PID DEPOT/Concourse/Concourse 1");
                    PIDZone.Add("PID DEPOT/Platform/Platform 1");
                    Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "Depot PID Platform West and PID Concourse West " + value1 + ", Line : " + PIDLine);
                }
                else if (LocationStation.Equals(3) && Direction.Equals("2"))
                {
                    PIDZone.Add("PID KGM/Concourse/Concourse 1");
                    PIDZone.Add("PID KGM/Platform/Platform 1");
                    Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "Kelapa Gading Mall PID Platform West and PID Concourse West " + value1 + ", Line : " + PIDLine);
                }
                else if (LocationStation.Equals(4) && Direction.Equals("2"))
                {
                    PIDZone.Add("PID KGB/Concourse/Concourse 1");
                    PIDZone.Add("PID KGB/Platform/Platform 1");
                    Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "Kelapa Gading Boulevard PID Platform West and PID Concourse West " + value1 + ", Line : " + PIDLine);
                }
                else if (LocationStation.Equals(5) && Direction.Equals("2"))
                {
                    PIDZone.Add("PID PULOMAS/Concourse/Concourse 1");
                    PIDZone.Add("PID PULOMAS/Platform/Platform 1");
                    Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "Pulomas PID Platform West and PID Concourse West " + value1 + ", Line : " + PIDLine);
                }
                else if (LocationStation.Equals(6) && Direction.Equals("2"))
                {
                    PIDZone.Add("PID PACUANKUDA/Concourse/Concourse 1");
                    PIDZone.Add("PID PACUANKUDA/Platform/Platform 1");
                    Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "Equestrian PID Platform West and PID Concourse West " + value1 + ", Line : " + PIDLine);
                }
                else if (LocationStation.Equals(7) && Direction.Equals("2"))
                {
                    PIDZone.Add("PID VEL/Concourse/Concourse 1");
                    PIDZone.Add("PID VEL/Platform/Platform 1");
                    Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "Velodrome PID Platform West and PID Concourse West " + value1 + ", Line : " + PIDLine);
                }
                // Filter new zone
                //if (PIDZone.Count <= 0)
                //{
                //    Console.WriteLine("No Zone For PID");
                //    return;
                //}
                foreach (string zone in PIDZone)
                {
                    //Console.WriteLine("Zone Added: " + zone);
                   // _logger.Debug(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "Zone PID : " + zone + "");
                }

                // request change template
                DisplayVariableValueMap displayVariableList = new DisplayVariableValueMap();
                DisplayValueList valueList = new DisplayValueList();
                if (keyLine1.Length > 0 && value1.Length > 0)
                {
                    DISPLAY_VALUE_INFO newValue = new DISPLAY_VALUE_INFO(value1, timeout);
                    valueList.Add(newValue);
                }
                if (valueList.Count > 0)
                {
                    displayVariableList.Add(keyLine1, valueList);
                }

                DisplayValueList valueList2 = new DisplayValueList();
                if (keyLine2.Length > 0 && value2.Length > 0)
                {
                    DISPLAY_VALUE_INFO newValue = new DISPLAY_VALUE_INFO(value2, timeout);
                    valueList2.Add(newValue);
                }
                if (valueList2.Count > 0)
                {
                    displayVariableList.Add(keyLine2, valueList2);
                }

                netspire.Message newMessage = new netspire.Message();
                MessagePriority msgPriority = new MessagePriority();
                msgPriority.setPriorityMode(AnnouncementPriorityMode.APM_RELATIVE_PRIORITY);
                msgPriority.setPrioritylevel(500);
                newMessage.setPriority(msgPriority);
                newMessage.setVisualMessageType(netspire.Message.Type.DISPLAY_TEMPLATE);
                int validity = 0;   // until overridden
                newMessage.setVisualMessage(templateName, displayVariableList, validity);
                string requestId = _AudioServer.getPAController().playMessage(PIDZone, newMessage);
                //Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "Update PID! With RequestId : " + requestId + "");
           //     _logger.Debug(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "Update PID! With RequestId : " + requestId + "");
            }
            catch (Exception e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }
        }
        public List<TrainTable> GetTimeTable()
        {
            List<TrainTable> trainTables = new List<TrainTable>();
            try
            {
                
                _Con = new Connection();
                _Con.Connected();
                _Con.SqlCmd = new SqlCommand
                {
                    Connection = _Con.SqlCon,
                    CommandType = CommandType.Text,
                    CommandText = "SELECT TrainNo, TrainID, Status FROM dbo.Journey WHERE(Status = 1)",
                };
                _Con.SqlRead = _Con.SqlCmd.ExecuteReader();
                while (_Con.SqlRead.Read())
                {
                    TrainTable trainTable = new TrainTable
                    {
                        TrainNumber = _Con.SqlRead.GetString(0),
                        TrainID = _Con.SqlRead.GetString(1)
                    };
                    trainTables.Add(trainTable);
                }
                _Con.Disconnected();

                for (int i = 0; i < trainTables.Count; i++)
                {
                    _Con = new Connection();
                    _Con.Connected();
                    String data = trainTables[i].TrainNumber;
                    string dataExecute = "SELECT TOP (1) Direction FROM dbo.JourneyStop WHERE(TrainNo = '" + data + "')";
                    _Con.SqlCmd = new SqlCommand
                    {
                        Connection = _Con.SqlCon,
                        CommandType = CommandType.Text,
                        CommandText = dataExecute,
                    };
                    _Con.SqlRead = _Con.SqlCmd.ExecuteReader();
                    while (_Con.SqlRead.Read())
                    {
                        trainTables[i].Direction = _Con.SqlRead.GetInt32(0).ToString();
                    }
                    _Con.Disconnected();
                }
                return trainTables;
            }
            catch (SqlException e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }
            return trainTables;

        }
        public string GetTimeStation(int TrainNo, int LocationStation)
        {
            string Value = null;
            try
            {               
                _Con = new Connection();
                _Con.Connected();
                _Con.SqlCmd = new SqlCommand
                {
                    Connection = _Con.SqlCon,
                    CommandType = CommandType.Text,
                    CommandText = "SELECT TrainNo, " +
                    "LocationId, " +
                    "ScheduledArrival " +
                    "FROM dbo.JourneyStop WHERE(TrainNo ='" + TrainNo + "') AND(LocationId =" + LocationStation + ")"
                };
                _Con.SqlRead = _Con.SqlCmd.ExecuteReader();
                if (_Con.SqlRead.Read())
                {
                    Value = _Con.SqlRead.GetString(2);
                }
                _Con.Disconnected();
            }
            catch (SqlException e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }          
            return Value;
        }
        public int GetNextTrainNumber(int TrainNo, int Direction)
        {
            int Value = 0;
            try
            {
                _Con = new Connection();
                _Con.Connected();
                _Con.SqlCmd = new SqlCommand
                {
                    Connection = _Con.SqlCon,
                    CommandType = CommandType.Text,
                    CommandText = "SELECT DISTINCT TOP (1) TrainNo " +
                    "FROM dbo.JourneyStop " +
                    "WHERE (TrainNo < '" + TrainNo + "') AND (Direction = " + Direction + ") ORDER BY TrainNo Desc"
                };
                _Con.SqlRead = _Con.SqlCmd.ExecuteReader();
                if (_Con.SqlRead.Read())
                {
                    string Value1 = _Con.SqlRead.GetString(0);
                    Value = Convert.ToInt32(Value1);
                }
                _Con.Disconnected();
            }
            catch (SqlException e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }
            return Value;
        }
        public int GetAfterNextTrainNumber(int TrainNo, int Direction)
        {
            List<int> _Value = new List<int>();
            try
            {
                _Con = new Connection();
                _Con.Connected();
                _Con.SqlCmd = new SqlCommand
                {
                    Connection = _Con.SqlCon,
                    CommandType = CommandType.Text,
                    CommandText = "SELECT DISTINCT TOP (2) TrainNo " +
                    "FROM dbo.JourneyStop " +
                    "WHERE (TrainNo < '" + TrainNo + "') AND (Direction = " + Direction + ") ORDER BY TrainNo Desc"
                };
                _Con.SqlRead = _Con.SqlCmd.ExecuteReader();
                while (_Con.SqlRead.Read())
                {
                    string Value1 = _Con.SqlRead.GetString(0);
                    _Value.Add(Convert.ToInt32(Value1));
                }
                _Con.Disconnected();
            }           
            catch (SqlException e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }
            if (_Value.Count >= 2)
            {
                return _Value[1];
            }
            else
            {
                return 0;
            }
        }
        public int GetBeforeTrain(int TrainNo, int Direction)
        {
            int Value = 0;
            try
            {
                _Con = new Connection();
                _Con.Connected();
                _Con.SqlCmd = new SqlCommand
                {
                    Connection = _Con.SqlCon,
                    CommandType = CommandType.Text,
                    CommandText = "SELECT DISTINCT TOP (1) TrainNo " +
                    "FROM dbo.JourneyStop " +
                    "WHERE (TrainNo < '" + TrainNo + "') AND (Direction = " + Direction + ") ORDER BY TrainNo Desc"
                };
                _Con.SqlRead = _Con.SqlCmd.ExecuteReader();
                if (_Con.SqlRead.Read())
                {
                    Value = _Con.SqlRead.GetInt32(0);
                }
                _Con.Disconnected();
            }
            catch (SqlException e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }
            return Value;
        }
        public int GetStationTrainNo(int TrainNo, int Direction)
        {
            string Sort = null;
            int StationIndex = 0;
            try
            {
                if (Direction.Equals(1))
                {
                    Sort = "desc";
                }
                else if (Direction.Equals(2))
                {
                    Sort = "desc";
                }
              
                _Con = new Connection();
                _Con.Connected();
                _Con.SqlCmd = new SqlCommand
                {
                    Connection = _Con.SqlCon,
                    CommandType = CommandType.Text,
                    CommandText = "SELECT TOP (1) PERCENT LocationId FROM dbo.JourneyStop " +
                    "WHERE(TrainNo = '" + TrainNo + "') " +
                    "AND ((Status = 2) OR (Status=3)) order by [index] " + Sort + ""
                };
                _Con.SqlRead = _Con.SqlCmd.ExecuteReader();
                if (_Con.SqlRead.Read())
                {
                    StationIndex = _Con.SqlRead.GetInt32(0);
                }
                _Con.Disconnected();
            }
            catch (Exception e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }
            return StationIndex;
        }
        public string GetScheduledArrival(int TrainNo,int Direction)
        {
            string Value=null;
            string Sort = null;
            try
            {
                if (Direction.Equals(1))
                {
                    Sort = "desc";
                }
                else if (Direction.Equals(2))
                {
                    Sort = "asc";
                }
                _Con = new Connection();
                _Con.Connected();
                _Con.SqlCmd = new SqlCommand
                {
                    Connection = _Con.SqlCon,
                    CommandType = CommandType.Text,
                    CommandText = "select ArrivalTime from ActiveSchedule where TrainNo='" + TrainNo + "'order by stationindex " + Sort + ""
                };
                _Con.SqlRead = _Con.SqlCmd.ExecuteReader();
                while (_Con.SqlRead.Read())
                {
                    Value = _Con.SqlRead.GetString(0);
                }
                _Con.Disconnected();
            }
            catch (SqlException e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }
            return Value;
        }
        #endregion
        // Defining the Destructor 
        // for class ProjectServiceExtension 
        ~OpenAccessDriver()
        {

        }
    }
}
