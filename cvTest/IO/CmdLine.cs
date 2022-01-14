using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cvTest.IO
{
    public static class CmdLine
    {
        public enum WriteState
        {
            clear,
            no_clear
        }
        public static void Write(string msg, WriteState writeState)
        {
            switch (writeState)
            {
                case WriteState.clear:
                    Console.Clear();
                    break;
                case WriteState.no_clear:
                    break;
                default:
                    break;
            }
            Console.WriteLine(msg);
        }
        public static void Write(string msg)
        {
            Write(msg, WriteState.clear);
        }
        //输出格式化类
        public static class Formatter
        {
            public enum Align
            {
                Right,
                Left,
                Center
            }
            //文字对齐
            public static string Aligner(string text, int step, Align align)
            {
                string result = text;
                switch (align)
                {
                    case Align.Right:
                        result = string.Format("{0," + step + "}", result);
                        break;
                    case Align.Left:
                        result = string.Format("{0," + (-1) * step + "}", result);
                        break;
                    case Align.Center:
                        result = string.Format("{0," + (-1) * step + "}", string.Format("{0," + step + "}", result));
                        break;
                    default:
                        break;
                }
                return result;
            }
            public static void Lister(string col1, string col2)
            {
                string Col1 = CmdLine.Formatter.Aligner(col1, 5, CmdLine.Formatter.Align.Left);
                string Col2 = CmdLine.Formatter.Aligner(col2, 5, CmdLine.Formatter.Align.Right);
                CmdLine.Write(Col1 + Col2, CmdLine.WriteState.no_clear);
            }

        }
        public static bool GetInput()
        {
            switch (Console.ReadLine())
            {
                case "Y":
                case "y":
                    return true;
                case "N":
                case "n":
                    return false;
                default:
                    return false;
            }

        }
    }

}
