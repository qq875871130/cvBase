using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cvBase.DS
{
    /// <summary>
    /// 双链结点类
    /// </summary>
    /// <typeparam name="T">数据域数据类型</typeparam>
    public class Node<T>
    {
        /// <summary>
        /// 数据域
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 前指针
        /// </summary>
        public Node<T> Prev { get; set; }
        /// <summary>
        /// 后指针
        /// </summary>
        public Node<T> Next { get; set; }
        /// <summary>
        /// 完整信息结点构造函数
        /// </summary>
        /// <param name="item">数据域</param>
        /// <param name="p">前指针域</param>
        /// <param name="n">后指针域</param>
        public Node(T item, Node<T> p,Node<T> n)
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
    /// 双向链表类
    /// <para>next与prev构成的双指针域链表</para>
    /// </summary>
    /// <typeparam name="T">数据域的数据类型</typeparam>
    public class LinkList<T>
    {
        /// <summary>
        /// 头结点
        /// </summary>
        public Node<T> Head { get; set; }
        /// <summary>
        /// 表长
        /// </summary>
        public int Length { get; internal set; }
        /// <summary>
        /// 无参构造函数
        /// <para>初始化头结点与表长</para>
        /// </summary>
        public LinkList()
        {
            Head = new Node<T>();
            Length = 0;
        }
        /// <summary>
        /// 带数据的头结点构造函数
        /// </summary>
        /// <param name="head">头结点数据</param>
        public LinkList(T head)
        {
            Head = new Node<T>(head);
            Length = 0;
        }
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
        /// 插入数据函数
        /// </summary>
        /// <param name="data">待插数据</param>
        /// <param name="type">插入方向</param>
        public void Add(T data,AddType type)
        {
            Node<T> node;
            if (Head.Next==null)
            {
                node = new Node<T>(data, Head, null);
                Head.Next = node;
            }
            else
            {
                switch (type)
                {
                    case AddType.forward:
                        node = new Node<T>(data, Head, Head.Next);
                        Head.Next = Head.Next.Prev = node;
                        break;
                    case AddType.backward:
                        Node<T> p = Head.Next;
                        while (p.Next!=null)
                        {
                            p = p.Next;
                        }
                        node = new Node<T>(data, p, null);
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
        public void Add(T data,int index, AddType type)
        {
            Node<T> node;
            //表长异常检测
            if (index>Length)
            {
                return;
            }
            if (Head.Next == null)
            {
                node = new Node<T>(data, Head, null);
                Head.Next = node;
            }
            else
            {
                Node<T> p = Head;
                for (int i = 0; i < index; i++)
                {
                    p = p.Next;
                }
                switch (type)
                {
                    case AddType.forward:
                        node = new Node<T>(data, p.Prev, p);
                        p.Prev = p.Prev.Next = node;
                        break;
                    case AddType.backward:
                        node = new Node<T>(data, p, p.Next);
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
        public Node<T> GetNode(int index)
        {
            Node<T> p = Head;
            //表异常检测
            if (index>Length)
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
        public T GetData(int index)
        {
            return GetNode(index).Data;
        }
       /// <summary>
       /// 以数据查找最近索引值
       /// </summary>
       /// <param name="data">待查数据</param>
       /// <returns>第一个匹配的索引值</returns>
        public int GetIndex(T data)
        {
            Node<T> p = Head;
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
        public int[] GetIndexes(T data)
        {
            List<int> result = new List<int>();
            Node<T> p = Head;
            int i = 0;
            while (p.Next!=null)
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
        public void Delete(int[] indexes)
        {
            Node<T> p = Head;
            List<int> indList = indexes.ToList();
            indList.Sort();
            int[] indexes_sorted = indList.ToArray();
            int i = 0, j = 0;
            while (p.Next!=null)
            {
                p = p.Next;
                i++;
                if (j<indexes_sorted.Length&&i==indexes_sorted[j])
                {
                    p.Prev.Next = p.Next;
                    if (p.Next!=null)
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
        public void Delete(int index)
        {
            int[] indexes = { index };
            Delete(indexes);
        }
        /// <summary>
        /// 删除指定数据的所有结点
        /// </summary>
        /// <param name="data">待删匹配数据</param>
        public void Delete(T data)
        {
            Delete(GetIndexes(data));
        }
        /// <summary>
        /// 区间索引批量删除函数
        /// <para>保证O(n)时间复杂度与足够低的空间复杂度下的实现</para>
        /// </summary>
        /// <param name="st">起点位</param>
        /// <param name="dst">终点位</param>
        public void Delete(int st, int dst)
        {
            if (st <= dst && st >= 1 && dst <= Length)
            {
                Node<T> p = Head;
                Node<T> front;
                Node<T> rear;
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
                if (rear.Next!=null)
                {
                    rear.Next.Prev = front;
                }
                Length = Length - (dst - st + 1);
            }
        }
       /// <summary>
       /// 清空表
       /// </summary>
        public void Clear()
        {
            if (Length>0)
            {
                Delete(1, Length);
            }
        }
       /// <summary>
       /// 列出所有表数据&转化为数组
       /// </summary>
       /// <returns>表数据数组</returns>
        public T[] ToArray()
        {
            List<T> list = new List<T>();
            Node<T> p = Head;
            while (p.Next!=null)
            {
                p = p.Next;
                list.Add(p.Data);
            }
            return list.ToArray();
        }
    }


}
