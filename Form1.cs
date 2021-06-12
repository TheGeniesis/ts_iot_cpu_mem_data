using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;

namespace ThingSpeak
{
    public partial class Form1 : Form
    {
        private PerformanceCounter CPUCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        private PerformanceCounter MemCounter = new PerformanceCounter("Memory", "Available MBytes");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            float cpu = CPUCounter.NextValue();
            float mem = MemCounter.NextValue();
            label3.Text = cpu.ToString("f2");
            label4.Text = mem.ToString("f2");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            float cpu = CPUCounter.NextValue();
            float mem = MemCounter.NextValue();
            label3.Text = cpu.ToString("f2");
            label4.Text = mem.ToString("f2");

            const string WRITEKEY = "";
            string strUpdateBase = "http://api.thingspeak.com/update";
            string strUpdateURI = strUpdateBase + "?key=" + WRITEKEY;
            HttpWebRequest ThingsSpeakReq;
            HttpWebResponse ThingsSpeakResp;

            strUpdateURI += "&field1=" + label3.Text;
            strUpdateURI += "&field2=" + label4.Text;

            ThingsSpeakReq = (HttpWebRequest)WebRequest.Create(strUpdateURI);
        }
    }
}
