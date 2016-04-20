using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace MoecraftFramework
{
    public class myApp
    {
        CQ应用 CQ = new CQ应用();
        /// <summary>
        /// Type=21 私聊消息
        /// </summary>
        /// <param name="subType">子类型，11/来自好友 1/来自在线状态 2/来自群 3/来自讨论组</param>
        /// <param name="sendTime">发送时间(时间戳)</param>
        /// <param name="fromQQ">来源QQ</param>
        /// <param name="msg">消息内容</param>
        /// <param name="font">字体</param>
        /// <returns></returns>
        public string PrivateMsg(int subType, int sendTime, int fromQQ, string msg, int font)
        {
            CQ.cmd = "";//初始化CQ指令  
            CQ.发送私聊消息(fromQQ, "这是来自全新的托管代码开发库！");      
                 
            return CQ.cmd;
        }
        /// <summary>
        /// Type=2 群消息
        /// </summary>
        /// <param name="subType">子类型，11/来自好友 1/来自在线状态 2/来自群 3/来自讨论组</param>
        /// <param name="sendTime">发送时间(时间戳)</param>
        /// <param name="fromGroup">来源群号</param>
        /// <param name="fromQQ">来源QQ</param>
        /// <param name="fromAnonymous">来源匿名者</param>
        /// <param name="msg">消息内容</param>
        /// <param name="font">字体</param>
        /// <returns></returns>
        public string GroupMsg(int subType, int sendTime, int fromGroup, int fromQQ, string fromAnonymous, string msg, int font)
        {
            CQ.cmd = "";//初始化CQ指令

            return CQ.cmd;
        }
        /// <summary>
        /// Type=4 讨论组消息
        /// </summary>
        /// <param name="subType">子类型，11/来自好友 1/来自在线状态 2/来自群 3/来自讨论组</param>
        /// <param name="sendTime">发送时间(时间戳)</param>
        /// <param name="fromDiscuss">来源讨论组</param>
        /// <param name="fromQQ">来源QQ</param>
        /// <param name="msg">消息内容</param>
        /// <param name="font">字体</param>
        /// <returns></returns>
        public string DiscussMsg(int subType, int sendTime, int fromDiscuss, int fromQQ, string msg, int font)
        {
            CQ.cmd = "";//初始化CQ指令

            return CQ.cmd;
        }
        /// <summary>
        /// Type=11 群文件上传事件
        /// </summary>
        /// <param name="subType">子类型，11/来自好友 1/来自在线状态 2/来自群 3/来自讨论组</param>
        /// <param name="sendTime">发送时间(时间戳)</param>
        /// <param name="fromQQ">来源QQ</param>
        /// <param name="msg">消息内容</param>
        /// <param name="file">上传文件信息</param>
        /// <returns></returns>
        public string GroupUpload(int subType, int sendTime, int fromQQ, string msg, string file)
        {
            CQ.cmd = "";//初始化CQ指令

            return CQ.cmd;
        }
        /// <summary>
        /// Type=101 群事件-管理员变动
        /// </summary>
        /// <param name="subType">子类型，11/来自好友 1/来自在线状态 2/来自群 3/来自讨论组</param>
        /// <param name="sendTime">发送时间(时间戳)</param>
        /// <param name="fromGroup">来源群号</param>
        /// <param name="beingOperateQQ">被操作QQ</param>
        /// <returns></returns>
        public string System_GroupAdmin(int subType, int sendTime, int fromGroup, int beingOperateQQ)
        {
            CQ.cmd = "";//初始化CQ指令

            return CQ.cmd;
        }
        /// <summary>
        /// Type=102 群事件-群成员减少
        /// </summary>
        /// <param name="subType">子类型，11/来自好友 1/来自在线状态 2/来自群 3/来自讨论组</param>
        /// <param name="sendTime">发送时间(时间戳)</param>
        /// <param name="fromGroup">来源群号</param>
        /// <param name="fromQQ">来源QQ</param>
        /// <param name="beingOperateQQ">被操作QQ</param>
        /// <returns></returns>
        public string GroupMemberDecrease(int subType, int sendTime, int fromGroup, int fromQQ, int beingOperateQQ)
        {
            CQ.cmd = "";//初始化CQ指令

            return CQ.cmd;
        }
        /// <summary>
        /// Type=103 群事件-群成员增加
        /// </summary>
        /// <param name="subType">子类型，11/来自好友 1/来自在线状态 2/来自群 3/来自讨论组</param>
        /// <param name="sendTime">发送时间(时间戳)</param>
        /// <param name="fromGroup">来源群号</param>
        /// <param name="fromQQ">来源QQ</param>
        /// <param name="beingOperateQQ">被操作QQ</param>
        /// <returns></returns>
        public string GroupMemberIncrease(int subType, int sendTime, int fromGroup, int fromQQ, int beingOperateQQ)
        {
            CQ.cmd = "";//初始化CQ指令

            return CQ.cmd;
        }
        /// <summary>
        /// Type=201 好友事件-好友已添加
        /// </summary>
        /// <param name="subType">子类型，11/来自好友 1/来自在线状态 2/来自群 3/来自讨论组</param>
        /// <param name="sendTime">发送时间(时间戳)</param>
        /// <param name="fromQQ">来源QQ</param>
        /// <returns></returns>
        public string Friend_Added(int subType, int sendTime, int fromQQ)
        {
            CQ.cmd = "";//初始化CQ指令

            return CQ.cmd;
        }
        /// <summary>
        /// Type=301 请求-好友添加
        /// </summary>
        /// <param name="subType">子类型，11/来自好友 1/来自在线状态 2/来自群 3/来自讨论组</param>
        /// <param name="sendTime">发送时间(时间戳)</param>
        /// <param name="fromQQ">来源QQ</param>
        /// <param name="msg">消息内容</param>
        /// <param name="responseFlag">反馈标识(处理请求用)</param>
        /// <returns></returns>
        public string Request_AddFriend(int subType, int sendTime, int fromQQ, string msg, string responseFlag)
        {
            CQ.cmd = "";//初始化CQ指令

            return CQ.cmd;
        }
        /// <summary>
        /// Type=302 请求-群添加
        /// </summary>
        /// <param name="subType">子类型，11/来自好友 1/来自在线状态 2/来自群 3/来自讨论组</param>
        /// <param name="sendTime">发送时间(时间戳)</param>
        /// <param name="fromGroup">来源群号</param>
        /// <param name="fromQQ">来源QQ</param>
        /// <param name="msg">消息内容</param>
        /// <param name="responseFlag">反馈标识(处理请求用)</param>
        /// <returns></returns>
        public string Request_AddGroup(int subType, int sendTime, int fromGroup, int fromQQ, string msg, string responseFlag)
        {
            CQ.cmd = "";//初始化CQ指令

            return CQ.cmd;
        }
    }
    public class CQ应用
    {
        //1.事件2.子类型3.时间4.群号5.讨论组6.QQ号7.匿名8.被操作9.msg10.字体11.反馈12.文件
        public string cmd { get; set; }
        #region 发送
        public void 发送群消息(int QQID, string msg)
        {
            cmd = cmd + "<群消息&群号=" + QQID + "&msg=" + msg + ">";
        }
        public void 发送私聊消息(int QQID, string msg)
        {
            cmd = cmd + "<私聊消息&QQ号=" + QQID + "&msg=" + msg + ">";
        }
        public void 发送讨论组消息(int QQID, string msg)
        {
            cmd = cmd + "<讨论组消息&讨论组=" + QQID + "&msg=" + msg + ">";
        }
        public void 发送赞(int QQID)
        {
            cmd = cmd + "<讨论组消息&被操作=" + QQID + ">";
        }
        public void 接收语音(string fileName, string postfixName)
        {
            cmd = cmd + "<接收语音&文件=" + fileName + "&msg=" + postfixName + ">";
        }
        #endregion
        #region CQ码
        public string CQ码_At(int QQID)
        {
            return "[CQ:at,qq=" + (QQID == -1 ? "all" : QQID.ToString()) + "]";
        }
        public string CQ码_emoji(int ID)
        {
            return "[CQ:emoji,id=" + ID + "]";
        }
        public string CQ码_表情(int ID)
        {
            return "[CQ:face,id=" + ID + "]";
        }
        public string CQ码_窗口抖动()
        {
            return "[CQ:shake]";
        }
        public string CQ码_匿名()
        {
            return "[CQ:anonymous]";
        }
        public string CQ码_匿名(bool force)
        {
            return "[CQ:anonymous" + (force ? ",ignore=true" : "") + "]";
        }
        public string CQ码_图片(string fileName)
        {
            return "[CQ:image,file=" + fileName + "]";
        }
        public string CQ码_音乐(int ID)
        {
            return "[CQ:music,id=" + ID + "]";
        }
        public string CQ码_语音(string fileName)
        {
            return "[CQ:record,file=" + fileName + "]";
        }

        #endregion
        #region 管理类
        public void 置群员禁言(int groupID, int QQID, int time)
        {
            cmd = cmd + "<禁言&群号=" + groupID + "&被操作=" + QQID + "&时长=" + time + ">";
        }
        public void 置群成员名片(int groupID, int QQID, string newName)
        {
            cmd = cmd + "<改名&群号=" + groupID + "&被操作=" + QQID + "&msg=" + newName + ">";
        }
        public void 置群成员专属头衔(int groupID, int QQID, string newName, int time)
        {
            cmd = cmd + "<改名&群号=" + groupID + "&被操作=" + QQID + "&msg=" + newName + "&时长=" + time + ">";
        }
        public void 置群员移除(int groupID, int QQID)
        {
            cmd = cmd + "<移除&群号=" + groupID + "&被操作=" + QQID + ">";
        }
        public void 置群员移除(int groupID, int QQID, bool refuse)
        {
            cmd = cmd + "<移除&群号=" + groupID + "&被操作=" + QQID + "&拒绝=" + refuse + ">";
        }
        public void 置好友添加请求(string react, int type)
        {
            cmd = cmd + "<加好友&反馈=" + react + "&时长=" + type + "&msg=%不备注%>";
        }
        public void 置好友添加请求(string react,int type,string name)
        {
            cmd = cmd + "<加好友&反馈=" + react+ "&时长=" + type + "&msg=" + name + ">";
        }
        public void 置群添加请求(string react, int src, int type, string reason)
        {
            cmd = cmd + "<加群&反馈=" + react + "&事件=" + src +"&时长=" + type + "&msg=" + reason + ">";
        }
        #endregion
        #region 预处理
        public static string 事件预处理(string EventType, int subType, int sendTime, int fromGroup, int fromDiscuss, int fromQQ,
            string fromAnonymous, int beingOperateQQ, string msg, int font, string responseFlag, string file)
        {
            myApp app = new myApp();
            string returntext = "";
            #region 事件自动分歧
            switch (EventType)
            {
                case "私聊消息":
                    returntext = app.PrivateMsg(subType, sendTime, fromQQ, msg, font);
                    break;
                case "群消息":
                    returntext = app.GroupMsg(subType, sendTime, fromGroup, fromQQ, fromAnonymous, msg, font);
                    break;
                case "讨论组消息":
                    returntext = app.DiscussMsg(subType, sendTime, fromDiscuss, fromQQ, msg, font);
                    break;
                case "群文件上传事件":
                    returntext = app.GroupUpload(subType, sendTime, fromQQ, msg, file);
                    break;
                case "群事件-管理员变动":
                    returntext = app.System_GroupAdmin(subType, sendTime, fromGroup, beingOperateQQ);
                    break;
                case "群事件-群成员减少":
                    returntext = app.GroupMemberDecrease(subType, sendTime, fromGroup, fromQQ, beingOperateQQ);
                    break;
                case "群事件-群成员增加":
                    returntext = app.GroupMemberIncrease(subType, sendTime, fromGroup, fromQQ, beingOperateQQ);
                    break;
                case "好友事件-好友已添加":
                    returntext = app.Friend_Added(subType, sendTime, fromQQ);
                    break;
                case "请求-好友添加":
                    returntext = app.Request_AddFriend(subType, sendTime, fromQQ, msg, responseFlag);
                    break;
                case "请求-群添加":
                    returntext = app.Request_AddGroup(subType, sendTime, fromGroup, fromQQ, msg, responseFlag);
                    break;
            }
            #endregion
            return returntext;
        }
        #endregion
    }
}
