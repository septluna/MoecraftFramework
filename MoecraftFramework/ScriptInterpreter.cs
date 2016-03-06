using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MoecraftFramework
{
    public class MoeScript
    {
        #region 声明属性
        static string path { get; set; }
        internal static string[] script { get; set; }//这是用来储存脚本的数组
        internal static int mainIndicator { get; set; }//这是逻辑指针
        //脚本变量字典
        public static Dictionary<string, string> varsdic = new Dictionary<string, string>();
        #endregion        
        /// <summary>
        /// 这是moe脚本的解释器，分析语法用的
        /// </summary>
        public class Interpreter
        {
            string cmd { get; set; }
            public class logicObject
            {
                public static string name { get; set; }
                public static List<string> atb { get; set; }
            }
            public void ScriptAnalyze(string moepath)
            {
                script = fileReader(moepath);//载入脚本
                mainIndicator = 0;
                string rdGuid = System.Guid.NewGuid().ToString();
                for (; mainIndicator < script.Length; mainIndicator++)
                {
                    cmd = script[mainIndicator];//获取当前命令
                    if (cmd.IndexOf("\\<", 0) >= 0 || cmd.IndexOf("\\>", 0) >= 0)
                    {
                        rdGuid = System.Guid.NewGuid().ToString();
                        cmd = cmd.Replace("\\<", "@01@" + rdGuid + "@");
                        cmd = cmd.Replace("\\>", "@02@" + rdGuid + "@");
                        cmd = cmd.Replace("\\\\", "@03@" + rdGuid + "@");
                    }
                    //解析对象
                    if (cmd.IndexOf("<", 0) >= 0)
                    {
                        cmd = getReg("<[^<>]+>", cmd);//以"<"和">"，获得该行脚本
                        //对[和]进行预翻译
                        while (cmd.IndexOf("[", 0) >= 0 && cmd.IndexOf("]", 0) >= 0)
                        {
                            string s0 = getReg("\\[[^\\[\\]]+\\]", cmd);
                            string s1 = getReg("[^\\[\\]]+", s0);
                            varsdic.TryGetValue(s1, out s1);
                            cmd = cmd.Replace(s0, s1);
                        }
                        if (cmd.IndexOf(" ", 0) >= 0 || cmd.IndexOf("@01@", 0) >= 0 || cmd.IndexOf("@02@", 0) >= 0)
                        {
                            logicObject.name = getReg("\\w+ ", cmd);
                            logicObject.name = logicObject.name.Replace(" ", "");
                            cmd = cmd.Replace(logicObject.name, "");
                            cmd = cmd.Replace("@01@" + rdGuid + "@", "<");
                            cmd = cmd.Replace("@02@" + rdGuid + "@", ">");
                            cmd = cmd.Replace("@03@" + rdGuid + "@", "\\");
                            logicObject.atb = getRegs("[^\" ]+=\"[^\"]+\"", cmd);
                        }
                        else
                        {
                            cmd = cmd.Replace("<", "");
                            cmd = cmd.Replace(">", "");
                            logicObject.name = cmd;
                        }
                    }
                    flowControl.moeIf.estimate(logicObject.atb);
                    flowControl.moeIf.flow(logicObject.name);
                    flowControl.moeFunction.estimate(Interpreter.logicObject.atb);
                    flowControl.moeFunction.flow(Interpreter.logicObject.name);
                    if (flowControl.moeIf.flag)
                    {
                        Excuter.excute();
                    }
                    flowControl.moeFor.estimate(logicObject.atb);
                    flowControl.moeFor.flow(logicObject.name);
                }
            }
        }
        /// <summary>
        /// 这是单句的脚本语言的执行器
        /// </summary>
        public class Excuter
        {
            public static void excute()
            {
                switch (Interpreter.logicObject.name)
                {
                    case "文件读取":
                        ioInterface.moeReader rd = new ioInterface.moeReader();
                        rd.setValues(Interpreter.logicObject.atb);
                        rd.read();
                        break;
                    case "文件写入":
                        ioInterface.moeWriter wt = new ioInterface.moeWriter();
                        wt.setValues(Interpreter.logicObject.atb);
                        wt.write();
                        break;
                    case "设置变量":
                        ioInterface.moeVar sv = new ioInterface.moeVar();
                        sv.setValues(Interpreter.logicObject.atb);
                        sv.set();
                        break;
                    case "数组排序":
                        ioInterface.moeVar ss = new ioInterface.moeVar();
                        ss.setValues(Interpreter.logicObject.atb);
                        ss.setssort();
                        break;
                    case "新建画布":
                        canvas.moeCanvas cvc = new canvas.moeCanvas();
                        cvc.setValues(Interpreter.logicObject.atb);
                        cvc.reNew();
                        break;
                    case "绘制方形":
                        canvas.moeRectangle cvr = new canvas.moeRectangle();
                        cvr.graphType = "方形";
                        cvr.setValues(Interpreter.logicObject.atb);
                        cvr.draw();
                        break;
                    case "绘制圆形":
                        canvas.moeRectangle cve = new canvas.moeRectangle();
                        cve.graphType = "圆形";
                        cve.setValues(Interpreter.logicObject.atb);
                        cve.draw();
                        break;
                    case "绘制直线":
                        canvas.moeLine cvl = new canvas.moeLine();
                        cvl.graphType = "直线";
                        cvl.setValues(Interpreter.logicObject.atb);
                        cvl.draw();
                        break;
                    case "绘制弧线":
                        canvas.moeRectangle cva = new canvas.moeRectangle();
                        cva.graphType = "弧线";
                        cva.setValues(Interpreter.logicObject.atb);
                        cva.draw();
                        break;
                    case "绘制饼形":
                        canvas.moeRectangle cvp = new canvas.moeRectangle();
                        cvp.graphType = "饼形";
                        cvp.setValues(Interpreter.logicObject.atb);
                        cvp.draw();
                        break;
                    case "绘制曲线":
                        canvas.moeLine cvb = new canvas.moeLine();
                        cvb.graphType = "曲线";
                        cvb.setValues(Interpreter.logicObject.atb);
                        cvb.draw();
                        break;
                    case "绘制文本":
                        canvas.moeText cvt = new canvas.moeText();
                        cvt.setValues(Interpreter.logicObject.atb);
                        cvt.draw();
                        break;
                    case "绘制图片":
                        canvas.moeImage cvi = new canvas.moeImage();
                        cvi.setValues(Interpreter.logicObject.atb);
                        cvi.draw();
                        break;
                    case "清空画布":
                        canvas.gf.Clear(Color.FromArgb(0));
                        break;
                    case "图片另存":
                        canvas.moeSave cvs = new canvas.moeSave();
                        cvs.setValues(Interpreter.logicObject.atb);
                        break;
                }
            }
        }
        #region 辅助方法
        public static string clsHandle(string exp)
        {
            string result = "exp";
            char[] compair = { '+', '-', '*', '/' };
            string[] exptmp = exp.Split(',');
            List<string> rescom = new List<string>();
            foreach (var item in exptmp)
            {
                string expro = item;
                if (expro.IndexOf("-", 0) == 0)
                {
                    expro = "0" + expro;
                }
                #region 进入四则运算判断            
                if (item.IndexOfAny(compair, 0) > -1)
                {
                    try
                    {
                        GrammerAnalyzer ga = new GrammerAnalyzer(expro);
                        ga.Analyze();
                        Token[] toks = ga.TokenList;
                        SyntaxAnalyzer sa = new SyntaxAnalyzer(toks);
                        sa.Analyze();
                        Calculator calc = new Calculator(sa.SyntaxTree);
                        double value = calc.Calc();
                        rescom.Add(value.ToString());//加入到list中
                    }
                    catch
                    {

                    }
                }
                else
                {
                    rescom.Add(item.ToString());
                }
                #endregion
            }
            result = string.Join(",", rescom);
            return result;
        }
        public static string[] fileReader(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllLines(path, Encoding.GetEncoding("GB2312"));
            }
            else
            {
                string[] a = new string[1];
                a[0] = "初始化";
                return a;
            }
        }
        public static string getReg(string exp, string str)
        {
            string text = "";
            var reg = new Regex(exp, RegexOptions.IgnoreCase);
            var m = reg.Match(str);
            text = m.Value;
            return text;
        }
        public static List<string> getRegs(string exp, string str)
        {
            List<string> text = new List<string>();
            var reg = new Regex(exp, RegexOptions.IgnoreCase);
            MatchCollection m = reg.Matches(str);
            foreach (Match mc in m)
            {
                text.Add(mc.Value);
            }
            return text;
        }
        public static List<string> getString(string str1)
        {
            int startIndex = 0;
            int endIndex = str1.IndexOf("=", 0);
            List<string> lst = new List<string>();
            if (endIndex > 0)
            {
                string atbName = str1.Substring(0, endIndex);
                lst.Add(atbName);
                startIndex = str1.IndexOf("\"", 0);
                endIndex = str1.IndexOf("\"", startIndex + 1);
                string value = str1.Substring(startIndex + 1, endIndex - startIndex - 1);
                value = MoeScript.clsHandle(value);
                lst.Add(value);
            }
            return lst;
        }
        public static class Transformer
        {
            public static void EscapeCharacterEncode(string cmd, string rdGuid)
            {
                string charType = getReg("\\\\w+", cmd);

                switch (charType)
                {
                    default:
                        break;
                }


                cmd = cmd.Replace("\\<", "@01@" + rdGuid + "@");
                cmd = cmd.Replace("\\>", "@02@" + rdGuid + "@");
                cmd = cmd.Replace("\\\\", "@03@" + rdGuid + "@");
            }
            public static void EscapeCharacterDecode() { }
        }
        #endregion
    }
    public static class canvas
    {
        public static Bitmap bmp = new Bitmap(100, 100);
        public static Graphics gf = Graphics.FromImage(bmp);
        static string path = "无路径";
        public class moeCanvas
        {
            int endIndex;
            public void reNew()
            {
                gf = Graphics.FromImage(bmp);
                gf.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            }
            public void setValues(List<string> atb)
            {
                foreach (var str1 in atb)
                {
                    //初始化预设属性值
                    int height = 100;
                    int width = 100;
                    //判断是否需要赋值
                    endIndex = str1.IndexOf("=", 0);
                    if (endIndex > 0)
                    {
                        #region 设置逻辑对象属性
                        string atbName = MoeScript.getString(str1)[0];
                        string value = MoeScript.getString(str1)[1];
                        //设置属性
                        switch (atbName)
                        {
                            case "宽度":
                                height = bmp.Height;
                                int.TryParse(value, out width);
                                bmp = new Bitmap(width, height);
                                break;
                            case "高度":
                                int.TryParse(value, out height);
                                width = bmp.Width;
                                bmp = new Bitmap(width, height);
                                break;
                            default:
                                break;
                        }
                        #endregion
                    }
                }
            }
        }
        public class moeImage
        {
            Point position = new Point(0, 0);
            Point position1 = new Point(0, 0);
            Size size = new Size(10, 10);
            Size size1 = new Size(10, 10);
            Pen p = new Pen(Color.Black);
            Brush bs = new SolidBrush(Color.Black);
            int endIndex;
            static int rtDegree = 0;
            static Rectangle cut = new Rectangle();
            static bool imagecut = false;
            public void draw()
            {
                Image pic = Rotate.RotateImage(Image.FromFile(path), rtDegree);
                if (imagecut)
                {
                    cut.Width = pic.Width;
                    cut.Height = pic.Height;
                    gf.DrawImage(pic, cut);
                }
                else
                {
                    gf.DrawImage(pic, position);
                }
            }
            public void setValues(List<string> atb)
            {
                foreach (var str1 in atb)
                {
                    //判断是否需要赋值
                    endIndex = str1.IndexOf("=", 0);
                    if (endIndex > 0)
                    {
                        #region 设置逻辑对象属性
                        string atbName = MoeScript.getString(str1)[0];
                        string value = MoeScript.getString(str1)[1];
                        //设置属性
                        switch (atbName)
                        {
                            default:
                                break;
                            case "位置":
                                string[] pos = value.Split(',');
                                int X = 0;
                                int Y = 0;
                                int.TryParse(pos[0], out X);
                                int.TryParse(pos[1], out Y);
                                if (X < 0 || Y < 0)
                                {
                                    imagecut = true;
                                    cut.X = X;
                                    cut.Y = Y;
                                    X = 0;
                                    Y = 0;
                                }
                                position.X = X;
                                position.Y = Y;
                                break;
                            case "路径":
                                path = value;
                                break;
                            case "旋转":
                                int.TryParse(value, out rtDegree);
                                break;
                        }
                        #endregion
                    }
                }
            }
        }
        public class moeText
        {
            Point position = new Point(0, 0);
            Point position1 = new Point(0, 0);
            Size size = new Size(10, 10);
            Size size1 = new Size(10, 10);
            Pen p = new Pen(Color.Black);
            Brush bs = new SolidBrush(Color.Black);
            int endIndex;
            string words = "未定义";
            static FontFamily ff = new FontFamily("宋体");
            Font ft = new Font(ff, 10);
            public void draw()
            {
                gf.DrawString(words, ft, bs, position);
            }
            public void setValues(List<string> atb)
            {
                #region 处理文本的属性
                foreach (var str1 in atb)
                {
                    //判断是否需要赋值
                    endIndex = str1.IndexOf("=", 0);
                    if (endIndex > 0)
                    {
                        #region 设置逻辑对象属性
                        string atbName = MoeScript.getString(str1)[0];
                        string value = MoeScript.getString(str1)[1];
                        //设置属性
                        switch (atbName)
                        {
                            case "内容":
                                words = value;
                                words = words.Replace("\\s", " ");
                                break;
                            case "字体":
                                ff = new FontFamily(value);
                                break;
                            case "字号":
                                int em = 10;
                                int.TryParse(value, out em);
                                ft = new Font(ff, em);
                                break;
                            case "位置":
                                string[] pos = value.Split(',');
                                int X = 0;
                                int Y = 0;
                                int.TryParse(pos[0], out X);
                                position.X = X;
                                int.TryParse(pos[1], out Y);
                                position.Y = Y;
                                break;
                            case "颜色":
                                Color cltp = Color.Black;
                                if (value.IndexOf("#", 0) >= 0)
                                {
                                    cltp = ColorTranslator.FromHtml(value);
                                }
                                else
                                {
                                    cltp = Color.FromName(value);
                                }
                                bs = new SolidBrush(cltp);
                                break;
                            default:
                                break;
                        }
                        #endregion
                    }
                }

                #endregion
            }
        }
        public class moeLine
        {
            Point position = new Point(0, 0);
            Point position1 = new Point(0, 0);
            Point position2 = new Point(0, 0);
            Point position3 = new Point(0, 0);
            Point position4 = new Point(0, 0);
            Size size = new Size(10, 10);
            Size size1 = new Size(10, 10);
            Pen p = new Pen(Color.Black);
            Brush bs = new SolidBrush(Color.Black);
            int endIndex;
            public string graphType;
            public void draw()
            {
                switch (graphType)
                {
                    case "直线":
                        gf.DrawLine(p, position1, position4);
                        break;
                    case "曲线":
                        gf.DrawBezier(p, position1, position2, position3, position4);
                        break;
                }
            }
            public void setValues(List<string> atb)
            {
                foreach (var str1 in atb)
                {
                    //判断是否需要赋值
                    endIndex = str1.IndexOf("=", 0);
                    if (endIndex > 0)
                    {
                        #region 设置逻辑对象属性                                
                        string atbName = MoeScript.getString(str1)[0];
                        string value = MoeScript.getString(str1)[1];
                        //设置属性
                        switch (atbName)
                        {
                            case "位置":
                                string[] pos = value.Split(',');
                                int X = 0;
                                int Y = 0;
                                int.TryParse(pos[0], out X);
                                position1.X = X;
                                int.TryParse(pos[1], out Y);
                                position1.Y = Y;
                                int.TryParse(pos[2], out X);
                                position4.X = X;
                                int.TryParse(pos[3], out Y);
                                position4.Y = Y;
                                break;
                            case "扭曲":
                                string[] pos2 = value.Split(',');
                                int nX = 0;
                                int nY = 0;
                                int.TryParse(pos2[0], out nX);
                                position2.X = nX;
                                int.TryParse(pos2[1], out nY);
                                position2.Y = nY;
                                int.TryParse(pos2[2], out nX);
                                position3.X = nX;
                                int.TryParse(pos2[3], out nY);
                                position3.Y = nY;
                                break;
                            case "颜色":
                                if (value.IndexOf("#", 0) >= 0)
                                {
                                    p.Color = ColorTranslator.FromHtml(value);
                                }
                                else
                                {
                                    p.Color = Color.FromName(value);
                                }
                                break;
                            default:
                                break;
                        }
                        #endregion
                    }
                }
            }
        }
        public class moeRectangle
        {
            Point position = new Point(0, 0);
            Point position1 = new Point(0, 0);
            Size size = new Size(10, 10);
            Size size1 = new Size(10, 10);
            Pen p = new Pen(Color.Black);
            Brush bs = new SolidBrush(Color.Black);
            bool fill = false;
            bool transit = false;
            string fillType = "左上";
            LinearGradientMode transitType = LinearGradientMode.Horizontal;
            Color cltp = Color.Black;
            Color clts = Color.Black;
            Point transitStart = new Point(0, 0);
            Size transitSize = new Size(0, 0);
            int endIndex;
            float startAngle;
            float sweepAngle;
            public string graphType;
            public bool toPie = false;
            public void draw()
            {
                Rectangle rec = new Rectangle(position, size);
                if (fill)
                {
                    getOrigin(fillType);
                    Rectangle rec1 = new Rectangle(position1, size1);
                    if (transit)
                    {
                        Rectangle rec2 = new Rectangle(transitStart, transitSize);
                        bs = new LinearGradientBrush(rec2, cltp, clts, transitType);
                    }
                    switch (graphType)
                    {
                        case "圆形":
                            gf.FillEllipse(bs, rec1);
                            break;
                        case "方形":
                            gf.FillRectangle(bs, rec1);
                            break;
                        case "饼形":
                            gf.FillPie(bs, rec1, startAngle, sweepAngle);
                            break;
                    }
                    fill = false;
                    transit = false;
                }
                switch (graphType)
                {
                    case "圆形":
                        gf.DrawEllipse(p, rec);
                        break;
                    case "方形":
                        gf.DrawRectangle(p, rec);
                        break;
                    case "饼形":
                        gf.DrawPie(p, rec, startAngle, sweepAngle);
                        break;
                    case "弧线":
                        gf.DrawArc(p, rec, startAngle, sweepAngle);
                        break;
                }
            }
            public void setValues(List<string> atb)
            {
                foreach (var str1 in atb)
                {
                    //判断是否需要赋值
                    endIndex = str1.IndexOf("=", 0);
                    if (endIndex > 0)
                    {
                        #region 设置逻辑对象属性
                        string atbName = MoeScript.getString(str1)[0];
                        string value = MoeScript.getString(str1)[1];
                        //设置属性
                        switch (atbName)
                        {
                            case "位置":
                                string[] pos = value.Split(',');
                                int X = 0;
                                int Y = 0;
                                int.TryParse(pos[0], out X);
                                position.X = X;
                                int.TryParse(pos[1], out Y);
                                position.Y = Y;
                                break;
                            case "大小":
                                string[] sz = value.Split(',');
                                int ht = 10;
                                int wd = 10;
                                int.TryParse(sz[0], out wd);
                                size.Width = wd;
                                int.TryParse(sz[1], out ht);
                                size.Height = ht;
                                break;
                            case "角度":
                                string[] de = value.Split(',');
                                startAngle = 0;
                                sweepAngle = 30;
                                float.TryParse(de[0], out startAngle);
                                float.TryParse(de[1], out sweepAngle);
                                break;
                            case "颜色":
                                if (value.IndexOf("#", 0) >= 0)
                                {
                                    p.Color = ColorTranslator.FromHtml(value);
                                }
                                else
                                {
                                    p.Color = Color.FromName(value);
                                }
                                break;
                            case "填充色":
                                if (value.IndexOf("#", 0) >= 0)
                                {
                                    cltp = ColorTranslator.FromHtml(value);
                                }
                                else
                                {
                                    cltp = Color.FromName(value);
                                }
                                bs = new SolidBrush(cltp);
                                break;
                            case "渐变色":
                                transit = true;
                                if (value.IndexOf("#", 0) >= 0)
                                {
                                    clts = ColorTranslator.FromHtml(value);
                                }
                                else
                                {
                                    clts = Color.FromName(value);
                                }
                                break;
                            case "填充":
                                fill = true;
                                string[] tcsz = value.Split(',');
                                int tcht = 10;
                                int tcwd = 10;
                                int.TryParse(tcsz[0], out tcwd);
                                size1.Width = tcwd;
                                int.TryParse(tcsz[1], out tcht);
                                size1.Height = tcht;
                                break;
                            case "填充方式":
                                fillType = value;
                                break;
                            case "渐变方式":
                                switch (value)
                                {
                                    case "水平":
                                        transitType = LinearGradientMode.Vertical;
                                        break;
                                    case "垂直":
                                        transitType = LinearGradientMode.Horizontal;
                                        break;
                                    case "反斜向":
                                        transitType = LinearGradientMode.BackwardDiagonal;
                                        break;
                                    case "斜向":
                                        transitType = LinearGradientMode.ForwardDiagonal;
                                        break;
                                }
                                break;
                            case "渐变起点":
                                string[] ts = value.Split(',');
                                int tstemp = 0;
                                int.TryParse(ts[0], out tstemp);
                                transitStart.X = tstemp;
                                int.TryParse(ts[1], out tstemp);
                                transitStart.Y = tstemp;
                                break;
                            case "渐变区域":
                                string[] te = value.Split(',');
                                int tetemp = 0;
                                int.TryParse(te[0], out tetemp);
                                transitSize.Width = tetemp;
                                int.TryParse(te[1], out tetemp);
                                transitSize.Height = tetemp;
                                break;
                            default:
                                break;
                        }
                        #endregion
                    }
                }
            }
            void getOrigin(string type)
            {
                switch (type)
                {
                    case "左上":
                        position1.X = position.X;
                        position1.Y = position.Y;
                        break;
                    case "右上":
                        position1.X = position.X + size.Width - size1.Width;
                        position1.Y = position.Y;
                        break;
                    case "右下":
                        position1.X = position.X + size.Width - size1.Width;
                        position1.Y = position.Y + size.Height - size1.Height;
                        break;
                    case "左下":
                        position1.X = position.X;
                        position1.Y = position.Y + size.Height - size1.Height;
                        break;
                    case "中心":
                        position1.X = position.X + size.Width / 2 - size1.Width / 2;
                        position1.Y = position.Y + size.Height / 2 - size1.Height / 2;
                        break;
                }
            }
        }
        public class moeSave
        {
            int endIndex;
            public void setValues(List<string> atb)
            {
                foreach (var str1 in atb)
                {
                    //判断是否需要赋值
                    endIndex = str1.IndexOf("=", 0);
                    if (endIndex > 0)
                    {
                        #region 设置逻辑对象属性
                        string atbName = MoeScript.getString(str1)[0];
                        string value = MoeScript.getString(str1)[1];
                        //设置属性
                        switch (atbName)
                        {
                            case "路径":
                                bmp.Save(value);

                                break;
                            default:
                                break;
                        }
                        #endregion
                    }
                }
            }
        }
    }
    internal static class ioInterface
    {
        public class moeReader
        {
            int endIndex;
            string varname = "变量" + MoeScript.varsdic.Count;
            string strpath = "";
            string strsection = "";
            string strkey = "";
            string strtext = "默认文本";
            public void setValues(List<string> atb)
            {
                foreach (var str1 in atb)
                {
                    //初始化预设属性值
                    //判断是否需要赋值
                    endIndex = str1.IndexOf("=", 0);
                    if (endIndex > 0)
                    {
                        #region 设置逻辑对象属性
                        string atbName = MoeScript.getString(str1)[0];
                        string value = MoeScript.getString(str1)[1];
                        //设置属性
                        switch (atbName)
                        {
                            case "名字":
                                varname = value;
                                break;
                            case "路径":
                                strpath = value;
                                break;
                            case "键名":
                                strsection = value;
                                break;
                            case "值名":
                                strkey = value;
                                break;
                            case "内容":
                                strtext = value;
                                break;
                            default:
                                break;
                        }
                        #endregion
                    }
                }
            }
            public void read()
            {
                strtext = FileHandle.INIGetStringValue(strpath, strsection, strkey, strtext);
                if (MoeScript.varsdic.ContainsKey(varname))
                {
                    MoeScript.varsdic.Remove(varname);
                    MoeScript.varsdic.Add(varname, strtext);
                }
                else
                {
                    MoeScript.varsdic.Add(varname, strtext);
                }
            }
        }
        public class moeWriter
        {
            int endIndex;
            string strpath = "";
            string strsection = "";
            string strkey = "";
            string strtext = "默认文本";
            public void setValues(List<string> atb)
            {
                foreach (var str1 in atb)
                {
                    //初始化预设属性值
                    //判断是否需要赋值
                    endIndex = str1.IndexOf("=", 0);
                    if (endIndex > 0)
                    {
                        #region 设置逻辑对象属性
                        string atbName = MoeScript.getString(str1)[0];
                        string value = MoeScript.getString(str1)[1];
                        //设置属性
                        switch (atbName)
                        {
                            case "路径":
                                strpath = value;
                                break;
                            case "键名":
                                strsection = value;
                                break;
                            case "值名":
                                strkey = value;
                                break;
                            case "内容":
                                strtext = value;
                                break;
                            default:
                                break;
                        }
                        #endregion
                    }
                }
            }
            public void write()
            {
                FileHandle.INIWriteValue(strpath, strsection, strkey, strtext);
            }
        }
        public class moeVar
        {
            int endIndex;
            string tname;
            string tvalue;
            char split = '@';
            string[] sLine;
            List<string> temp = new List<string>();
            public void setValues(List<string> atb)
            {
                temp.Clear();
                foreach (var str1 in atb)
                {
                    //初始化预设属性值
                    //判断是否需要赋值
                    endIndex = str1.IndexOf("=", 0);
                    if (endIndex > 0)
                    {
                        #region 设置逻辑对象属性
                        string atbName = MoeScript.getString(str1)[0];
                        string value = MoeScript.getString(str1)[1];
                        //设置属性
                        switch (atbName)
                        {
                            case "名字":
                                tname = value;
                                break;
                            case "内容":
                                tvalue = value;
                                break;
                            case "分隔符":
                                split = value.ToCharArray()[0];
                                break;
                        }                    
                        #endregion
                    }
                }
            }
            public void set()
            {
                sLine = tvalue.Split(split);
                temp.AddRange(sLine);
                int i = 1;
                if (split == '@')
                {
                    if (MoeScript.varsdic.ContainsKey(tname))
                    {
                        MoeScript.varsdic[tname] = tvalue;
                    }
                    else
                    {
                        MoeScript.varsdic.Add(tname, tvalue);
                    }
                }
                else
                {
                    foreach (var item in temp)
                    {
                        if (MoeScript.varsdic.ContainsKey(tname + 1))
                        {
                            MoeScript.varsdic[tname + i] = item;
                        }
                        else
                        {
                            MoeScript.varsdic.Add(tname + i, item);
                        }
                        i++;
                    }
                }
            }
            public void setssort()
            {
                int i = 1;
                while (MoeScript.varsdic.ContainsKey(tname + i))
                {
                    temp.Add(MoeScript.varsdic[tname + i]);
                    i++;
                }
                temp.Sort();
                i = 1;
                foreach (var item in temp)
                {
                    MoeScript.varsdic[tname + i] = item;
                    i++;
                }
            }
        }
    }
    internal static class flowControl
    {
        internal class moeIf
        {
            public static List<bool> result = new List<bool>();
            public static bool resTemp;
            public static bool flag = true;//最终结果
            public static void flow(string keyword)
            {
                switch (keyword)
                {
                    default:

                        break;
                    case "否则":
                        if (result.Count != 0)
                        {
                            result[result.Count - 1] = (result[result.Count - 1] == true ? false : true);
                        }
                        break;
                    case "结束":
                        if (result.Count != 0)
                        {
                            result.Remove(result[result.Count - 1]);
                        }
                        flag = true;
                        break;
                    case "如果":
                        result.Add(resTemp);
                        break;
                }
                foreach (var item in result)
                {
                    flag = (item == true ? true : false);
                }
            }
            public static void estimate(List<string> atb)
            {
                int endIndex;
                string A = "";
                int a = 0;
                string B = "";
                int b = 0;
                string C = "等于";
                if (atb != null)
                {
                    foreach (var str1 in atb)
                    {
                        //初始化预设属性值
                        //判断是否需要赋值
                        endIndex = str1.IndexOf("=", 0);
                        if (endIndex > 0)
                        {
                            #region 设置逻辑对象属性
                            string atbName = MoeScript.getString(str1)[0];
                            string value = MoeScript.getString(str1)[1];
                            //设置属性
                            switch (atbName)
                            {
                                case "A":
                                    A = value;
                                    break;
                                case "B":
                                    B = value;
                                    break;
                                case "条件":
                                    C = value;
                                    break;
                                default:
                                    break;
                            }
                            #endregion
                        }
                    }
                    int.TryParse(A, out a);
                    int.TryParse(B, out b);
                    #region 这里进行判断
                    switch (C)
                    {
                        case "等于":
                            if (A == B) { resTemp = true; } else { resTemp = false; }
                            break;
                        case "不等于":
                            if (A != B) { resTemp = true; } else { resTemp = false; }
                            break;
                        case "大于":
                            if (a > b) { resTemp = true; } else { resTemp = false; }
                            break;
                        case "小于":
                            if (a < b) { resTemp = true; } else { resTemp = false; }
                            break;
                        case "大于等于":
                            if (a >= b) { resTemp = true; } else { resTemp = false; }
                            break;
                        case "小于等于":
                            if (a <= b) { resTemp = true; } else { resTemp = false; }
                            break;
                        case "不大于":
                            if (a <= b) { resTemp = true; } else { resTemp = false; }
                            break;
                        case "不小于":
                            if (a >= b) { resTemp = true; } else { resTemp = false; }
                            break;
                    }
                    #endregion}
                }
            }
        }
        internal class moeFor
        {
            public static List<int> index = new List<int>();//这里是返回点
            public static List<int> times = new List<int>();//这里是执行次数
            public static void flow(string keyword)
            {
                switch (keyword)
                {
                    default:
                        break;
                    case "循环":
                        index.Add(MoeScript.mainIndicator);
                        break;
                    case "跳出":
                        if (index.Count != 0 && times.Count != 0)
                        {
                            if (times[times.Count - 1] > 1)
                            {
                                MoeScript.mainIndicator = index[index.Count - 1];
                                times[times.Count - 1]--;
                            }
                            else
                            {
                                times.Remove(times[times.Count - 1]);
                                index.Remove(index[index.Count - 1]);
                            }
                        }
                        break;
                }
            }
            public static void estimate(List<string> atb)
            {
                int endIndex;
                int tryTimes = 0;//这是声明执行次数
                if (atb != null)
                {
                    foreach (var str1 in atb)
                    {
                        //初始化预设属性值
                        //判断是否需要赋值
                        endIndex = str1.IndexOf("=", 0);
                        if (endIndex > 0)
                        {
                            #region 设置逻辑对象属性
                            string atbName = MoeScript.getString(str1)[0];
                            string value = MoeScript.getString(str1)[1];
                            //设置属性
                            switch (atbName)
                            {
                                case "次数":
                                    if (MoeScript.Interpreter.logicObject.name == "循环")
                                    {
                                        int.TryParse(value, out tryTimes);
                                        times.Add(tryTimes);
                                    }
                                    break;
                            }
                            #endregion
                        }
                    }
                }
            }
        }
        internal class moeFunction
        {
            public static List<int> index = new List<int>();//这里是返回点
            public static Dictionary<string, int> fDic = new Dictionary<string, int>();
            static string fName;
            static int jmp = 0;
            public static void flow(string keyword)
            {
                string tname = fName;
                int tvalue = MoeScript.mainIndicator;
                switch (keyword)
                {
                    case "声明方法":
                        moeIf.flag = false;
                        if (moeFor.index.Count == 0 || moeIf.result.Count == 0)
                        {
                            if (fDic.ContainsKey(tname))
                            {
                                //方法名重复                                
                                fDic[tname] = tvalue;
                            }
                            else
                            {
                                fDic.Add(tname, tvalue);
                            }
                            tvalue = 0;
                        }
                        break;
                    case "调用方法":
                        if (moeIf.flag)
                        {
                            if (fDic.ContainsKey(tname))
                            {
                                jmp = fDic[tname];
                                index.Add(MoeScript.mainIndicator);
                                if (jmp != 0)
                                {
                                    MoeScript.mainIndicator = jmp;
                                }
                                jmp = 0;
                            }
                            else
                            {
                                for (int i = MoeScript.mainIndicator; i < MoeScript.script.Length; i++)
                                {
                                    if (MoeScript.script[i].IndexOf("声明方法") > 0)
                                    {
                                        if (MoeScript.script[i].IndexOf("名字=" + "\"" + tname + "\"") > 0)
                                        {
                                            MoeScript.mainIndicator = i;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "返回":
                        if (index.Count != 0)
                        {
                            MoeScript.mainIndicator = index[index.Count - 1];
                            index.Remove(index[index.Count - 1]);
                        }
                        else
                        {
                            moeIf.flag = true;
                        }
                        break;
                }
            }
            public static void estimate(List<string> atb)
            {
                int endIndex;
                if (atb != null)
                {
                    foreach (var str1 in atb)
                    {
                        //初始化预设属性值
                        //判断是否需要赋值
                        endIndex = str1.IndexOf("=", 0);
                        if (endIndex > 0)
                        {
                            #region 设置逻辑对象属性
                            string atbName = MoeScript.getString(str1)[0];
                            string value = MoeScript.getString(str1)[1];
                            //设置属性
                            switch (atbName)
                            {
                                case "名字":
                                    fName = value;
                                    break;
                            }
                            #endregion
                        }
                    }
                }
            }
        }
    }
}
