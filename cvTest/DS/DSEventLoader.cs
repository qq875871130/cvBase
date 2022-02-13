using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cvTest.IO;
using cvTest.Event;
using cvBase.DS;

namespace cvTest.DS
{
    public class DSEventLoader
    {

        public DSEventLoader()
        {
            new ListEventSystem();
        }

        public class ListEventSystem : DSBEventSystem
        {
            //表操作类
            public static class mList
            {
                public static class LinkedList
                {
                    //单链表
                    public static List.LinkList<string>.Single slinkedList = new();
                    //双链表
                    public static List.LinkList<string>.Double dlinkedList = new();
                }
            }
            public ListEventSystem() : base(EventCenter.SystemType.list)
            {

            }
            private void debug(CmdItem sender)
            {
                CmdLine.Write(sender.Name + CmdEventLoader.MenuEventSystem.RootCurrent.Name);
                if (!CmdLine.GetInput())
                {
                    debug(sender);
                }
            }
            protected override void Add(CmdItem sender)
            {
                BList<string> list;
                switch (CmdEventLoader.MenuEventSystem.RootCurrent.Key)
                {
                    case "list_linked_single":
                        list = mList.LinkedList.slinkedList;
                        break;
                    case "list_linked_double":
                        list = mList.LinkedList.dlinkedList;
                        break;
                    default:
                        debug(sender);
                        break;
                }



                base.Add(sender);
            }
           
            protected override void Check(CmdItem sender)
            {
                switch (CmdEventLoader.MenuEventSystem.RootCurrent.Key)
                {
                    case "list_linked_single":
                        break;
                    case "list_linked_double":
                        break;
                    default:
                        debug(sender);
                        break;
                }
                base.Check(sender);
            }

            protected override void Get(CmdItem sender)
            {
                switch (CmdEventLoader.MenuEventSystem.RootCurrent.Key)
                {
                    case "list_linked_single":
                        break;
                    case "list_linked_double":
                        break;
                    default:
                        debug(sender);
                        break;
                }
                base.Get(sender);
            }

            protected override void Delete(CmdItem sender)
            {
                switch (CmdEventLoader.MenuEventSystem.RootCurrent.Key)
                {
                    case "list_linked_single":
                        break;
                    case "list_linked_double":
                        break;
                    default:
                        debug(sender);
                        break;
                }
                base.Delete(sender);
            }

            protected override void Search(CmdItem sender)
            {
                switch (CmdEventLoader.MenuEventSystem.RootCurrent.Key)
                {
                    case "list_linked_single":
                        break;
                    case "list_linked_double":
                        break;
                    default:
                        debug(sender);
                        break;
                }
                base.Search(sender);
            }

            protected override void Clear(CmdItem sender)
            {
                switch (CmdEventLoader.MenuEventSystem.RootCurrent.Key)
                {
                    case "list_linked_single":
                        break;
                    case "list_linked_double":
                        break;
                    default:
                        debug(sender);
                        break;
                }
                base.Clear(sender);
            }
        }
    }
}
