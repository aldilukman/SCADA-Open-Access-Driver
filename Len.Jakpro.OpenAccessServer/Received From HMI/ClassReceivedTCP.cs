using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using Len.Jakpro.ClassOpenAccess;
using SimpleTCP;
using System.Data.SqlClient;
using System.Data;
using Len.Jakpro.OA.Logging;
using NLog;
using Len.Jakpro.Database.Rendundancy;
using Microsoft.Win32;
using Len.Jakpro.ClassOpenAccess.Model_Train;
using System.Globalization;
using System.Net.Sockets;

namespace Len.Jakpro.OpenAccessServer
{
    class ClassReceivedTCP
    {
        private bool[] westArvPos = new bool[8];
        private bool[] eastArvPos = new bool[8];
        private bool[] westDepFrmPos = new bool[8];
        private bool[] eastDepFrmPos = new bool[8];

        private int[,] ScreenPID = new int[8, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
        private List<int> listWestPIDLine2 = new List<int>();
        private List<int> listEastPIDLine1 = new List<int>();
        private List<int> listEastPIDLine2 = new List<int>();

        private List<string> _ZoneList = new List<string>();
        private List<string> _ZoneListPA = new List<string>();
        private readonly List<uint> _PreDVA = new List<uint>(new uint[] { 99200 }); // Pre DVA
        private OpenAccessDriver _OA;
        private string _Variable;
        private string _Data;
        private string _ZoneData;
        private Connection _Con;
        private Thread _Listener;
        private Thread _ListenerData;
        private IPAddress _IPAddr = null;
        private int _Ports = 0;
        private int _PortsComm = 0;
        private Logger _logger = LogManager.GetCurrentClassLogger();
        public ClassReceivedTCP(OpenAccessDriver OA)
        {
            int i;
            for (i = 0; i <= 7; i++)
            {
                westArvPos[i] = false;
                eastArvPos[i] = false;
                westDepFrmPos[i] = false;
                eastDepFrmPos[i] = false;
            }
            _OA = OA;
            try
            {
                OA.ConnectAudioServer();
                _OA.SetSignallingPID(7, "1", "1", 0);
                _OA.SetSignallingPID(7, "1", "2", 0);
                _OA.SetSignallingPID(7, "2", "1", 0);
                _OA.SetSignallingPID(7, "2", "2", 0);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
        }
       
        // Listen for data incoming from HMI
        public void StartListen(IPAddress ListenIP, int Port)
        {
            var _logging = new NLogConfigurator();
            _logging.Configure();

            _IPAddr = ListenIP;
            _Ports = Port;
            // create new thread
            _Listener = new Thread(new ThreadStart(ListenTCP))
            {
                IsBackground = true
            };
            _Listener.Start();
        }
        public void StartListenCommunication(IPAddress ListenIP, int Port)
        {
            _IPAddr = ListenIP;
            _PortsComm = Port;
            // create new thread
            _ListenerData = new Thread(new ThreadStart(ListenTCPCommunication))
            {
                IsBackground = true
            };
            _ListenerData.Start();
        }
        // a method for listen TCP
        public void ListenTCP()
        {
            SimpleTcpServer _Server = new SimpleTcpServer
            {
                Delimiter = 0x13, //Config
                StringEncoder = Encoding.UTF8 //Config
            }; //Instantiate server.
            try
            {
                _Server.DataReceived += Server_DataReceived; //Subscribe to DataRecieved event.
                _Server.Start(_IPAddr, _Ports); //Start listening to incoming connections and data.
                Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss :") + "Listen TCP ON");
            }
            catch (SocketException e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }
        }
        // Event handler for data received
        public void ListenTCPCommunication()
        {
            SimpleTcpServer _Server = new SimpleTcpServer
            {
                Delimiter = 0x13, //Config
                StringEncoder = Encoding.UTF8 //Config
            }; //Instantiate server.
            try
            {
                _Server.DataReceived += Server_DataReceivedHeartbeat; //Subscribe to DataRecieved event.
                _Server.Start(_IPAddr, _PortsComm); //Start listening to incoming connections and data.
                Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss :") + "Listen TCP Communication Heartbeat ON");
            }
            catch (SocketException e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            }
        }
        public void Server_DataReceived(object sender, Message m)
        {
            string[] _SplitItems = m.MessageString.Split(',');
            // Added Zoning For DVA,PA,PID            
            if (_SplitItems[0] == "ZONE")
            {
                try
                {
                    _ZoneList.Clear();
                    _ZoneListPA.Clear();
                    foreach (string zone in _SplitItems)
                    {
                        _ZoneList.Add(zone);
                        _ZoneListPA.Add(zone);
                    }
                    // Remove format STNZONE
                    _ZoneList.RemoveAt(0);
                    _ZoneListPA.RemoveAt(0);
                    // Get Zone Data Received
                    foreach (string zone in _ZoneList)
                    {
                        Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss :") + "Zone Data Received :" + zone);
                    }
                }
                catch (SocketException e)
                {
                    _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
                }
            }
            // Live Announcements from Commands HMI
            if (_SplitItems[0] == "PAActive")
            {
                try
                {
                    RegistryKey _key = Registry.CurrentUser.OpenSubKey("Software\\Len.Jakpro.OpenAccess\\IPPASettings");
                    object _ObjMic = _key.GetValue("DefaultMicrophone");
                    int _Mic = Convert.ToInt32(_ObjMic);
                    _OA.GetPreDVALiveAnnouncements(_ZoneListPA, _PreDVA);
                    _OA.GetLiveAnnouncement(_Mic, _ZoneListPA);
                    _key.Close();
                    Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "Microphone Registered with ID:" + _Mic);
                }
                catch (Exception e)
                {
                    _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
                }
            }
            if (_SplitItems[0] == "PAInactive")
            {
                try
                {
                    _OA.GetDisconnectAnnoucement();
                }
                catch (Exception e)
                {
                    _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
                }
            }
            // PID Message from commands HMI
            if (_SplitItems[0] == "PIDMessage")
            {
                try
                {
                    _OA.SetPIDText(_ZoneList, _SplitItems[1]);
                }
                catch (Exception e)
                {
                    _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
                }
            }
            // Initiate PA DVA Audio From HMI
            if (_SplitItems[0] == "PADVAAudio")
            {
                try
                {
                    List<uint> DVA = new List<uint>(new uint[] {
                        uint.Parse(_SplitItems[1]),
                        uint.Parse(_SplitItems[2]) }); // Pre DVA
                    _OA.GetPlayDVA(_ZoneListPA, DVA);
                }
                catch (Exception e)
                {
                    _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
                }
            }
            // Initiate DVA PA Signalling
            if (_SplitItems[0] == "PASignalling")
            {
                Console.WriteLine("=====================================================================================================================");
                List<string> _Zonestation = new List<string>();
                List<uint> _DVAApproach = new List<uint>(new uint[] { });
                List<uint> _DVAArrival = new List<uint>(new uint[] { });
                List<uint> _DVADeparting = new List<uint>(new uint[] { });
                try
                {
                    string location = _SplitItems[1];
                    string direction = _SplitItems[2];
                    string status = _SplitItems[3];
                    string trainNumber = _SplitItems[5];
                    if (status.Equals("1"))
                    {
                        status = "Approach";
                    }
                    else if (status.Equals("2"))
                    {
                        status = "Arrival";
                    }
                    else if (status.Equals("3"))
                    {
                        status = "Depart";
                    }
                    // PA
                    _Con = new Connection();
                    _Con.Connected();
                    _Con.SqlCmd = new SqlCommand
                    {
                        Connection = _Con.SqlCon,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "OA_ExecuteSignalling"
                    };
                    // Location ID Station
                    _Con.SqlCmd.Parameters.AddWithValue("@LocationID", _SplitItems[1]);
                    // Platform/Peron
                    _Con.SqlCmd.Parameters.AddWithValue("@Platform", _SplitItems[2]);
                    // Category Arrival/Depart/Approach  
                    if (_SplitItems[3] == "1")
                    {
                        _Variable = "Approach";
                    }
                    else if (_SplitItems[3] == "2")
                    {
                        _Variable = "Arrival";
                    }
                    else if (_SplitItems[3] == "3")
                    {
                        _Variable = "Depart";
                    }
                    _Con.SqlCmd.Parameters.AddWithValue("@Category", _Variable);
                    _Con.SqlRead = _Con.SqlCmd.ExecuteReader();
                    while (_Con.SqlRead.Read())
                    {
                        // Zone Station from table location
                        _ZoneData = _Con.SqlRead.GetString(1);
                        string[] _MyZoneSplit = _ZoneData.Split(',');
                        foreach (string _split in _MyZoneSplit)
                        {
                            _Zonestation.Add(_split.ToString());
                        }
                        if (_Con.SqlRead.GetString(3) == "Approach")
                        {
                            Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss :") + "Train Position : Approaching  Now !");
                        }
                        else if (_Con.SqlRead.GetString(3) == "Arrival")
                        {
                            _Data = _Con.SqlRead.GetString(4);
                            string[] _MySplit = _Data.Split(',');
                            foreach (string _split in _MySplit)
                            {
                                _DVAArrival.Add(Convert.ToUInt32(_split));
                            }
                            _OA.SetDVAAnnouncementsSignalling(_Zonestation, _DVAArrival);
                            foreach (string _zone in _Zonestation)
                            {
                                Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss :") + "Initiate Zone :" + _zone);
                            }
                            Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss :") + "Train Position : Arrival  Now !");
                            _DVAArrival.Clear();
                        }
                        else if (_Con.SqlRead.GetString(3) == "Depart")
                        {
                            Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss :") + "Train Position : Departing Now !");

                        }
                    }

                    //// Closed connectiion read after used
                    //if (!_Con.SqlRead.IsClosed)
                    //{
                    //    _Con.SqlRead.Close();
                    //}

                    int trainNumberInt = 0;
                    int.TryParse(trainNumber, out trainNumberInt);
                    int locationInt = 0;
                    int.TryParse(location, out locationInt);
                    int nextTrainNumber = _OA.GetNextTrainNumber(trainNumberInt, int.Parse(direction));
                    int afterNextTrainNumber = _OA.GetAfterNextTrainNumber(trainNumberInt, int.Parse(direction));

                    int count = getCountTrain(direction.ToString());
                    int stationNext = _OA.GetStationTrainNo(nextTrainNumber, int.Parse(direction));
                    int stationAfterNext = _OA.GetStationTrainNo(afterNextTrainNumber, int.Parse(direction));
                    int headway = 3;
                    Console.WriteLine("Your Station : " + TranslateNumberTrain(locationInt) + " (" + locationInt + ")");
                    Console.WriteLine("Current Next Station :" + TranslateNumberTrain(stationNext) + " (" + stationNext + ")");
                    Console.WriteLine("Your Train Number : " + trainNumberInt);
                    Console.WriteLine("Next Train Number : " + nextTrainNumber);
                    Console.WriteLine("Direction : " + direction);
                    Console.WriteLine("Status : " + status);
                    #region fixPID

                    if (status.Equals("Arrival"))
                    {
                        //if (Convert.ToInt32(location) != 2 && Convert.ToInt32(location) != 7)
                        //{
                            _OA.SetSignallingPID(Convert.ToInt32(location), direction, "1", 2 + headway);
                            _OA.SetSignallingPID(Convert.ToInt32(location), direction, "2", 2 + headway + headway);
                            if (direction.Equals("2"))
                            {
                                ScreenPID[Convert.ToInt32(location), 2] = 2 + headway;
                                ScreenPID[Convert.ToInt32(location), 3] = 2 + headway + headway;
                            }
                            else
                            {
                                ScreenPID[Convert.ToInt32(location), 0] = 2 + headway;
                                ScreenPID[Convert.ToInt32(location), 1] = 2 + headway + headway;
                            }
                        //}

                        if (direction.Equals("2"))
                        {
                            int z, TT, n;
                            TT = 3; n = 1;
                            for (z = Convert.ToInt32(location) + 1; z <= stationNext; z++)
                            {
                                    _OA.SetSignallingPID(z, direction, "1", n * TT);
                                    _OA.SetSignallingPID(z, direction, "2", (n * TT) + headway);

                                    ScreenPID[z, 2] = n * TT;
                                    ScreenPID[z, 3] = (n * TT) + headway;
                                    n++;

                            }
                            getLineWest();
                        }
                        else if (direction.Equals("1"))
                        {
                            int z, TT, n;
                            TT = 3; n = 1;
                            for (z = Convert.ToInt32(location) - 1; z >= stationNext; z--)
                            {
                                    _OA.SetSignallingPID(z, direction, "1", n * TT);
                                    _OA.SetSignallingPID(z, direction, "2", (n * TT) + headway);

                                    ScreenPID[z, 0] = n * TT;
                                    ScreenPID[z, 1] = (n * TT) + headway;
                                    n++;
                            }
                            getLineEast();
                        }
                    }
                    else if (status.Equals("Depart"))
                    {
                        if (direction.Equals("2"))
                        {
                            int z, TT, n;
                            TT = 3; n = 1;

                            for (z = Convert.ToInt32(location) + 1; z <= stationNext; z++)
                            {
                                    _OA.SetSignallingPID(z, direction, "1", (n * TT) - 1);
                                    _OA.SetSignallingPID(z, direction, "2", (n * TT) - 1 + headway);

                                    ScreenPID[z, 2] = (n * TT) - 1;
                                    ScreenPID[z, 3] = (n * TT) - 1 + headway;
                                    n++;

                            }
                            getLineWest();
                        }
                        else if (direction.Equals("1"))
                        {
                            int z, TT, n;
                            TT = 3; n = 1;


                            for (z = Convert.ToInt32(location) - 1; z >= stationNext; z--)
                            {

                                    _OA.SetSignallingPID(z, direction, "1", (n * TT) - 1);
                                    _OA.SetSignallingPID(z, direction, "2", (n * TT) - 1 + headway);

                                    ScreenPID[z, 0] = (n * TT) - 1;
                                    ScreenPID[z, 1] = (n * TT) - 1 + headway;
                                    n++;
                            }
                            getLineEast();
                        }
                    }
                    else if (status.Equals("Approach"))
                    {

                            _OA.SetSignallingPID(Convert.ToInt32(location), direction, "1", 30);
                            ScreenPID[Convert.ToInt32(location), 0] = 30;
                            getLineWest();
                            getLineEast();

                    }
                    #endregion
                    _Data = "";
                    _ZoneData = "";
                    //added
                    _Con.Disconnected();

                    _Con.SqlCmd.Parameters.Clear();
                }
                catch (SqlException e)
                {
                    _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
                }
                catch (Exception e)
                {
                    _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
                }
            }
            //Send device state to HMI when runtime running
            if (_SplitItems[0] == "GetDeviceState" && _SplitItems[1] == "AllDevice")
            {
                try
                {
                    Thread.Sleep(5000);
                    _OA.SendAllCurrentDevicesState();
                    Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss :") + "Request All Device State ....");
                }
                catch (Exception e)
                {
                    _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
                }
            }
        }
        public void Server_DataReceivedHeartbeat(object sender, Message m)
        {
            string[] _SplitItems = m.MessageString.Split(',');
            byte[] _mydata;
            _mydata = m.Data;
            try
            {
                bool _isStartByteCorrect = _mydata[0] == 0x23;
                bool _isLengthCorrect = _mydata.Length == _mydata[1];
                bool _isHeartBeat = _mydata[3] == 0x24;
                bool _isStopByteCorrect = _mydata[_mydata.Length - 1] == 0x2A;
                if (_isStartByteCorrect && _isLengthCorrect && _isHeartBeat && _isStopByteCorrect)
                {
                    byte _sequence = _mydata[2];
                    byte[] _reply = new byte[5] { 0x23, 0x05, _sequence, 0x25, 0x2A };
                    SimpleTcpClient _client;
                    _client = new SimpleTcpClient().Connect("127.0.0.1", 1507);
                    _client.Write(_reply);
                    _client.Dispose();
                    //Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + "TX: " + BitConverter.ToString(_reply) + " Heartbeat received!");
                }
            }
            catch (SocketException e)
            {
                _logger.Error(DateTime.Now.ToString("dd-MM-yyyy : hh:mm:ss : ") + string.Concat(e.StackTrace, e.Message));
            };
        }
        public string TranslateNumberTrain(int train)
        {
            if (train == 2)
            {
                return "Pegangsaan Dua";
            }
            else if (train == 3)
            {
                return "Boulevard Utara";
            }
            else if (train == 4)
            {
                return "Boulevard Selatan";
            }
            else if (train == 5)
            {
                return "Pulomas";
            }
            else if (train == 6)
            {
                return "Equestrian";
            }
            else if (train == 7)
            {
                return "Velodrome";
            }
            return "";
        }
        private void getLineWest()
        {
            Console.WriteLine("PID West Now : ");
            Console.WriteLine("DPT \tKGM \tKGB \tPLM \tPKD \tVLD");
            for (int j = 2; j < 4; j++)
            {
                for (int i = 2; i < 8; i++)
                {
                    Console.Write(ScreenPID[i, j]);
                    Console.Write("\t");
                }
                Console.WriteLine("");
            }
        }
        private void getLineEast()
        {

            Console.WriteLine("PID East Now : ");
            Console.WriteLine("DPT \tKGM \tKGB \tPLM \tPKD \tVLD");
            for (int j = 0; j < 2; j++)
            {
                for (int i = 2; i < 8; i++)
                {
                    Console.Write(ScreenPID[i, j]);
                    Console.Write("\t");
                }
                Console.WriteLine("");
            }
        }
        private void SetArrivalTrain(string direction, string location)
        {
            if (location.Equals("2"))
            {
                //depo
                if (direction.Equals("1"))
                {
                    //east
                    eastArvPos[2] = true;

                    if (eastDepFrmPos[3] == true)
                        eastDepFrmPos[3] = false;
                }
                else if (direction.Equals("2"))
                {
                    //west
                    westArvPos[2] = true;

                    if (westDepFrmPos[3] == true)
                        westDepFrmPos[3] = false;

                }
            }
            else if (location.Equals("3"))
            {
                //kgm
                if (direction.Equals("1"))
                {
                    //east
                    eastArvPos[3] = true;

                    if (eastDepFrmPos[4] == true)
                        eastDepFrmPos[4] = false;
                }
                else if (direction.Equals("2"))
                {
                    //west
                    westArvPos[3] = true;

                    if (westDepFrmPos[2] == true)
                        westDepFrmPos[2] = false;
                }
            }
            else if (location.Equals("4"))
            {
                //kgb
                if (direction.Equals("1"))
                {
                    //east     
                    eastArvPos[4] = true;
                    if (eastDepFrmPos[5] == true)
                        eastDepFrmPos[5] = false;
                }
                else if (direction.Equals("2"))
                {
                    //west
                    westArvPos[4] = true;
                    if (westDepFrmPos[3] == true)
                        westDepFrmPos[3] = false;
                }
            }
            else if (location.Equals("5"))
            {
                //plm
                if (direction.Equals("1"))
                {
                    //east
                    eastArvPos[5] = true;
                    if (eastDepFrmPos[6] == true)
                        eastDepFrmPos[6] = false;
                }
                else if (direction.Equals("2"))
                {
                    //west
                    westArvPos[5] = true;
                    if (westDepFrmPos[4] == true)
                        westDepFrmPos[4] = false;
                }
            }
            else if (location.Equals("6"))
            {
                //pkd
                if (direction.Equals("1"))
                {
                    //east
                    eastArvPos[6] = true;
                    if (eastDepFrmPos[7] == true)
                        eastDepFrmPos[7] = false;
                }
                else if (direction.Equals("2"))
                {
                    //west
                    westArvPos[6] = true;
                    if (westDepFrmPos[5] == true)
                        westDepFrmPos[5] = false;
                }
            }
            else if (location.Equals("7"))
            {
                //vld
                if (direction.Equals("1"))
                {
                    //east
                    eastArvPos[7] = true;
                    if (eastDepFrmPos[6] == true)
                        eastDepFrmPos[6] = false;
                }
                else if (direction.Equals("2"))
                {
                    //west
                    westArvPos[7] = true;
                    if (westDepFrmPos[6] == true)
                        westDepFrmPos[6] = false;
                }
            }
        }
        private void SetDepartTrain(string direction, string location)
        {
            if (location.Equals("2"))
            {
                //depo
                if (direction.Equals("1"))
                {
                    //east  write kgm
                    eastDepFrmPos[2] = true;
                    eastArvPos[2] = false;
                }
                else if (direction.Equals("2"))
                {
                    //west
                    westDepFrmPos[2] = true;
                    westArvPos[2] = false;

                }
            }
            else if (location.Equals("3"))
            {
                //kgm
                if (direction.Equals("1"))
                {
                    //east  write kgb
                    eastDepFrmPos[3] = true;
                    eastArvPos[3] = false;
                }
                else if (direction.Equals("2"))
                {
                    //west  write depo
                    westDepFrmPos[3] = true;
                    westArvPos[3] = false;
                }
            }
            else if (location.Equals("4"))
            {
                //kgb
                if (direction.Equals("1"))
                {
                    //east  write plm
                    eastDepFrmPos[4] = true;
                    eastArvPos[4] = false;
                }
                else if (direction.Equals("2"))
                {
                    //west  write kgm
                    westDepFrmPos[4] = true;
                    westArvPos[4] = false;
                }
            }
            else if (location.Equals("5"))
            {
                //plm
                if (direction.Equals("1"))
                {
                    //east  write pkd
                    eastDepFrmPos[5] = true;
                    eastArvPos[5] = false;
                }
                else if (direction.Equals("2"))
                {
                    //west  write kgb
                    westDepFrmPos[5] = true;
                    westArvPos[5] = false;
                }
            }
            else if (location.Equals("6"))
            {
                //pkd
                if (direction.Equals("1"))
                {
                    //east  write vld
                    eastDepFrmPos[6] = true;
                    eastArvPos[6] = false;
                }
                else if (direction.Equals("2"))
                {
                    //west  write plm
                    westDepFrmPos[6] = true;
                    westArvPos[6] = false;
                }
            }
            else if (location.Equals("7"))
            {
                //vld
                if (direction.Equals("1"))
                {
                    //east
                    eastDepFrmPos[7] = true;
                    eastArvPos[7] = false;
                }
                else if (direction.Equals("2"))
                {
                    //west  write pkd
                    westDepFrmPos[7] = true;
                    westArvPos[7] = false;
                }
            }
        }
        private int getCountTrain(string direction)
        {
            List<TrainTable> trainTables = _OA.GetTimeTable();
            int dataCount = 0;
            foreach (TrainTable trainTable in trainTables)
            {
                if (trainTable.Direction.Contains(direction))
                {
                    dataCount++;
                }
            }
            return dataCount;
        }
        ~ClassReceivedTCP()
        {
            Console.WriteLine("The instance of" +
                       " ClassReceivedTCP class Destroyed");
        }
    }
}
