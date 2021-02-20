using System;
using System.Collections.Generic;
using System.Linq;

namespace Kyobo_Msg_Server
{
    public sealed class MainMgr
    {
        public static MainMgr Instance { get; } = new MainMgr();

        MainMgr() { }

        ServerMain frmMain;
        public static ServerMain GetFrmMain() { return Instance.frmMain; }

        public void Init()
        {
            this.frmMain = new ServerMain();

            //ReadRegConfig();
            //ReadSQLite("Data Source=APCExam.db;Pooling=true;FailIfMissing=false");
            //FirstReadExamData();            
            Console.WriteLine("MainProgram Init Server");
        }
    }
}
