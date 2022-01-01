using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cvBase.Type
{
    /// <summary>
    /// 扩展bool，可监测变量变化以实现一次方法输出
    /// </summary>
    public class cvBool
    {
        private bool m_bool;   
        private bool m_bool_tmp;   //缓存布尔  
        public enum boolState   //是否生成同状态布尔
        {
            same,
            different
        }
        public cvBool(bool b)
        {
            m_bool = b;
            m_bool_tmp = b;
        }
        public cvBool(bool b, boolState state)
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

        public void setBool(bool b)
        {
            m_bool_tmp = b;
        }


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
