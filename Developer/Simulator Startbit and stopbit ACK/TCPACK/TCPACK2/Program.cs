using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using SimpleTCP;
namespace TCPACK2
{
    class Program
    {
        private static SimpleTcpServer server = new SimpleTcpServer();
        private static byte sequenceSend = 0;
        private static byte sequenceReceive = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("HeartBeat Sender");
            server.Delimiter = 0x13;
            server.StringEncoder = Encoding.UTF8;
            server.DataReceived += Server_DataReceived; //Subscribe to DataRecieved event.
            server.Start(IPAddress.Parse("127.0.0.1"), 1505); //Start listening to incoming connections and data.

            Timer refresh = new Timer(1000);
            refresh.Elapsed += new ElapsedEventHandler(RefreshData);
            refresh.Enabled = true;
            refresh.Start();
            Console.WriteLine("Sent.."); 
            Console.ReadKey();
        }
        public static void Server_DataReceived(object sender, Message m)
        {
            byte[] mydata;
            string data;
            mydata = m.Data;
            data = BitConverter.ToString(mydata);
            Console.WriteLine("RX: " + data);
            bool isStartByteCorrect = mydata[0] == 0x23;
            bool isLengthCorrect = mydata.Length == mydata[1];
            bool isAck = mydata[3] == 0x25;
            bool isStopByteCorrect = mydata[mydata.Length - 1] == 0x2A;
            bool isCorrectSequence = mydata[2] == sequenceSend;
            if (isStartByteCorrect && isLengthCorrect && isAck && isStopByteCorrect && isCorrectSequence)
            {
                sequenceReceive = sequenceSend;
            }
        }
        private static void RefreshData(object sender, ElapsedEventArgs e)
        {
            if(sequenceReceive == sequenceSend)
            {
                Console.WriteLine("Service 1: ON!");
            }
            else
            {
                Console.WriteLine("Service 1: OFF!");
            }
            sequenceSend++;
            if (sequenceSend == 0) sequenceSend = 1;
            byte[] heartBeat = new byte[5] { 0x23, 0x05, sequenceSend, 0x24, 0x2A };
            SimpleTcpClient client;
            client = new SimpleTcpClient().Connect("127.0.0.1", 1504);

            client.Write(heartBeat);
            string data ="TX: " + BitConverter.ToString(heartBeat);
            Console.WriteLine(data);
            client.Dispose();
        }
    }
}
