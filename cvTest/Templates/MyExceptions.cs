using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cvTest.Templates
{
    public class MyExceptions
    {
        public class NoCompatibleDSException : Exception
        {
            public NoCompatibleDSException() : base("没有匹配的数据结构用于操作！") { }
        }
        public class NoMethodDSException : Exception
        {
            public NoMethodDSException() : base("没有匹配的操作！") { }
        }
        public class InputException : Exception
        {
            public InputException() : base("输入有误！") { }
        }
        public class EmptyDSException : Exception
        {
            public EmptyDSException() : base("数据结构或对象为空，等待用户操作！") { }
        }
    }
}
