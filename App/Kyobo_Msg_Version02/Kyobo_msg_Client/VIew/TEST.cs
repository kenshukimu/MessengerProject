using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kyobo_Msg_Client
{
    public partial class TEST : Form
    {
        public TEST()
        {
            InitializeComponent();
            this.Text = "Text that you want to display";
        }

        public const int FLASHW_STOP = 0;
        public const int FLASHW_ALL = 3;


        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO
        {
            public int cbSize;
            public IntPtr hwnd;
            public int dwFlags;
            public int uCount;
            public int dwTimeout;
        }



        [DllImport("user32.dll")]
        static extern bool FlashWindowEx(ref FLASHWINFO pwfi);



        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            Flash(false);
        }



        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (this.WindowState == FormWindowState.Minimized)
                Flash(true);
        }

        private void Flash(bool flashed)
        {
            FLASHWINFO fi = new FLASHWINFO();
            fi.cbSize = Marshal.SizeOf(typeof(FLASHWINFO));
            fi.hwnd = this.Handle;
            fi.dwFlags = flashed ? FLASHW_ALL : FLASHW_STOP;
            fi.uCount = 10;
            fi.dwTimeout = 500;

            FlashWindowEx(ref fi);
        }
    }
}
