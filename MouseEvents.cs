using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace paintPratice1
{
    class MouseEvents
    {


        [DllImport("user32")]

        public static extern int SetCursorPos(int x, int y);

        private const int MOUSEEVENTF_MOVE = 0x0001;
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
       

        [DllImport("user32.dll",
            CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]

        public static extern void mouse_event(int dwflags, int dx, int dy, int cButtons, int dwExtraInfo);

        public static void LeftClick()
        {  mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);

       

        }

        public static void LeftClickUp()
        { mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0); }

    }
}
