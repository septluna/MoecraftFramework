using MoecraftFramework;
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
using System.Xml;

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
                //pictureBox1.Image = canvas.bmp;
            }
            if (filePath != "none" && notInDebug)
            {
                MoeScript.Interpreter ex = new MoeScript.Interpreter();
                ex.ScriptAnalyze(filePath);
                //pictureBox1.Image = canvas.bmp;
            }
            if (filePath == "none" && notInDebug)
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
            //MoeDllImport.PluginManager pm = new MoeDllImport.PluginManager();
            //string x = string.Join("", pm.GetPlugins("私聊消息", 0, 0, 0, 0, 495073131, "", 0, "", 0, "", ""));
            loadPluginInfo();
        }
        public void loadPluginInfo()
        {
            int i = 0;
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(new TreeNode("moecraft插件管理器"));
            XmlDocument doc = new XmlDocument();
            doc.Load(@"D:\project\crossover\什么鬼.xml");
            foreach (XmlNode node in doc.DocumentElement.GetElementsByTagName("插件"))
            {
                treeView1.Nodes[0].Nodes.Add(((XmlElement)node).GetAttribute("名称"));
                int j = 0;
                foreach (XmlNode subNode in node.ChildNodes)
                {
                    treeView1.Nodes[0].Nodes[i].Expand();
                    string subName = subNode.FirstChild.Value;
                    if (subName == null)
                    {
                        treeView1.Nodes[0].Nodes[i].Nodes.Add(subNode.Name);
                        foreach (XmlNode item in subNode.ChildNodes)
                        {
                            if (item.FirstChild.Value == "允许")
                            {
                                treeView1.Nodes[0].Nodes[i].Nodes[j].Nodes.Add(item.Name);
                            }
                        }
                    }
                    else
                    {
                        treeView1.Nodes[0].Nodes[i].Nodes.Add(subName);
                    }
                    j++;
                }
                i++;
            }
            treeView1.Nodes[0].Expand();
        }
        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (treeView1.SelectedNode.Level > 1 && treeView1.SelectedNode.Parent.Text == "插件权限")
            {
                if (MessageBox.Show("确定后将暂时禁止该权限。", "询问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    treeView1.SelectedNode.Remove();
                }
            }
        }
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point ClickPoint = new Point(e.X, e.Y);
                TreeNode CurrentNode = treeView1.GetNodeAt(ClickPoint);
                if (CurrentNode != null)
                {
                    treeView1.SelectedNode = CurrentNode;
                    if (treeView1.SelectedNode.Level > 1 && treeView1.SelectedNode.Parent.Text == "插件权限")
                    {
                        contextMenuStrip1.Show(new Point(e.X,e.Y));    
                    }
                }
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView1.SelectedNode.Remove();
        }
        private void 新增ToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            treeView1.SelectedNode.Nodes.Add("");//现在正在编写中
        }
        private void button2_Click(object sender, EventArgs e)
        {
            MoeDllImport.MoePluginManager mp = new MoeDllImport.MoePluginManager();
            mp.LoadAllPlugins();
            foreach (IMoePlugin item in mp.plugins)
            {
                MessageBox.Show(item.main("私聊消息", 0, 0, 0, 0, 495073131, "", 0, "", 0, "", ""));
            }
        }
    }
}
