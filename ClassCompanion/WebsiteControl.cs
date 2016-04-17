using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClassCompanion
{
    class WebsiteControl
    {

        public void ScrollDown()
        {
            SendKeys.SendWait("{PGDN}");
        }

        public void ScrollUp()
        {
            SendKeys.SendWait("{PGUP}");
        }

        public void FullScreen()
        {
            SendKeys.SendWait("{F11}");
        }

    }
}
