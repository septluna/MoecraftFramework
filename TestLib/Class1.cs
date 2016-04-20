using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoecraftFramework;

namespace MoecraftFramework
{
    public class MyPlugin : IMoePlugin
    {
        public string main(string EventType, int subType, int sendTime, int fromGroup, int fromDiscuss, int fromQQ,
            string fromAnonymous, int beingOperateQQ, string msg, int font, string responseFlag, string file)
        {
            return "the 2nd plugin!";
        }
    }
}
