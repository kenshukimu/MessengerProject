using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kyobo_Msg_Client
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Get Reference to the current Process
            Process thisProc = Process.GetCurrentProcess();

            if (IsProcessOpen("Kyobo_Msg_Client") == false)
            {
                //System.Windows.MessageBox.Show("Application not open!");
                //System.Windows.Application.Current.Shutdown();
                MainProg.Instance.Init();
                Application.Run(MainProg.GetLoginForm());
            }
            else
            {
                // Check how many total processes have the same name as the current one
                if (Process.GetProcessesByName(thisProc.ProcessName).Length > 1)
                {
                    // If ther is more than one, than it is already running.
                    System.Windows.MessageBox.Show("프로그램이 실행 중에 있습니다.");
                    Application.Exit();
                    return;
                }
                else
                {
                    MainProg.Instance.Init();
                    Application.Run(MainProg.GetLoginForm());
                }
            }
        }

        public static bool IsProcessOpen(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.Contains(name))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
