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


namespace ControlApp
{
    public partial class Spinner : Form
    {
        private System.Windows.Forms.Timer timer;
        private int angle = 15;
        private int duration = 0;
        Bitmap bmp_img;
        string bitmpath = "";
        Utils utils;
        string[] seperateitems;
        Random random;
        int item_poition = 0;
        public Spinner(string spinlist)
        {
            random = new Random();
            duration = random.Next(50, 401);
            utils = new Utils();
            seperateitems = utils.seperate_string(spinlist);            
            InitializeComponent();
            timer = new System.Windows.Forms.Timer { Interval = 25 }; // Timer for rotation
            timer.Tick += Timer_Tick;
            pictureBox1.Image = bmp_img;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            resultbox_txt.Text= seperateitems[item_poition];
            if (duration == 0) timer.Stop(); // Stop when slow enough            
            pictureBox1.Image = RotateImage(bmp_img, angle);
            pictureBox1.Refresh();
            duration--;
            item_poition++;
            if(item_poition == seperateitems.Length) {item_poition = 0;}
        }

        private void SpinBtn_Click(object sender, EventArgs e)
        {
            duration = random.Next(1, 101);
            timer.Start();
        }

        private Bitmap RotateImage(Bitmap bmp, float angle)
        {
            Bitmap rotated = bmp;
            float centerX = bmp.Width / 2.0f;
            float centerY = bmp.Height / 2.0f;

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.TranslateTransform(centerX, centerY);
                g.RotateTransform(angle);
                g.TranslateTransform(-centerX, -centerY);
                g.DrawImage(bmp, new Point(0, 0));
                g.ResetTransform();

            }
            return rotated;
        }

        private void Spinner_Load(object sender, EventArgs e)
        {
            bitmpath = AppDomain.CurrentDomain.BaseDirectory + "spinwheels\\spinner_"+ seperateitems.Count().ToString() + "_slices.png";
            bmp_img = new Bitmap(bitmpath);
            pictureBox1.Refresh(); 
            if (ConfigurationManager.AppSettings["DarkMode"] != null)
            {
                string dmode = ConfigurationManager.AppSettings["DarkMode"].ToString();
                if (Convert.ToBoolean(dmode))
                {
                    this.BackColor = Color.Black;
                    this.ForeColor = Color.White;
                    foreach (Control control in this.Controls)
                    {
                        if (control is Panel)
                        {
                            control.BackColor = Color.Black;
                            control.ForeColor = Color.White;
                        }
                        if (control is Button)
                        {
                            control.BackColor = Color.DarkGray;
                            control.ForeColor = Color.White;
                        }
                    }
                }
            }
        }
    }
}
