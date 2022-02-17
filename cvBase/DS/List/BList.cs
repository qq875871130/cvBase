using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cvBase.DS
{
    /// <summary>
    /// 表结构基类
    /// </summary>
    /// <typeparam name="T">数据泛型</typeparam>
    public class BList<T>
    {
        /// <summary>
        /// 插入结点函数
        /// </summary>
        /// <param name="data">插入数据</param>
        public virtual void Add(T data) { }
        /// <summary>
        /// 插入结点函数
        /// </summary>
        /// <param name="data">插入数据</param>
        /// <param name="type">插入方向</param>
        public virtual void Add(T data, List.AddType type) { }
        /// <summary>
        /// 插入结点函数
        /// </summary>
        /// <param name="data">插入数据</param>
        /// <param name="pos">插入位置</param>
        public virtual void Add(T data, int pos) { }
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
        public virtual void Add(T data, int index, List.AddType type) { }
        /// <summary>
        /// 获取结点
        /// </summary>
        /// <param name="index">结点位置</param>
        /// <returns>对应位置结点</returns>
        public virtual List.LinkList<T>.Single.Node GetSNode(int index) { return default; }
        /// <summary>
        /// 获取索引位置的结点
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public virtual List.LinkList<T>.Double.Node GetDNode(int index) { return default; }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="index">数据位置</param>
        /// <returns>对应位置的数据</returns>
        public virtual T GetData(int index) { return default; }
        /// <summary>
        /// 获取匹配数据的索引
        /// </summary>
        /// <param name="data">待匹配数据</param>
        /// <returns>匹配数据首个索引</returns>
        public virtual int GetIndex(T data) { return default; }
        /// <summary>
        /// 获取匹配数据的索引集
        /// </summary>
        /// <param name="data">待匹配数据</param>
        /// <returns>匹配的所有索引组成的数组</returns>
        public virtual int[] GetIndexes(T data)
        {
            return default;
        }
        /// <summary>
        /// 按索引批量删除结点
        /// </summary>
        /// <param name="indexes">待删索引数组</param>
        public virtual void Delete(int[] indexes)
        {
        }
        /// <summary>
        /// 删除结点
        /// </summary>
        /// <param name="index">待删索引</param>
        public virtual void Delete(int index)
        {
        }
        /// <summary>
        /// 删除结点
        /// </summary>
        /// <param name="data">待删结点数据</param>
        public virtual void Delete(T data)
        {
        }
        /// <summary>
        /// 按区间批量删除结点
        /// </summary>
        /// <param name="st">起始位置</param>
        /// <param name="dst">终点位置</param>
        public virtual void Delete(int st, int dst)
        {
        }
        /// <summary>
        /// 清空表
        /// </summary>
        public virtual void Clear()
        {
        }
        /// <summary>
        /// 转化为数组
        /// </summary>
        /// <returns>对应数据域数组</returns>
        public virtual T[] ToArray()
        {
            return default;
        }
        /// <summary>
        /// 判断方法是否重写
        /// </summary>
        /// <param name="methodName">方法名称</param>
        /// <param name="paramArray">参数数组</param>
        /// <returns>是否重写</returns>
        public bool IsOverride(string methodName, System.Type[] paramArray)
        {
            return !(GetType().GetMethod(methodName, paramArray).DeclaringType.Equals(typeof(BList<T>)));
        }

    }
}
