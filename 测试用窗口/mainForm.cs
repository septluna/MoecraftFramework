using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace 测试用窗口
{
    public partial class mainForm : Form
    {
        List<Thread> subThreadLst = new List<Thread>();
        Thread subThread = null;
        DateTime dt = new DateTime();
        delegate void drawDelegate(int i);
        public mainForm()
        {
            dt = DateTime.Now;
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            closeThread();
            subThread = new Thread(new ThreadStart(ShowMsg));
            subThread.Start();
        }
        public void ShowMsg()
        {
            TimeSpan i = DateTime.Now - dt;
            if (i.TotalSeconds > 1)
            {
                dt = DateTime.Now;
                Form1 fm = new Form1();
                fm.Location = new Point(100, 100);
                fm.ShowDialog();
            }
            else
            {
                Thread.Sleep(1500);
                dt = DateTime.Now;
                Form1 fm = new Form1();
                fm.Location = new Point(100, 100);
                fm.ShowDialog();
            }
        }
        private void closeThread()
        {
            if (subThread != null)
            {
                if (subThread.IsAlive)
                {
                    subThread.Abort();
                }
            }
        }
        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            closeThread();
        }

        private void mainForm_Shown(object sender, EventArgs e)
        {
            closeThread();
            subThread = new Thread(new ThreadStart(ShowMsg));
            subThread.Start();
        }
    }
}
