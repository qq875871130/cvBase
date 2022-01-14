using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using cvTest.Event;

namespace cvTest.IO
{
    public class CmdItem
    {
        public string Name { get; internal set; }
        public string Key { get; internal set; }
        public EventCenter.SystemType Type { get; internal set; }
        public CmdItem Father { get; internal set; } = null;
        public List<CmdItem> CmdItems { get; internal set; } = null;
        public delegate void Do();
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
                    case "item":
                        Type = EventCenter.SystemType.item;
                        break;
                    default:
                        Type = EventCenter.SystemType.system;
                        break;
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
        public CmdItem(string name, string key, EventCenter.SystemType eventType)
        {
            Name = name;
            Key = key;
            Type = eventType;
        }
        public void SetFather(CmdItem f)
        {
            Father = f == null ? this : f;
        }
        public void AddCmd(CmdItem cmdItem)
        {
            if (cmdItem != null)
            {
                cmdItem.SetFather(this);
                CmdItems.Add(cmdItem);
            }
        }
        public CmdItem GetItem(string key, EventCenter.SystemType systemType)
        {
            if (Key == key && Type == systemType)
            {
                return this;
            }
            foreach (CmdItem item in CmdItems)
            {
                if (item.GetItem(key, systemType) != null)
                {
                    return item.GetItem(key, systemType);
                }
            }
            return null;
        }
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
