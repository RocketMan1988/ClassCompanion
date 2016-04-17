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
    class PowerPointControl
    {

        public void NextSlide()
        {
            SendKeys.SendWait("{RIGHT}");
        }

        public void PerviousSlide()
        {
            SendKeys.SendWait("{LEFT}");
        }

        public void FullScreen()
        {
            SendKeys.SendWait("{F5}");
        }

        public void VideoPlayPause()
        {
            SendKeys.SendWait("%(P)");
        }

        public void VolumeUp()
        {
            SendKeys.SendWait("%({UP})");
        }

        public void VolumeDown()
        {
            SendKeys.SendWait("%({DOWN})");
        }

        public void Exit()
        {
            SendKeys.SendWait("{ESC}");
        }
    }
}
