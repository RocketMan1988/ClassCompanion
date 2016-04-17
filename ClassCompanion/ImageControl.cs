using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClassCompanion
{
    class ImageControl
    {

        public void Next_Picture()
        {
            SendKeys.SendWait("{LEFT}");
        }

        public void Last_Picture()
        {
            SendKeys.SendWait("{RIGHT}");
        }

    }
}
