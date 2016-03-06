//using MySql.Data;
//using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Sql;
//using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace MoecraftFramework
{
    //你可以在这里自己写常用的那些方法库    
    public class 正则
    {
        public static string 匹配(string exp, string str)
        {
            return 匹配(exp, str, "");
        }
        public static string 匹配(string exp, string str, string split)
        {
            List<string> result = new List<string>();
            var reg = new Regex(exp, RegexOptions.IgnoreCase);
            var m = reg.Matches(str);
            foreach (Match mc in m)
            {
                result.Add(mc.Value);
            }
            return string.Join(split, result);
        }
        public static string 替换(string exp, string str, string rex)
        {
            return 替换(exp, str, rex, -1);
        }
        public static string 替换(string exp, string str, string rex, int times)
        {
            List<string> result = new List<string>();
            var reg = new Regex(exp, RegexOptions.IgnoreCase);
            if (times < 0)
            {
                return reg.Replace(str, rex);
            }
            else
            {
                return reg.Replace(str, rex, times);
            }
        }
    }
    public class 易语言
    {
        public static string 读配置项(string 配置文件名, string 节名称, string 配置项名称, string 默认文本)
        {
            return FileHandle.INIGetStringValue(配置文件名, 节名称, 配置项名称, 默认文本);
        }
        public static bool 写配置项(string 配置文件名, string 节名称, string 配置项名称, string 欲写入值)
        {
            return FileHandle.INIWriteValue(配置文件名, 节名称, 配置项名称, 欲写入值);
        }
        public static List<string> 取配置节点(string 配置文件名)
        {
            return FileHandle.INIGetAllSectionNames(配置文件名).ToList();
        }
    }
    //数据库支持使用mysql和sqlite
    //这里不提供实际方法，请确保你明白sql语句的正确使用方法
    public class 数据库
    {
        public static void sqlite(string dbPath, string cmd)
        {
            //SQLiteConnection conn = null;
            //conn = new SQLiteConnection(dbPath);//声明db文件路径
            //conn.Open();//打开连接
            //SQLiteCommand execute = new SQLiteCommand(cmd, conn);//声明sql命令
            //execute.ExecuteNonQuery();//执行命令
            //conn.Close();//关闭连接
        }
        public static void mysql(string dbPath,string cmd)
        {
            //string constr = "server=localhost;User Id=root;password=23333;Database=reg";
            //MySqlConnection mycon = new MySqlConnection(constr);
            //mycon.Open();
            //MySqlCommand mycmd = new MySqlCommand(cmd, mycon);
            //mycmd.ExecuteNonQuery();
            //mycon.Close();
        }

    }
    public class moe脚本
    {
        //在这里调用moe脚本
        public static void 执行(string path,string cqpath,int fromQQ)
        {
            //moe脚本是免费项目，但目前不开放→_→
        } 
    }
}
