using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;


//using System.EnterpriseServices;//引用COM+的类

namespace MoecraftFramework
{
    [Guid("EC1DECD9-927F-4D33-9939-DB2080311AF7")]
    public interface IMoecraft
    {
        string main(string info);
        bool show();
    }
    [Guid("3898EEB5-D51B-449A-BB7C-DDD7921E7227")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Entry : IMoecraft
    {
        public string text;
        public string main(string info)
        {
            MessageBox.Show("123456");
            return info;
        }
        public bool show()
        {
            CTransfShow ct = new CTransfShow();
            bool bl = ct.aaa();
            return bl;
        }
    }
    public class CShow : IShow
    {
        public bool Show()
        {
            Form frm = new Form();
            frm.Text = "测试窗口";
            frm.FormClosing += fmclose;
            frm.ShowDialog();
            return false;
        }
        public void fmclose(object obj,EventArgs e)
        {
            FileHandle.INIWriteValue(@"D:\moecraft.ini","窗体","状态","关闭");
        }
    }
}