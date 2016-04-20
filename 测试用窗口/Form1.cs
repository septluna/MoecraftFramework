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
        XmlDocument doc = new XmlDocument();
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
            loadPluginInfo();
        }
        public void loadPluginInfo()
        {
            doc.Load(Application.StartupPath + @"/plugins/plugin.xml");
            RecursionTreeControl(doc.DocumentElement, treeView1.Nodes);
        }

        private void RecursionTreeControl(XmlNode xmlNode, TreeNodeCollection nodes)
        {

            foreach (XmlNode node in xmlNode.ChildNodes)//循环遍历当前元素的子元素集合
            {
                TreeNode new_child = new TreeNode();//定义一个TreeNode节点对象
                new_child.Text = node.Name != "#text" ? node.Name : node.Value;
                if (node.Attributes != null && node.Attributes.Count > 0)
                {
                    foreach (XmlAttribute atb in node.Attributes)
                    {
                        if (atb.Name == "名称")
                        {
                            new_child.Text = node.Attributes["名称"].Value;
                            continue;
                        }                            
                    }
                }
                nodes.Add(new_child);//向当前TreeNodeCollection集合中添加当前节点
                RecursionTreeControl(node, new_child.Nodes);//调用本方法进行递归
            }
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
            treeView1.SelectedNode.Text = 
                treeView1.SelectedNode.Text == "是" ? "否" :
                treeView1.SelectedNode.Text == "否" ? "是":
                treeView1.SelectedNode.Text;
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
                        contextMenuStrip1.Show(treeView1,new Point(e.X,e.Y));    
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
            Form fm = new Form();
            fm.Size = new Size(100,30);
            Label lb = new Label();
            lb.Text = "请选择权限名:";
            lb.Location = new Point(10,10);
            ComboBox cbx = new ComboBox();
            cbx.Location = new Point(110,5);
            cbx.Items.Add("私聊消息处理");
            cbx.Items.Add("群消息处理");
            cbx.Items.Add("讨论组消息处理");
            cbx.Items.Add("群文件上传事件处理");
            cbx.Items.Add("群管理变动事件处理");
            cbx.Items.Add("群成员减少事件处理");
            cbx.Items.Add("群成员增加事件处理");
            cbx.Items.Add("好友已添加事件处理");
            cbx.Items.Add("好友添加请求处理");
            cbx.Items.Add("群添加请求处理");
            fm.Controls.Add(lb);
            fm.Controls.Add(cbx);
            fm.ShowDialog();
            if (cbx.Text!="")
            {
                treeView1.SelectedNode.Parent.Nodes.Add(cbx.Text);
            }
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
