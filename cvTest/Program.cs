using System;
using cvBase.DS;
using cvTest.IO;

namespace cvTest
{
    class Program
    {
        static void Main()
        {
            string path = "..//..//..//DS/zh-CN.xml";
            CmdProcessor cmdProcessor = new CmdProcessor(path);
            cmdProcessor.Start();
        }

        public static void ListAll<T>(List.LinkList<T>.Double linkList)
        {
            Console.WriteLine(linkList.Length);
            T[] list = linkList.ToArray();
            for (int i = 0; i < list.Length; i++)
            {
                Console.WriteLine(i + 1 + "\t" + list[i]);
            }
        }
    }
}
