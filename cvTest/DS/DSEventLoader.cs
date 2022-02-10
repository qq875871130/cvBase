using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cvTest.IO;
using cvTest.Event;
using cvBase.DS;

namespace cvTest.DS
{
    public class DSEventLoader
    {
        public DSEventLoader()
        {
            new ListEventSystem();
        }

        public class ListEventSystem : DSBEventSystem
        {
            public ListEventSystem() : base(EventCenter.SystemType.list)
            {
            }
            private void debug(CmdItem sender)
            {
                CmdLine.Write(sender.Name + CmdEventLoader.MenuEventSystem.RootCurrent.Name);
                if (!CmdLine.GetInput())
                {
                    debug(sender);
                }
            }
            protected override void Add(CmdItem sender)
            {
                debug(sender);
                base.Add(sender);
            }
            protected override void Check(CmdItem sender)
            {
                debug(sender);
                base.Check(sender);
            }

            protected override void Get(CmdItem sender)
            {
                debug(sender);
                base.Get(sender);
            }

            protected override void Delete(CmdItem sender)
            {
                debug(sender);
                base.Delete(sender);
            }

            protected override void Search(CmdItem sender)
            {
                debug(sender);
                base.Search(sender);
            }

            protected override void Clear(CmdItem sender)
            {
                debug(sender);
                base.Clear(sender);
            }
        }
    }
}
