using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cvTest.IO;

namespace cvTest.Event
{
    /// <summary>
    /// 事件消息系统控制中心类
    /// <para>
    /// 统一管理Menu,System与其他事件消息系统的事件触发与执行，系统间数据通信
    /// </para>
    /// </summary>
    public class EventCenter
    {
        public static CmdItem Root { get; internal set; } = null;
        /// <summary>
        /// 消息系统分类枚举
        /// </summary>
        public enum SystemType
        {
            menu,
            system,
            list
        }
        /// <summary>
        /// 消息系统池
        /// </summary>
        public  readonly static Dictionary<SystemType, EventSystem> SystemPool = new();
        /// <summary>
        /// 设置全局根结点
        /// </summary>
        /// <param name="root">根结点</param>
        public static void SetRoot(CmdItem root)
        {
            Root = root;
        }
        public static CmdItem GetRoot()
        {
            if (Root!=null)
            {
                return Root;
            }
            return CmdItem.Null;
        }
        /// <summary>
        /// 消息系统在控制中心注册
        /// </summary>
        /// <param name="type">待注册分类</param>
        /// <param name="system">待注册系统</param>
        public static void Add(SystemType type, EventSystem system)
        {
            if (!isInPool(type))
            {
                SystemPool[type] = system;
            }
        }
        /// <summary>
        /// 消息系统在控制中心注销
        /// </summary>
        /// <param name="type">待注销分类</param>
        public static void Remove(SystemType type)
        {
            if (SystemPool.ContainsKey(type))
            {
                SystemPool.Remove(type);
            }
        }
        /// <summary>
        /// 判别系统是否在系统池
        /// </summary>
        /// <param name="type">待判别分类</param>
        /// <returns>存在与否</returns>
        public static bool isInPool(SystemType type)
        {
            if (SystemPool.ContainsKey(type))
            {
                if (SystemPool[type] != null)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 获取系统
        /// </summary>
        /// <param name="type">待获取系统</param>
        /// <returns></returns>
        public static EventSystem GetSubSystem(SystemType type)
        {
            if (isInPool(type))
            {
                return SystemPool[type];
            }
            return null;
        }
        /// <summary>
        /// 触发消息系统事件
        /// </summary>
        /// <param name="type">系统所在分类</param>
        /// <param name="key">键值</param>
        public static void invoke(SystemType type, string key)
        {
            if (GetSubSystem(type) != null)
            {
                GetSubSystem(type).invoke(key,type);
            }
        }
    }
}
