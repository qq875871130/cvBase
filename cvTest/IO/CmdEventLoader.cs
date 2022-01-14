using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cvTest.Event;
using cvTest.Templates;

namespace cvTest.IO
{
    /// <summary>
    /// 初始化加载预制消息系统并生成通信单例
    /// </summary>
    public class CmdEventLoader
    {
        public CmdEventLoader(CmdItem root)
        {
            //注册事件系统，连接至控制中心
            EventCenter.InstanceMenu = MenuEventSystem.Instance(root);
            EventCenter.InstanceSystem = SystemEventSystem.Instance(root);
        }
        /// <summary>
        /// 菜单消息系统
        /// </summary>
        public class MenuEventSystem : BEventSystem
        {
            #region 单例模式
            private static MenuEventSystem instance = null;
            public static MenuEventSystem Instance(CmdItem root)
            {
                if (instance == null)
                {
                    instance = new MenuEventSystem(root);
                }
                return instance;
            }
            #endregion
            /// <summary>
            /// 系统附加控制菜单项目
            /// </summary>
            public static class MenuExtra
            {
                /// <summary>
                /// 附加菜单键
                /// </summary>
                public enum Key
                {
                    exit,
                    back,
                    home
                }
                /// <summary>
                /// 菜单项字典
                /// </summary>
                public static Dictionary<string, CmdItem> Dictionary = new()
                {
                    { Key.exit.ToString(), new CmdItem("退出", Key.exit.ToString(), EventCenter.SystemType.system) },
                    { Key.back.ToString(), new CmdItem("返回", Key.back.ToString(), EventCenter.SystemType.menu) },
                    { Key.home.ToString(), new CmdItem("主页", Key.home.ToString(), EventCenter.SystemType.system) },
                };
            }
            /// <summary>
            /// 当前根项目
            /// </summary>
            public CmdItem RootCurrent { get; internal set; }
            /// <summary>
            /// 当前菜单项集
            /// </summary>
            public static List<CmdItem> MenuCurrent { get; internal set; } = new();
            private MenuEventSystem(CmdItem root) : base(EventCenter.SystemType.menu, root)
            {
                Register();
            }
            /// <summary>
            /// 继承自基类的注册函数
            /// <para>注册当前系统所有委托并在基类将系统注册至中心</para>
            /// </summary>
            protected override void Register()
            {
                //注册所有列菜单事件
                foreach (string key in base.Root.GetKeys(SystemType))
                {
                    base.EventSystem.Add(key, new Method<string>(MenuPrinter));
                    base.EventSystem.Add(key, new Method<string>(ReadCmd));
                }
                base.EventSystem.Add(MenuExtra.Key.back.ToString(), new Method<string>(Back));
                base.Register();
            }
            #region 菜单消息系统所有委托方法
            /// <summary>
            /// 菜单控制列表生成函数
            /// <para>根据附加菜单集与现有测试集生成总控制列表，并更新当前菜单项集</para>
            /// </summary>
            /// <param name="root">用于生成列表的项目载点</param>
            public void CmdGenerator(CmdItem root)
            {
                MenuCurrent.Clear();
                foreach (var item in MenuExtra.Dictionary)
                {
                    if (root.Father == null && item.Key == MenuExtra.Key.back.ToString())
                    {
                        continue;
                    }
                    item.Value.Father = root;
                    MenuCurrent.Add(item.Value);
                };
                if (root.CmdItems != null)
                {
                    foreach (CmdItem item in root.CmdItems)
                    {
                        MenuCurrent.Add(item);
                    }
                }
            }
            /// <summary>
            /// 菜单打印函数
            /// <para>浏览菜单并输出项目至控制台，并更新当前根项目</para>
            /// </summary>
            /// <param name="key">菜单项目键值</param>
            public void MenuPrinter(string key)
            {
                try
                {
                    RootCurrent = base.Root.GetItem(key, base.SystemType);
                    if (RootCurrent != null)
                    {
                        //打印标题
                        string title = CmdLine.Formatter.Aligner(RootCurrent.Name, 5, CmdLine.Formatter.Align.Center);
                        CmdLine.Write(title);
                        //构造现有菜单
                        CmdGenerator(RootCurrent);
                        //列出当前菜单
                        CmdItem[] cmds = MenuCurrent.ToArray();
                        for (int i = 0; i < cmds.Length; i++)
                        {
                            CmdLine.Formatter.Lister(i.ToString(), cmds[i].Name);
                        }
                    }
                }
                catch (Exception e)
                {
                    CmdLine.Write(e.ToString());
                    throw;
                }
            }
            /// <summary>
            /// 菜单返回函数
            /// </summary>
            /// <param name="key">空，应付委托格式</param>
            public void Back(string key)
            {
                EventCenter.invoke(RootCurrent.Father.Type, RootCurrent.Father.Key);
            }
            /// <summary>
            /// 用户输入读取函数
            /// </summary>
            /// <param name="key">应付委托格式</param>
            public void ReadCmd(string key)
            {
                try
                {
                    //转化输入为数字指令
                    int cmdNum = Convert.ToInt32(Console.ReadLine());
                    //在当前菜单集中寻访项目
                    if (cmdNum >= 0 && cmdNum <= MenuCurrent.ToArray().Length)
                    {
                        CmdItem cmd = MenuCurrent.ToArray()[cmdNum];
                        //执行项目指令
                        EventCenter.invoke(cmd.Type, cmd.Key);
                    }
                }
                catch (Exception)
                {
                    //输入格式为非数字时，提示错误并要求重新输入
                    CmdLine.Write("输入有误，请重试", CmdLine.WriteState.no_clear);
                    ReadCmd(key);
                    throw;
                }
            }
            #endregion
        }
        /// <summary>
        /// 系统消息系统，结构与菜单相似
        /// </summary>
        public class SystemEventSystem : BEventSystem
        {
            #region 单例模式
            private static SystemEventSystem instance = null;
            public static SystemEventSystem Instance(CmdItem root)
            {
                if (instance == null)
                {
                    instance = new SystemEventSystem(root);
                }
                return instance;
            }
            #endregion
            private SystemEventSystem(CmdItem root) : base(EventCenter.SystemType.system, root)
            {
                Register();
            }
            protected override void Register()
            {
                base.EventSystem.Add(MenuEventSystem.MenuExtra.Key.exit.ToString(), new Method<string>(Exit));
                base.EventSystem.Add(MenuEventSystem.MenuExtra.Key.home.ToString(), new Method<string>(Home));
                base.Register();
            }
            #region 系统消息委托
            //退出控制台
            public void Exit(string key)
            {
                CmdLine.Write("确定要退出吗？(Y/N)");
                if (CmdLine.GetInput())
                {
                    Environment.Exit(0);
                }
                else
                {
                    //返回主页
                    Home("");
                }
            }
            //返回主菜单
            public void Home(string key)
            {
                EventCenter.invoke(EventCenter.SystemType.menu, base.Root.Key);
            }
            #endregion
        }

    }
}
