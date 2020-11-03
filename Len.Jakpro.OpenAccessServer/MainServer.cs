    using System;
    using System.Net;
    using Len.Jakpro.ClassOpenAccess;
    namespace Len.Jakpro.OpenAccessServer
    {
        public class MainServer
        {
            /// <summary>
            /// Main Process OA Driver.
            /// 1. Run Send TCP
            /// 2. Run Received TCP
            /// </summary>
            public static void Main(string[] args)
            {
                try
                {
                    OpenAccessDriver _OA = new OpenAccessDriver();
                    _OA.StartSend("127.0.0.1", 1505);
                    // Note Send All State Devices to HMI and communication heartbeat between driver and addins
                    ClassReceivedTCP _TCP = new ClassReceivedTCP(_OA);
                    _TCP.StartListen(IPAddress.Parse("127.0.0.1"), 1504);
                    _TCP.StartListenCommunication(IPAddress.Parse("127.0.0.1"), 1506);
                    // hold a process
                    Console.ReadKey();
                } catch (Exception )
                {

                }
          
            }
        }
    }
