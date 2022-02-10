using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using cvTest.Event;

namespace cvTest.IO
{
    /// <summary>
    /// 菜单项列表类
    /// </summary>
    public class CmdItem
    {
        public static CmdItem Null = new CmdItem("null", "null", EventCenter.SystemType.system);
        /// <summary>
        /// 项名
        /// </summary>
        public string Name { get; internal set; }
        /// <summary>
        /// 用于触发消息的唯一标识键
        /// </summary>
        public string Key { get; internal set; }
        /// <summary>
        /// 触发的事件系统类别
        /// </summary>
        public EventCenter.SystemType Type { get; internal set; }
        /// <summary>
        /// 父菜单项目
        /// </summary>
        public CmdItem Father { get; internal set; } = null;
        /// <summary>
        /// 子菜单项目列表
        /// </summary>
        public List<CmdItem> CmdItems { get; internal set; } = null;
        /// <summary>
        /// 从Xml结点构造菜单项目
        /// </summary>
        /// <param name="root"></param>
        public CmdItem(XmlNode root)
        {
            XmlElement element = (XmlElement)root;
            CmdItems = new();
            //读取name并赋值
            if (element.HasAttribute("name"))
            {
                Name = element.GetAttribute("name");
            }
            //读取key并赋值
            if (element.HasAttribute("key"))
            {
                Key = element.GetAttribute("key");
                switch (element.Name)
                {
                    case "commands":
                        Type = EventCenter.SystemType.menu;
                        break;
                    default:
                        Type = EventCenter.SystemType.system;
                        break;
                }
                if (Enum.TryParse(element.Name, out EventCenter.SystemType type) == true)
                {
                    Type = type;
                }
            }
            //添加子节点
            if (root.HasChildNodes)
            {
                foreach (XmlNode child in root.ChildNodes)
                {
                    CmdItem child_item = new CmdItem(child);
                    AddCmd(child_item);
                }
            }
        }
        /// <summary>
        /// 构造自定义菜单项
        /// </summary>
        /// <param name="name"></param>
        /// <param name="key"></param>
        /// <param name="eventType"></param>
        public CmdItem(string name, string key, EventCenter.SystemType eventType)
        {
            Name = name;
            Key = key;
            Type = eventType;
        }
        /// <summary>
        /// 设置父菜单项
        /// </summary>
        /// <param name="f"></param>
        public void SetFather(CmdItem f)
        {
            Father = f == null ? this : f;
        }
        /// <summary>
        /// 在项目下添加子项目
        /// </summary>
        /// <param name="cmdItem"></param>
        public void AddCmd(CmdItem cmdItem)
        {
            if (cmdItem != null)
            {
                cmdItem.SetFather(this);
                CmdItems.Add(cmdItem);
            }
        }
        public void ClearCmd()
        {
            if (CmdItems!=null)
            {
                CmdItems.Clear();
            }
        }
        /// <summary>
        /// 由键与事件种类获取项目
        /// </summary>
        /// <param name="key"></param>
        /// <param name="systemType"></param>
        /// <returns></returns>
        public CmdItem GetItem(string key, EventCenter.SystemType systemType)
        {
            if (Key == key && Type == systemType)
            {
                return this;
            }
            if (CmdItems!=null)
            {
                foreach (CmdItem item in CmdItems)
                {
                    if (item.GetItem(key, systemType) != null)
                    {
                        return item.GetItem(key, systemType);
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 获取特定事件种类的菜单键集合
        /// </summary>
        /// <param name="systemType"></param>
        /// <returns></returns>
        public string[] GetKeys(EventCenter.SystemType systemType)
        {
            List<string> keys = new();
            if (Type == systemType)
            {
                keys.Add(this.Key);
            }
            foreach (CmdItem item in CmdItems)
            {
                keys = keys.Union(item.GetKeys(systemType).ToList()).ToList();
            }
            return keys.ToArray();
        }
    }
}
