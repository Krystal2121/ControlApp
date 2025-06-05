using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Runtime.InteropServices;

namespace ControlApp
{
    public partial class CustomMessage : Form
    {

        string msg;
        string btn;
        int time;
        bool ttspeech;
        private System.Windows.Forms.Timer tmr;

        public CustomMessage(string message, string button, int timeopen, bool tts)
        {
            InitializeComponent();
            msg = message; btn = button;
            ttspeech = tts;
            time = timeopen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CustomMessage_Load(object sender, EventArgs e)
        {
            if (ttspeech)
            {
                this.Opacity = 0;
                var synthesizer = new SpeechSynthesizer();
                synthesizer.SetOutputToDefaultAudioDevice();
                synthesizer.Speak(msg);
                synthesizer.Dispose();
                try
                {
                    this.Close();
                }
                catch { }

            }
            else
            {
                label1.Text = msg;
                if (btn != "")
                {
                    button1.Text = btn;
                }
                if (time != 0)
                {
                    tmr = new System.Windows.Forms.Timer();
                    tmr.Tick += delegate
                    {
                        this.Close();
                    };

                    tmr.Interval = (int)TimeSpan.FromSeconds(time).TotalMilliseconds;

                    tmr.Start();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
