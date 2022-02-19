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

    }
}
