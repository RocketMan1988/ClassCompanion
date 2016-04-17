using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//** add PowerPoint namespace 
using Microsoft.Office.Core;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;

//Using Threading Dispatcher
using System.Windows.Threading;

namespace ClassCompanion
{
    class VideoControl
    {

        //Quicktime
        //Play/Pause: Space Bar
        //Volume Up: Up Arrow
        //Volume Down: Down Arrow

        public void QT_Play_Pause()
        {
            SendKeys.SendWait(" ");
        }

        public void QT_Volume_Up()
        {
            SendKeys.SendWait("{UP}");
        }

        public void QT_Volume_Down()
        {
            SendKeys.SendWait("{DOWN}");
        }


        //VLC and Itunes
        //Play/Pause: Space Bar
        //Volume Up: Cntl + Up
        //Volume Down: Cntl + Down
        public void VLC_ITUNES_Play_Pause()
        {
            SendKeys.SendWait(" ");
        }

        public void VLC_ITUNES_Volume_Up()
        {
            SendKeys.SendWait("^({UP})");
        }

        public void VLC_ITUNES_Volume_Down()
        {
            SendKeys.SendWait("^({DOWN})");
        }


        // % - Alt
        // ^ - Cntl
        // + - SHIFT

        //Windows Media Player - 
        //Fullscreen: Alt + Enter 
        //Play/Pause: Ctrl+P
        //Stop: Ctrl+S
        //Volume Up: F9
        //Volume Down:F8
        //Mute F7
        public void WMI_Fullscreen()
        {
            SendKeys.SendWait("%({ENTER})");
        }

        public void WMI_Play_Pause()
        {
            SendKeys.SendWait("^(p)");
        }

        public void WMI_Stop_Restart()
        {
            SendKeys.SendWait("^(s)");
        }

        public void WMI_Volume_Up()
        {
            SendKeys.SendWait("{F9}");
        }

        public void WMI_Volume_Down()
        {
            SendKeys.SendWait("{F8}");
        }

        public void WMI_Mute()
        {
            SendKeys.SendWait("{F7}");
        }


    }
}
