using netspire;
using SimpleTCP;
using System;
using Len.Jakpro.Database.Rendundancy;
using System.Data.SqlClient;
using System.Collections.Generic;
using Len.Jakpro.OpenAccess.Logging;
using NLog;
using System.Data;
using System.Net.Sockets;

namespace Len.Jakpro.ClassOpenAccess
{
    /// <summary>
    /// Get All information about OA Audioserver ex : CXS Server,IPPA,PA,PID,PHP,NAC  
    /// </summary>
    public class AudioObserver : AudioServerObserver
    {
        private List<string> _ListHadwareIPPAID = new List<string>();
        private List<string> _ListHadwareNACID = new List<string>();
        private List<string> _ListHadwareCXSServerID = new List<string>();
        private List<string> _ListHadwarePHPID = new List<string>();
        private List<string> _ListHadwarePIDID = new List<string>();
        private Logger _logger = LogManager.GetCurrentClassLogger();
        private Connection _Con;
        private readonly string _IPServer;
        private readonly int _Port;
        public AudioObserver(string IP, int Port)
        {
            _IPServer = IP;
            _Port = Port;
            GetTblDynamic("OA_GetDeviceCXS", _ListHadwareCXSServerID);
            GetTblDynamic("OA_GetDeviceIPPA", _ListHadwareIPPAID);
            GetTblDynamic("OA_GetDeviceNAC", _ListHadwareNACID);            
            GetTblDynamic("OA_GetDevicePHP", _ListHadwarePHPID);
            GetTblDynamic("OA_GetDevicePID", _ListHadwarePIDID);
            var _logging = new NLogConfigurator();
            _logging.Configure();
        }
       
        public void GetTblDynamic(string QueryProcedure, List<string> _ListHadwareID)
        {
            _Con = new Connection();
            _Con.Connected();
            _Con.SqlCmd = new SqlCommand
            {
                Connection = _Con.SqlCon,
                CommandType = CommandType.StoredProcedure,
                CommandText = QueryProcedure
            };
            try
            {
                _Con.SqlRead = _Con.SqlCmd.ExecuteReader();
                while (_Con.SqlRead.Read())
                {
                    _ListHadwareID.Add(_Con.SqlRead.GetValue(0).ToString());
                }
            }
            catch (SqlException e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }
        }
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
            else if(State.Equals("HELD"))
            {
                return 6;
            }
            else
            {
                return 10;
            }
        }       
        public void SendCurrentDeviceState(string Communication)
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
        public override void onDeviceStateChange(Device device)
        {
            Device thisDevice = new Device(device);
           // thisDevice.getHealthTestStatus();
            // Server List  CXS Server     
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
                        SendCurrentDeviceState(_Con.SqlRead.GetString(1) + "," + GetDeviceState(thisDevice.getState().ToString()));
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
                        SendCurrentDeviceState(_Con.SqlRead.GetString(1) + "," + GetDeviceState(thisDevice.getState().ToString()));
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
                        SendCurrentDeviceState(_Con.SqlRead.GetString(1) + "," + GetDeviceState(thisDevice.getState().ToString()));
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
                        SendCurrentDeviceState(_Con.SqlRead.GetString(1) + "," + GetDeviceState(thisDevice.getState().ToString()));
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
                        SendCurrentDeviceState(_Con.SqlRead.GetString(1) + "," + GetDeviceState(thisDevice.getState().ToString()));
                    }
                    _Con.Disconnected();
                }
                catch (SqlException e)
                {
                    _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
                }
            }
        }
        ~AudioObserver()
        {
            
        }
    }
}
