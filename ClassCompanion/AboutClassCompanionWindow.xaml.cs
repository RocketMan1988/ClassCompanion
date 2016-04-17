using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClassCompanion
{
    /// <summary>
    /// Interaction logic for AboutClassCompanionWindow.xaml
    /// </summary>
    public partial class AboutClassCompanionWindow : Window
    {
        public AboutClassCompanionWindow()
        {
            InitializeComponent();

        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

    }
}
