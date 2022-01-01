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
            switch (type)
            {
                case AddType.forward:
                    if (Head.Prev==null)
                    {
                        node = new Node<T>(data, null, Head);
                        Head.Prev = node;
                    }
                    else
                    {
                        node = new Node<T>(data, Head.Prev, Head);
                        Head.Prev = Head.Prev.Next = node;
                    }
                    break;
                case AddType.backward:
                    if (Head.Next == null)
                    {
                        node = new Node<T>(data, Head, null);
                        Head.Next = node;
                    }
                    else
                    {
                        node = new Node<T>(data, Head, Head.Next);
                        Head.Next = Head.Next.Prev = node;
                    }
                    break;
                default:
                    break;
            }
            Length++;
        }


    }


}
