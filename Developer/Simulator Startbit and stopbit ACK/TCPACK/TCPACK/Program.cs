using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SimpleTCP;
namespace TCPACK
{
    class Program
    {
        private static SimpleTcpServer server = new SimpleTcpServer();
        static void Main(string[] args)
        {
            Console.WriteLine("HeartBeat Receiver");
            server.Delimiter = 0x13;
            server.StringEncoder = Encoding.UTF8;
            server.DataReceived += Server_DataReceived; //Subscribe to DataRecieved event.
            server.Start(IPAddress.Parse("127.0.0.1"), 1504); //Start listening to incoming connections and data.

            Console.ReadKey();
        }
        public static  void Server_DataReceived(object sender, Message m)
        {
            byte[] mydata;
            string data;
            mydata = m.Data;
            data = BitConverter.ToString(mydata);
            Console.WriteLine("RX: " + data);
            bool isStartByteCorrect = mydata[0] == 0x23;
            bool isLengthCorrect = mydata.Length == mydata[1];
            bool isHeartBeat = mydata[3] == 0x24;
            bool isStopByteCorrect = mydata[mydata.Length - 1] == 0x2A;
            if (isStartByteCorrect && isLengthCorrect && isHeartBeat && isStopByteCorrect)
            {
                byte sequence = mydata[2];
                byte[] reply = new byte[5] { 0x23, 0x05, sequence, 0x25, 0x2A };
                
                SimpleTcpClient client;
                client = new SimpleTcpClient().Connect("127.0.0.1", 1505);
                client.Write(reply);
                client.Dispose();
                Console.WriteLine("TX: " + BitConverter.ToString(reply) + " Heartbeat received!");
            }           
        }

    }
}
