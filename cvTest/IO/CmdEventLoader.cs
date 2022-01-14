using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cvTest.Event;
using cvTest.Templates;

namespace cvTest.IO
{

    public class CmdEventLoader
    {

        public CmdEventLoader(CmdItem root)
        {
            EventCenter.InstanceMenu = MenuEventSystem.Instance(root);
            EventCenter.InstanceSystem = SystemEventSystem.Instance(root);
        }

        public class MenuEventSystem : BEventSystem
        {
            private static MenuEventSystem instance = null;
            public static MenuEventSystem Instance(CmdItem root)
            {
                if (instance == null)
                {
                    instance = new MenuEventSystem(root);
                }
                return instance;
            }
            public static class MenuExtra
            {
                public enum Key
                {
                    exit,
                    back,
                    home
                }
                public static Dictionary<string, CmdItem> Dictionary = new()
                {
                    { Key.exit.ToString(), new CmdItem("退出", Key.exit.ToString(), EventCenter.SystemType.system) },
                    { Key.back.ToString(), new CmdItem("返回", Key.back.ToString(), EventCenter.SystemType.menu) },
                    { Key.home.ToString(), new CmdItem("主页", Key.home.ToString(), EventCenter.SystemType.system) },
                };
            }
            public CmdItem RootCurrent { get; internal set; }
            public static List<CmdItem> MenuCurrent { get; internal set; } = new();
            private MenuEventSystem(CmdItem root) : base(EventCenter.SystemType.menu, root)
            {
                Register();
            }
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
            public void Back(string key)
            {
                EventCenter.invoke(RootCurrent.Father.Type, RootCurrent.Father.Key);
            }
            public void ReadCmd(string key)
            {
                int cmdNum = int.Parse(Console.ReadLine());
                if (cmdNum >= 0 && cmdNum <= MenuCurrent.ToArray().Length)
                {
                    CmdItem cmd = MenuCurrent.ToArray()[cmdNum];
                    EventCenter.invoke(cmd.Type, cmd.Key);
                }
            }
        }

        public class SystemEventSystem : BEventSystem
        {
            private static SystemEventSystem instance = null;
            public static SystemEventSystem Instance(CmdItem root)
            {
                if (instance == null)
                {
                    instance = new SystemEventSystem(root);
                }
                return instance;
            }
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
            public void Home(string key)
            {
                EventCenter.invoke(EventCenter.SystemType.menu, base.Root.Key);
            }

        }

    }
}
