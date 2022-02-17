using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cvTest.IO;
using cvTest.Event;
using cvBase.DS;
using cvTest.Templates;

namespace cvTest.DS
{
    public class DSEventLoader
    {

        public DSEventLoader()
        {
            new ListEventSystem<string>();
        }

        public class ListEventSystem<T> : DSBEventSystem
        {
            //表操作类
            private BList<T> bList = null;
            private Dirs.list<T> dir = null;
            public ListEventSystem() : base(EventCenter.SystemType.list)
            {
                dir = new Dirs.list<T>();
            }
            /// <summary>
            /// 判断当前根对象
            /// </summary>
            /// <returns>是否存在根对象</returns>
            protected override bool isDSExist()
            {
                //由当前根获取键对应操作的数据对象
                if (Enum.TryParse(CmdEventLoader.MenuEventSystem.RootCurrent.Key, out Dirs.list<T>.Key key))
                {
                    if (dir.GetDir(key) != bList)
                    {
                        bList = dir.GetDir(key);
                    }
                }
                if (bList != null)
                {
                    return true;
                }
                return false;
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
                base.Add(sender);
                if (bList.IsOverride("Add", new Type[] { typeof(T) }))
                {
                    CmdLine.ReadStream<T>("输入数据个数：", bList.Add, CmdLine.WriteState.clear);
                }
                if (bList.IsOverride("Add", new Type[] { typeof(T), typeof(List.AddType) }))
                {
                    CmdLine.ReadStream<T, List.AddType>("前插法输入数据个数：", bList.Add, List.AddType.forward, CmdLine.WriteState.no_clear);
                    CmdLine.ReadStream<T, List.AddType>("后插法输入数据个数：", bList.Add, List.AddType.backward, CmdLine.WriteState.no_clear);
                }
                if (bList.IsOverride("Add", new Type[] { typeof(T), typeof(int) }))
                {
                    int count = CmdLine.Read<int>("插入多个位置数据个数：", CmdLine.WriteState.no_clear, false);
                    for (int i = 0; i < count; i++)
                    {
                        int pos = CmdLine.Read<int>("输入插入位置：", CmdLine.WriteState.no_clear, false);
                        CmdLine.ReadStream<T, int>("该位置插入数据个数：", bList.Add, pos, CmdLine.WriteState.no_clear);
                    }
                }
                if (bList.IsOverride("Add", new Type[] { typeof(T), typeof(int), typeof(List.AddType) }))
                {
                    int pos = CmdLine.Read<int>("输入插入位置：", CmdLine.WriteState.no_clear, false);
                    CmdLine.ReadStream<T, int, List.AddType>("该位置前插入数据个数：", bList.Add, pos, List.AddType.forward, CmdLine.WriteState.no_clear);
                    CmdLine.ReadStream<T, int, List.AddType>("该位置后插入数据个数：", bList.Add, pos, List.AddType.backward, CmdLine.WriteState.no_clear);
                }
            }

            protected override void Check(CmdItem sender)
            {
                base.Check(sender);
                if (bList.IsOverride("ToArray", new Type[] { }))
                {
                    T[] list = bList.ToArray();
                    if (list.Length == 0)
                    {
                        throw new MyExceptions.EmptyDSException();
                    }
                    CmdLine.Write("当前表结点数：" + list.Length);
                    for (int i = 0; i < list.Length; i++)
                    {
                        CmdLine.Formatter.Lister((i + 1).ToString(), list[i].ToString());
                    }
                    CmdLine.GetInput("返回菜单？");
                }
                else
                {
                    throw new MyExceptions.NoMethodDSException();
                }
            }

            protected override void Get(CmdItem sender)
            {
                base.Get(sender);
                if (bList.IsOverride("GetData", new Type[] { typeof(int) }))
                {
                    do
                    {
                        T obj = bList.GetData(CmdLine.Read<int>("输入获取数据的索引：", CmdLine.WriteState.clear, false));
                        if (obj == null)
                        {
                            throw new MyExceptions.EmptyDSException();
                        }
                        string result = obj.ToString();
                        CmdLine.Write(result, CmdLine.WriteState.no_clear);
                    } while (CmdLine.GetInput("继续获取数据？"));
                }
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
                base.Clear(sender);
                if (bList.IsOverride("Clear", new Type[] { }))
                {
                    bList.Clear();
                    Console.Clear();
                    CmdLine.GetInput("清空完成，返回菜单？");
                }
            }
        }
    }
}
