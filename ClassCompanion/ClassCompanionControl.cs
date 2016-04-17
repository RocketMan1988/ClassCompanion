using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassCompanion
{

    class ClassCompanionControl
    {

        MainWindow form;

        public ClassCompanionControl(MainWindow form1)
        {
            form = form1;
        }

        public void NextItem()
        {
            form.Dispatcher.Invoke((Action)(() =>
            {
                if (form.contentListView.SelectedIndex >= form.contentListView.Items.Count)
                {

                }
                else
                {
                    form.contentListView.SelectedIndex = form.contentListView.SelectedIndex + 1;
                }
            }));
        }

        public void LastItem()
        {

                form.Dispatcher.Invoke((Action)(() =>
                {
                    if (form.contentListView.SelectedIndex <= 0)
                    {

                    }
                    else
                    {
                        form.contentListView.SelectedIndex = form.contentListView.SelectedIndex - 1;
                    }
                }));
        }

        public void StartItem()
        {
            form.Dispatcher.Invoke((Action)(() =>
            {
                form.startContentAndControl();
            }));
        }

        public void ActivateWindow()
        {
            form.Dispatcher.Invoke((Action)(() =>
            {
                form.WindowState = System.Windows.WindowState.Minimized;
                form.WindowState = System.Windows.WindowState.Normal;
                form.Activate();
            }));
        }

        public void MinimizeWindow()
        {
            form.Dispatcher.Invoke((Action)(() =>
            {
                form.WindowState = System.Windows.WindowState.Minimized;
            }));
        }

        public void ExitControl(){

            form.Dispatcher.Invoke((Action)(() =>
            {
                form.IsEnabled = true;
            }));
            
        }


    }
}
