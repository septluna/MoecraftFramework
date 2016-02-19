using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            if (filePath != "none")
            {
                MoeScript.Explainer ex = new MoeScript.Explainer();
                ex.scriptExplaine(filePath);
                pictureBox1.Image = canvas.bmp;
            }
            else
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
    }
}
