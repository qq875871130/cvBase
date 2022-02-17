using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cvTest.IO;
using cvTest.Event;
using cvTest.Templates;

namespace cvTest.DS
{
    /// <summary>
    /// 数据结构事件基类
    /// </summary>
    public class DSBEventSystem : BEventSystem
    {
        public DSBEventSystem(EventCenter.SystemType systemType) : base(systemType, EventCenter.GetRoot())
        {
            Register();
        }
        protected override void Register()
        {
            //构造委托链
            Method home = new Method(Menu) + new Method(CmdEventLoader.MenuEventSystem.ReadCmd);
            Method refresh = new Method(CmdEventLoader.MenuEventSystem.RefreshMenu) + new Method(CmdEventLoader.MenuEventSystem.ReadCmd);
            //注册所有列表菜单事件
            foreach (string key in base.Root.GetKeys(SystemType))
            {
                base.EventSystem.Add(key, home);
            }
            //注册操作事件
            base.EventSystem.Add(Key.add.ToString(), new Method(Add) + refresh);
            base.EventSystem.Add(Key.check.ToString(), new Method(Check) + refresh);
            base.EventSystem.Add(Key.get.ToString(), new Method(Get) + refresh);
            base.EventSystem.Add(Key.clear.ToString(), new Method(Clear) + refresh);
            base.EventSystem.Add(Key.search.ToString(), new Method(Search) + refresh);
            base.EventSystem.Add(Key.delete.ToString(), new Method(Delete) + refresh);
            base.Register();
        }
        public enum Key
        {
            check,
            get,
            clear,
            search,
            add,
            delete
        }
        protected virtual bool isDSExist() { return false; }
        private static CmdItem InitCmd(CmdItem root)
        {
            CmdItem Root = root;
            Root.ClearCmd();
            Root.AddCmd(new CmdItem("检查", Key.check.ToString(), root.Type));
            Root.AddCmd(new CmdItem("获取", Key.get.ToString(), root.Type));
            Root.AddCmd(new CmdItem("清空", Key.clear.ToString(), root.Type));
            Root.AddCmd(new CmdItem("搜索", Key.search.ToString(), root.Type));
            Root.AddCmd(new CmdItem("插入", Key.add.ToString(), root.Type));
            Root.AddCmd(new CmdItem("删除", Key.delete.ToString(), root.Type));
            return Root;
        }
        public void Menu(CmdItem sender)
        {
            CmdEventLoader.MenuEventSystem.MenuPrinter(InitCmd(sender));
        }

        protected virtual void Check(CmdItem sender)
        {
            if (!isDSExist())
            {
                throw new MyExceptions.NoCompatibleDSException();
            }
        }

        protected virtual void Get(CmdItem sender)
        {
            if (!isDSExist())
            {
                throw new MyExceptions.NoCompatibleDSException();
            }
        }

        protected virtual void Clear(CmdItem sender)
        {
            if (!isDSExist())
            {
                throw new MyExceptions.NoCompatibleDSException();
            }
        }

        protected virtual void Search(CmdItem sender)
        {
            if (!isDSExist())
            {
                throw new MyExceptions.NoCompatibleDSException();
            }
        }

        protected virtual void Add(CmdItem sender)
        {
            if (!isDSExist())
            {
                throw new MyExceptions.NoCompatibleDSException();
            }
        }

        protected virtual void Delete(CmdItem sender)
        {
            if (!isDSExist())
            {
                throw new MyExceptions.NoCompatibleDSException();
            }
        }

    }

}
