using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Interaction logic for AddWebsiteWindow.xaml
    /// </summary>
    public partial class AddWebsiteWindow : Window
    {


        MainWindow form;

        string directory;
        string filename;
        string extension;
        string filepath;
        string subject_global;
        string contentType_global;
        string contentControl_global;

        int indexNumber;

        public ObservableCollection<string> subject_List = new ObservableCollection<string>();
        public ObservableCollection<string> contentControl_List = new ObservableCollection<string>();
        public ObservableCollection<string> contentType_List = new ObservableCollection<string>();
        

        public AddWebsiteWindow(MainWindow form1)
        {
            form = form1;

            InitializeComponent();

            SetUpContent();
        }

        public void SetUpContent()
        {
            //Add Subjects
            subject_List.Add("English");
            subject_List.Add("Math");
            subject_List.Add("Science");
            subject_List.Add("Social Studies");
            subject_List.Add("Foreign Language");
            subject_List.Add("Special Education");
            subject_List.Add("Other");

            this.subject_Input.ItemsSource = subject_List;

            //Add Content Types
            contentType_List.Add("Document");
            contentType_List.Add("PowerPoint");
            contentType_List.Add("Video");
            contentType_List.Add("Audio");
            contentType_List.Add("Image");
            contentType_List.Add("Website");
            contentType_List.Add("Unsupported");
            
            this.contentTypeInput.ItemsSource = contentType_List;

            //Add Content Controls
            contentControl_List.Add("Voice");
            contentControl_List.Add("Gesture");
            contentControl_List.Add("Remote");
            contentControl_List.Add("Remote + Voice");

            this.contentControlnput.ItemsSource = contentControl_List;

        }

        public void LoadData(String Subject, String ContentControl)
        {
            this.contentControlnput.Text = ContentControl;
            this.subject_Input.Text = Subject;
            this.contentTypeInput.Text = "WebSite";

            this.name_Input.Text = "Input Name";
            this.file_Location_Box.Text = "https://";
        }

        public void ClearFields()
        {
            this.contentControlnput.SelectedIndex = -1;
            this.subject_Input.SelectedIndex = -1;
            this.contentTypeInput.SelectedIndex = -1;

            this.name_Input.Text = "";
            this.file_Location_Box.Text = "";

        }

        public void AddContent()
        {
            form.add_Content(filename, directory, subject_global, contentType_global, contentControl_global, filepath);
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Accept Button Pressed

            //Check that all data is entered
            if(filename == "" || directory == "" || subject_global == "" || contentControl_global == "" || contentType_global == "" || filepath == ""){
                MessageBox.Show("Please Fill out all fields.");
            }
            else
            {
                filename = this.name_Input.Text;
                subject_global = this.subject_Input.Text;
                contentType_global = this.contentTypeInput.Text;
                contentControl_global = this.contentControlnput.Text;
                directory = this.file_Location_Box.Text;
                filepath = this.file_Location_Box.Text;

                AddContent();
                this.Hide();
            }

            form.IsEnabled = true;
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Cancel Button Pressed
            this.Hide();

            form.IsEnabled = true;
        }


        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();

            form.IsEnabled = true;
        }
    }
}
