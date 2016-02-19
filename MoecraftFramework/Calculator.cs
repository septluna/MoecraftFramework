using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoecraftFramework
{
    //===========================================================================
    // 表达式计算器
    //===========================================================================
    public class Calculator
    {
        //=========================================================================
        // 构造函数
        //   tree: 表达式语法树, 由SyntaxAnalyzer创建;
        //=========================================================================
        public Calculator(SyntaxTreeNode tree)
        {
            this.tree = tree;
            log = new StringBuilder();
        }
        //=========================================================================
        // 计算表达式值
        public double Calc()
        {
            if (tree == null)
            {
                throw new Exception("计算树为空!");
            }
            log.Remove(0, log.Length);
            return Travel(tree);
        }
        //=========================================================================
        // 求解过程日志
        //=========================================================================
        public string Log
        {
            get { return log.ToString(); }
        }
        //=========================================================================
        // 遍历表达式分析树,求取结果
        //=========================================================================
        private double Travel(SyntaxTreeNode tree)
        {
            if (tree.Token is Number)
            {
                return (double)tree.Token.Value;
            }
            double lsh = Travel(tree.LChild);
            double rsh = Travel(tree.RChild);
            return Op((char)tree.Token.Value, lsh, rsh);
        }
        //=========================================================================
        // 实际计算操作
        //=========================================================================
        private double Op(char op, double lsh, double rsh)
        {
            double rslt = 0;
            switch (op)
            {
                case '+':
                    rslt = lsh + rsh;
                    break;
                case '-':
                    rslt = lsh - rsh;
                    break;
                case '*':
                    rslt = lsh * rsh;
                    break;
                case '/':
                    if (rsh == 0)
                    {
                        throw new Exception("除0溢出!");
                    }
                    rslt = lsh / rsh;
                    break;
                default:
                    throw new Exception("未能识别的操作类型!");
            }

            log.AppendFormat("{0,5}{1,5}{2,5}={3,5}/n", lsh, op, rsh, rslt);
            return rslt;
        }
        private SyntaxTreeNode tree;
        private StringBuilder log;
    }
}
