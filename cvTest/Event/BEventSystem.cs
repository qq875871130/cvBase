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
        /// <summary>
        /// 需注册的系统类别
        /// </summary>
        public EventCenter.SystemType SystemType { get; internal set; }
        /// <summary>
        /// 系统依赖根
        /// </summary>
        public CmdItem Root { get; internal set; }
        /// <summary>
        /// 用于操作的消息系统
        /// </summary>
        public EventSystem EventSystem { get; internal set; }
        /// <summary>
        /// 初始化构造函数
        /// </summary>
        /// <param name="type">系统类别</param>
        /// <param name="root">根项目</param>
        public BEventSystem(EventCenter.SystemType type, CmdItem root)
        {
            SystemType = type;
            Root = root;
            EventSystem = new();
        }
        /// <summary>
        /// 需要重写的注册器
        /// </summary>
        protected virtual void Register()
        {
            //注册系统至中心，在注册系统委托最后执行
            EventCenter.Add(SystemType, EventSystem);
        }
    }
}
