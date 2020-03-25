using System;
using System.Windows.Forms;
using System.Timers;
using System.Data;
using SimpleTCP;
using System.Net.Sockets;
using System.Net;
using System.Drawing;

namespace Len.Jakpro.TrainPosDriverSimulation
{
    public partial class Form1 : Form
    {
        private System.Timers.Timer _Timers;
        private int Ports;
        private string IP;
        
        public Form1()
        {
            InitializeComponent();                 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;

            label5.ForeColor = Color.OrangeRed;
            label5.Text = "Not Running";
        }

        private void ValidationEnable(bool x)
        {
            Interval.Enabled = x;
            Host.Enabled = x;
            Port.Enabled = x;
            button1.Enabled =  ! x;
            BtnStart.Enabled = x;
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            Ports = Convert.ToUInt16(Port.Text);
            IP = Host.Text;


            label5.ForeColor = Color.Green;
            label5.Text = "Running";

            _Timers = new System.Timers.Timer(Convert.ToDouble(Interval.Text));
            _Timers.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            _Timers.Enabled = true;
            _Timers.Start();

            ValidationEnable(false);
        }
        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {                    
            try
            {
                if (dataGridView1.InvokeRequired)
                {
                    dataGridView1.Invoke(new Action(SendData));
                    return;
                }                
            }
            catch (Exception ex)
            {
                dataGridView1.Rows.Add(DateTime.Now.ToString(), ex.ToString());
            }
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
        public string TranslatePlatformTrain(int Platform)
        {
            if (Platform == 1)
            {
                return "East";
            }
            else if (Platform == 2)
            {
                return "West";
            }            
            return "";
        }
        public string TranslateTypeMessageTrain(int Message)
        {
            if (Message == 1)
            {
                return "Approach";
            }
            else if (Message == 2)
            {
                return "Arrival";
            }
            else if (Message == 3)
            {
                return "Depart";
            }
            return "";
        }
        private void SendData()
        {
            if (dataGridView1.Rows.Count <= 25)
            {
                SimpleTcpClient _sendvalues;
                try
                {
                    Random rnd1 = new Random();
                    int Location = rnd1.Next(2, 7);
                    Random rnd2 = new Random();
                    int Platform = rnd2.Next(1, 2);
                    Random rnd3 = new Random();
                    int TypeMessage = rnd3.Next(1, 3);
                    Random rnd4 = new Random();
                    int TrainNumb = rnd4.Next(1001,1010);
                    Random rnd5 = new Random();
                    int TrainID = rnd5.Next(100, 999);

                    string date = DateTime.Now.ToString("dd:MM:yyyy");

                    _sendvalues = new SimpleTcpClient().Connect(IP,Ports);
                    _sendvalues.Write("PASignalling,"+Location+","+ Platform +","+ TypeMessage+"," + date + "," + TrainNumb + "," + TrainID + "");

                    dataGridView1.Rows.Add(DateTime.Now.ToString("dd:MM:yyyy hh:mm:ss"), TranslateNumberTrain(Location)+"-"+ TranslatePlatformTrain(Platform) + "-" + TranslateTypeMessageTrain(TypeMessage) + "-" + TrainNumb + "-" + TrainID);

                    _sendvalues.Dispose();
                    _sendvalues.Disconnect();
                }
                catch (SocketException e)
                {
                    dataGridView1.Rows.Add(DateTime.Now.ToString(), string.Concat(e.StackTrace, e.Message));
                }         
            }
            else
            {
                dataGridView1.Rows.Clear();
            }           
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _Timers.Stop();
            ValidationEnable(true);

            label5.ForeColor = Color.OrangeRed;
            label5.Text = "Not Running";
        }
    }
}
