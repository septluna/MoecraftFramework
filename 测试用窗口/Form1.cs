using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MoecraftFramework;

namespace 测试用窗口
{
    public partial class Form1 : Form
    {

        string filePath = "none";
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool notInDebug = true;
            #if DEBUG
            notInDebug = false;
            #endif
            if (notInDebug == false)
            {
                MoeScript.Interpreter dbex = new MoeScript.Interpreter();
                dbex.ScriptAnalyze(@"D:\000.moe");
                pictureBox1.Image = canvas.bmp;
            }
            if (filePath != "none" && notInDebug)
            {
                MoeScript.Interpreter ex = new MoeScript.Interpreter();
                ex.ScriptAnalyze(filePath);
                pictureBox1.Image = canvas.bmp;
            }
            if (filePath =="none" && notInDebug)
            {
                OpenFileDialog ofDialog = new OpenFileDialog();
                ofDialog.AddExtension = true;
                ofDialog.CheckFileExists = true;
                ofDialog.CheckPathExists = true;
                ofDialog.Filter = "moe脚本(*.moe)|*.moe|所有文件 (*.*)|*.*";
                ofDialog.DefaultExt = "*.txt";
                if (ofDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = ofDialog.FileName;
                    button1.Text = "运行脚本";
                    MessageBox.Show("脚本路径设定完成(<ゝω·)Kira☆~");
                }
                else
                {
                    filePath = "none";
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PluginManager pm = new PluginManager();
            string x = string.Join("", pm.GetPlugins("私聊消息", 0, 0, 0, 0, 495073131, "", 0, "", 0, "", ""));
            MessageBox.Show(x);
        }
        public void test ()
        {
            while (true)
            {
                this.Text = DateTime.Now.ToString("hh:mm:ss");
                Thread.Sleep(100);
            }       
        }
    }
}
