using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlApp
{
    public partial class Audiopop : Form
    {
        string whatlocal;
        public Audiopop(string what)
        {
            whatlocal = what;
            InitializeComponent();
        }

        private void Audiopop_Load(object sender, EventArgs e)
        {
            if (whatlocal.Substring(whatlocal.Length - 3,3) == "wav")
            {
                SoundPlayer simpleSound = new SoundPlayer(whatlocal);
                simpleSound.Play();
                this.Close();
            }
            else
            {
                WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();

                wplayer.URL = whatlocal;
                wplayer.controls.play();
                this.Close();
            }
        }
    }
}
