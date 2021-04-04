using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace InactiveTaskKiller
{
    public partial class InactiveTaskKiller : Form
    {


        //program çalışınca sadece görev yöneticisinde görünmesi için
        protected override CreateParams CreateParams
        {
            get
            {
                var createParams = base.CreateParams;
                createParams.ExStyle |= 0x80;
                return createParams;
            }
        }
        //zaman bilgisini okuyoruz
        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
        [StructLayout(LayoutKind.Sequential)]
        struct LASTINPUTINFO
        {
            public uint cbSize;
            public int dwTime;
        }
        //pc acilis zamanini aldik
        DateTime bootTime = DateTime.UtcNow.AddMilliseconds(-Environment.TickCount);
        public InactiveTaskKiller()
        {
            InitializeComponent();
            // this.FormBorderStyle = FormBorderStyle.None;
            // this.ShowInTaskbar = false;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.Visible = false;
            this.Opacity = 0;
            this.ShowInTaskbar = false;


        }
        public static List<string> Params()
        {
            List<string> programlar = new List<string>();
            var progs = File.ReadLines("parametreler");
            foreach (string item in progs)
            {
                programlar.Add(item);
            }
            return programlar;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Start();
            timer1.Interval = 1000;//timer 100ms bir çalışacak
            LASTINPUTINFO lii = new LASTINPUTINFO();
            lii.cbSize = (uint)Marshal.SizeOf(typeof(LASTINPUTINFO));
            GetLastInputInfo(ref lii);
            DateTime lastInputTime = bootTime.AddMilliseconds(lii.dwTime);
            TimeSpan idleTime = DateTime.UtcNow.Subtract(lastInputTime);//bosta kalma süresini aldık
            int dakika = Convert.ToInt32(Math.Floor(idleTime.TotalMinutes));
            List<string> listem = Params();
            int dakparam = Convert.ToInt32(listem[0]);
            listem.RemoveAt(0);
            if (dakika > dakparam)
            {
                dakika = 0;
                foreach (string item in listem)
                {
                    string processName = item; // Kapatmak İstediğimiz Program
                    foreach (var x in Process.GetProcessesByName(item))
                    {
                        x.Kill();
                    }
                }
            }
        }
    }
}
