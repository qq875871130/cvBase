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
            //LinkList<string>.Double linkList = new();
            //for (int i = 1; i <= 10; i++)
            //{
            //    linkList.Add(i + "", LinkList<string>.AddType.backward);
            //}
            //linkList.Add("8.5", 9, LinkList<string>.AddType.forward);
            //linkList.Add("5.5", 5, LinkList<string>.AddType.backward);
            //for (int i = 0; i < 5; i++)
            //{
            //    linkList.Add("117", LinkList<string>.AddType.backward);
            //}

            ////输出流
            //ListAll<string>(linkList);
            //foreach (var index in linkList.GetIndexes("117"))
            //{
            //    Console.Write(index + "\t");
            //}
            //Console.Write("\n");
            //linkList.Delete("117");
            //linkList.Delete(1, 5);
            //ListAll<string>(linkList);
            //linkList.Clear();
            //ListAll<string>(linkList);
        }

        public static void ListAll<T>(LinkList<T>.Double linkList)
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
