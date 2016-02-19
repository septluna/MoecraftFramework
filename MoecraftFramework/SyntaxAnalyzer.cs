using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoecraftFramework
{
    //===========================================================================
    // 语法树结点
    //===========================================================================
    public class SyntaxTreeNode
    {
        public SyntaxTreeNode(Token tok,
          SyntaxTreeNode lc,
          SyntaxTreeNode rc,
          int nest)
        {
            token = tok;
            lchild = lc;
            rchild = rc;
            this.nest = nest;
        }
        //=========================================================================
        // 标记
        //=========================================================================
        public Token Token
        {
            set { token = value; }
            get { return token; }
        }
        //=========================================================================
        // 左孩子
        //=========================================================================
        public SyntaxTreeNode LChild
        {
            set { lchild = value; }
            get { return lchild; }
        }
        //=========================================================================
        // 右孩子
        //=========================================================================
        public SyntaxTreeNode RChild
        {
            set { rchild = value; }
            get { return rchild; }
        }
        //=========================================================================
        // 嵌套层数
        //=========================================================================
        public int Nest
        {
            set { nest = value; }
            get { return nest; }
        }
        private Token token;
        private SyntaxTreeNode lchild;
        private SyntaxTreeNode rchild;
        private int nest;
    }
    //===========================================================================
    // 语法分析器
    //===========================================================================
    public class SyntaxAnalyzer
    {
        public SyntaxAnalyzer(Token[] toks)
        {
            tokens = toks;
            stack = new Stack<SyntaxTreeNode>();
            tree = null;
            index = 0;
        }
        //=========================================================================
        // 获取语法树
        //=========================================================================  
        public SyntaxTreeNode SyntaxTree
        {
            get { return tree; }
        }
        //=========================================================================
        // 分析表达式
        //=========================================================================  
        public void Analyze()
        {
            // 还原初始状态
            stack.Clear();
            index = 0;
            tree = null;
            nest = 0;
            if (!HasToken) return;
            // 表达式必须以数值或左括号开头
            if (CurrentToken is Number)
            {
                N(ref tree);
            }
            else if (CurrentToken is LP)
            {
                Lp(ref tree);
            }
            else
            {
                throw new Exception("无效表达式! 表达式只能数值或左括号开头!");
            }
            if (stack.Count != 0)
            {
                throw new Exception("无效表达式! 左右括号不配对");
            }
        }
        //=========================================================================
        // 数值标记分析
        //=========================================================================  
        private void N(ref SyntaxTreeNode curr)
        {
            SyntaxTreeNode node = new SyntaxTreeNode(CurrentToken,
              null,
              null,
              nest);
            if (curr == null)
            {
                curr = node;
            }
            else
            {
                if (curr.RChild != null)
                {
                    curr.RChild.RChild = node;
                }
                else
                {
                    curr.RChild = node;
                }
            }
            MoveNext();
            // 数值后的标记必须是操作符或右括号
            if (!HasToken) return;
            if (CurrentToken is Oper)
            {
                Op(ref curr);
            }
            else if (CurrentToken is RP)
            {
                Rp(ref curr);
            }
            else
            {
                throw new Exception("无效表达式! 数值之后发现标记:'"
                  + CurrentToken.Value + "'!");
            }
        }
        //=========================================================================
        // 左括号分析
        //=========================================================================
        private void Lp(ref SyntaxTreeNode curr)
        {
            stack.Push(curr);     // 压入括号外分析树
            curr = null;          // 清空分析树
            ++nest;               // 嵌套层增加
            MoveNext();
            if (!HasToken) return;
            // 左括号后必须是数值或左括号
            if (CurrentToken is Number)
            {
                N(ref curr);
            }
            else if (CurrentToken is LP)
            {
                Lp(ref curr);
            }
            else
            {
                throw new Exception("无效表达式! 左括号之后发现标记:'"
                  + CurrentToken.Value + "'!");
            }
        }
        //=========================================================================
        // 右括号分析
        //=========================================================================
        private void Rp(ref SyntaxTreeNode curr)
        {
            if (stack.Count == 0)
            {
                throw new Exception("无效表达式! 左右括号不配对!");
            }
            --nest; // 嵌套层递减
            SyntaxTreeNode node = stack.Pop();
            if (node != null)
            {
                // 将括号内表达式树作为子树挂到括号外表达式树上
                if (node.RChild != null)
                {
                    node.RChild.RChild = curr;
                    curr = node;
                }
                else
                {
                    node.RChild = curr;
                    curr = node;
                }
            }
            MoveNext();
            if (!HasToken) return;
            // 右括号后面必须是操作符或右括号
            if (CurrentToken is Oper)
            {
                Op(ref curr);
            }
            else if (CurrentToken is RP)
            {
                Rp(ref curr);
            }
            else
            {
                throw new Exception("无效表达式! 右括号之后发现标记:'"
                  + CurrentToken.Value + "'!");
            }
        }
        //=========================================================================
        // 操作符分析
        //=========================================================================
        private void Op(ref SyntaxTreeNode curr)
        {
            SyntaxTreeNode node = new SyntaxTreeNode(CurrentToken, null, null, nest);
            // 只有位于同一嵌套层中的操作符有优先级可比性
            if ((curr.Token is Oper)
              && (node.Nest == curr.Nest)
              && IsPrior((char)CurrentToken.Value, (char)curr.Token.Value) > 0)
            {
                node.LChild = curr.RChild;
                curr.RChild = node;
            }
            else
            {
                node.LChild = curr;
                curr = node;
            }
            MoveNext();
            // 操作符之后必须是数或左括号
            if (CurrentToken is Number)
            {
                N(ref curr);
            }
            else if (CurrentToken is LP)
            {
                Lp(ref curr);
            }
            else
            {
                throw new Exception("无效表达式! 操作符之后发现标记:'"
                  + CurrentToken.Value + "'!");
            }
        }
        //=========================================================================
        // 比较操作符优先级
        //=========================================================================
        private int IsPrior(char op1, char op2)
        {
            int rslt = 0;
            if (op1 == '+' || op1 == '-')
            {
                if (op2 == '*' || op2 == '/')
                    rslt = -1;
            }
            else if (op1 == '*' || op1 == '/')
            {
                if (op2 == '+' || op2 == '-')
                    rslt = 1;
            }
            return rslt;
        }
        //=========================================================================
        // 是否还有标记可读
        //=========================================================================
        private bool HasToken
        {
            get { return index < tokens.Length; }
        }
        //=========================================================================
        // 转到下一标记
        //=========================================================================
        private void MoveNext()
        {
            ++index;
        }
        //=========================================================================
        // 当前标记
        //=========================================================================  
        private Token CurrentToken
        {
            get { return (Token)tokens[index]; }
        }
        private int index;
        private int nest;
        private SyntaxTreeNode tree;
        private Token[] tokens;
        private Stack<SyntaxTreeNode> stack;
    }
}
