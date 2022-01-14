using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using cvTest.Event;

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
                Xml.Load(path);
                Root = new CmdItem(Xml.SelectSingleNode("commands"));
                CmdEventLoader CmdEventLoader = new CmdEventLoader(Root);
            }
            catch (Exception e)
            {
                CmdLine.Write(e.ToString());
                throw;
            }
        }
        
        public void Start()
        {
            EventCenter.invoke(Root.Type, Root.Key);
        }
        

    }


}
