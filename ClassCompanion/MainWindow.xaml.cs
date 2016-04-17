using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using libpxcclr;


namespace ClassCompanion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<ContentData> _ContentCollection =
        new ObservableCollection<ContentData>();






















































































































        VoiceRecognition voice = new VoiceRecognition();

        IntelHandRecognition gesture = new IntelHandRecognition();

        touchlessController controller = new touchlessController();

        GestureControlOverlayMenu gestureControlOverlayMenu = new GestureControlOverlayMenu();

        ContentOption contentOptionsMenu;

        ClassCompanionOptions classCompanionOptionsMenu;

        AddWebsiteWindow addWebsiteMenu;

        LoadAddInWindow loadAddInWindow;

        AboutClassCompanionWindow aboutClassCompanionWindow = new AboutClassCompanionWindow();

        ClassCompanionOverlayMenu classCompanionOverlayMenu = new ClassCompanionOverlayMenu();

        public System.Diagnostics.Process process;

        // Create OpenFileDialog
        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

        //PowerPointControl powerpointcontrol = new PowerPointControl();

        Boolean stop = false;

        //Global Variables Used to set Default Values
        string global_Default_Content_Control = "Gesture";
        string global_Default_Subject = "English";
        string global_class_Companion_Control = "Gesture";

        public MainWindow()
        {
            //add_Content("SeaCreatures", "C:\\Users\\SarDon\\", "Science", "PowerPoint", "Gesture Control", "");
            //add_Content("Area", "C:\\Users\\SarDon\\", "Math", "PowerPoint", "Gesture Control", "");
            //add_Content("Wikipedia", "http://en.wikipedia.org/wiki/NASA", "Science", "Webpage", "Voice Control", "");

            Initialize();

            contentOptionsMenu = new ContentOption(this);

            classCompanionOptionsMenu = new ClassCompanionOptions(this);

            addWebsiteMenu = new AddWebsiteWindow(this);

            loadAddInWindow = new LoadAddInWindow(this);
        }

        private void Initialize()
        {

            //Start Voice Recongnization on a seprate Thread
            System.Threading.Thread thread = new System.Threading.Thread(startVoice);
            thread.Start();
            System.Threading.Thread.Sleep(5);

            //Start Hand Control
            //System.Threading.Thread thread2 = new System.Threading.Thread(startIntelHandRecognition);
            //thread2.Start();
            //System.Threading.Thread.Sleep(5);

            //Start Touchless Controler
            System.Threading.Thread thread3 = new System.Threading.Thread(startTouchlessController);
            thread3.Start();
            System.Threading.Thread.Sleep(5);

            //gestureControlOverlayMenu.Show();

            //classCompanionOverlayMenu.Show();

            //startVoice();
            //startIntelHandRecognition();

            this.minimize();

        }

        public bool IsStop()
        {
            return stop;
        }

        private delegate void VoiceRecognitionCompleted();

        private void startVoice()
        {
            //Create an istance of a PXCMSession
            PXCMSession session = PXCMSession.CreateInstance();
            if (session != null)
            {
                voice.SetUp(this, session);
                session.Dispose();
            }


        }

        private void startTouchlessController()
        {
            PXCMSenseManager session = PXCMSenseManager.CreateInstance();
            if (session != null)
            {
                gestureControlOverlayMenu.SetUpTouchlessController(this, gestureControlOverlayMenu, session);
                session.Dispose();
            }
        }

        private void startIntelHandRecognition()
        {
            PXCMSenseManager session = PXCMSenseManager.CreateInstance();
            if (session != null)
            {
                gesture.SimplePipeline(this, session);
                session.Dispose();
            }
        }

        public ObservableCollection<ContentData> ContentCollection
        { get { return _ContentCollection; } }

        private void OnDrop(object sender, DragEventArgs e)
        {
            if (e.Data != null)
            {
                string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                string filepath = string.Join(".", FileList);

                if (Directory.Exists(filepath))
                {
                    // then it is a directory
                    MessageBox.Show("Please drop a file and not a directory into ClassCompanion.");
                }
                else
                {
                    // then it is a file
                    string directory = System.IO.Path.GetDirectoryName(filepath);
                    string filename = System.IO.Path.GetFileNameWithoutExtension(filepath);
                    string extension = System.IO.Path.GetExtension(filepath);

                    if (CheckIfSupported(extension) == true)
                    {
                        add_Content(filename, directory, global_Default_Subject, identify_Content(extension), global_Default_Content_Control, filepath);
                    }
                    else
                    {
                        MessageBox.Show("Your Content is not supported. It will be added, but it may not work.");
                        add_Content(filename, directory, global_Default_Subject, identify_Content(extension), global_Default_Content_Control, filepath);
                    }

                }

            }


        }

        public Boolean CheckIfSupported(string extension){

            if (identify_Content(extension) == "Unsupported")
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        private string identify_Content(string fileExtension)
        {
            switch (fileExtension)
            {
                case ".txt":
                    return "Document";
                    break;
                case ".pdf":
                    return "Document";
                    break;
                case ".doc":
                    return "Document";
                    break;
                case ".docx":
                    return "Document";
                    break;
                case ".xls":
                    return "Document";
                    break;
                case ".wpd":
                    return "Document";
                    break;
                case ".pub":
                    return "Document";
                    break;
                case ".wps":
                    return "Document";
                    break;
                case ".ppt":
                    return "PowerPoint";
                    break;
                case ".pptx":
                    return "PowerPoint";
                    break;
                case ".pps":
                    return "PowerPoint";
                    break;
                case ".wmv":
                    return "Video";
                    break;
                case ".avi":
                    return "Video";
                    break;
                case ".mpg":
                    return "Video";
                    break;
                case ".mov":
                    return "Video";
                    break;
                case ".divx":
                    return "Video";
                    break;
                case ".mpeg":
                    return "Video";
                    break;
                case ".wav":
                    return "Audio";
                    break;
                case ".wma":
                    return "Audio";
                    break;
                case ".mp3":
                    return "Audio";
                    break;
                case ".mp2":
                    return "Audio";
                    break;
                case ".tif":
                    return "Image";
                    break;
                case ".jpg":
                    return "Image";
                    break;
                case ".bmp":
                    return "Image";
                    break;
                case ".gif":
                    return "Image";
                    break;
                case ".png":
                    return "Image";
                    break;
                case ".jpeg":
                    return "Image";
                    break;
                case ".htm":
                    return "WebSite";
                    break;
                case ".html":
                    return "WebSite";
                    break;
                default:
                    return "Unsupported";
            }

        }

        private void Content_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effects = DragDropEffects.All;
            else
                e.Effects = DragDropEffects.None;

            e.Handled = true;
        }

        public void add_Content(string name, string location, string subject, string contentType, string contentControl, string filepath)
        {
            _ContentCollection.Add(new ContentData
            {
                Name = name,
                Location = location,
                Subject = subject,
                ContentType = contentType,
                ContentControl = contentControl,
                filePath = filepath
            });
        }

        public void modify_Content(string name, string location, string subject, string contentType, string contentControl, string filepath, int index)
        {

            _ContentCollection[index].Name = name;
            _ContentCollection[index].Location = location;
            _ContentCollection[index].Subject = subject;
            _ContentCollection[index].ContentType = contentType;
            _ContentCollection[index].ContentControl = contentControl;
            _ContentCollection[index].filePath = filepath;

            contentListView.Items.Refresh();
        }

        private void add_Content_Button(object sender, RoutedEventArgs e)
        {

            // Set filter for file extension and default file extension
            // Causes major issues
            //dlg.Filter = "Text documents (.txt)|*.txt|PDF Files (*.pdf)|*.pdf|DOC Files (*.doc)|*.doc|DOCX Files (*.docx)|*.docx|Excel Files (*.xls)|*.xls|WPD Files (*.wpd)|*.wpd|PUB Files (*.pub)|*.pub|WPS Files (*.wps)|*.wps|PowerPoint Files (*.ppt,*.pptx,*.pps)|*.ppt;*.pptx;*.pps|Video Files (*.wmv, *.avi, *.mpg, *.mov, *.divx, *.mpeg)|*.wmv;*.avi;*.mpg;*.mov;*.divx;*.mpeg|Audio Files (*.wav,*.wma,*.mp3,*.mp2)|*.wav;*.wma;*.mp3;*.mp2|Image Files (*.bmp, *.jpg, *.jpeg, *.gif, *.png)|*.bmp;*.jpg;*.jpeg;*gif;*.png|html Files (*.html,*.htm)|*.html;*.htm";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filepath = dlg.FileName;
                if (Directory.Exists(filepath))
                {
                    // then it is a directory
                    MessageBox.Show("Please drop a file and not a directory into ClassCompanion.");
                }
                else
                {
                    // then it is a file
                    string directory = System.IO.Path.GetDirectoryName(filepath);
                    string filename = System.IO.Path.GetFileNameWithoutExtension(filepath);
                    string extension = System.IO.Path.GetExtension(filepath);

                    if (CheckIfSupported(extension) == true)
                    {
                        add_Content(filename, directory, global_Default_Subject, identify_Content(extension), global_Default_Content_Control, filepath);
                    }
                    else
                    {
                        MessageBox.Show("Your Content is not supported. It will be added, but it may not work.");
                        add_Content(filename, directory, global_Default_Subject, identify_Content(extension), global_Default_Content_Control, filepath);
                    }

                }

            }
        }

        private void remove_Content_Button(object sender, RoutedEventArgs e)
        {
            var selected = contentListView.SelectedItems.Cast<Object>().ToArray();
            foreach (var item in selected) ContentCollection.Remove((ContentData)item);
        }

        private void up_Content_Button(object sender, RoutedEventArgs e)
        {
            var selectedIndex = this.contentListView.SelectedIndex;

            if (selectedIndex > 0)
            {

                var itemToMoveUp = this.ContentCollection[selectedIndex];
                this.ContentCollection.RemoveAt(selectedIndex);
                this.ContentCollection.Insert(selectedIndex - 1, itemToMoveUp);
                this.contentListView.SelectedIndex = selectedIndex - 1;
            }
        }

        private void down_Content_Button(object sender, RoutedEventArgs e)
        {
            var selectedIndex = this.contentListView.SelectedIndex;

            if (selectedIndex + 1 < this.ContentCollection.Count)
            {
                var itemToMoveDown = this.ContentCollection[selectedIndex];
                this.ContentCollection.RemoveAt(selectedIndex);
                this.ContentCollection.Insert(selectedIndex + 1, itemToMoveDown);
                this.contentListView.SelectedIndex = selectedIndex + 1;
            }
        }

        private void close_Application(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void move_Up_Edit_Menu(object sender, RoutedEventArgs e)
        {
            var selectedIndex = this.contentListView.SelectedIndex;

            if (selectedIndex > 0)
            {

                var itemToMoveUp = this.ContentCollection[selectedIndex];
                this.ContentCollection.RemoveAt(selectedIndex);
                this.ContentCollection.Insert(selectedIndex - 1, itemToMoveUp);
                this.contentListView.SelectedIndex = selectedIndex - 1;
            }
        }

        private void move_Down_Edit_Menu(object sender, RoutedEventArgs e)
        {
            var selectedIndex = this.contentListView.SelectedIndex;

            if (selectedIndex + 1 < this.ContentCollection.Count)
            {
                var itemToMoveDown = this.ContentCollection[selectedIndex];
                this.ContentCollection.RemoveAt(selectedIndex);
                this.ContentCollection.Insert(selectedIndex + 1, itemToMoveDown);
                this.contentListView.SelectedIndex = selectedIndex + 1;
            }
        }

        private void delete_Content_Edit_Menu(object sender, RoutedEventArgs e)
        {
            var selected = contentListView.SelectedItems.Cast<Object>().ToArray();
            foreach (var item in selected) ContentCollection.Remove((ContentData)item);
        }

        private void add_Content_Edit_Menu(object sender, RoutedEventArgs e)
        {

            //// Set filter for file extension and default file extension
            //Causes issues
            //dlg.Filter = "Text documents (.txt)|*.txt|PDF Files (*.pdf)|*.pdf|DOC Files (*.doc)|*.doc|DOCX Files (*.docx)|*.docx"; 

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filepath = dlg.FileName;
                if (Directory.Exists(filepath))
                {
                    // then it is a directory
                    MessageBox.Show("Please drop a file and not a directory into ClassCompanion.");
                }
                else
                {
                    // then it is a file
                    string directory = System.IO.Path.GetDirectoryName(filepath);
                    string filename = System.IO.Path.GetFileNameWithoutExtension(filepath);
                    string extension = System.IO.Path.GetExtension(filepath);

                    if (CheckIfSupported(extension) == true)
                    {
                        add_Content(filename, directory, global_Default_Subject, identify_Content(extension), global_Default_Content_Control, filepath);
                    }
                    else
                    {
                        MessageBox.Show("Your Content is not supported. It will be added, but it may not work.");
                        add_Content(filename, directory, global_Default_Subject, identify_Content(extension), global_Default_Content_Control, filepath);
                    }
                }

            }
        }

        private void preview_Content_Edit_Menu(object sender, RoutedEventArgs e)
        {
            preview_Content();
        }

        private void preview_Content()
        {
            try
            {
                var selectedIndex = this.contentListView.SelectedIndex;
                process = Process.Start(this.ContentCollection[selectedIndex].filePath);

                try
                {
                    while (process.MainWindowHandle == IntPtr.Zero)
                    {
                        // Discard cached information about the process
                        // because MainWindowHandle might be cached.
                        process.Refresh();

                        System.Threading.Thread.Sleep(10);
                    }

                    var handle = process.MainWindowHandle;
                }
                catch
                {
                    // The process has probably exited,
                    // so accessing MainWindowHandle threw an exception
                }


            }
            catch (InvalidOperationException exc)
            {
                if (exc.Source != null)
                {
                    MessageBox.Show("Your content could not be found. Make sure that it wasn't moved.");
                    //Console.WriteLine("IOException source: {0}", exc.Source);
                }
                return;

            }
            catch (ArgumentOutOfRangeException exc)
            {
                if (exc.Source != null)
                {
                    MessageBox.Show("Your content could not be found. Make sure that it wasn't moved.");
                    //Console.WriteLine("IOException source: {0}", exc.Source);
                }
                return;
            }
        }

        private void preview_Content_Button(object sender, RoutedEventArgs e)
        {
            preview_Content();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void about_menu_open(object sender, RoutedEventArgs e)
        {

            aboutClassCompanionWindow.Show();

        }

        private string[] GetItemsAsString()
        {
            string[] arr = new string[contentListView.Items.Count];
            for (int i = 0; i < contentListView.Items.Count; i++)
            {
                arr[i] = _ContentCollection[i].Name;
            }

            return arr;
        }

        public string AlertToString(PXCMSpeechRecognition.AlertType label)
        {
            switch (label)
            {
                case PXCMSpeechRecognition.AlertType.ALERT_SNR_LOW: return "SNR_LOW";
                case PXCMSpeechRecognition.AlertType.ALERT_SPEECH_UNRECOGNIZABLE: return "SPEECH_UNRECOGNIZABLE";
                case PXCMSpeechRecognition.AlertType.ALERT_VOLUME_HIGH: return "VOLUME_HIGH";
                case PXCMSpeechRecognition.AlertType.ALERT_VOLUME_LOW: return "VOLUME_LOW";
                case PXCMSpeechRecognition.AlertType.ALERT_SPEECH_BEGIN: return "SPEECH_BEGIN";
                case PXCMSpeechRecognition.AlertType.ALERT_SPEECH_END: return "SPEECH_END";
                case PXCMSpeechRecognition.AlertType.ALERT_RECOGNITION_ABORTED: return "REC_ABORT";
                case PXCMSpeechRecognition.AlertType.ALERT_RECOGNITION_END: return "REC_END";
            }
            return "Unknown";
        }

        private void start_Lesson_Click(object sender, RoutedEventArgs e)
        {

            //Start Voice and Gesture Control in a different Thread is needed

            //Based upon Class Companion ContentControl Options: Start the right module: Voice, Gesture, Remote, or Remote + Voice

            //Make sure Class Companion is in the forground and item 1 is selected
            this.contentListView.SelectedIndex = 0;

            //Prevent Class Companion from being modified until a specific button is pressed or command is given.
            this.IsEnabled = false;

            //Center Screen
            //CenterWindowOnScreen();

            //Start Class Companion Control - Later add an if statment to choose between voice, gestrue, and etc...

            //gesture.SetUpClassCompanionGesture(this);

            if (global_class_Companion_Control == "Voice")
            {
                gestureControlOverlayMenu.Hide();
                gestureControlOverlayMenu.StopAll();
                voice.SetUpClassCompanionVoice(this);
            }
            else if (global_class_Companion_Control == "Gesture")
            {
                voice.StopAll();
                gestureControlOverlayMenu.SetUpClassCompanionGesture(this);
                gestureControlOverlayMenu.Show();
            }
            else
            {
                MessageBox.Show("Currently Class Companion only supports Voice or Gesture Control. In the future more hybrid controls will be added.");
            }

        }

        public void maximixe()
        {

            Application.Current.MainWindow.WindowState = WindowState.Maximized;
            this.Activate();

        }

        public void Restore()
        {

            Application.Current.MainWindow.WindowState = WindowState.Normal;
            this.Activate();

        }

        public void minimize()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
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

        private void open_Content()
        {
            try
            {
                var selectedIndex = this.contentListView.SelectedIndex;


                System.Threading.Thread.Sleep(1000);

                process = Process.Start(this.ContentCollection[selectedIndex].filePath);

                //ProcessStartInfo startInfo = new ProcessStartInfo(this.ContentCollection[selectedIndex].filePath);
                //startInfo.WindowStyle = ProcessWindowStyle.Maximized;
                //Process.Start(startInfo);

                //try
                //{
                //    while (process.MainWindowHandle == IntPtr.Zero)
                //    {
                        // Discard cached information about the process
                        // because MainWindowHandle might be cached.
                //        process.Refresh();

                //        System.Threading.Thread.Sleep(10);
                //    }

                //    var handle = process.MainWindowHandle;
                //}
                //catch
                //{
                    // The process has probably exited,
                    // so accessing MainWindowHandle threw an exception
                //}

                //Start webpage
                //Process.Start("IExplore.exe", "C:\\myPath\\myFile.asp");


            }
            catch (InvalidOperationException exc)
            {
                if (exc.Source != null)
                {
                    MessageBox.Show("Your content could not be found. Make sure that it wasn't moved.");
                    //Console.WriteLine("IOException source: {0}", exc.Source);
                }
                return;

            }
            catch (ArgumentOutOfRangeException exc)
            {
                if (exc.Source != null)
                {
                    MessageBox.Show("Your content could not be found. Make sure that it wasn't moved.");
                    //Console.WriteLine("IOException source: {0}", exc.Source);
                }
                return;
            }
        }

        public void startContentAndControl()
        {

            var selectedIndex = this.contentListView.SelectedIndex;

            //Get selected item and Call the Setup Function in Voice, Gesture, Remote, or Remote + Voice
            switch (_ContentCollection[selectedIndex].ContentControl)
            {
                case "Voice":
                    switch (_ContentCollection[selectedIndex].ContentType)
                    {
                        case "PowerPoint":
                            voice.StopAllButClassCompanion();
                            voice.SetUpPowerPointVoice();
                            voice.SetClassCompanionBackgrounMode(true);
                            this.open_Content();
                            break;
                        case "Document":
                            voice.StopAllButClassCompanion();
                            voice.SetUpDocumentVoice();
                            voice.SetClassCompanionBackgrounMode(true);
                            this.open_Content();
                            break;
                        case "Audio":
                            voice.StopAllButClassCompanion();
                            //Detect the audio player
                            //Assumes WMP for now
                            voice.SetUpAudioVoice();
                            voice.SetClassCompanionBackgrounMode(true);
                            this.open_Content();
                            break;
                        case "Video":
                            voice.StopAllButClassCompanion();
                            //Detect the video player
                            //Assumes WMP for now
                            voice.SetUpVideoVoice();
                            voice.SetClassCompanionBackgrounMode(true);
                            this.open_Content();
                            break;
                        case "Image":
                            voice.StopAllButClassCompanion();
                            voice.SetUpImageVoice();
                            voice.SetClassCompanionBackgrounMode(true);
                            this.open_Content();
                            break;
                        case "WebSite":
                            voice.StopAllButClassCompanion();
                            voice.SetUpWebpageVoice();
                            voice.SetClassCompanionBackgrounMode(true);
                            this.open_Content();
                            break;
                        default:
                            //Unsupported Maybe open in Website mode?
                            MessageBox.Show("You are trying to start an unsuported filetype.");
                            break;
                    }
                    break;
                case "Gesture":
                    switch (_ContentCollection[selectedIndex].ContentType)
                    {
                        case "PowerPoint":
                            gestureControlOverlayMenu.Disable = true;
                            gestureControlOverlayMenu.Opacity = 0;
                            gestureControlOverlayMenu.StopAllButClassCompanion();
                            gestureControlOverlayMenu.SetUpPowerPointGesture(process);
                            gestureControlOverlayMenu.SetClassCompanionBackgrounMode(true);
                            this.open_Content();
                            gestureControlOverlayMenu.Disable = false;
                            break;
                        case "Document":
                            gestureControlOverlayMenu.StopAllButClassCompanion();
                            gestureControlOverlayMenu.SetUpDocumentGesture();
                            gestureControlOverlayMenu.SetClassCompanionBackgrounMode(true);
                            this.open_Content();
                            break;
                        case "Audio":
                            gestureControlOverlayMenu.StopAllButClassCompanion();
                            //Detect the audio player
                            //Assumes WMP for now
                            gestureControlOverlayMenu.SetUpAudioGesture();
                            gestureControlOverlayMenu.SetClassCompanionBackgrounMode(true);
                            this.open_Content();
                            break;
                        case "Video":
                            gestureControlOverlayMenu.StopAllButClassCompanion();
                            //Detect the video player
                            //Assumes WMP for now
                            gestureControlOverlayMenu.SetUpVideoGesture();
                            gestureControlOverlayMenu.SetClassCompanionBackgrounMode(true);
                            this.open_Content();
                            break;
                        case "Image":
                            gestureControlOverlayMenu.StopAllButClassCompanion();
                            gestureControlOverlayMenu.SetUpImageGesture();
                            gestureControlOverlayMenu.SetClassCompanionBackgrounMode(true);
                            this.open_Content();
                            break;
                        case "WebSite":
                            gestureControlOverlayMenu.StopAllButClassCompanion();
                            gestureControlOverlayMenu.SetUpWebpageGesture();
                            gestureControlOverlayMenu.SetClassCompanionBackgrounMode(true);
                            this.open_Content();
                            break;
                        default:
                            //Unsupported Maybe open ina default mode?
                            MessageBox.Show("You are trying to start an unsuported filetype.");
                            break;
                    }
                    break;
                case "Remote":
                    break;
                case "Remote + Voice":
                    break;
                default:
                    break;
            }

        }

        private void ContentOptionsMenuBar(object sender, RoutedEventArgs e)
        {

            var selectedIndex = this.contentListView.SelectedIndex;

            //Pass Data to the Content Options Menu
            if (selectedIndex == -1)
            {
                MessageBox.Show("Please select a content item from the Lesson Content box.");
            }
            else
            {
                contentOptionsMenu.ClearFields();

                //Load Data into the content options menu
                contentOptionsMenu.LoadData(this._ContentCollection[selectedIndex].Name, this._ContentCollection[selectedIndex].Location, this._ContentCollection[selectedIndex].Subject, this._ContentCollection[selectedIndex].ContentType, this._ContentCollection[selectedIndex].ContentControl, this._ContentCollection[selectedIndex].filePath, selectedIndex);

                //Open the content window in a different window
                //Catch error caused by closing a window and recreate a window if needed.
                contentOptionsMenu.Show();

                this.IsEnabled = false;
            }


        }

        private void MenuItem_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            //Preview Content and Control
            startContentAndControl();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ContentOptionsMenuBar(sender, e);
        }

        public void SetDefaults(string Subject, string Content_Control, string class_Companion_Control)
        {
            global_Default_Content_Control = Content_Control;
            global_Default_Subject = Subject;
            global_class_Companion_Control = class_Companion_Control;
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            //Class Companion Options
            classCompanionOptionsMenu.LoadDefaults(global_Default_Subject, global_Default_Content_Control, global_class_Companion_Control);

            classCompanionOptionsMenu.Show();

            this.IsEnabled = false;
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            //Contact Me
            string command = "mailto:donald.d.durm@gmail.com?subject=ClassCompanion";
            Process.Start(command);
        }

        private void contentListView_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Console_TextBox.ScrollToEnd();
        }

        private void AddWebsiteMenu_Click(object sender, RoutedEventArgs e)
        {

                addWebsiteMenu.ClearFields();
                addWebsiteMenu.LoadData(global_Default_Subject,global_Default_Content_Control);

                //Open the content window in a different window
                //Catch error caused by closing a window and recreate a window if needed.
                addWebsiteMenu.Show();

                this.IsEnabled = false;
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            loadAddInWindow.ClearFields();
            loadAddInWindow.LoadData(global_Default_Subject);

            //Open the content window in a different window
            //Catch error caused by closing a window and recreate a window if needed.
            loadAddInWindow.Show();

            this.IsEnabled = false;
        }

        private void This_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            voice.kill = true;
            gestureControlOverlayMenu.kill = true;
            Application.Current.Shutdown();
        }

    }
     
    public class ContentData
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Subject { get; set; }
        public string ContentType { get; set; }
        public string ContentControl { get; set; }
        public string filePath { get; set; }
    }

}
