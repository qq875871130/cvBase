using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cvTest.Event;

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
        /// 输出信息函数
        /// </summary>
        /// <param name="msg">待输出信息</param>
        /// <param name="writeState">输出种类</param>
        /// <param name="isWrap">是否换行读取输入</param>
        public static void Write(string msg, WriteState writeState, bool isWrap)
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
            if (isWrap)
            {
                Console.WriteLine(msg);
            }
            else
                Console.Write(msg);
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
        /// 返回用户输入值
        /// </summary>
        /// <typeparam name="T">输入类型</typeparam>
        /// <returns></returns>
        public static T Read<T>()
        {
            string result = Console.ReadLine();
            return (T)Convert.ChangeType(result, typeof(T));
        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 提示消息并返回用户输入值
        /// </summary>
        /// <typeparam name="T">输入类型</typeparam>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        public static T Read<T>(string msg, WriteState writeState, bool isWrap)
        {
            Write(msg, writeState, isWrap);
            return Read<T>();
        }
        /// <summary>
        /// 流式输入函数
        /// </summary>
        /// <typeparam name="T">用户输入数据类型</typeparam>
        /// <param name="description">输入前提示消息</param>
        /// <param name="method">接受每次输入的委托方法</param>
        public static void ReadStream<T>(string description, Method<T> method, WriteState writeState)
        {
            int count = Read<int>(description, writeState, false);
            for (int i = 1; i <= count; i++)
            {
                Write("输入第" + i + "个数据：", WriteState.no_clear, false);
                method(Read<T>());
            }
        }
        /// <summary>
        /// 多参数流式输入
        /// </summary>
        /// <typeparam name="T">输入类型</typeparam>
        /// <typeparam name="K">额外参数</typeparam>
        /// <param name="description">描述提示</param>
        /// <param name="method">委托方法</param>
        /// <param name="extraParam">额外参数</param>
        /// <param name="writeState">消息输出方式</param>
        public static void ReadStream<T, K>(string description, Method<T, K> method, K extraParam, WriteState writeState)
        {
            int count = Read<int>(description, writeState, false);
            for (int i = 1; i <= count; i++)
            {
                Write("输入第" + i + "个数据：", WriteState.no_clear, false);
                method(Read<T>(), extraParam);
            }
        }
        /// <summary>
        /// 多参数流式输入
        /// </summary>
        /// <typeparam name="T">输入类型</typeparam>
        /// <typeparam name="K">额外类型1</typeparam>
        /// <typeparam name="V">额外类型2</typeparam>
        /// <param name="description">描述提示</param>
        /// <param name="method">委托方法</param>
        /// <param name="extraParam1">额外参数1</param>
        /// <param name="extraParam2">额外参数2</param>
        /// <param name="writeState">消息输出方式</param>
        public static void ReadStream<T, K, V>(string description, Method<T, K, V> method, K extraParam1, V extraParam2, WriteState writeState)
        {
            int count = Read<int>(description, writeState, false);
            for (int i = 1; i <= count; i++)
            {
                Write("输入第" + i + "个数据：", WriteState.no_clear, false);
                method(Read<T>(), extraParam1, extraParam2);
            }
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
        /// <summary>
        /// 提示消息并以 Y/N 形式获取用户布尔指令
        /// </summary>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        public static bool GetInput(string msg)
        {
            Write(msg + "(Y/N)", WriteState.no_clear);
            return GetInput();
        }
    }

}
