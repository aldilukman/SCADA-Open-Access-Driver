using System;
using System.Timers;
using Scada.AddIn.Contracts;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Text;
using SimpleTCP;
using Scada.AddIn.Contracts.Variable;
using Len.Jakpro.AddInSampleLibrary.Subscription;
using System.Net.Sockets;
using NLog;
using System.Data.SqlClient;
using System.Data;
using Len.Jakpro.Database.Rendundancy;
using Len.Jakpro.Addins.Logging;
using Microsoft.Win32;

namespace Len.Jakpro.AddinsOpenAccess
{
    /// <summary>
    /// Addins for communication between addins and OA Driver
    /// Note :   VLD = Velodrome Station
    ///          EQU = Equestrian Station
    ///          PLM = Pulomas Station
    ///          BOS = Boulevard Selatan
    ///          BOU = Boulevard Utara
    ///          DPD = Depot Station      
    /// </summary>
    [AddInExtension(
        "AddIn OpenAccess",
        "Communicate using TCP IP and receive all status from OA Driver",
        DefaultStartMode = DefaultStartupModes.Auto)]
    public class ProjectServiceExtension : IProjectServiceExtension
    {
        private List<string> _ZoneList = new List<string>();
        // Communication data hmi`
        private static readonly string _IPaddrDefault = "127.0.0.1";
        private static readonly int _PortSendObject = 1504;
        private static readonly int _PortReceiveStatus = 1505;
        // Communication heartbeat
        private static readonly int _PortSendObjectHeartBeat = 1506;
        private static readonly int _PortReceiveStatusHeartBeat = 1507;

        private Thread _TcpListenerThread;
        private Thread _TcpListenerThreadHeartBeat;
        // Zenon Variable Control Events
        private VariableSubscription _VariableSubscriptionEvents;
        // Zenon Variable 
        private static IVariableCollection _GetZenonVariables;
        // Context Zenon Projects
        private static IProject _Context;
        //Timer Threads
        private System.Timers.Timer _ACKCommunication;
        private string _ZoneMessage;
        // Heartbeat communication
        private bool _HasValidationState = false;
        private static byte _sequenceSend = 0;
        private static byte _sequenceReceive = 0;
        // Logger
        private Logger _logger = LogManager.GetCurrentClassLogger();
        // Database
        private Connection _Con;
        // Main Server / Backup Driver OA
        private List<string> _ServerLists = new List<string>();
        private List<string> _ServerVariables = new List<string>();
        // Main CXS Server / Backup
        private static List<string> _CXSServerLists = new List<string>();
        private static List<string> _CXSServerVariables = new List<string>();
        // Get Zone PID From DB
        private static List<string> _PIDZoneVariablesVLD = new List<string>();
        private static List<string> _PIDZoneVariablesEQU = new List<string>();
        private static List<string> _PIDZoneVariablesPLM = new List<string>();
        private static List<string> _PIDZoneVariablesBOS = new List<string>();
        private static List<string> _PIDZoneVariablesBOU = new List<string>();
        private static List<string> _PIDZoneVariablesDPD = new List<string>();
        // Get Zone PA From DB
        private List<string> _PAZoneVariablesVLD = new List<string>();
        private List<string> _PAZoneVariablesEQU = new List<string>();
        private List<string> _PAZoneVariablesPLM = new List<string>();
        private List<string> _PAZoneVariablesBOS = new List<string>();
        private List<string> _PAZoneVariablesBOU = new List<string>();
        private List<string> _PAZoneVariablesDPD = new List<string>();
        // Get Zone PA Depot Area
        private List<string> _PAZoneVariablesDPDArea = new List<string>();
        // Get List VOIP
        private static List<string> _VoIPZoneVariablesVLD = new List<string>();
        private static List<string> _VoIPZoneVariablesEQU = new List<string>();
        private static List<string> _VoIPZoneVariablesPLM = new List<string>();
        private static List<string> _VoIPZoneVariablesBOS = new List<string>();
        private static List<string> _VoIPZoneVariablesBOU = new List<string>();
        private static List<string> _VoIPZoneVariablesDPD = new List<string>();
        
        private static List<string> _VoIPZoneVariablesDPDMCC = new List<string>();
        #region Database
        // Get all configuration from database
        public void GetDbConfiguration()
        {
            // Server Device
            _GetServerList("OA_ServerListConfiguration", _ServerLists, _ServerVariables);
            _GetCXSServerList("OA_CXSServerListConfiguration", _CXSServerLists, _CXSServerVariables);
            // PA Zone
           _GetQueryList("OA_GetPAZoneVLD", _PAZoneVariablesVLD);
           _GetQueryList("OA_GetPAZoneEQU", _PAZoneVariablesEQU);
           _GetQueryList("OA_GetPAZonePLM", _PAZoneVariablesPLM);
           _GetQueryList("OA_GetPAZoneBOS", _PAZoneVariablesBOS);
           _GetQueryList("OA_GetPAZoneBOU", _PAZoneVariablesBOU);
           _GetQueryList("OA_GetPAZoneDPD", _PAZoneVariablesDPD);
           _GetQueryList("OA_GetPAZoneDPDArea", _PAZoneVariablesDPDArea);
            // PID Zone
           _GetQueryList("OA_GetPIDZoneVLD", _PIDZoneVariablesVLD);
           _GetQueryList("OA_GetPIDZoneEQU", _PIDZoneVariablesEQU);
           _GetQueryList("OA_GetPIDZonePLM", _PIDZoneVariablesPLM);
           _GetQueryList("OA_GetPIDZoneBOS", _PIDZoneVariablesBOS);
           _GetQueryList("OA_GetPIDZoneBOU", _PIDZoneVariablesBOU);
            _GetQueryList("OA_GetPIDZoneDPD", _PIDZoneVariablesDPD);
            // Get VOIP
           _GetQueryList("OA_GetVoIPVariablesVLD", _VoIPZoneVariablesVLD);
           _GetQueryList("OA_GetVoIPVariablesEQU", _VoIPZoneVariablesEQU);
           _GetQueryList("OA_GetVoIPVariablesPLM", _VoIPZoneVariablesPLM);
           _GetQueryList("OA_GetVoIPVariablesBOS", _VoIPZoneVariablesBOS);
           _GetQueryList("OA_GetVoIPVariablesBOU", _VoIPZoneVariablesBOU);
           _GetQueryList("OA_GetVoIPVariablesDPD", _VoIPZoneVariablesDPD);
           _GetQueryList("OA_GetVoIPVariablesDPDMCC", _VoIPZoneVariablesDPDMCC);
        }
        private void _GetServerList(string QueryProcedure, List<string> _ServerList, List<string> _VariableServerList)
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
                    _ServerList.Add(_Con.SqlRead.GetString(0));
                    _VariableServerList.Add(_Con.SqlRead.GetString(1));
                }
            }
            catch (Exception e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }
            if (!_Con.SqlRead.IsClosed)
            {
                _Con.SqlRead.Close();
            }
        }
        private void _GetCXSServerList(string QueryProcedure, List<string> _CXSServerList, List<string> _CXSVariableServerList)
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
                    _CXSServerList.Add(_Con.SqlRead.GetString(0));
                    _CXSVariableServerList.Add(_Con.SqlRead.GetString(2));
                }
            }
            catch (Exception ex)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + ex.Message.ToString());
            }

            if (!_Con.SqlRead.IsClosed)
            {
                _Con.SqlRead.Close();
            }
        }
        private void _GetDeviceList(string QueryProcedure, List<string> _TempVariables)
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
                    _TempVariables.Add(_Con.SqlRead.GetString(2));
                }
            }
            catch (Exception ex)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + ex.Message.ToString());
            }

            if (!_Con.SqlRead.IsClosed)
            {
                _Con.SqlRead.Close();
            }
        }
        private void _GetQueryList(string QueryProcedure, List<string> _TempVariables)
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
                    _TempVariables.Add(_Con.SqlRead.GetString(3));
                }
            }
            catch (Exception ex)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + ex.Message.ToString());
            }

            if (!_Con.SqlRead.IsClosed)
            {
                _Con.SqlRead.Close();
            }
        }
        // Request Current Device Status from OA Driver to HMI Runtime  
        private void _RequestDeviceState(string Variable, string Events)
        {
            SimpleTcpClient _SendRequest;
            try
            {
                _SendRequest = new SimpleTcpClient().Connect(_IPaddrDefault, _PortSendObject);
                _SendRequest.Write(Variable + "," + Events + "");
                _SendRequest.Dispose();
                _logger.Debug(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "Request Device State");
            }
            catch (Exception e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }
        }
        #endregion

        public ProjectServiceExtension()
        {
            GetDbConfiguration();
            var _logging = new NLogConfigurator();
            _logging.Configure();
            _VariableSubscriptionEvents = new VariableSubscription(StatusVariableChanged);
        }
        // Start the add ins when the Runtime active        
        public void Start(IProject context, IBehavior behavior)
        {
            try
            {
                _Context = context;
                // enter your code which should be executed when starting the SCADA Runtime Service
                // Create new Thread TCP IP Listen            
                _TcpListenerThread = new Thread(new ThreadStart(_ListenTCP))
                {
                    IsBackground = true
                };
                _TcpListenerThread.Start();

                _TcpListenerThreadHeartBeat = new Thread(new ThreadStart(_ListenTCPCommunication))
                {
                    IsBackground = true
                };
                _TcpListenerThreadHeartBeat.Start();
                // Request ACK to server From Add Ins
                _RequestDeviceState("GetDeviceState", "AllDevice");
                // Create timer for send to HMI
                _ACKCommunication = new System.Timers.Timer(1000);
                _ACKCommunication.Elapsed += new ElapsedEventHandler(_CommunicationHeartbeat);
                _ACKCommunication.Enabled = true;
                _ACKCommunication.Start();
                // Define to Zenon Variable
                _GetZenonVariables = context.VariableCollection;
                // Button Also Status PA
                List<string> _SetVariableList = new List<string>
            {
                // Variable To Initiate Microphone PA on OCC and BCC
                "OA.Microphone.PA",
                // Variable To Initiate DVA Announcements On OCC and BCC
                "OA.DVAAnnouncements",
                // Variable To Initiate PID Text Announcements OCC
                "OA.DVATextAnnouncements",
                // Variable To Initiate Signalling OA PA
                "OA.Signalling.String"
            };
                _VariableSubscriptionEvents.Start(context, _SetVariableList);
            }
            catch (Exception Ex)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + Ex.Message.ToString());
            }

        }
        // Stop the add ins when the runtime inactive
        public void Stop()
        {
            // enter your code which should be executed when stopping the SCADA Runtime Service
            try
            {
                _TcpListenerThread.Abort();
                _ACKCommunication.Stop();
            }
            catch (Exception e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }
        }

        #region Zoning HMI Each Station
        // Add Zoning Configuration for PA and PID
        private void _VelodromeZone()
        {
            // Public Area
            if (_GetZenonVariables["OA.PA.VLD.Line.1"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.VLD.Line.1.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesVLD[0]);
            }
            if (_GetZenonVariables["OA.PA.VLD.Line.2"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.VLD.Line.2.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesVLD[1]);
            }
            if (_GetZenonVariables["OA.PA.VLD.Line.3"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.VLD.Line.3.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesVLD[2]);
            }
            if (_GetZenonVariables["OA.PA.VLD.Line.4"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.VLD.Line.4.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesVLD[3]);
            }
            // Non Public Area
            if (_GetZenonVariables["OA.NPA.VLD.Line.1"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.NPA.VLD.Line.1.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesVLD[4]);
            }
            if (_GetZenonVariables["OA.NPA.VLD.Line.2"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.NPA.VLD.Line.2.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesVLD[5]);
            }
            if (_GetZenonVariables["OA.NPA.VLD.Line.3"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.NPA.VLD.Line.3.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesVLD[6]);
            }
            if (_GetZenonVariables["OA.NPA.VLD.Line.4"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.NPA.VLD.Line.4.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesVLD[7]);
            }
            if (_GetZenonVariables["OA.NPA.VLD.Line.5"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.NPA.VLD.Line.5.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesVLD[8]);
            }
            // Platform
            if (_GetZenonVariables["OA.PF.VLD.Line.1"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PF.VLD.Line.1.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesVLD[9]);
            }
            if (_GetZenonVariables["OA.PF.VLD.Line.2"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PF.VLD.Line.2.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesVLD[10]);
            }
            if (_GetZenonVariables["OA.PF.VLD.Line.3"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PF.VLD.Line.3.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesVLD[11]);
            }
            // AFIL
            if (_GetZenonVariables["OA.AFIL.VLD.Line.1"].GetValue(0).ToString() == "1" &&
              _GetZenonVariables["OA.AFIL.VLD.Line.1.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesVLD[12]);
            }
            if (_GetZenonVariables["OA.AFIL.VLD.Line.2"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.AFIL.VLD.Line.2.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesVLD[13]);
            }
        }
        private void _EquestrianZone()
        {
            // Public Area
            if (_GetZenonVariables["OA.PA.EQU.Line.1"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.EQU.Line.1.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesEQU[0]);
            }
            if (_GetZenonVariables["OA.PA.EQU.Line.2"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.EQU.Line.2.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesEQU[1]);
            }
            if (_GetZenonVariables["OA.PA.EQU.Line.3"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.EQU.Line.3.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesEQU[2]);
            }
            if (_GetZenonVariables["OA.PA.EQU.Line.4"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.EQU.Line.4.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesEQU[3]);
            }
            if (_GetZenonVariables["OA.PA.EQU.Line.5"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.EQU.Line.5.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesEQU[4]);
            }
            // Non Public Area
            if (_GetZenonVariables["OA.NPA.EQU.Line.1"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.NPA.EQU.Line.1.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesEQU[5]);
            }
            if (_GetZenonVariables["OA.NPA.EQU.Line.2"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.NPA.EQU.Line.2.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesEQU[6]);
            }
            if (_GetZenonVariables["OA.NPA.EQU.Line.3"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.NPA.EQU.Line.3.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesEQU[7]);
            }
            if (_GetZenonVariables["OA.NPA.EQU.Line.4"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.NPA.EQU.Line.4.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesEQU[8]);
            }
            if (_GetZenonVariables["OA.NPA.EQU.Line.5"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.NPA.EQU.Line.5.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesEQU[9]);
            }
            // Platform
            if (_GetZenonVariables["OA.PF.EQU.Line.1"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PF.EQU.Line.1.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesEQU[10]);
            }
            if (_GetZenonVariables["OA.PF.EQU.Line.2"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PF.EQU.Line.2.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesEQU[11]);
            }
            // AFIL
            if (_GetZenonVariables["OA.AFIL.EQU.Line.1"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.AFIL.EQU.Line.1.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesEQU[12]);
            }
            if (_GetZenonVariables["OA.AFIL.EQU.Line.2"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.AFIL.EQU.Line.2.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesEQU[13]);
            }
        }
        private void _PulomasZone()
        {
            // Public Area
            if (_GetZenonVariables["OA.PA.PLM.Line.1"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.PLM.Line.1.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesPLM[0]);
            }
            if (_GetZenonVariables["OA.PA.PLM.Line.2"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.PLM.Line.2.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesPLM[1]);
            }
            if (_GetZenonVariables["OA.PA.PLM.Line.3"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.PLM.Line.3.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesPLM[2]);
            }
            if (_GetZenonVariables["OA.PA.PLM.Line.4"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.PLM.Line.4.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesPLM[3]);
            }
            // non Public area
            if (_GetZenonVariables["OA.NPA.PLM.Line.1"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.NPA.PLM.Line.1.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesPLM[4]);
            }
            if (_GetZenonVariables["OA.NPA.PLM.Line.2"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.NPA.PLM.Line.2.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesPLM[5]);
            }
            if (_GetZenonVariables["OA.NPA.PLM.Line.3"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.NPA.PLM.Line.3.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesPLM[6]);
            }
            if (_GetZenonVariables["OA.NPA.PLM.Line.4"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.NPA.PLM.Line.4.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesPLM[7]);
            }
            if (_GetZenonVariables["OA.NPA.PLM.Line.5"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.NPA.PLM.Line.5.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesPLM[8]);
            }
            if (_GetZenonVariables["OA.NPA.PLM.Line.6"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.NPA.PLM.Line.6.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesPLM[9]);
            }
            // platform
            if (_GetZenonVariables["OA.PF.PLM.Line.1"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PF.PLM.Line.1.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesPLM[10]);
            }
            if (_GetZenonVariables["OA.PF.PLM.Line.2"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PF.PLM.Line.2.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesPLM[11]);
            }
            // AFIL
            if (_GetZenonVariables["OA.AFIL.PLM.Line.1"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.AFIL.PLM.Line.1.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesPLM[12]);
            }
            if (_GetZenonVariables["OA.AFIL.PLM.Line.2"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.AFIL.PLM.Line.2.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesPLM[13]);
            }

        }
        private void _BOSZone()
        {
            // Public Area
            if (_GetZenonVariables["OA.PA.BOS.Line.1"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.BOS.Line.1.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesBOS[0]);
            }
            if (_GetZenonVariables["OA.PA.BOS.Line.2"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.BOS.Line.2.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesBOS[1]);
            }
            if (_GetZenonVariables["OA.PA.BOS.Line.3"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.BOS.Line.3.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesBOS[2]);
            }
            if (_GetZenonVariables["OA.PA.BOS.Line.4"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.BOS.Line.4.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesBOS[3]);
            }
            // Non Public Area
            if (_GetZenonVariables["OA.NPA.BOS.Line.1"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.NPA.BOS.Line.1.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesBOS[4]);
            }
            if (_GetZenonVariables["OA.NPA.BOS.Line.2"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.NPA.BOS.Line.2.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesBOS[5]);
            }
            if (_GetZenonVariables["OA.NPA.BOS.Line.3"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.NPA.BOS.Line.3.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesBOS[6]);
            }
            if (_GetZenonVariables["OA.NPA.BOS.Line.4"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.NPA.BOS.Line.4.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesBOS[7]);
            }
            if (_GetZenonVariables["OA.NPA.BOS.Line.5"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.NPA.BOS.Line.5.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesBOS[8]);
            }
            // Platform
            if (_GetZenonVariables["OA.PF.BOS.Line.1"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PF.BOS.Line.1.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesBOS[9]);
            }
            if (_GetZenonVariables["OA.PF.BOS.Line.2"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PF.BOS.Line.2.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesBOS[10]);
            }
            if (_GetZenonVariables["OA.PF.BOS.Line.3"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PF.BOS.Line.3.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesBOS[11]);
            }
            // AFIL
            if (_GetZenonVariables["OA.AFIL.BOS.Line.1"].GetValue(0).ToString() == "1" &&
               _GetZenonVariables["OA.AFIL.BOS.Line.1.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesBOS[12]);
            }
            if (_GetZenonVariables["OA.AFIL.BOS.Line.2"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.AFIL.BOS.Line.2.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesBOS[13]);
            }
        }
        private void _BOUZone()
        {
            // Public Area
            if (_GetZenonVariables["OA.PA.BOU.Line.1"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.BOU.Line.1.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesBOU[0]);
            }
            if (_GetZenonVariables["OA.PA.BOU.Line.2"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.BOU.Line.2.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesBOU[1]);
            }
            if (_GetZenonVariables["OA.PA.BOU.Line.3"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.BOU.Line.3.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesBOU[2]);
            }
            // Non Public Area
            if (_GetZenonVariables["OA.NPA.BOU.Line.1"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.NPA.BOU.Line.1.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesBOU[3]);
            }
            if (_GetZenonVariables["OA.NPA.BOU.Line.2"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.NPA.BOU.Line.2.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesBOU[4]);
            }
            if (_GetZenonVariables["OA.NPA.BOU.Line.3"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.NPA.BOU.Line.3.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesBOU[5]);
            }
            if (_GetZenonVariables["OA.NPA.BOU.Line.4"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.NPA.BOU.Line.4.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesBOU[6]);
            }
            if (_GetZenonVariables["OA.NPA.BOU.Line.5"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.NPA.BOU.Line.5.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesBOU[7]);
            }
            if (_GetZenonVariables["OA.NPA.BOU.Line.6"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.NPA.BOU.Line.6.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesBOU[8]);
            }
            // Platform
            if (_GetZenonVariables["OA.PF.BOU.Line.1"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PF.BOU.Line.1.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesBOU[9]);
            }
            if (_GetZenonVariables["OA.PF.BOU.Line.2"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PF.BOU.Line.2.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesBOU[10]);
            }
            if (_GetZenonVariables["OA.PF.BOU.Line.3"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PF.BOU.Line.3.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesBOU[11]);
            }
            // AFIL
            if (_GetZenonVariables["OA.AFIL.BOU.Line.1"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.AFIL.BOU.Line.1.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesBOU[12]);
            }
            if (_GetZenonVariables["OA.AFIL.BOU.Line.2"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.AFIL.BOU.Line.2.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesBOU[13]);
            }
        }
        private void _DPDZone()
        {
            // Public Area
            if (_GetZenonVariables["OA.PA.DPD.Line.1"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Line.1.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPD[0]);
            }
            if (_GetZenonVariables["OA.PA.DPD.Line.2"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Line.2.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPD[1]);
            }
            if (_GetZenonVariables["OA.PA.DPD.Line.3"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Line.3.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPD[2]);
            }
            if (_GetZenonVariables["OA.PA.DPD.Line.4"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Line.4.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPD[3]);
            }
            // Non Public Area
            if (_GetZenonVariables["OA.NPA.DPD.Line.1"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.NPA.DPD.Line.1.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPD[4]);
            }
            if (_GetZenonVariables["OA.NPA.DPD.Line.2"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.NPA.DPD.Line.2.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPD[5]);
            }
            if (_GetZenonVariables["OA.NPA.DPD.Line.3"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.NPA.DPD.Line.3.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPD[6]);
            }
            if (_GetZenonVariables["OA.NPA.DPD.Line.4"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.NPA.DPD.Line.4.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPD[7]);
            }
            // Platform
            if (_GetZenonVariables["OA.PF.DPD.Line.1"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PF.DPD.Line.1.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPD[8]);
            }
            if (_GetZenonVariables["OA.PF.DPD.Line.2"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PF.DPD.Line.2.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPD[9]);
            }
            // AFIL
            if (_GetZenonVariables["OA.AFIL.DPD.Line.1"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.AFIL.DPD.Line.1.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPD[10]);
            }
            if (_GetZenonVariables["OA.AFIL.DPD.Line.2"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.AFIL.DPD.Line.2.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPD[11]);
            }
        }
        private void _DPDAreaZone()
        {
            // TOD 1
            if (_GetZenonVariables["OA.PA.DPD.Area.Line1A.Status.1.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line1A.Status.1"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[0]);
            }
            if (_GetZenonVariables["OA.PA.DPD.Area.Line2A.Status.1.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line2A.Status.1"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[1]);
            }
            if (_GetZenonVariables["OA.PA.DPD.Area.Line3A.Status.1.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line3A.Status.1"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[2]);
            }
            if (_GetZenonVariables["OA.PA.DPD.Area.Line4A.Status.1.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line4A.Status.1"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[3]);
            }
            if (_GetZenonVariables["OA.PA.DPD.Area.Line5A.Status.1.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line5A.Status.1"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[4]);
            }
            if (_GetZenonVariables["OA.PA.DPD.Area.Line6A.Status.1.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line6A.Status.1"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[5]);
            }

            if (_GetZenonVariables["OA.PA.DPD.Area.Line1B.Status.2.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line1B.Status.2"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[6]);
            }
            if (_GetZenonVariables["OA.PA.DPD.Area.Line2B.Status.2.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line2B.Status.2"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[7]);
            }
            if (_GetZenonVariables["OA.PA.DPD.Area.Line3B.Status.2.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line3B.Status.2"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[8]);
            }
            if (_GetZenonVariables["OA.PA.DPD.Area.Line4B.Status.2.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line4B.Status.2"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[9]);
            }
            if (_GetZenonVariables["OA.PA.DPD.Area.Line5B.Status.2.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line5B.Status.2"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[10]);
            }
            if (_GetZenonVariables["OA.PA.DPD.Area.Line6B.Status.2.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line6B.Status.2"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[11]);
            }

            if (_GetZenonVariables["OA.PA.DPD.Area.Line1C.Status.3.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line1C.Status.3"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[12]);
            }
            if (_GetZenonVariables["OA.PA.DPD.Area.Line2C.Status.3.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line2C.Status.3"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[13]);
            }
            if (_GetZenonVariables["OA.PA.DPD.Area.Line3C.Status.3.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line3C.Status.3"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[14]);
            }
            if (_GetZenonVariables["OA.PA.DPD.Area.Line4C.Status.3.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line4C.Status.3"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[15]);
            }
            if (_GetZenonVariables["OA.PA.DPD.Area.Line5C.Status.3.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line5C.Status.3"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[16]);
            }
            if (_GetZenonVariables["OA.PA.DPD.Area.Line6C.Status.3.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line6C.Status.3"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[17]);
            }

            if (_GetZenonVariables["OA.PA.DPD.Area.Line1D.Status.4.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line1D.Status.4"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[18]);
            }
            if (_GetZenonVariables["OA.PA.DPD.Area.Line2D.Status.4.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line2D.Status.4"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[19]);
            }
            if (_GetZenonVariables["OA.PA.DPD.Area.Line3D.Status.4.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line3D.Status.4"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[20]);
            }
            if (_GetZenonVariables["OA.PA.DPD.Area.Line4D.Status.4.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line4D.Status.4"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[21]);
            }
            if (_GetZenonVariables["OA.PA.DPD.Area.Line5D.Status.4.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line5D.Status.4"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[22]);
            }
            if (_GetZenonVariables["OA.PA.DPD.Area.Line6D.Status.4.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line6D.Status.4"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[23]);
            }

            if (_GetZenonVariables["OA.PA.DPD.Area.Line1E.Status.5.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line1E.Status.5"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[24]);
            }
            if (_GetZenonVariables["OA.PA.DPD.Area.Line2E.Status.5.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line2E.Status.5"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[25]);
            }
            if (_GetZenonVariables["OA.PA.DPD.Area.Line3E.Status.5.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line3E.Status.5"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[26]);
            }
            if (_GetZenonVariables["OA.PA.DPD.Area.Line4E.Status.5.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line4E.Status.5"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[27]);
            }
            if (_GetZenonVariables["OA.PA.DPD.Area.Line5E.Status.5.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line5E.Status.5"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[28]);
            }
            if (_GetZenonVariables["OA.PA.DPD.Area.Line6E.Status.5.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line6E.Status.5"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[29]);
            }

            if (_GetZenonVariables["OA.PA.DPD.Area.Line1F.Status.6.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line1F.Status.6"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[30]);
            }
            if (_GetZenonVariables["OA.PA.DPD.Area.Line2F.Status.6.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line2F.Status.6"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[31]);
            }
            if (_GetZenonVariables["OA.PA.DPD.Area.Line3F.Status.6.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line3F.Status.6"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[32]);
            }
            if (_GetZenonVariables["OA.PA.DPD.Area.Line4F.Status.6.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line4F.Status.6"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[33]);
            }
            if (_GetZenonVariables["OA.PA.DPD.Area.Line5F.Status.6.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line5F.Status.6"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[34]);
            }
            if (_GetZenonVariables["OA.PA.DPD.Area.Line6F.Status.6.SW"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PA.DPD.Area.Line6F.Status.6"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PAZoneVariablesDPDArea[35]);
            }
        }
        private void _AddPIDZone()
        {
            // Councourse
            // Velodrome
            if (_GetZenonVariables["OA.PID.VLD.CouncourseA"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PID.VLD.CouncourseA.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PIDZoneVariablesVLD[0]);
            }
            if (_GetZenonVariables["OA.PID.VLD.CouncourseB"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PID.VLD.CouncourseB.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PIDZoneVariablesVLD[1]);
            }
            // Equestrian
            if (_GetZenonVariables["OA.PID.EQU.CouncourseA"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PID.EQU.CouncourseA.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PIDZoneVariablesEQU[0]);
            }
            if (_GetZenonVariables["OA.PID.EQU.CouncourseB"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PID.EQU.CouncourseB.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PIDZoneVariablesEQU[1]);
            }
            // Pulomas
            if (_GetZenonVariables["OA.PID.PLM.CouncourseA"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PID.PLM.CouncourseA.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PIDZoneVariablesPLM[0]);
            }
            if (_GetZenonVariables["OA.PID.PLM.CouncourseB"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PID.PLM.CouncourseB.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PIDZoneVariablesPLM[1]);
            }
            // Boulevard Selatan
            if (_GetZenonVariables["OA.PID.BOS.CouncourseA"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PID.BOS.CouncourseA.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PIDZoneVariablesBOS[0]);
            }
            if (_GetZenonVariables["OA.PID.BOS.CouncourseB"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PID.BOS.CouncourseB.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PIDZoneVariablesBOS[1]);
            }
            // Boulevard Utara
            if (_GetZenonVariables["OA.PID.BOU.CouncourseA"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PID.BOU.CouncourseA.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PIDZoneVariablesBOU[0]);
            }
            if (_GetZenonVariables["OA.PID.BOU.CouncourseB"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PID.BOU.CouncourseB.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PIDZoneVariablesBOU[1]);
            }
            // Depot
            if (_GetZenonVariables["OA.PID.DPD.CouncourseA"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PID.DPD.CouncourseA.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PIDZoneVariablesDPD[0]);
            }
            if (_GetZenonVariables["OA.PID.DPD.CouncourseB"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PID.DPD.CouncourseB.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PIDZoneVariablesDPD[1]);
            }

            // Platform

            // Velodrome
            if (_GetZenonVariables["OA.PID.VLD.PlatformA"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PID.VLD.PlatformA.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PIDZoneVariablesVLD[2]);
            }
            if (_GetZenonVariables["OA.PID.VLD.PlatformB"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PID.VLD.PlatformB.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PIDZoneVariablesVLD[3]);
            }
            // Equestrian
            if (_GetZenonVariables["OA.PID.EQU.PlatformA"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PID.EQU.PlatformA.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PIDZoneVariablesEQU[2]);
            }
            if (_GetZenonVariables["OA.PID.EQU.PlatformB"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PID.EQU.PlatformB.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PIDZoneVariablesEQU[3]);
            }
            // Pulomas
            if (_GetZenonVariables["OA.PID.PLM.PlatformA"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PID.PLM.PlatformA.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PIDZoneVariablesPLM[2]);
            }
            if (_GetZenonVariables["OA.PID.PLM.PlatformB"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PID.PLM.PlatformB.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PIDZoneVariablesPLM[3]);
            }
            // Boulevard Selatan
            if (_GetZenonVariables["OA.PID.BOS.PlatformA"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PID.BOS.PlatformA.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PIDZoneVariablesBOS[2]);
            }
            if (_GetZenonVariables["OA.PID.BOS.PlatformB"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PID.BOS.PlatformB.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PIDZoneVariablesBOS[3]);
            }
            // Boulevard Utara
            if (_GetZenonVariables["OA.PID.BOU.PlatformA"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PID.BOU.PlatformA.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PIDZoneVariablesBOU[2]);
            }
            if (_GetZenonVariables["OA.PID.BOU.PlatformB"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PID.BOU.PlatformB.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PIDZoneVariablesBOU[3]);
            }
            // Depot
            if (_GetZenonVariables["OA.PID.DPD.PlatformA"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PID.DPD.PlatformA.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PIDZoneVariablesDPD[2]);
            }
            if (_GetZenonVariables["OA.PID.DPD.PlatformB"].GetValue(0).ToString() == "1" &&
                _GetZenonVariables["OA.PID.DPD.PlatformB.Status"].GetValue(0).ToString() == "1")
            {
                _ZoneList.Add(_PIDZoneVariablesDPD[3]);
            }
        }
        #endregion

        // Event Handler status variable Zenon
        private void StatusVariableChanged(IEnumerable<IVariable> obj)
        {
            SimpleTcpClient _SendZone, _SendCommand, _SendEndCommand;
            //Function to Send a command Microphone to OA Driver
            if (_GetZenonVariables["OA.Microphone.PA"].GetValue(0).ToString() == "1")
            {
                _SendZone = new SimpleTcpClient().Connect(_IPaddrDefault, _PortSendObject);
                _SendCommand = new SimpleTcpClient().Connect(_IPaddrDefault, _PortSendObject);
                _ZoneList.Clear();
                // Add new Zone
                _VelodromeZone();
                _EquestrianZone();
                _PulomasZone();
                _BOSZone();
                _BOUZone();
                _DPDZone();
                _DPDAreaZone();
                int _count = 1;
                _ZoneMessage = "ZONE,";
                foreach (string _zone in _ZoneList)
                {
                    if (_count == _ZoneList.Count)
                    {
                        // Last Data
                        _ZoneMessage = _ZoneMessage + _zone;
                    }
                    else
                    {
                        // Insert New Data
                        _ZoneMessage = _ZoneMessage + _zone + ",";
                    }
                    _count++;
                }
                // Get Zone Value From HMI
                _GetZenonVariables["OA.ZoneText"].SetValue(0, _ZoneMessage);
                try
                {
                    _SendZone.Write(_ZoneMessage);
                    _SendCommand.Write("PAActive");
                    _SendZone.Dispose();
                    _SendCommand.Dispose();

                    _HasValidationState = true;
                    _logger.Debug(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "Request PA Annoucements");
                }
                catch (Exception e)
                {
                    _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
                }
            }
            else if (_GetZenonVariables["OA.Microphone.PA"].GetValue(0).ToString() == "0")
            {
                if (_HasValidationState)
                {
                    try
                    {
                        _SendEndCommand = new SimpleTcpClient().Connect(_IPaddrDefault, _PortSendObject);
                        _SendEndCommand.Write("PAInactive");
                        _SendEndCommand.Dispose();
                        _HasValidationState = false;

                        _logger.Debug(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "End PA Announcements");
                    }
                    catch (SocketException e)
                    {
                        _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
                    }
                }
            }
            // Function to Send a command PA to OA Driver
            if (_GetZenonVariables["OA.DVAAnnouncements"].GetValue(0).ToString() == "1")
            {
                SimpleTcpClient _SendDVAAudio, _SendZoneList;
                // split value
                string[] _Items = _GetZenonVariables["OA.WPF.GetValueDVAMedia"].GetValue(0).ToString().Split(',');
                // set the commands
                try
                {
                    _SendDVAAudio = new SimpleTcpClient().Connect(_IPaddrDefault, _PortSendObject);
                    _SendZoneList = new SimpleTcpClient().Connect(_IPaddrDefault, _PortSendObject);
                    _ZoneList.Clear();
                    // Add new Zone
                    _VelodromeZone();
                    _EquestrianZone();
                    _PulomasZone();
                    _BOSZone();
                    _BOUZone();
                    _DPDZone();
                    //_DPDAreaZone();
                    int _count = 1;
                    _ZoneMessage = "ZONE,";
                    foreach (string _zone in _ZoneList)
                    {
                        if (_count == _ZoneList.Count)
                        {
                            // Last Data
                            _ZoneMessage = _ZoneMessage + _zone;
                        }
                        else
                        {
                            // Insert New Data
                            _ZoneMessage = _ZoneMessage + _zone + ",";
                        }
                        _count++;
                    }
                    _SendZoneList.Write(_ZoneMessage);
                    Thread.Sleep(500);
                    _SendDVAAudio.Write("PADVAAudio," + _Items[0] + "," + _Items[1] + "");
                    _SendZoneList.Dispose();
                    _SendDVAAudio.Dispose();

                    // Get Zone Value From HMI
                    _GetZenonVariables["OA.ZoneText"].SetValue(0, _ZoneMessage);
                    _GetZenonVariables["OA.DVAAnnouncements"].SetValue(0, 0);
                    _logger.Debug(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "Request DVA Announcements");
                }
                catch (SocketException e)
                {
                    _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
                }
            }
            // Function to Send a command PID Text to OA Driver    
            if (_GetZenonVariables["OA.DVATextAnnouncements"].GetValue(0).ToString() == "1")
            {
                SimpleTcpClient _AnnText, _SelectedZone;
                Thread.Sleep(1000);
                // set the commands
                try
                {
                    _SelectedZone = new SimpleTcpClient().Connect(_IPaddrDefault, _PortSendObject);
                    _AnnText = new SimpleTcpClient().Connect(_IPaddrDefault, _PortSendObject);
                    _ZoneList.Clear();
                    _AddPIDZone();

                    int _count = 1;
                    _ZoneMessage = "ZONE,";
                    foreach (string _zone in _ZoneList)
                    {
                        if (_count == _ZoneList.Count)
                        {
                            // Last Data
                            _ZoneMessage = _ZoneMessage + _zone;
                        }
                        else
                        {
                            // Insert New Data
                            _ZoneMessage = _ZoneMessage + _zone + ",";
                        }
                        _count++;
                    }

                    // Get Value from Variable WPF 
                    string _Text = _GetZenonVariables["OA.WPF.GetValueTextPID"].GetValue(0).ToString();
                    _SelectedZone.Write(_ZoneMessage);
                    _AnnText.Write("PIDMessage," + _Text + "");
                    _SelectedZone.Dispose();
                    _AnnText.Dispose();
                    // Get Zone Value From HMI
                    _GetZenonVariables["OA.ZoneText"].SetValue(0, _ZoneMessage);
                    _GetZenonVariables["OA.DVATextAnnouncements"].SetValue(0, 0);
                    _logger.Debug(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "Request PID Text With Text :" + _Text + "");
                }
                catch (SocketException e)
                {
                    _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
                }
            }
            // Function to Send a command PA Signalling Announcements
            _AutomaticSignallingAnnouncementsEvents(
                _ServerLists[0],
                _ServerVariables[0],
                _ServerLists[1],
                _ServerVariables[1],
                "OA.Signalling.String");
        }
        // Rendundancy Function One of server initiate PA from signalling eg: Arrival,Depart,Approach
        private void _AutomaticSignallingAnnouncementsEvents(
            string _PrimaryServer,
            string _PrimaryVariables,
            string _SecondaryServer,
            string _SecondaryVariables,
            string _TriggerVariables)
        {
            SimpleTcpClient _SendSignallingPA;
            _SendSignallingPA = new SimpleTcpClient().Connect(_IPaddrDefault, _PortSendObject);
            try
            {
                if (Environment.MachineName == _PrimaryServer)
                {
                    if (_GetZenonVariables[_PrimaryVariables].GetValue(0).ToString() == "1")
                    {
                        _SendSignallingPA.Write(_GetZenonVariables[_TriggerVariables].GetValue(0).ToString());
                        _SendSignallingPA.Dispose();
                        _GetZenonVariables[_TriggerVariables].SetValue(0, "");
                    }
                }
                if (Environment.MachineName == _SecondaryServer)
                {
                    if (_GetZenonVariables[_PrimaryVariables].GetValue(0).ToString() == "0" && _GetZenonVariables[_SecondaryVariables].GetValue(0).ToString() == "1")
                    {
                        _SendSignallingPA.Write(_GetZenonVariables[_TriggerVariables].GetValue(0).ToString());
                        _SendSignallingPA.Dispose();
                        _GetZenonVariables[_TriggerVariables].SetValue(0, "");
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }
        }

        //Event Handler Communication Send Heartbeat between Addins and OA Driver
        private void _SendHeatBeat(
            string _PrimaryServer,
            string _PrimaryVariables,
            string _SecondaryServer,
            string _SecondaryVariables)
        {
            if (_sequenceReceive == _sequenceSend)
            {
                if (Environment.MachineName == _PrimaryServer)
                {
                    _GetZenonVariables[_PrimaryVariables].SetValue(0, 1);
                }
                else if (Environment.MachineName == _SecondaryServer)
                {
                    _GetZenonVariables[_SecondaryVariables].SetValue(0, 1);
                }
            }
            else
            {
                if (Environment.MachineName == _PrimaryServer)
                {
                    _GetZenonVariables[_PrimaryVariables].SetValue(0, 0);

                }
                else if (Environment.MachineName == _SecondaryServer)
                {
                    _GetZenonVariables[_SecondaryVariables].SetValue(0, 0);
                }

            }
            _sequenceSend++;
            if (_sequenceSend >= 255)
            {
                _sequenceSend = 1;
            }
            byte[] _heartBeat = new byte[5] { 0x23, 0x05, _sequenceSend, 0x24, 0x2A };
            SimpleTcpClient _client;
            try
            {
                _client = new SimpleTcpClient().Connect(_IPaddrDefault, _PortSendObjectHeartBeat);
                _client.Write(_heartBeat);
                _client.Dispose();
            }
            catch (SocketException ex)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + ex.Message.ToString());
            }
        }
        private void _CommunicationHeartbeat(object sender, ElapsedEventArgs e)
        {
            try
            {
                _SendHeatBeat(
                    _ServerLists[0],
                    _ServerVariables[0],
                    _ServerLists[1],
                    _ServerVariables[1]);
            }
            catch (Exception ex)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + ex.Message.ToString());
            }
        }
        private void _ListenTCP()
        {
            SimpleTcpServer _Server = new SimpleTcpServer
            {
                Delimiter = 0x13, //Config
                StringEncoder = Encoding.UTF8 //Config
            }; //Instantiate server.
            _Server.DataReceived += _Server_DataReceived; //Subscribe to DataRecieved event.
            IPAddress ip = IPAddress.Parse(_IPaddrDefault); //IP Address using .Parse()
            try
            {
                _Server.Start(ip, _PortReceiveStatus); //Start listening to incoming connections and data.
                _logger.Debug(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "TCP Server Server Listen..");
            }
            catch (SocketException e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }
        }
        private void _ListenTCPCommunication()
        {
            SimpleTcpServer _Server = new SimpleTcpServer
            {
                Delimiter = 0x13, //Config
                StringEncoder = Encoding.UTF8 //Config
            }; //Instantiate server.
            _Server.DataReceived += _Server_DataReceivedHeartBeat; //Subscribe to DataRecieved event.
            IPAddress ip = IPAddress.Parse(_IPaddrDefault); //IP Address using .Parse()
            try
            {
                _Server.Start(ip, _PortReceiveStatusHeartBeat); //Start listening to incoming connections and data.
                _logger.Debug(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "TCP Server Server Communication HearBeat is Listening..");
            }
            catch (SocketException e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }
        }
        //Event Handler Communication Received Heartbeat between Addins and OA Driver
        static void _Server_DataReceivedHeartBeat(object sender, Message m)
        {
            // Get All Devices State From OA Server
            string[] _SplitItems = m.MessageString.Split(',');
            byte[] _mydata;
            string data;
            _mydata = m.Data;
            data = BitConverter.ToString(_mydata);
            bool _isStartByteCorrect = _mydata[0] == 0x23;
            bool _isLengthCorrect = _mydata.Length == _mydata[1];
            bool _isAck = _mydata[3] == 0x25;
            bool _isStopByteCorrect = _mydata[_mydata.Length - 1] == 0x2A;
            bool _isCorrectSequence = _mydata[2] == _sequenceSend;
            if (_isStartByteCorrect && _isLengthCorrect && _isAck && _isStopByteCorrect && _isCorrectSequence)
            {
                _sequenceReceive = _sequenceSend;
            }
        }
        //Event Handler Data received from OA Driver
        static void _Server_DataReceived(object sender, Message m)
        {
            // Get All Devices State From OA Server
            string[] _SplitItems = m.MessageString.Split(',');
            // Get all status CXS Server
            // Format Value _SplitItems[1] is down here
            // 0 = Server Primary,
            // 1 = Server Secondary,
            if (_SplitItems[0] == "CXSServer")
            {
                if (_SplitItems[1] == "0")
                {
                    _GetZenonVariables["OA.CXS.MainServer"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "1")
                {
                    _GetZenonVariables["OA.CXS.SecondaryServer"].SetValue(0, _SplitItems[2]);
                }
            }
            // Get all status PHP, Passenger Help Point
            // Format Value _SplitItems[1] is down here
            // 0 = PHP 1 Platform 1,
            // 1 = Concourse 1,
            // 2 = PHP 2 Platform 1,
            // 3 = PHP 1 Platform 2,
            // 4 = Concourse 2,
            // 5 = PHP 2 Platform 2.
            // 1=idle,2=incoming,3=active,4=fault,5=faultcomm,6=hold
            if (_SplitItems[0] == "PHPVLD")
            {
                if (_SplitItems[1] == "0")
                {
                    _GetZenonVariables["OA.PHP.VLD.StatusCall.1"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "1")
                {
                    _GetZenonVariables["OA.PHP.VLD.StatusCall.2"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "2")
                {
                    _GetZenonVariables["OA.PHP.VLD.StatusCall.3"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "3")
                {
                    _GetZenonVariables["OA.PHP.VLD.StatusCall.4"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "4")
                {
                    _GetZenonVariables["OA.PHP.VLD.StatusCall.5"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "5")
                {
                    _GetZenonVariables["OA.PHP.VLD.StatusCall.6"].SetValue(0, _SplitItems[2]);
                }
            }
            if (_SplitItems[0] == "PHPEQU")
            {
                if (_SplitItems[1] == "0")
                {
                    _GetZenonVariables["OA.PHP.EQU.StatusCall.1"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "1")
                {
                    _GetZenonVariables["OA.PHP.EQU.StatusCall.2"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "2")
                {
                    _GetZenonVariables["OA.PHP.EQU.StatusCall.3"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "3")
                {
                    _GetZenonVariables["OA.PHP.EQU.StatusCall.4"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "4")
                {
                    _GetZenonVariables["OA.PHP.EQU.StatusCall.5"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "5")
                {
                    _GetZenonVariables["OA.PHP.EQU.StatusCall.6"].SetValue(0, _SplitItems[2]);
                }
            }
            if (_SplitItems[0] == "PHPPLM")
            {
                if (_SplitItems[1] == "0")
                {
                    _GetZenonVariables["OA.PHP.PLM.StatusCall.1"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "1")
                {
                    _GetZenonVariables["OA.PHP.PLM.StatusCall.2"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "2")
                {
                    _GetZenonVariables["OA.PHP.PLM.StatusCall.3"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "3")
                {
                    _GetZenonVariables["OA.PHP.PLM.StatusCall.4"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "4")
                {
                    _GetZenonVariables["OA.PHP.PLM.StatusCall.5"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "5")
                {
                    _GetZenonVariables["OA.PHP.PLM.StatusCall.6"].SetValue(0, _SplitItems[2]);
                }
            }
            if (_SplitItems[0] == "PHPBOS")
            {
                if (_SplitItems[1] == "0")
                {
                    _GetZenonVariables["OA.PHP.BOS.StatusCall.1"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "1")
                {
                    _GetZenonVariables["OA.PHP.BOS.StatusCall.2"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "2")
                {
                    _GetZenonVariables["OA.PHP.BOS.StatusCall.3"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "3")
                {
                    _GetZenonVariables["OA.PHP.BOS.StatusCall.4"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "4")
                {
                    _GetZenonVariables["OA.PHP.BOS.StatusCall.5"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "5")
                {
                    _GetZenonVariables["OA.PHP.BOS.StatusCall.6"].SetValue(0, _SplitItems[2]);
                }
            }
            if (_SplitItems[0] == "PHPBOU")
            {
                if (_SplitItems[1] == "0")
                {
                    _GetZenonVariables["OA.PHP.BOU.StatusCall.1"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "1")
                {
                    _GetZenonVariables["OA.PHP.BOU.StatusCall.2"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "2")
                {
                    _GetZenonVariables["OA.PHP.BOU.StatusCall.3"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "3")
                {
                    _GetZenonVariables["OA.PHP.BOU.StatusCall.4"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "4")
                {
                    _GetZenonVariables["OA.PHP.BOU.StatusCall.5"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "5")
                {
                    _GetZenonVariables["OA.PHP.BOU.StatusCall.6"].SetValue(0, _SplitItems[2]);
                }
            }
            if (_SplitItems[0] == "PHPDPD")
            {
                if (_SplitItems[1] == "0")
                {
                    _GetZenonVariables["OA.PHP.DPD.StatusCall.1"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "1")
                {
                    _GetZenonVariables["OA.PHP.DPD.StatusCall.2"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "2")
                {
                    _GetZenonVariables["OA.PHP.DPD.StatusCall.3"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "3")
                {
                    _GetZenonVariables["OA.PHP.DPD.StatusCall.4"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "4")
                {
                    _GetZenonVariables["OA.PHP.DPD.StatusCall.5"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "5")
                {
                    _GetZenonVariables["OA.PHP.DPD.StatusCall.6"].SetValue(0, _SplitItems[2]);
                }
            }
            // Get all status IPPA Device Each Station
            // Format Value _SplitItems[1] is down here
            // 0 = Velodrome,
            // 1 = Equestrian,
            // 2 = Pulomas,
            // 3 = BouSelatan,
            // 4 = BouUtara,
            // 5 = Depot
            if (_SplitItems[0] == "IPPA.Status")
            {
                if (_SplitItems[1] == "0")
                {
                    _GetZenonVariables["OA.IPPA.VLD.Status"].SetValue(0, _SplitItems[2]);
                    if (_SplitItems[2].Equals("4") || _SplitItems[2].Equals("5"))
                    {
                        _GetZenonVariables["OA.IPPA.Mic.VLD.Status"].SetValue(0, 5);
                    }
                }
                else if (_SplitItems[1] == "1")
                {
                    _GetZenonVariables["OA.IPPA.EQU.Status"].SetValue(0, _SplitItems[2]);
                    if (_SplitItems[2].Equals("4") || _SplitItems[2].Equals("5"))
                    {
                        _GetZenonVariables["OA.IPPA.Mic.EQU.Status"].SetValue(0, 5);
                    }
                }
                else if (_SplitItems[1] == "2")
                {
                    _GetZenonVariables["OA.IPPA.PLM.Status"].SetValue(0, _SplitItems[2]);
                    if (_SplitItems[2].Equals("4") || _SplitItems[2].Equals("5"))
                    {
                        _GetZenonVariables["OA.IPPA.Mic.PLM.Status"].SetValue(0, 5);
                    }
                }
                else if (_SplitItems[1] == "3")
                {
                    _GetZenonVariables["OA.IPPA.BOS.Status"].SetValue(0, _SplitItems[2]);
                    if (_SplitItems[2].Equals("4") || _SplitItems[2].Equals("5"))
                    {
                        _GetZenonVariables["OA.IPPA.Mic.BOS.Status"].SetValue(0, 5);
                    }
                }
                else if (_SplitItems[1] == "4")
                {
                    _GetZenonVariables["OA.IPPA.BOU.Status"].SetValue(0, _SplitItems[2]);
                    if (_SplitItems[2].Equals("4") || _SplitItems[2].Equals("5"))
                    {
                        _GetZenonVariables["OA.IPPA.Mic.BOU.Status"].SetValue(0, 5);
                    }
                }
                else if (_SplitItems[1] == "5")
                {
                    _GetZenonVariables["OA.IPPA.DPD.Status"].SetValue(0, _SplitItems[2]);
                    if (_SplitItems[2].Equals("4") || _SplitItems[2].Equals("5"))
                    {
                        _GetZenonVariables["OA.IPPA.Mic.DPD.Status"].SetValue(0, 5);
                    }
                }
                else if (_SplitItems[1] == "6")
                {
                    _GetZenonVariables["OA.IPPA.OCC.Status"].SetValue(0, _SplitItems[2]);
                    if (_SplitItems[2].Equals("4") || _SplitItems[2].Equals("5"))
                    {
                        _GetZenonVariables["OA.IPPA.Mic.OCC.Status"].SetValue(0, 5);
                    }
                }
                else if (_SplitItems[1] == "7")
                {
                    _GetZenonVariables["OA.IPPA.BCC.Status"].SetValue(0, _SplitItems[2]);
                    if (_SplitItems[2].Equals("4") || _SplitItems[2].Equals("5"))
                    {
                        _GetZenonVariables["OA.IPPA.Mic.BCC.Status"].SetValue(0, 5);
                    }
                }
            }
            // Get Dynamic status Microphone Device on IPPA Station
            // Format Value _SplitItems[1] is down here
            // 0 = Velodrome,
            // 1 = Equestrian,
            // 2 = Pulomas,
            // 3 = BouSelatan,
            // 4 = BouUtara,
            // 5 = Depot,
            // 6 = OCC,
            // 7 = BCC
            if (_SplitItems[0] == "IPPA.Status.Mic")
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Len.Jakpro.OpenAccess\\IPPASettings");
                object _Obj = key.GetValue("DefaultIPPAStatus");
                if (_SplitItems[1] == "0")
                {
                    _GetZenonVariables["OA.IPPA.Mic.VLD.Status"].SetValue(0, _SplitItems[2]);
                    if (_Obj.Equals("0"))
                    {
                        _GetZenonVariables["OA.IPPA.Mic.Dynamic.Status"].SetValue(0, _SplitItems[2]);
                    }
                    key.Close();
                }
                else if (_SplitItems[1] == "1")
                {
                    _GetZenonVariables["OA.IPPA.Mic.EQU.Status"].SetValue(0, _SplitItems[2]);
                    if (_Obj.Equals("1"))
                    {
                        _GetZenonVariables["OA.IPPA.Mic.Dynamic.Status"].SetValue(0, _SplitItems[2]);
                    }
                    key.Close();
                }
                else if (_SplitItems[1] == "2")
                {
                    _GetZenonVariables["OA.IPPA.Mic.PLM.Status"].SetValue(0, _SplitItems[2]);
                    if (_Obj.Equals("2"))
                    {
                        _GetZenonVariables["OA.IPPA.Mic.Dynamic.Status"].SetValue(0, _SplitItems[2]);
                    }
                    key.Close();
                }
                else if (_SplitItems[1] == "3")
                {
                    _GetZenonVariables["OA.IPPA.Mic.BOS.Status"].SetValue(0, _SplitItems[2]);
                    if (_Obj.Equals("3"))
                    {
                        _GetZenonVariables["OA.IPPA.Mic.Dynamic.Status"].SetValue(0, _SplitItems[2]);
                    }
                    key.Close();
                }
                else if (_SplitItems[1] == "4")
                {
                    _GetZenonVariables["OA.IPPA.Mic.BOU.Status"].SetValue(0, _SplitItems[2]);
                    if (_Obj.Equals("4"))
                    {
                        _GetZenonVariables["OA.IPPA.Mic.Dynamic.Status"].SetValue(0, _SplitItems[2]);
                    }
                    key.Close();
                }
                else if (_SplitItems[1] == "5")
                {
                    _GetZenonVariables["OA.IPPA.Mic.DPD.Status"].SetValue(0, _SplitItems[2]);
                    if (_Obj.Equals("5"))
                    {
                        _GetZenonVariables["OA.IPPA.Mic.Dynamic.Status"].SetValue(0, _SplitItems[2]);
                    }
                    key.Close();
                }
                else if (_SplitItems[1] == "6")
                {
                    _GetZenonVariables["OA.IPPA.Mic.OCC.Status"].SetValue(0, _SplitItems[2]);
                    if (_Obj.Equals("6"))
                    {
                        _GetZenonVariables["OA.IPPA.Mic.Dynamic.Status"].SetValue(0, _SplitItems[2]);
                    }
                    key.Close();
                }
                else if (_SplitItems[1] == "7")
                {
                    _GetZenonVariables["OA.IPPA.Mic.BCC.Status"].SetValue(0, _SplitItems[2]);
                    if (_Obj.Equals("7"))
                    {
                        _GetZenonVariables["OA.IPPA.Mic.Dynamic.Status"].SetValue(0, _SplitItems[2]);
                    }
                    key.Close();
                }
            }
            // Get all status NAC Device for all station
            // Format Value _SplitItems[1] is down here
            // 0,1 = Velodrome,
            // 2,3 = Equestrian,
            // 4,5 = Pulomas,
            // 6,7 = BouSelatan,
            // 8,9 = BouUtara,
            // 10,11 = Depot
            if (_SplitItems[0] == "NAC.Status")
            {
                if (_SplitItems[1] == "0")
                {
                    _GetZenonVariables["OA.NAC.VLD.Status.1"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "1")
                {
                    _GetZenonVariables["OA.NAC.VLD.Status.2"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "2")
                {
                    _GetZenonVariables["OA.NAC.EQU.Status.1"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "3")
                {
                    _GetZenonVariables["OA.NAC.EQU.Status.2"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "4")
                {
                    _GetZenonVariables["OA.NAC.PLM.Status.1"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "5")
                {
                    _GetZenonVariables["OA.NAC.PLM.Status.2"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "6")
                {
                    _GetZenonVariables["OA.NAC.BOS.Status.1"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "7")
                {
                    _GetZenonVariables["OA.NAC.BOS.Status.2"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "8")
                {
                    _GetZenonVariables["OA.NAC.BOU.Status.1"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "9")
                {
                    _GetZenonVariables["OA.NAC.BOU.Status.2"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "10")
                {
                    _GetZenonVariables["OA.NAC.DPD.Status.1"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "11")
                {
                    _GetZenonVariables["OA.NAC.DPD.Status.2"].SetValue(0, _SplitItems[2]);
                }
                //Depot Area
                else if (_SplitItems[1] == "12")
                {
                    _GetZenonVariables["OA.NAC.DPD.Area.Status.1"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "13")
                {
                    _GetZenonVariables["OA.NAC.DPD.Area.Status.2"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "14")
                {
                    _GetZenonVariables["OA.NAC.DPD.Area.Status.3"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "15")
                {
                    _GetZenonVariables["OA.NAC.DPD.Area.Status.4"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "16")
                {
                    _GetZenonVariables["OA.NAC.DPD.Area.Status.5"].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "17")
                {
                    _GetZenonVariables["OA.NAC.DPD.Area.Status.6"].SetValue(0, _SplitItems[2]);
                }

            }
            // Get all status VOIP Station
            if (_SplitItems[0] == "VOIP.VLD")
            {
                if (_SplitItems[1] == "0")
                {
                    _GetZenonVariables[_VoIPZoneVariablesVLD[0]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "1")
                {
                    _GetZenonVariables[_VoIPZoneVariablesVLD[1]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "2")
                {
                    _GetZenonVariables[_VoIPZoneVariablesVLD[2]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "3")
                {
                    _GetZenonVariables[_VoIPZoneVariablesVLD[3]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "4")
                {
                    _GetZenonVariables[_VoIPZoneVariablesVLD[4]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "5")
                {
                    _GetZenonVariables[_VoIPZoneVariablesVLD[5]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "6")
                {
                    _GetZenonVariables[_VoIPZoneVariablesVLD[6]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "7")
                {
                    _GetZenonVariables[_VoIPZoneVariablesVLD[7]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "8")
                {
                    _GetZenonVariables[_VoIPZoneVariablesVLD[8]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "9")
                {
                    _GetZenonVariables[_VoIPZoneVariablesVLD[9]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "10")
                {
                    _GetZenonVariables[_VoIPZoneVariablesVLD[10]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "11")
                {
                    _GetZenonVariables[_VoIPZoneVariablesVLD[11]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "12")
                {
                    _GetZenonVariables[_VoIPZoneVariablesVLD[12]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "13")
                {
                    _GetZenonVariables[_VoIPZoneVariablesVLD[13]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "14")
                {
                    _GetZenonVariables[_VoIPZoneVariablesVLD[14]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "15")
                {
                    _GetZenonVariables[_VoIPZoneVariablesVLD[15]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "16")
                {
                    _GetZenonVariables[_VoIPZoneVariablesVLD[16]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "17")
                {
                    _GetZenonVariables[_VoIPZoneVariablesVLD[17]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "18")
                {
                    _GetZenonVariables[_VoIPZoneVariablesVLD[18]].SetValue(0, _SplitItems[2]);
                }
            }
            if (_SplitItems[0] == "VOIP.EQU")
            {
                if (_SplitItems[1] == "0")
                {
                    _GetZenonVariables[_VoIPZoneVariablesEQU[0]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "1")
                {
                    _GetZenonVariables[_VoIPZoneVariablesEQU[1]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "2")
                {
                    _GetZenonVariables[_VoIPZoneVariablesEQU[2]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "3")
                {
                    _GetZenonVariables[_VoIPZoneVariablesEQU[3]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "4")
                {
                    _GetZenonVariables[_VoIPZoneVariablesEQU[4]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "5")
                {
                    _GetZenonVariables[_VoIPZoneVariablesEQU[5]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "6")
                {
                    _GetZenonVariables[_VoIPZoneVariablesEQU[6]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "7")
                {
                    _GetZenonVariables[_VoIPZoneVariablesEQU[7]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "8")
                {
                    _GetZenonVariables[_VoIPZoneVariablesEQU[8]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "9")
                {
                    _GetZenonVariables[_VoIPZoneVariablesEQU[9]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "10")
                {
                    _GetZenonVariables[_VoIPZoneVariablesEQU[10]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "11")
                {
                    _GetZenonVariables[_VoIPZoneVariablesEQU[11]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "12")
                {
                    _GetZenonVariables[_VoIPZoneVariablesEQU[12]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "13")
                {
                    _GetZenonVariables[_VoIPZoneVariablesEQU[13]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "14")
                {
                    _GetZenonVariables[_VoIPZoneVariablesEQU[14]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "15")
                {
                    _GetZenonVariables[_VoIPZoneVariablesEQU[15]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "16")
                {
                    _GetZenonVariables[_VoIPZoneVariablesEQU[16]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "17")
                {
                    _GetZenonVariables[_VoIPZoneVariablesEQU[17]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "18")
                {
                    _GetZenonVariables[_VoIPZoneVariablesEQU[18]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "19")
                {
                    _GetZenonVariables[_VoIPZoneVariablesEQU[19]].SetValue(0, _SplitItems[2]);
                }
            }
            if (_SplitItems[0] == "VOIP.PLM")
            {
                if (_SplitItems[1] == "0")
                {
                    _GetZenonVariables[_VoIPZoneVariablesPLM[0]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "1")
                {
                    _GetZenonVariables[_VoIPZoneVariablesPLM[1]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "2")
                {
                    _GetZenonVariables[_VoIPZoneVariablesPLM[2]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "3")
                {
                    _GetZenonVariables[_VoIPZoneVariablesPLM[3]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "4")
                {
                    _GetZenonVariables[_VoIPZoneVariablesPLM[4]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "5")
                {
                    _GetZenonVariables[_VoIPZoneVariablesPLM[5]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "6")
                {
                    _GetZenonVariables[_VoIPZoneVariablesPLM[6]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "7")
                {
                    _GetZenonVariables[_VoIPZoneVariablesPLM[7]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "8")
                {
                    _GetZenonVariables[_VoIPZoneVariablesPLM[8]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "9")
                {
                    _GetZenonVariables[_VoIPZoneVariablesPLM[9]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "10")
                {
                    _GetZenonVariables[_VoIPZoneVariablesPLM[10]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "11")
                {
                    _GetZenonVariables[_VoIPZoneVariablesPLM[11]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "12")
                {
                    _GetZenonVariables[_VoIPZoneVariablesPLM[12]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "13")
                {
                    _GetZenonVariables[_VoIPZoneVariablesPLM[13]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "14")
                {
                    _GetZenonVariables[_VoIPZoneVariablesPLM[14]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "15")
                {
                    _GetZenonVariables[_VoIPZoneVariablesPLM[15]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "16")
                {
                    _GetZenonVariables[_VoIPZoneVariablesPLM[16]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "17")
                {
                    _GetZenonVariables[_VoIPZoneVariablesPLM[17]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "18")
                {
                    _GetZenonVariables[_VoIPZoneVariablesPLM[18]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "19")
                {
                    _GetZenonVariables[_VoIPZoneVariablesPLM[19]].SetValue(0, _SplitItems[2]);
                }
            }
            if (_SplitItems[0] == "VOIP.BOS")
            {
                if (_SplitItems[1] == "0")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOS[0]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "1")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOS[1]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "2")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOS[2]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "3")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOS[3]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "4")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOS[4]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "5")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOS[5]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "6")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOS[6]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "7")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOS[7]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "8")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOS[8]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "9")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOS[9]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "10")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOS[10]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "11")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOS[11]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "12")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOS[12]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "13")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOS[13]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "14")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOS[14]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "15")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOS[15]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "16")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOS[16]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "17")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOS[17]].SetValue(0, _SplitItems[2]);
                }

            }
            if (_SplitItems[0] == "VOIP.BOU")
            {
                if (_SplitItems[1] == "0")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOU[0]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "1")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOU[1]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "2")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOU[2]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "3")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOU[3]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "4")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOU[4]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "5")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOU[5]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "6")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOU[6]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "7")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOU[7]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "8")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOU[8]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "9")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOU[9]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "10")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOU[10]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "11")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOU[11]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "12")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOU[12]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "13")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOU[13]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "14")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOU[14]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "15")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOU[15]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "16")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOU[16]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "17")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOU[17]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "18")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOU[18]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "19")
                {
                    _GetZenonVariables[_VoIPZoneVariablesBOU[19]].SetValue(0, _SplitItems[2]);
                }
            }
            if (_SplitItems[0] == "VOIP.DPD")
            {
                if (_SplitItems[1] == "0")
                {
                    _GetZenonVariables[_VoIPZoneVariablesDPD[0]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "1")
                {
                    _GetZenonVariables[_VoIPZoneVariablesDPD[1]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "2")
                {
                    _GetZenonVariables[_VoIPZoneVariablesDPD[2]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "3")
                {
                    _GetZenonVariables[_VoIPZoneVariablesDPD[3]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "4")
                {
                    _GetZenonVariables[_VoIPZoneVariablesDPD[4]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "5")
                {
                    _GetZenonVariables[_VoIPZoneVariablesDPD[5]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "6")
                {
                    _GetZenonVariables[_VoIPZoneVariablesDPD[6]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "7")
                {
                    _GetZenonVariables[_VoIPZoneVariablesDPD[7]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "8")
                {
                    _GetZenonVariables[_VoIPZoneVariablesDPD[8]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "9")
                {
                    _GetZenonVariables[_VoIPZoneVariablesDPD[9]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "10")
                {
                    _GetZenonVariables[_VoIPZoneVariablesDPD[10]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "11")
                {
                    _GetZenonVariables[_VoIPZoneVariablesDPD[11]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "12")
                {
                    _GetZenonVariables[_VoIPZoneVariablesDPD[12]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "13")
                {
                    _GetZenonVariables[_VoIPZoneVariablesDPD[13]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "14")
                {
                    _GetZenonVariables[_VoIPZoneVariablesDPD[14]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "15")
                {
                    _GetZenonVariables[_VoIPZoneVariablesDPD[15]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "16")
                {
                    _GetZenonVariables[_VoIPZoneVariablesDPD[16]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "17")
                {
                    _GetZenonVariables[_VoIPZoneVariablesDPD[17]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "18")
                {
                    _GetZenonVariables[_VoIPZoneVariablesDPD[18]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "19")
                {
                    _GetZenonVariables[_VoIPZoneVariablesDPD[19]].SetValue(0, _SplitItems[2]);
                }
                else if (_SplitItems[1] == "20")
                {
                    _GetZenonVariables[_VoIPZoneVariablesDPD[20]].SetValue(0, _SplitItems[2]);
                }
            }

            // Get all status VOIP Depot Area / MCC / APSS
            if (_SplitItems[0] == "VOIP.DPD.A")
            {
                for (int i = 0; i < 110; i++)
                {
                    if (_SplitItems[1] == i.ToString())
                    {
                        try
                        {
                            _GetZenonVariables[_VoIPZoneVariablesDPDMCC[i]].SetValue(0, _SplitItems[2]);
                        }
                        catch(Exception e)
                        {
                            System.Windows.Forms.MessageBox.Show(e.Message);                        }
                    }
                }
            }

            // Get all status PID Station
            // Format Value _SplitItems[1] is down here
            // 0,1 = Velodrome,
            // 2,3 = Equestrian,
            // 4,5 = Pulomas,
            // 6,7 = BouSelatan,
            // 8,9 = BouUtara,
            // 10,11 = Depot
            if (_SplitItems[0] == "PID.CNC")
            {
                // Station List
                if (_SplitItems[1] == "0")
                {
                    _GetZenonVariables["OA.PID.VLD.CouncourseA.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "1")
                {
                    _GetZenonVariables["OA.PID.VLD.CouncourseB.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "2")
                {
                    _GetZenonVariables["OA.PID.EQU.CouncourseA.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "3")
                {
                    _GetZenonVariables["OA.PID.EQU.CouncourseB.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "4")
                {
                    _GetZenonVariables["OA.PID.PLM.CouncourseA.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "5")
                {
                    _GetZenonVariables["OA.PID.PLM.CouncourseB.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "6")
                {
                    _GetZenonVariables["OA.PID.BOS.CouncourseA.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "7")
                {
                    _GetZenonVariables["OA.PID.BOS.CouncourseB.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "8")
                {
                    _GetZenonVariables["OA.PID.BOU.CouncourseA.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "9")
                {
                    _GetZenonVariables["OA.PID.BOU.CouncourseB.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "10")
                {
                    _GetZenonVariables["OA.PID.DPD.CouncourseA.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "11")
                {
                    _GetZenonVariables["OA.PID.DPD.CouncourseB.Status"].SetValue(0, _SplitItems[2]);
                }
            }
            if (_SplitItems[0] == "PID.PFM")
            {
                // Station List
                if (_SplitItems[1] == "0")
                {
                    _GetZenonVariables["OA.PID.VLD.PlatformA.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "1")
                {
                    _GetZenonVariables["OA.PID.VLD.PlatformB.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "2")
                {
                    _GetZenonVariables["OA.PID.EQU.PlatformA.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "3")
                {
                    _GetZenonVariables["OA.PID.EQU.PlatformB.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "4")
                {
                    _GetZenonVariables["OA.PID.PLM.PlatformA.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "5")
                {
                    _GetZenonVariables["OA.PID.PLM.PlatformB.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "6")
                {
                    _GetZenonVariables["OA.PID.BOS.PlatformA.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "7")
                {
                    _GetZenonVariables["OA.PID.BOS.PlatformB.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "8")
                {
                    _GetZenonVariables["OA.PID.BOU.PlatformA.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "9")
                {
                    _GetZenonVariables["OA.PID.BOU.PlatformB.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "10")
                {
                    _GetZenonVariables["OA.PID.DPD.PlatformA.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "11")
                {
                    _GetZenonVariables["OA.PID.DPD.PlatformB.Status"].SetValue(0, _SplitItems[2]);
                }
            }
            // Get all status PA Station
            if (_SplitItems[0] == "PA.PA")
            {
                // Velodrome
                if (_SplitItems[1] == "0")
                {
                    _GetZenonVariables["OA.PA.VLD.Line.1.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "1")
                {
                    _GetZenonVariables["OA.PA.VLD.Line.2.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "2")
                {
                    _GetZenonVariables["OA.PA.VLD.Line.3.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "3")
                {
                    _GetZenonVariables["OA.PA.VLD.Line.4.Status"].SetValue(0, _SplitItems[2]);
                }
                // Equestrian
                if (_SplitItems[1] == "4")
                {
                    _GetZenonVariables["OA.PA.EQU.Line.1.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "5")
                {
                    _GetZenonVariables["OA.PA.EQU.Line.2.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "6")
                {
                    _GetZenonVariables["OA.PA.EQU.Line.3.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "7")
                {
                    _GetZenonVariables["OA.PA.EQU.Line.4.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "8")
                {
                    _GetZenonVariables["OA.PA.EQU.Line.5.Status"].SetValue(0, _SplitItems[2]);
                }
                // Pulomas
                if (_SplitItems[1] == "9")
                {
                    _GetZenonVariables["OA.PA.PLM.Line.1.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "10")
                {
                    _GetZenonVariables["OA.PA.PLM.Line.2.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "11")
                {
                    _GetZenonVariables["OA.PA.PLM.Line.3.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "12")
                {
                    _GetZenonVariables["OA.PA.PLM.Line.4.Status"].SetValue(0, _SplitItems[2]);
                }
                // Boulevard Selatan
                if (_SplitItems[1] == "13")
                {
                    _GetZenonVariables["OA.PA.BOS.Line.1.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "14")
                {
                    _GetZenonVariables["OA.PA.BOS.Line.2.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "15")
                {
                    _GetZenonVariables["OA.PA.BOS.Line.3.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "16")
                {
                    _GetZenonVariables["OA.PA.BOS.Line.4.Status"].SetValue(0, _SplitItems[2]);
                }
                // Boulevard Utara
                if (_SplitItems[1] == "17")
                {
                    _GetZenonVariables["OA.PA.BOU.Line.1.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "18")
                {
                    _GetZenonVariables["OA.PA.BOU.Line.2.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "19")
                {
                    _GetZenonVariables["OA.PA.BOU.Line.3.Status"].SetValue(0, _SplitItems[2]);
                }
                // Depot
                if (_SplitItems[1] == "20")
                {
                    _GetZenonVariables["OA.PA.DPD.Line.1.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "21")
                {
                    _GetZenonVariables["OA.PA.DPD.Line.2.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "22")
                {
                    _GetZenonVariables["OA.PA.DPD.Line.3.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "23")
                {
                    _GetZenonVariables["OA.PA.DPD.Line.4.Status"].SetValue(0, _SplitItems[2]);
                }
                // Depot Area
                if (_SplitItems[1] == "24")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line1A.Status.1"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "25")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line2A.Status.1"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "26")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line3A.Status.1"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "27")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line4A.Status.1"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "28")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line5A.Status.1"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "29")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line6A.Status.1"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "30")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line1B.Status.2"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "31")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line2B.Status.2"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "32")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line3B.Status.2"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "33")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line4B.Status.2"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "34")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line5B.Status.2"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "35")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line6B.Status.2"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "36")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line1C.Status.3"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "37")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line2C.Status.3"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "38")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line3C.Status.3"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "39")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line4C.Status.3"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "40")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line5C.Status.3"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "41")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line6C.Status.3"].SetValue(0, _SplitItems[2]);
                }

                if (_SplitItems[1] == "42")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line1D.Status.4"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "43")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line2D.Status.4"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "44")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line3D.Status.4"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "45")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line4D.Status.4"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "46")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line5D.Status.4"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "47")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line6D.Status.4"].SetValue(0, _SplitItems[2]);
                }

                if (_SplitItems[1] == "48")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line1E.Status.5"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "49")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line2E.Status.5"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "50")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line3E.Status.5"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "51")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line4E.Status.5"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "52")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line5E.Status.5"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "53")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line6E.Status.5"].SetValue(0, _SplitItems[2]);
                }

                if (_SplitItems[1] == "54")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line1F.Status.6"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "55")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line2F.Status.6"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "56")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line3F.Status.6"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "57")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line4F.Status.6"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "58")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line5F.Status.6"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "59")
                {
                    _GetZenonVariables["OA.PA.DPD.Area.Line6F.Status.6"].SetValue(0, _SplitItems[2]);
                }
            }
            if (_SplitItems[0] == "PA.NPA")
            {
                // Velodrome
                if (_SplitItems[1] == "0")
                {
                    _GetZenonVariables["OA.NPA.VLD.Line.1.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "1")
                {
                    _GetZenonVariables["OA.NPA.VLD.Line.2.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "2")
                {
                    _GetZenonVariables["OA.NPA.VLD.Line.3.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "3")
                {
                    _GetZenonVariables["OA.NPA.VLD.Line.4.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "4")
                {
                    _GetZenonVariables["OA.NPA.VLD.Line.5.Status"].SetValue(0, _SplitItems[2]);
                }

                // Equestrian
                if (_SplitItems[1] == "5")
                {
                    _GetZenonVariables["OA.NPA.EQU.Line.1.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "6")
                {
                    _GetZenonVariables["OA.NPA.EQU.Line.2.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "7")
                {
                    _GetZenonVariables["OA.NPA.EQU.Line.3.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "8")
                {
                    _GetZenonVariables["OA.NPA.EQU.Line.4.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "9")
                {
                    _GetZenonVariables["OA.NPA.EQU.Line.5.Status"].SetValue(0, _SplitItems[2]);
                }

                //Pulomas
                if (_SplitItems[1] == "10")
                {
                    _GetZenonVariables["OA.NPA.PLM.Line.1.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "11")
                {
                    _GetZenonVariables["OA.NPA.PLM.Line.2.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "12")
                {
                    _GetZenonVariables["OA.NPA.PLM.Line.3.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "13")
                {
                    _GetZenonVariables["OA.NPA.PLM.Line.4.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "14")
                {
                    _GetZenonVariables["OA.NPA.PLM.Line.5.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "15")
                {
                    _GetZenonVariables["OA.NPA.PLM.Line.6.Status"].SetValue(0, _SplitItems[2]);
                }

                //Boulevard Selatan
                if (_SplitItems[1] == "16")
                {
                    _GetZenonVariables["OA.NPA.BOS.Line.1.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "17")
                {
                    _GetZenonVariables["OA.NPA.BOS.Line.2.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "18")
                {
                    _GetZenonVariables["OA.NPA.BOS.Line.3.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "19")
                {
                    _GetZenonVariables["OA.NPA.BOS.Line.4.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "20")
                {
                    _GetZenonVariables["OA.NPA.BOS.Line.5.Status"].SetValue(0, _SplitItems[2]);
                }

                //Boulevard Utara
                if (_SplitItems[1] == "21")
                {
                    _GetZenonVariables["OA.NPA.BOU.Line.1.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "22")
                {
                    _GetZenonVariables["OA.NPA.BOU.Line.2.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "23")
                {
                    _GetZenonVariables["OA.NPA.BOU.Line.3.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "24")
                {
                    _GetZenonVariables["OA.NPA.BOU.Line.4.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "25")
                {
                    _GetZenonVariables["OA.NPA.BOU.Line.5.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "26")
                {
                    _GetZenonVariables["OA.NPA.BOU.Line.6.Status"].SetValue(0, _SplitItems[2]);
                }

                //Depot
                if (_SplitItems[1] == "27")
                {
                    _GetZenonVariables["OA.NPA.DPD.Line.1.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "28")
                {
                    _GetZenonVariables["OA.NPA.DPD.Line.2.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "29")
                {
                    _GetZenonVariables["OA.NPA.DPD.Line.3.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "30")
                {
                    _GetZenonVariables["OA.NPA.DPD.Line.4.Status"].SetValue(0, _SplitItems[2]);
                }
            }
            if (_SplitItems[0] == "PA.PFM")
            {
                // Velodrome
                if (_SplitItems[1] == "0")
                {
                    _GetZenonVariables["OA.PF.VLD.Line.1.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "1")
                {
                    _GetZenonVariables["OA.PF.VLD.Line.2.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "2")
                {
                    _GetZenonVariables["OA.PF.VLD.Line.3.Status"].SetValue(0, _SplitItems[2]);
                }
                // Equestrian
                if (_SplitItems[1] == "3")
                {
                    _GetZenonVariables["OA.PF.EQU.Line.1.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "4")
                {
                    _GetZenonVariables["OA.PF.EQU.Line.2.Status"].SetValue(0, _SplitItems[2]);
                }
                // Pulomas
                if (_SplitItems[1] == "5")
                {
                    _GetZenonVariables["OA.PF.PLM.Line.1.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "6")
                {
                    _GetZenonVariables["OA.PF.PLM.Line.2.Status"].SetValue(0, _SplitItems[2]);
                }
                // Boulevard Selatan
                if (_SplitItems[1] == "7")
                {
                    _GetZenonVariables["OA.PF.BOS.Line.1.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "8")
                {
                    _GetZenonVariables["OA.PF.BOS.Line.2.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "9")
                {
                    _GetZenonVariables["OA.PF.BOS.Line.3.Status"].SetValue(0, _SplitItems[2]);
                }
                // Boulevard Utara
                if (_SplitItems[1] == "10")
                {
                    _GetZenonVariables["OA.PF.BOU.Line.1.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "11")
                {
                    _GetZenonVariables["OA.PF.BOU.Line.2.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "12")
                {
                    _GetZenonVariables["OA.PF.BOU.Line.3.Status"].SetValue(0, _SplitItems[2]);
                }
                // Depot
                if (_SplitItems[1] == "13")
                {
                    _GetZenonVariables["OA.PF.DPD.Line.1.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "14")
                {
                    _GetZenonVariables["OA.PF.DPD.Line.2.Status"].SetValue(0, _SplitItems[2]);
                }
            }
            // Get all status PA AFIL Station
            // Format Value _SplitItems[1] is down here
            // 0,1 = Velodrome,
            // 2,3 = Equestrian,
            // 4,5 = Pulomas,
            // 6,7 = BouSelatan,
            // 8,9 = BouUtara,
            // 10,11 = Depot
            if (_SplitItems[0] == "PA.AFIL")
            {
                // Velodrome
                if (_SplitItems[1] == "0")
                {
                    _GetZenonVariables["OA.AFIL.VLD.Line.1.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "1")
                {
                    _GetZenonVariables["OA.AFIL.VLD.Line.2.Status"].SetValue(0, _SplitItems[2]);
                }
                // Equestrian
                if (_SplitItems[1] == "2")
                {
                    _GetZenonVariables["OA.AFIL.EQU.Line.1.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "3")
                {
                    _GetZenonVariables["OA.AFIL.EQU.Line.2.Status"].SetValue(0, _SplitItems[2]);
                }
                // Pulomas
                if (_SplitItems[1] == "4")
                {
                    _GetZenonVariables["OA.AFIL.PLM.Line.1.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "5")
                {
                    _GetZenonVariables["OA.AFIL.PLM.Line.2.Status"].SetValue(0, _SplitItems[2]);
                }
                // Boulevard Selatan
                if (_SplitItems[1] == "6")
                {
                    _GetZenonVariables["OA.AFIL.BOS.Line.1.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "7")
                {
                    _GetZenonVariables["OA.AFIL.BOS.Line.2.Status"].SetValue(0, _SplitItems[2]);
                }
                // Boulevard Utara
                if (_SplitItems[1] == "8")
                {
                    _GetZenonVariables["OA.AFIL.BOU.Line.1.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "9")
                {
                    _GetZenonVariables["OA.AFIL.BOU.Line.2.Status"].SetValue(0, _SplitItems[2]);
                }
                // Depot
                if (_SplitItems[1] == "10")
                {
                    _GetZenonVariables["OA.AFIL.DPD.Line.1.Status"].SetValue(0, _SplitItems[2]);
                }
                if (_SplitItems[1] == "11")
                {
                    _GetZenonVariables["OA.AFIL.DPD.Line.2.Status"].SetValue(0, _SplitItems[2]);
                }

            }            
        }        
        // Defining the Destructor 
        // for class ProjectServiceExtension 
        ~ProjectServiceExtension()
        {
            Console.WriteLine("The instance of" +
                       " ProjectServiceExtension class Destroyed");
        }
    }
}