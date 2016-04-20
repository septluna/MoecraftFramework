using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MoecraftFramework
{
    //定义程序的接口
    public interface IMoePlugin
    {
        string main(string EventType, int subType, int sendTime, int fromGroup, int fromDiscuss, int fromQQ,
        string fromAnonymous, int beingOperateQQ, string msg, int font, string responseFlag, string file);
    }
    //控制加载
    public class MoeDllImport
    {
        public class MoePluginManager
        {
            public ArrayList plugins = new ArrayList();
            //此方法只需要刷新的时候进行调用
            public void LoadAllPlugins()
            {
                string[] files = Directory.GetFiles(Application.StartupPath + @"/plugins");
                foreach (string file in files)
                {   
                    if (file.Substring(file.LastIndexOf(".")) == ".dll")
                    {
                        try
                        {   
                            Assembly ab = Assembly.LoadFile(file);
                            Type[] tempTs = ab.GetTypes();
                            foreach (Type tp in tempTs)
                            {
                                if (IsValidPlugin(tp))
                                {   
                                    plugins.Add(ab.CreateInstance(tp.FullName));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "加载插件出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            private bool IsValidPlugin(Type t)
            {
                bool ret = false;
                Type[] interfaces = t.GetInterfaces();
                foreach (Type theInterface in interfaces)
                {
                    if (theInterface.FullName == "MoecraftFramework.IMoePlugin")
                    {
                        ret = true;
                        break;
                    }
                }
                return ret;
            }
        }
    }
}
