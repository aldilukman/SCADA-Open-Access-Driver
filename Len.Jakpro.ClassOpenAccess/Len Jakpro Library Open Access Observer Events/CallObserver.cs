using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Sockets;
using Len.Jakpro.Database.Rendundancy;
using Len.Jakpro.OpenAccess.Logging;
using netspire;
using NLog;
using SimpleTCP;
namespace Len.Jakpro.ClassOpenAccess
{
    /// <summary>
    /// Get All VOIP activity status 
    /// </summary>
    public class CallObserverClass : CallControllerObserver
    {
        private readonly string _IPServer;
        private readonly int _Port;
        private Logger _logger;
        private Connection _Con;
        private List<string> _ListHadwareID = new List<string>();
        // Construct
        public CallObserverClass(string IP, int Port)
        {
            try
            {
                _logger = LogManager.GetCurrentClassLogger();
                _IPServer = IP;
                _Port = Port;
                GetTblDynamic("OA_GetDeviceVOIP", _ListHadwareID);
                var _logging = new NLogConfigurator();
                _logging.Configure();
            }
            catch (Exception e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }
        }
        public int GetStateVOIP(string State)
        {
            if (State.Equals("PROGRESS"))
            {
                return 1;
            }
            else if (State.Equals("CONNECTED"))
            {
                return 2;
            }
            else if (State.Equals("HELD"))
            {
                return 3;
            }
            else if (State.Equals("DISCONNECTED"))
            {
                return 5;
            }
            else if (State.Equals("UNKNOWN"))
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
       
     
        public override void onCallUpdate(CallInfo _CallInfo)
        {           
            try
            {
                CallInfo _Status = new CallInfo(_CallInfo);
                if (_ListHadwareID.Contains(_Status.callAPartyId.ToString()))
                {
                    _Con = new Connection();
                    _Con.Connected();
                    _Con.SqlCmd = new SqlCommand
                    {
                        Connection = _Con.SqlCon,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "OA_GetCommunicationVoIP"
                    };
                    _Con.SqlCmd.Parameters.AddWithValue("@HadwareID", _Status.callAPartyId.ToString());
                    _Con.SqlRead = _Con.SqlCmd.ExecuteReader();
                    while (_Con.SqlRead.Read())
                    {
                        SendCurrentDeviceState(_Con.SqlRead.GetString(1) + "," + GetStateVOIP(_Status.callStateText.ToString()));
                    }
                    _Con.Disconnected();
                }
                if (_ListHadwareID.Contains(_Status.callBPartyId.ToString()))
                {
                    _Con = new Connection();
                    _Con.Connected();
                    _Con.SqlCmd = new SqlCommand
                    {
                        Connection = _Con.SqlCon,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "OA_GetCommunicationVoIP"
                    };
                    _Con.SqlCmd.Parameters.AddWithValue("@HadwareID", _Status.callBPartyId.ToString());
                    _Con.SqlRead = _Con.SqlCmd.ExecuteReader();
                    while (_Con.SqlRead.Read())
                    {
                        SendCurrentDeviceState(_Con.SqlRead.GetString(1) + "," + GetStateVOIP(_Status.callStateText.ToString()));
                    }
                    _Con.Disconnected();
                }
            }
            catch (SqlException e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }
        
        }

        ~CallObserverClass()
        {

        }
    }
}
