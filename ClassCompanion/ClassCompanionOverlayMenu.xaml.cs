using System;
using System.Collections.Generic;
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
    /// Interaction logic for ClassCompanionOverlayMenu.xaml
    /// </summary>
    public partial class ClassCompanionOverlayMenu : Window
    {
        public ClassCompanionOverlayMenu()
        {
            InitializeComponent();
            
            CenterWindowOnScreen();
            this.Activate();

        }

        public void LoadItems(String[] items){

            lstBox.Items.Clear();

            for (int i = 0; i < items.Length; i++)
            {
                lstBox.Items.Add(items[i]);
            }
            lstBox.FontSize = 50;

            lstBox.SelectedItem = 0;
            lstBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;

        }

        public void LoadListBox(ListItemCollection listBoxLoad){



        }

        public void OpenMenu(String[] items)
        {
            LoadItems(items);

            CenterWindowOnScreen();
            
            this.Activate();
        }

        public void CloseMenu(String[] items)
        {
            this.Hide();
        }

        private void SendItemSelection()
        {

        }

        private void maximixe()
        {

            Application.Current.MainWindow.WindowState = WindowState.Maximized;
            this.Activate();

        }

        private void minimize()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void changeOpacity(Double value)
        {
            this.Opacity = value;
        }

        private void CenterWindowOnScreen()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
        }



    }


    public class Img
    {
        public Img(string value, Image img) { Str = value; Image = img; }
        public string Str { get; set; }
        public Image Image { get; set; }
    }


}

