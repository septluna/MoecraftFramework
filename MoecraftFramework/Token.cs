using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoecraftFramework
{        
    //===========================================================================
    // 抽象标记
    //===========================================================================
    public abstract class Token
    {
        public abstract object Value { get; }
        public override string ToString()
        {
            return Value.ToString();
        }
    }
    //===========================================================================
    // 左括号
    //===========================================================================
    public class LP : Token
    {
        public override object Value
        {
            get
            {
                return '(';
            }
        }
    }
    //===========================================================================
    // 右括号
    //===========================================================================
    public class RP : Token
    {
        public override object Value
        {
            get
            {
                return ')';
            }
        }
    }
    //===========================================================================
    // 数值
    //===========================================================================
    public class Number : Token
    {
        private double val;
        public Number(double value)
        {
            val = value;
        }
        public override object Value
        {
            get
            {
                return val;
            }
        }
    }
    //===========================================================================
    // 操作符
    //===========================================================================
    public class Oper : Token
    {
        private char op;
        public Oper(char oper)
        {
            op = oper;
        }
        public override object Value
        {
            get
            {
                return op;
            }
        }
    }
}
