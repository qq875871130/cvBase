using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cvBase.DS
{
    /// <summary>
    /// 存放属性数据
    /// </summary>
    public static class Dirs
    {
        /// <summary>
        /// List静态数据
        /// </summary>
        public class list<T>
        {
            /// <summary>
            /// 键值数据
            /// </summary>
            public enum Key
            {
                /// <summary>
                /// 顺序表
                /// </summary>
                list_order,
                /// <summary>
                /// 顺序循环表
                /// </summary>
                list_order_circular,
                /// <summary>
                /// 单链表
                /// </summary>
                list_linked_single,
                /// <summary>
                /// 双向链表
                /// </summary>
                list_linked_double,
                /// <summary>
                /// 循环链表
                /// </summary>
                list_linked_circular
            }
            /// <summary>
            /// 数据对象字典
            /// </summary>
            private Dictionary<Key, BList<T>> Dir;
            /// <summary>
            /// 构造函数
            /// </summary>
            public list()
            {
                //以类型构造字典
                Dir = new Dictionary<Key, BList<T>>()
                {
                    {Key.list_linked_single,new List.LinkList<T>.Single() },
                    {Key.list_linked_double,new List.LinkList<T>.Double() },
                };
            }
            /// <summary>
            /// 获取对应数据对象
            /// </summary>
            /// <param name="key">键</param>
            /// <returns></returns>
            public BList<T> GetDir(Key key)
            {
                //键存在判断
                if (Dir.ContainsKey(key))
                {
                    return Dir[key];
                }
                return null;
            }

        }
    }
}
