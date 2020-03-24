using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Len.Jakpro.TrainPosDriverSimulation
{
    public partial class Form1 : Form
    {
    
        public Form1()
        {
            InitializeComponent();
        }
        private static Timer timer;
        private void Form1_Load(object sender, EventArgs e)
        {
            timer = new System.Timers.Timer();
            timer.Interval = 5000;

            timer.Elapsed += OnTimedEvent;
       //     timer.AutoReset = true;
            timer.Enabled = true;

            listView1.View = View.Details;
            listView1.Columns.Add("Date");
            listView1.Columns.Add("Description");
            listView1.GridLines = true;         
        } 

        private void BtnStart_Click(object sender, EventArgs e)
        {
        
        }
        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            listView1.Items.Add(new ListViewItem(new string[] { "tes", "11234" }));
        }
  
    }
}
