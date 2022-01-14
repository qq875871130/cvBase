using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cvTest.IO;
using cvTest.Event;

namespace cvTest.Event
{
    //委托定义项
    public delegate void Method();
    public delegate void Method<T>(T p1);
    /// <summary>
    /// 消息事件系统
    /// <para>存放以键索引的委托链集合，以实现简单的消息机制</para>
    /// </summary>
    public class EventSystem
    {
        /// <summary>
        /// 委托事件池
        /// </summary>
        private Dictionary<string, Delegate> EventPool;
        public EventSystem()
        {
            EventPool = new();
        }
        /// <summary>
        /// 判别委托是否在委托池
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public bool isInPool(string key)
        {
            if (EventPool.ContainsKey(key))
            {
                if (EventPool[key] != null)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 注册消息
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="e">委托链结点</param>
        public void Add(string key, Method<string> e)
        {
            switch (EventPool.ContainsKey(key))
            {
                case true:
                    EventPool[key] = (Method<string>)EventPool[key] + e;
                    break;
                case false:
                    EventPool.Add(key, e);
                    break;
            }
        }
        /// <summary>
        /// 注销消息
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="Event">注销委托链结点</param>
        public void Remove(string key, Method<string> Event)
        {
            if (EventPool.ContainsKey(key))
            {
                EventPool[key] = (Method<string>)EventPool[key] - Event;
                if (EventPool[key] == null)
                {
                    EventPool.Remove(key);
                }
            }
        }
        /// <summary>
        /// 响应消息
        /// </summary>
        /// <param name="key">键</param>
        public void invoke(string key)
        {
            if (isInPool(key))
            {
                //执行委托链
                Method<string> e = EventPool[key] as Method<string>;
                e(key);
            }
        }
    }
}
