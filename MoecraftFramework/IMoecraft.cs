using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;


namespace MoecraftFramework
{
    [Guid("EC1DECD9-927F-4D33-9939-DB2080311AF7")]
    public interface IMoecraft
    {
        string main(string EventType, int subType, int sendTime, int fromGroup, int fromDiscuss, int fromQQ,
            string fromAnonymous, int beingOperateQQ, string msg, int font, string responseFlag, string file);
        void showMsg();
    }
    [Guid("3898EEB5-D51B-449A-BB7C-DDD7921E7227")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Entry : IMoecraft
    {
        public string main(string EventType, int subType, int sendTime, int fromGroup, int fromDiscuss, int fromQQ,
    string fromAnonymous, int beingOperateQQ, string msg, int font, string responseFlag, string file)
        {
            //string cmd = string.Join("", sm.GetPlugins(EventType, subType, sendTime, fromGroup, fromDiscuss, fromQQ,
            //fromAnonymous, beingOperateQQ, msg, font, responseFlag, file));
            return "第一次消息";// + cmd;
        }
        public void showMsg()
        {
            Form frm = new Form();
            FormShow fs = new FormShow(frm);//线程间窗体显示类，把需要载入的窗体放进这里实例化
            fs.Show();//这样可以显示出来
        }
    }
}