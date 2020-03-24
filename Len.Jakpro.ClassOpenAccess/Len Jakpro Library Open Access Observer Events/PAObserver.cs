using netspire;
using System;
using SimpleTCP;
using System.Collections.Generic;
using Len.Jakpro.AddInSampleLibrary.Logging;
using Len.Jakpro.Database.Rendundancy;
using NLog;
using System.Data.SqlClient;
using System.Data;
using System.Net.Sockets;

namespace Len.Jakpro.ClassOpenAccess
{
    /// <summary>
    /// Get All information PA status 
    /// </summary>
    class PAObserverClass : PAControllerObserver
    {
        private List<string> _ListHadwareSinkID = new List<string>();
        private List<string> _ListHadwareSourceID = new List<string>();
        private string _IPServer;
        private int _Port;
        // Database Connection
        private Connection _Con;
        private Logger _logger = LogManager.GetCurrentClassLogger();
        public PAObserverClass(string IP, int Port)
        {
            _IPServer = IP;
            _Port = Port;
            // Get Configuration PA Zone
            GetTblDynamic("OA_GetDevicePASink", _ListHadwareSinkID);
            GetTblDynamic("OA_GetDevicePASource", _ListHadwareSourceID);
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
                return 1;
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
        // Sink Audio Status
        public override void onPaSinkUpdate(PaSink _Devicesink)
        {
            PaSink _GetPASink = new PaSink(_Devicesink);
            if (_ListHadwareSinkID.Contains(_GetPASink.id.ToString()))
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
                    _Con.SqlCmd.Parameters.AddWithValue("@HadwareID", _GetPASink.id);
                    _Con.SqlRead = _Con.SqlCmd.ExecuteReader();
                    while (_Con.SqlRead.Read())
                    {
                        SendCurrentDeviceState(_Con.SqlRead.GetString(1) + "," + GetCurrentStatePA(_GetPASink.healthState.ToString()));
                    }
                    _Con.Disconnected();
                }
                catch (SqlException e)
                {
                    _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
                }
            }
        }
        public override void onPaSourceUpdate(PaSource _DeviceSource)
        {
            PaSource _PASource = new PaSource(_DeviceSource);
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
                        SendCurrentDeviceState(_Con.SqlRead.GetString(1) + "," + GetCurrentStateMIC(_PASource.healthState.ToString()));
                    }
                    _Con.Disconnected();
                }
                catch (SqlException e)
                {
                    _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
                }
            }
        }
        public override void onPaZoneUpdate(PaZone paZone)
        {
            PaZone TempPAZone = new PaZone(paZone);
            if (TempPAZone.activityText == "Active")
            {
                Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss :") + "ZoneID : " + TempPAZone.id + " Active " + TempPAZone.announcementTypeText.ToString());
            }
        }
        ~PAObserverClass()
        {
            Console.WriteLine("The instance of" +
                       " PAObserverClass class Destroyed");
        }
    }
}
