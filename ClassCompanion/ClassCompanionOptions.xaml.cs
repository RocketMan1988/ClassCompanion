using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ClassCompanionOptions.xaml
    /// </summary>
    public partial class ClassCompanionOptions : Window
    {
        
        MainWindow form;

        string subject_global;
        string contentControl_global;
        string classCompanionControl_global;

        public ObservableCollection<string> subject_List = new ObservableCollection<string>();
        public ObservableCollection<string> contentControl_List = new ObservableCollection<string>();
        public ObservableCollection<string> classCompanionControl_List = new ObservableCollection<string>();

        public ClassCompanionOptions(MainWindow form1)
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

            //Add Content Controls
            contentControl_List.Add("Voice");
            contentControl_List.Add("Gesture");
            contentControl_List.Add("Remote");
            contentControl_List.Add("Remote + Voice");

            this.contentControlnput.ItemsSource = contentControl_List;

            //Add Content Controls
            classCompanionControl_List.Add("Voice");
            classCompanionControl_List.Add("Gesture");
            classCompanionControl_List.Add("Remote");
            classCompanionControl_List.Add("Remote + Voice");

            this.classCompanionControlInput.ItemsSource = classCompanionControl_List;

        }

        public void LoadDefaults(string subjectLoad, string contentControlLoad, string classCompanionControlLoad)
        {
            subject_global = subjectLoad;
            contentControl_global = contentControlLoad;
            classCompanionControl_global = classCompanionControlLoad;

            //Select the default values
            this.subject_Input.SelectedValue = subject_global;
            this.contentControlnput.SelectedValue = contentControl_global;
            this.classCompanionControlInput.SelectedValue = classCompanionControl_global;
        }

        public void ClearFields()
        {
            this.contentControlnput.SelectedIndex = -1;
            this.subject_Input.SelectedIndex = -1;
            this.classCompanionControlInput.SelectedIndex = -1;

        }

        public void SetDefaultContent()
        {
            form.SetDefaults(subject_global,contentControl_global,classCompanionControl_global);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Accept Button Pressed

            //Check that all data is entered
            if (subject_global == "" || contentControl_global == "" || classCompanionControl_global == "")
            {
                MessageBox.Show("Please Fill out all fields.");
            }
            else
            {
                subject_global = this.subject_Input.Text;
                contentControl_global = this.contentControlnput.Text;
                classCompanionControl_global = this.classCompanionControlInput.Text;

                SetDefaultContent();
                this.Hide();

                form.IsEnabled = true;
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Cancel Button
            this.Hide();

            form.IsEnabled = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;

            this.Hide();

            form.IsEnabled = true;
        }

    }
}
