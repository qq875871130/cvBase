using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cvTest.IO
{
    /// <summary>
    /// 控制台输出类
    /// </summary>
    public static class CmdLine
    {
        /// <summary>
        /// 输出种类
        /// </summary>
        public enum WriteState
        {
            /// <summary>
            /// 洁净输出
            /// </summary>
            clear,
            no_clear
        }
        /// <summary>
        /// 输出信息函数
        /// </summary>
        /// <param name="msg">待输出信息</param>
        /// <param name="writeState">输出种类</param>
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
        /// <summary>
        /// 默认洁净输出
        /// </summary>
        /// <param name="msg"></param>
        public static void Write(string msg)
        {
            Write(msg, WriteState.clear);
        }
        /// <summary>
        ///输出格式化类
        /// </summary>
        public static class Formatter
        {
            /// <summary>
            /// 对齐方式
            /// </summary>
            public enum Align
            {
                Right,
                Left,
                Center
            }
            /// <summary>
            /// 对齐函数
            /// </summary>
            /// <param name="text">待格式化数据</param>
            /// <param name="step">占位步长</param>
            /// <param name="align">对齐方式</param>
            /// <returns>格式化结果</returns>
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
            /// <summary>
            /// 菜单列表项输出函数
            /// </summary>
            /// <param name="col1">第一列数据</param>
            /// <param name="col2">第二列数据</param>
            public static void Lister(string col1, string col2)
            {
                //分别一左一右对齐
                string Col1 = CmdLine.Formatter.Aligner(col1, 5, CmdLine.Formatter.Align.Left);
                string Col2 = CmdLine.Formatter.Aligner(col2, 5, CmdLine.Formatter.Align.Right);
                //不洁净输出
                CmdLine.Write(Col1 + Col2, CmdLine.WriteState.no_clear);
            }

        }
        /// <summary>
        /// Y/N 形式获取用户布尔指令
        /// </summary>
        /// <returns>是与否</returns>
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
