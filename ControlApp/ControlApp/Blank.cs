using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlApp
{
    public partial class Blank : Form
    {
        int count;
        public Blank(int what)
        {
            tmr = new System.Windows.Forms.Timer();
            tmr.Interval = (int)TimeSpan.FromSeconds(10).TotalMilliseconds;
            tmr.Tick += closewindow;
            lockMouse = new LockMouse();
            LockKeyboard = new LockKeyboard();
            whatlock = what;
            InitializeComponent();
            count = 1;
        }
        int whatlock;
        private System.Windows.Forms.Timer tmr;
        LockMouse lockMouse;
        LockKeyboard LockKeyboard;
        public void closewindow(object sender, EventArgs e)
        {
            count--;
            if (count == 0)
            {
                if (whatlock == 1)
                    lockMouse.Unlock();
                else if (whatlock == 2)
                {
                    lockMouse.Unlock();
                    LockKeyboard.Unlock();
                }
                this.Close();
            }
        }
        public void add_time(int what)
        {
            if (whatlock < what)
                whatlock = what;
            count++;
        }
        private void Blank_Load(object sender, EventArgs e)
        {
            this.Opacity = .001;
            StartPosition = FormStartPosition.CenterScreen;
            Screen[] my = Screen.AllScreens;
            Size = my[0].Bounds.Size;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;  // no borders

            TopMost = true;        // make the form always on top                     
            Visible = true;        // Important! if this isn't set, then the form is not shown at all
            if (whatlock == 1)
                lockMouse.Lock();
            else if (whatlock == 2)
            {
                lockMouse.Lock();
                LockKeyboard.Lock();
            }
            tmr.Start();
        }
    }
}
