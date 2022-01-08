using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace cvBase.Type
    {
    /// <summary>
    /// 扩展bool
    /// <para>可监测变量变化以实现一次方法输出</para>
    /// <para>在频繁调用函数体中使用可大大提高处理效率</para>
    /// </summary>
    public class bool_cv
        {
            private bool m_bool;
            private bool m_bool_tmp;   //缓存布尔  
            /// <summary>
            /// 布尔对生成方法
            /// </summary>
            public enum boolState  
            {
                /// <summary>
                /// 布尔对一致
                /// </summary>
                same,
                /// <summary>
                /// 缓存布尔与初始设置布尔相反，用于使第一次change调用生效
                /// </summary>
                different
            }
            /// <summary>
            /// 无参构造
            /// </summary>
            /// <param name="b">初始布尔值</param>
            public bool_cv(bool b)
            {
                m_bool = b;
                m_bool_tmp = b;
            }
           /// <summary>
           /// 分类构造函数
           /// </summary>
           /// <param name="b">初始布尔</param>
           /// <param name="state">布尔生成方式</param>
            public bool_cv(bool b, boolState state)
            {
                m_bool = b;
                switch (state)
                {
                    case boolState.same:
                        m_bool_tmp = b;
                        break;
                    case boolState.different:
                        m_bool_tmp = !b;
                        break;
                }
            }
            /// <summary>
            /// 设置变化布尔
            /// </summary>
            /// <param name="b">改变布尔值</param>
            public void setBool(bool b)
            {
                m_bool_tmp = b;
            }
            /// <summary>
            /// 布尔变化判别
            /// <para>用于函数体中按布尔调用，实现多次判断一次调用</para>
            /// </summary>
            /// <returns>布尔是否变化</returns>
            public bool isChange()
            {
                //缓存布尔与初始布尔不同时，说明发生改变
                if (m_bool != m_bool_tmp)
                {
                    m_bool_tmp = m_bool;
                    return true;
                }
                return false;
            }
        }
    }

