using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClockTest
{
    public class Message
    {
        static System.Threading.Timer _timeoutTimer;
        static string _caption;

        public static void AutoCloseMsb(string text, string caption, int timeout)
        {
            _caption = caption;
            _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                null, timeout, System.Threading.Timeout.Infinite);
            MessageBox.Show(text, caption);
        }

        private static void OnTimerElapsed(object state)
        {
            IntPtr mbWnd = FindWindow("#32770", _caption);
            if (mbWnd != IntPtr.Zero)
                SendMessage(mbWnd, WmClose, IntPtr.Zero, IntPtr.Zero);
            _timeoutTimer.Dispose();
        }

        private const int WmClose = 0x0010;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
    }
}
