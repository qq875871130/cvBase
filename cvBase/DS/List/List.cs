using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cvBase.DS
{
    /// <summary>
    /// 表类
    /// </summary>
    public class List
    {
        /// <summary>
        /// 插入方向枚举
        /// </summary>
        public enum AddType
        {
            /// <summary>
            /// 前插
            /// </summary>
            forward,
            /// <summary>
            /// 后插
            /// </summary>
            backward
        }
        /// <summary>
        /// 链表类
        /// </summary>
        public class LinkList<T>
        {
            #region 单链表
            /// <summary>
            /// 单链表
            /// </summary>
            public class Single : BList<T>
            {
                /// <summary>
                /// 单指针结点
                /// </summary>
                public class Node
                {
                    /// <summary>
                    /// 数据域
                    /// </summary>
                    public T Data { get; set; }
                    /// <summary>
                    /// 下一节点指针域
                    /// </summary>
                    public Node Next { get; set; }
                    /// <summary>
                    /// 构造函数，初始化为空
                    /// </summary>
                    public Node()
                    {
                        Data = default;
                        Next = null;
                    }
                    /// <summary>
                    /// 构造函数
                    /// </summary>
                    /// <param name="data">结点数据域</param>
                    public Node(T data)
                    {
                        Data = data;
                        Next = null;
                    }
                    /// <summary>
                    /// 构造函数
                    /// </summary>
                    /// <param name="data">结点数据域</param>
                    /// <param name="next">下一结点指针域</param>
                    public Node(T data, Node next)
                    {
                        Data = data;
                        Next = next;
                    }
                }
                /// <summary>
                /// 头结点
                /// </summary>
                public Node Head { get; internal set; }
                /// <summary>
                /// 表长
                /// </summary>
                public int Length { get; internal set; }
                /// <summary>
                /// 默认构造函数
                /// </summary>
                public Single()
                {
                    Head = new Node();
                    Length = 0;
                }
                /// <summary>
                /// 头结点初值构造函数
                /// </summary>
                /// <param name="head">头结点数据域</param>
                public Single(T head)
                {
                    Head = new Node(head);
                    Length = 0;
                }
                /// <summary>
                /// 插入结点函数
                /// </summary>
                /// <param name="data">插入数据</param>
                public override void Add(T data)
                {
                    Add(data, AddType.forward);
                }
                /// <summary>
                /// 插入结点函数
                /// </summary>
                /// <param name="data">插入数据</param>
                /// <param name="type">插入方向</param>
                public override void Add(T data, AddType type)
                {
                    Node node;
                    switch (type)
                    {
                        case AddType.forward:
                            node = new Node(data, Head.Next);
                            Head.Next = node;
                            break;
                        case AddType.backward:
                            Node p = Head;
                            while (p.Next != null)
                            {
                                p = p.Next;
                            }
                            node = new Node(data, null);
                            p.Next = node;
                            break;
                        default:
                            break;
                    }
                    Length++;
                }
                /// <summary>
                /// 插入结点函数
                /// </summary>
                /// <param name="data">插入数据</param>
                /// <param name="pos">插入位置</param>
                public override void Add(T data, int pos)
                {
                    Node p = Head;
                    Node node;
                    if (pos > Length || pos < 1)
                    {
                        return;
                    }
                    for (int i = 0; i < pos; i++)
                    {
                        p = p.Next;
                    }
                    node = new Node(data, p.Next);
                    p.Next = node;
                    Length++;
                }
                /// <summary>
                /// 获取结点
                /// </summary>
                /// <param name="index">结点位置</param>
                /// <returns>对应位置结点</returns>
                public override Node GetSNode(int index)
                {
                    Node p = Head;
                    if (index > Length || index < 1)
                    {
                        return null;
                    }
                    for (int i = 0; i < index; i++)
                    {
                        p = p.Next;
                    }
                    return p;
                }
                /// <summary>
                /// 获取数据
                /// </summary>
                /// <param name="index">数据位置</param>
                /// <returns>对应位置的数据</returns>
                public override T GetData(int index)
                {
                    if (GetSNode(index) != null)
                    {
                        return GetSNode(index).Data;
                    }
                    return default;
                }
                /// <summary>
                /// 获取匹配数据的索引
                /// </summary>
                /// <param name="data">待匹配数据</param>
                /// <returns>匹配数据首个索引</returns>
                public override int GetIndex(T data)
                {
                    Node p = Head;
                    int i = 0;
                    while (p.Next != null)
                    {
                        p = p.Next;
                        i++;
                        if (p.Data.Equals(data))
                        {
                            return i;
                        }
                    }
                    return -1;
                }
                /// <summary>
                /// 获取匹配数据的索引集
                /// </summary>
                /// <param name="data">待匹配数据</param>
                /// <returns>匹配的所有索引组成的数组</returns>
                public override int[] GetIndexes(T data)
                {
                    Node p = Head;
                    int i = 0;
                    List<int> result = new List<int>();
                    while (p.Next != null)
                    {
                        p = p.Next;
                        i++;
                        if (p.Data.Equals(data))
                        {
                            result.Add(i);
                        }
                    }
                    return result.ToArray();
                }
                /// <summary>
                /// 按索引批量删除结点
                /// </summary>
                /// <param name="indexes">待删索引数组</param>
                public override void Delete(int[] indexes)
                {
                    Node p = Head;
                    List<int> indList = indexes.ToList();
                    indList.Sort();
                    int[] indexes_sorted = indList.ToArray();
                    int i = 1, j = 0;
                    if (p.Next == null)
                    {
                        throw new Exception("结构为空！");
                    }
                    do
                    {
                        if (j < indexes_sorted.Length && i == indexes_sorted[j])
                        {
                            p.Next = p.Next.Next;
                            j++;
                            Length--;
                        }
                        else
                        {
                            p = p.Next;
                        }
                        i++;
                    } while (p.Next != null);
                }
                /// <summary>
                /// 删除结点
                /// </summary>
                /// <param name="index">待删索引</param>
                public override void Delete(int index)
                {
                    int[] list = { index };
                    Delete(list);
                }
                /// <summary>
                /// 删除结点
                /// </summary>
                /// <param name="data">待删结点数据</param>
                public override void Delete(T data)
                {
                    Delete(GetIndexes(data));
                }
                /// <summary>
                /// 按区间批量删除结点
                /// </summary>
                /// <param name="st">起始位置</param>
                /// <param name="dst">终点位置</param>
                public override void Delete(int st, int dst)
                {
                    if (st <= dst && st >= 1 && dst <= Length)
                    {
                        Node p = Head;
                        Node front;
                        Node rear;
                        for (int i = 1; i < st; i++)
                        {
                            p = p.Next;
                        }
                        front = p;
                        for (int i = st; i <= dst; i++)
                        {
                            p = p.Next;
                        }
                        rear = p;
                        front.Next = rear.Next;
                        Length = Length - (dst - st + 1);
                    }
                }
                /// <summary>
                /// 清空表
                /// </summary>
                public override void Clear()
                {
                    if (Length > 0)
                    {
                        Delete(1, Length);
                    }
                }
                /// <summary>
                /// 转化为数组
                /// </summary>
                /// <returns>对应数据域数组</returns>
                public override T[] ToArray()
                {
                    List<T> list = new List<T>();
                    Node p = Head;
                    while (p.Next != null)
                    {
                        p = p.Next;
                        list.Add(p.Data);
                    }
                    return list.ToArray();
                }
            }
            #endregion
            #region 双向链表
            /// <summary>
            /// 双向链表
            /// <para>next与prev构成的双指针域链表</para>
            /// </summary>
            public class Double : BList<T>
            {
                /// <summary>
                /// 双链结点类
                /// </summary>
                public class Node
                {
                    /// <summary>
                    /// 数据域
                    /// </summary>
                    public T Data { get; set; }
                    /// <summary>
                    /// 前指针
                    /// </summary>
                    public Node Prev { get; set; }
                    /// <summary>
                    /// 后指针
                    /// </summary>
                    public Node Next { get; set; }
                    /// <summary>
                    /// 完整信息结点构造函数
                    /// </summary>
                    /// <param name="item">数据域</param>
                    /// <param name="p">前指针域</param>
                    /// <param name="n">后指针域</param>
                    public Node(T item, Node p, Node n)
                    {
                        Data = item;
                        Prev = p;
                        Next = n;
                    }
                    /// <summary>
                    /// 单参仅数据构造
                    /// </summary>
                    /// <param name="item">结点数据</param>
                    public Node(T item)
                    {
                        Data = item;
                        Prev = null;
                        Next = null;
                    }
                    /// <summary>
                    /// 无参构造函数
                    /// </summary>
                    public Node()
                    {
                        Data = default;
                        Prev = null;
                        Next = null;
                    }

                }
                /// <summary>
                /// 头结点
                /// </summary>
                public Node Head { get; set; }
                /// <summary>
                /// 表长
                /// </summary>
                public int Length { get; internal set; }
                /// <summary>
                /// 无参构造函数
                /// <para>初始化头结点与表长</para>
                /// </summary>
                public Double()
                {
                    Head = new Node();
                    Length = 0;
                }
                /// <summary>
                /// 带数据的头结点构造函数
                /// </summary>
                /// <param name="head">头结点数据</param>
                public Double(T head)
                {
                    Head = new Node(head);
                    Length = 0;
                }
                /// <summary>
                /// 插入数据函数（默认前插法）
                /// </summary>
                /// <param name="data">插入数据</param>
                public override void Add(T data)
                {
                    Add(data, AddType.forward);
                }
                /// <summary>
                /// 插入数据函数
                /// </summary>
                /// <param name="data">待插数据</param>
                /// <param name="type">插入方向</param>
                public override void Add(T data, AddType type)
                {
                    Node node;
                    if (Head.Next == null)
                    {
                        node = new Node(data, Head, null);
                        Head.Next = node;
                    }
                    else
                    {
                        switch (type)
                        {
                            case AddType.forward:
                                node = new Node(data, Head, Head.Next);
                                Head.Next = Head.Next.Prev = node;
                                break;
                            case AddType.backward:
                                Node p = Head.Next;
                                while (p.Next != null)
                                {
                                    p = p.Next;
                                }
                                node = new Node(data, p, null);
                                p.Next = node;
                                break;
                            default:
                                break;
                        }
                    }
                    Length++;
                }
                /// <summary>
                /// 在特定位置的插入函数
                /// </summary>
                /// <param name="data">插入数据</param>
                /// <param name="index">插入位置索引</param>
                /// <param name="type">插入方向
                /// <para>
                /// 位置前插
                /// 位置后插
                /// </para>
                /// </param>
                public override void Add(T data, int index, AddType type)
                {
                    Node node;
                    //表长异常检测
                    if (index > Length || index < 1)
                    {
                        return;
                    }
                    if (Head.Next == null)
                    {
                        node = new Node(data, Head, null);
                        Head.Next = node;
                    }
                    else
                    {
                        Node p = Head;
                        for (int i = 0; i < index; i++)
                        {
                            p = p.Next;
                        }
                        switch (type)
                        {
                            case AddType.forward:
                                node = new Node(data, p.Prev, p);
                                p.Prev = p.Prev.Next = node;
                                break;
                            case AddType.backward:
                                node = new Node(data, p, p.Next);
                                p.Next = p.Next.Prev = node;
                                break;
                            default:
                                break;
                        }

                    }
                    Length++;
                }
                /// <summary>
                /// 获取索引位置的结点
                /// </summary>
                /// <param name="index">索引</param>
                /// <returns></returns>
                public override Node GetDNode(int index)
                {
                    Node p = Head;
                    //表异常检测
                    if (index > Length)
                    {
                        return null;
                    }
                    for (int i = 0; i < index; i++)
                    {
                        p = p.Next;
                    }
                    return p;
                }
                /// <summary>
                /// 获取索引位置的数据
                /// </summary>
                /// <param name="index">索引</param>
                /// <returns></returns>
                public override T GetData(int index)
                {
                    try
                    {
                        return GetDNode(index).Data;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                /// <summary>
                /// 以数据查找最近索引值
                /// </summary>
                /// <param name="data">待查数据</param>
                /// <returns>第一个匹配的索引值</returns>
                public override int GetIndex(T data)
                {
                    Node p = Head;
                    int i = 0;
                    while (p.Next != null)
                    {
                        p = p.Next;
                        i++;
                        if (p.Data.Equals(data))
                        {
                            return i;
                        }
                    }
                    return -1;
                }
                /// <summary>
                /// 以数据查找所有匹配索引值
                /// </summary>
                /// <param name="data">待查数据</param>
                /// <returns>匹配索引数组</returns>
                public override int[] GetIndexes(T data)
                {
                    List<int> result = new List<int>();
                    Node p = Head;
                    int i = 0;
                    while (p.Next != null)
                    {
                        p = p.Next;
                        i++;
                        if (p.Data.Equals(data))
                        {
                            result.Add(i);
                        }
                    }
                    return result.ToArray();
                }
                /// <summary>
                ///删除多结点函数
                /// </summary>
                /// <param name="indexes">待删索引集</param>
                public override void Delete(int[] indexes)
                {
                    Node p = Head;
                    List<int> indList = indexes.ToList();
                    indList.Sort();
                    int[] indexes_sorted = indList.ToArray();
                    int i = 0, j = 0;
                    while (p.Next != null)
                    {
                        p = p.Next;
                        i++;
                        if (j < indexes_sorted.Length && i == indexes_sorted[j])
                        {
                            p.Prev.Next = p.Next;
                            if (p.Next != null)
                            {
                                p.Next.Prev = p.Prev;
                            }
                            j++;
                            Length--;
                        }
                    }
                }
                /// <summary>
                /// 删除单节点函数
                /// </summary>
                /// <param name="index">待删索引</param>
                public override void Delete(int index)
                {
                    int[] indexes = { index };
                    Delete(indexes);
                }
                /// <summary>
                /// 删除指定数据的所有结点
                /// </summary>
                /// <param name="data">待删匹配数据</param>
                public override void Delete(T data)
                {
                    Delete(GetIndexes(data));
                }
                /// <summary>
                /// 区间索引批量删除函数
                /// <para>保证O(n)时间复杂度与足够低的空间复杂度下的实现</para>
                /// </summary>
                /// <param name="st">起点位</param>
                /// <param name="dst">终点位</param>
                public override void Delete(int st, int dst)
                {
                    if (st <= dst && st >= 1 && dst <= Length)
                    {
                        Node p = Head;
                        Node front;
                        Node rear;
                        for (int i = 0; i < st; i++)
                        {
                            p = p.Next;
                        }
                        front = p.Prev;
                        for (int i = st; i < dst; i++)
                        {
                            p = p.Next;
                        }
                        rear = p;
                        front.Next = rear.Next;
                        if (rear.Next != null)
                        {
                            rear.Next.Prev = front;
                        }
                        Length = Length - (dst - st + 1);
                    }
                }
                /// <summary>
                /// 清空表
                /// </summary>
                public override void Clear()
                {
                    if (Length > 0)
                    {
                        Delete(1, Length);
                    }
                }
                /// <summary>
                /// 列出所有表数据并转化为数组
                /// </summary>
                /// <returns>表数据数组</returns>
                public override T[] ToArray()
                {
                    List<T> list = new List<T>();
                    Node p = Head;
                    while (p.Next != null)
                    {
                        p = p.Next;
                        list.Add(p.Data);
                    }
                    return list.ToArray();
                }
            }
            #endregion
        }
    }



}
