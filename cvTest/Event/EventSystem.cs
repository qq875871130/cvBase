using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cvTest.IO;
using cvTest.Event;

namespace cvTest.Event
{
    public delegate void Method();
    public delegate void Method<T>(T p1);

    public class EventCenter
    {
        public static CmdEventLoader.MenuEventSystem InstanceMenu = null;
        public static CmdEventLoader.SystemEventSystem InstanceSystem = null;

        public enum SystemType
        {
            menu,
            system,
            item
        }

        private static Dictionary<SystemType, EventSystem> SystemPool = new();

        public static void Add(SystemType type, EventSystem system)
        {
            if (!SystemPool.ContainsKey(type))
            {
                SystemPool[type] = system;
            }
        }
        public static void Remove(SystemType type)
        {
            if (SystemPool.ContainsKey(type))
            {
                SystemPool.Remove(type);
            }
        }
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
        public static EventSystem GetSubSystem(SystemType type)
        {
            if (isInPool(type))
            {
                return SystemPool[type];
            }
            return null;
        }
        public static void invoke(SystemType type,string key)
        {
            if (GetSubSystem(type)!=null)
            {
                GetSubSystem(type).invoke(key);
            }
        }
    }

    public class EventSystem
    {
        private Dictionary<string, Delegate> EventPool;
        public enum MethodType
        {
            param_none,
            param_1
        }
        public EventSystem()
        {
            EventPool = new();
        }
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
        #region 单参数
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
        public void invoke(string key)
        {
            if (isInPool(key))
            {
                Method<string> e = EventPool[key] as Method<string>;
                e(key);
            }
        }
        #endregion


    }
}
