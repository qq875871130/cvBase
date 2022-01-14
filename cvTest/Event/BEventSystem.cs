using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cvTest.IO;
using cvTest.Templates;

namespace cvTest.Event
{
    /// <summary>
    /// 事件系统操作类基类
    /// </summary>
    public class BEventSystem 
    {
        public EventCenter.SystemType SystemType { get; internal set; }
        public CmdItem Root { get; internal set; }
        public EventSystem EventSystem { get; internal set; }
        public BEventSystem(EventCenter.SystemType type, CmdItem root)
        {
            SystemType = type;
            Root = root;
            EventSystem = new();
        }
        protected virtual void Register()
        {
            EventCenter.Add(SystemType, EventSystem);
        }
    }
}
