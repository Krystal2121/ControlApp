using System.Configuration;
using System.Resources;
using System.Security.Policy;
using System.Windows.Forms;

namespace ControlApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new MyCustomApplicationContext());
        }
    }


    public class MyCustomApplicationContext : ApplicationContext
    {
        private NotifyIcon trayIcon;
        private ControlApp myforminvis;
        private ControlApp myform;
        public MyCustomApplicationContext()
        {
            // Initialize Tray Icon
            trayIcon = new NotifyIcon();
            trayIcon.Icon = new Icon("App.ico");
            trayIcon.ContextMenuStrip = new ContextMenuStrip();
            trayIcon.ContextMenuStrip.Items.Add("Exit", null, Exit);
            trayIcon.ContextMenuStrip.Items.Add("Open", null, Open);
            trayIcon.ContextMenuStrip.Items.Add("Panic", null, Panic);
            trayIcon.ContextMenuStrip.Items.Add("Subliminal", null, Subliminal);
            trayIcon.MouseClick += TrayIcon_MouseClick;
            trayIcon.Visible = true;
            myforminvis = new ControlApp();
            myforminvis.Opacity = 0;
            myforminvis.ShowInTaskbar = false;
            myforminvis.Show();
            myform=new ControlApp();
        }

        private void TrayIcon_MouseClick(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (myform.IsDisposed)
                    myform = new ControlApp();
                    myform.Show();                
            }
        }

        void Exit(object sender, EventArgs e)
        {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            trayIcon.Visible = false;

            Application.Exit();
        }
        void Open(object sender, EventArgs e)
        {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            if (myform.IsDisposed)
                myform = new ControlApp();
            myform.Show();
        }
        void Subliminal(object sender, EventArgs e)
        {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            bool done = false;
            foreach (Form fm in System.Windows.Forms.Application.OpenForms)
            {
                if (fm.GetType() == typeof(SubLoop))
                {
                    SubLoop pop = (SubLoop)fm;
                    try
                    {
                        pop.Close();
                        done = true;
                        break;
                    }
                    catch { }
                }
            }
            if (!done)
            {
                SubLoop subLoop = new SubLoop();
                subLoop.Show();
            }
        }
        void Panic(object sender, EventArgs e)
        {
            foreach (Form fm in Application.OpenForms)
            {
                fm.Close();
            }
        }
    }
}