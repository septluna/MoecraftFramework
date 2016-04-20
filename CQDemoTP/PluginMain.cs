using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using MoecraftFramework;

namespace MoecraftFramework
{
    //这里是插件调用的接口，moe平台会自动通过反射调用
    [Guid("453611AF-68E1-467D-9F84-7F3042FBF9E2")]
    public class MyPlugin :IMoePlugin
    {
        #region 默认缺省值
        string EventType = "";
        int subType = 0;
        int sendTime = 0;
        int fromGroup = 0;
        int fromDiscuss = 0;
        int fromQQ = 0;
        string fromAnonymous = "";
        int beingOperateQQ = 0;
        string msg = "";
        int font = 0;
        string responseFlag = "";
        string file = "";
        #endregion
        public string main(string EventType, int subType, int sendTime, int fromGroup, int fromDiscuss, int fromQQ,
            string fromAnonymous, int beingOperateQQ, string msg, int font, string responseFlag, string file)
        {
            return CQ应用.事件预处理(EventType, subType, sendTime, fromGroup, fromDiscuss, fromQQ,
 fromAnonymous, beingOperateQQ, msg, font, responseFlag, file);
        }
    }
}
