using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using cvTest.Event;
using cvTest.DS;

namespace cvTest.IO
{
    public class CmdProcessor
    {
        public readonly static XmlDocument Xml  = new();
        public CmdItem Root { get; internal set; }
        public CmdProcessor(string path)
        {
            try
            {
                //按路径读取XML
                Xml.Load(path);
                //以第一个结点作根菜单项目
                Root = new CmdItem(Xml.SelectSingleNode("commands"));
                //为事件注册根
                EventCenter.SetRoot(Root);
                //从根项目启动事件加载器
                CmdEventLoader CmdEventLoader = new CmdEventLoader();
                DSEventLoader dSEventLoader = new();
            }
            catch (Exception e)
            {
                CmdLine.Write(e.ToString());
                throw;
            }
        }
        
        public void Start()
        {
            //展开根菜单
            EventCenter.invoke(Root.Type, Root.Key);
        }
        

    }


}
