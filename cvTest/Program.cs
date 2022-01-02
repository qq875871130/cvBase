using System;
using cvBase.DS;

namespace cvTest
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkList<string> linkList = new LinkList<string>();
            for (int i = 1; i <= 10; i++)
            {
                linkList.Add(i+"", LinkList<string>.AddType.backward);
            }
            linkList.Add("8.5", 9, LinkList<string>.AddType.forward);
            linkList.Add("5.5", 5, LinkList<string>.AddType.backward);
            for (int i = 0; i < 5; i++)
            {
                linkList.Add("117", LinkList<string>.AddType.backward);
            }
            Console.WriteLine(linkList.Length);
            for (int j = 1; j <= linkList.Length; j++)
            {
                Console.WriteLine(j+"\t"+linkList.GetData(j));
            }
            foreach (var index in linkList.GetIndexes("117"))
            {
                Console.Write(index + "\t");
            }
        }
    }
}
