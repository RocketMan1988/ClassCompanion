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
    class DocumentControl
    {

        public void ScrollDown()
        {
            SendKeys.SendWait("{PGDN}");
        }

        public void ScrollUp()
        {
            SendKeys.SendWait("{PGUP}");
        }

       
    }
}
