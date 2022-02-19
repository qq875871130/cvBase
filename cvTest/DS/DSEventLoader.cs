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
            /// <summary>
            /// 插入测试函数
            /// </summary>
            /// <param name="sender">发送者</param>
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
                    int offset = CmdLine.ReadStream<T, int, List.AddType>("该位置前插入数据个数：", bList.Add, pos, List.AddType.forward, CmdLine.WriteState.no_clear);
                    CmdLine.ReadStream<T, int, List.AddType>("该位置后插入数据个数：", bList.Add, pos + offset, List.AddType.backward, CmdLine.WriteState.no_clear);
                }
            }
            /// <summary>
            /// 检查测试函数
            /// </summary>
            /// <param name="sender">发送者</param>
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
                    CmdLine.Write("任意键返回...", CmdLine.WriteState.no_clear);
                    Console.ReadKey();
                }
                else
                {
                    throw new MyExceptions.NoMethodDSException();
                }
            }
            /// <summary>
            /// 获取测试函数
            /// </summary>
            /// <param name="sender">发送者</param>
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
            /// <summary>
            /// 删除测试函数
            /// </summary>
            /// <param name="sender">发送者</param>
            protected override void Delete(CmdItem sender)
            {
                base.Delete(sender);
                //按索引批量删除
                if (bList.IsOverride("Delete", new Type[] { typeof(int[]) }))
                {
                    string indexesStr = CmdLine.Read<string>("输入多个删除数据的索引号：", CmdLine.WriteState.clear, false);
                    int[] indexes = Array.ConvertAll(indexesStr.Split(' '), int.Parse);
                    bList.Delete(indexes);
                }
                //按数据批量删除
                if (bList.IsOverride("Delete", new Type[] { typeof(T) }))
                {
                    T target = CmdLine.Read<T>("输入待批量删除的数据：", CmdLine.WriteState.no_clear, false);
                    bList.Delete(target);
                }
                //按索引段批量删除
                if (bList.IsOverride("Delete", new Type[] { typeof(int), typeof(int) }))
                {
                    int st = CmdLine.Read<int>("输入删除起点：", CmdLine.WriteState.no_clear, false);
                    int dst = CmdLine.Read<int>("输入删除终点：", CmdLine.WriteState.no_clear, false);
                    bList.Delete(st, dst);
                }
            }
            /// <summary>
            /// 搜索测试函数
            /// </summary>
            /// <param name="sender">发送者</param>
            protected override void Search(CmdItem sender)
            {
                base.Search(sender);
                T target;
                //单匹配查找
                if (bList.IsOverride("GetIndex", new Type[] { typeof(T) }))
                {
                    target = CmdLine.Read<T>("输入首次匹配的数据：", CmdLine.WriteState.clear, false);
                    CmdLine.Write("首次匹配的索引号：" + bList.GetIndex(target), CmdLine.WriteState.no_clear, true);
                }
                //全匹配查找
                if (bList.IsOverride("GetIndexes", new Type[] { typeof(T) }))
                {
                    target = CmdLine.Read<T>("输入全部匹配的数据：", CmdLine.WriteState.no_clear, false);
                    string matchData = string.Join(" ", bList.GetIndexes(target));
                    CmdLine.Write("全部匹配的索引号：" + matchData, CmdLine.WriteState.no_clear, true);
                }
                //重新查找判定
                if (CmdLine.GetInput("是否继续查找？"))
                {
                    Search(sender);
                }
            }
            /// <summary>
            /// 清空测试函数
            /// </summary>
            /// <param name="sender">发送者</param>
            protected override void Clear(CmdItem sender)
            {
                base.Clear(sender);
                if (bList.IsOverride("Clear", new Type[] { }))
                {
                    bList.Clear();
                    CmdLine.Write("清空完成，任意键继续...");
                    Console.ReadKey();
                }
            }
        }
    }
}
