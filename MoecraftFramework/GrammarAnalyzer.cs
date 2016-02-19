using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoecraftFramework
{
    public class GrammerAnalyzer
    {
        //=========================================================================
        // 构造函数: 
        //   exp: 要分析的表达式
        //=========================================================================
        public GrammerAnalyzer(string exp)
        {
            expression = exp;
            index = 0;
            tokens = new List<Token>();
        }
        //=========================================================================
        // 表达式词法分析
        //=========================================================================
        public void Analyze()
        {
            tokens.Clear();
            index = 0;
            Token tok = null;
            while ((tok = GetToken()) != null)
            {
                tokens.Add(tok);
            }
        }
        //=========================================================================
        // 获取标记列表
        //=========================================================================
        public Token[] TokenList
        {
            get { return tokens.ToArray(); }
        }
        //=========================================================================
        // 分析后读取一个标记
        //=========================================================================
        private Token GetToken()
        {
            Token token = null;
            // 忽略空白字符
            while (HasChar && char.IsWhiteSpace(CurrentChar)) MoveNext();
            if (!HasChar) return null;
            if (CurrentChar == '(')             // 左括号
            {
                token = new LP();
            }
            else if (CurrentChar == ')')        // 右括号
            {
                token = new RP();
            }
            else if (IsOper(CurrentChar))       // 操作符
            {
                token = new Oper(CurrentChar);
            }
            else if (char.IsDigit(CurrentChar)) // 数字
            {
                bool dotFound = false;
                StringBuilder buf = new StringBuilder();
                do
                {
                    char c = CurrentChar;
                    if (dotFound && c == '.')
                    {
                        throw new Exception("无效的数字!");
                    }
                    buf.Append(c);
                    MoveNext();
                } while (HasChar && (char.IsDigit(CurrentChar) || CurrentChar == '.'));
                MovePrev(); // 回溯一次
                token = new Number(Convert.ToDouble(buf.ToString()));
            }
            else
            {
                throw new Exception("表达式中含有无效字符:'" + CurrentChar + "'!");
            }
            MoveNext();
            return token;
        }
        //=========================================================================
        // 是否操作符
        //=========================================================================
        private bool IsOper(char c)
        {
            string opers = "+-*/";
            return opers.IndexOf(c) != -1;
        }
        //=========================================================================
        // 是否还有可读字符
        //=========================================================================
        private bool HasChar
        {
            get { return index < expression.Length; }
        }
        //=========================================================================
        // 当前字符
        //=========================================================================
        private char CurrentChar
        {
            get { return expression[index]; }
        }
        //=========================================================================
        // 转到下一字符
        //=========================================================================
        private void MoveNext()
        {
            ++index;
        }
        //=========================================================================
        // 转到上一字符
        //=========================================================================
        private void MovePrev()
        {
            --index;
        }
        private int index;
        private string expression;
        private List<Token> tokens;
    }
}
