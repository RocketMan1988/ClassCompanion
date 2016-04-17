using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassCompanion
{
    class VoiceRecognition
    {


            MainWindow form;
            PXCMAudioSource source;
            PXCMSpeechRecognition sr;

            PowerPointControl powerpointcontrol;
            AudioControl audiocontrol;
            VideoControl videocontrol;
            ClassCompanionControl classcompanioncontrol;
            DocumentControl documentcontrol;
            ImageControl imagecontrol;
            WebsiteControl webpagecontrol;
            PXCMSpeechRecognition.Handler handler;

            Boolean powerpointVoiceFlag = false;
            Boolean audioVoiceFlag = false;
            Boolean videoVoiceFlag = false;
            Boolean classcompanionVoiceFlag = false;
            Boolean documentVoiceFlag = false;
            Boolean imageVoiceFlag = false;
            Boolean webpageVoiceFlag = false;
            Boolean wikiTextFlag = false;

            public Boolean kill = false;

            Boolean backgroundMode = false;

            string websiteURL;
            String[] cmds;

            //System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"c:\mywavfile.wav");
            
            SoundPlayer sp;


            [DllImport("user32.dll")]
            static extern IntPtr GetForegroundWindow();

            [DllImport("user32.dll")]
            static extern IntPtr SetActiveWindow(IntPtr hWnd);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
            static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
            public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

            [Flags]
            public enum MouseEventFlags
            {
                LeftDown = 0x00000002,
                LeftUp = 0x00000004,
                MiddleDown = 0x00000020,
                MiddleUp = 0x00000040,
                Move = 0x00000001,
                Absolute = 0x00008000,
                RightDown = 0x00000008,
                RightUp = 0x00000010
            }

            [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool SetCursorPos(int X, int Y);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool GetCursorPos(out MousePoint lpMousePoint);

            [DllImport("user32.dll")]
            private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

            public static void SetCursorPosition(int X, int Y)
            {
                SetCursorPos(X, Y);
            }

            public static void SetCursorPosition(MousePoint point)
            {
                SetCursorPos(point.X, point.Y);
            }

            public static MousePoint GetCursorPosition()
            {
                MousePoint currentMousePoint;
                var gotPoint = GetCursorPos(out currentMousePoint);
                if (!gotPoint) { currentMousePoint = new MousePoint(0, 0); }
                return currentMousePoint;
            }

            public static void MouseEvent(MouseEventFlags value)
            {
                MousePoint position = GetCursorPosition();

                mouse_event
                    ((int)value,
                     position.X,
                     position.Y,
                     0,
                     0)
                    ;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct MousePoint
            {
                public int X;
                public int Y;

                public MousePoint(int x, int y)
                {
                    X = x;
                    Y = y;
                }

            }
        
            void OnRecognition(PXCMSpeechRecognition.RecognitionData data)
            {

                if (data.scores[0].confidence > 0)
                {

                    //"Select", "Wiki", "Up", "Down", "FullScreen","Pervious","Last","Back", "Forward", "Next", "Start", "Open", "Begin", "Exit", "Class Companion", "Play", "Stop", "Volume Up", "Volume Down", "Mute" 
                    if (powerpointVoiceFlag == true)
                    {
                        switch (data.scores[0].sentence.ToUpper())
                        {
                            case "FORWARD":
                                powerpointcontrol.NextSlide();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Next Slide" + Environment.NewLine);
                                }));
                                break;
                            case "NEXT":
                                powerpointcontrol.NextSlide();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Next Slide" + Environment.NewLine);
                                }));
                                break;
                            case "FULLSCREEN":
                                powerpointcontrol.FullScreen();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("FullScreen" + Environment.NewLine);
                                }));
                                break;
                            case "PERVIOUS SLIDE":
                                powerpointcontrol.PerviousSlide();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Pervious Slide" + Environment.NewLine);
                                }));
                                break;
                            case "PERVIOUS":
                                powerpointcontrol.PerviousSlide();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Pervious Slide" + Environment.NewLine);
                                }));
                                break;
                            case "LAST SLIDE":
                                powerpointcontrol.PerviousSlide();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Pervious Slide" + Environment.NewLine);
                                }));
                                break;
                            case "BACK":
                                powerpointcontrol.PerviousSlide();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Pervious Slide" + Environment.NewLine);
                                }));
                                break;
                            case "PLAY":
                                powerpointcontrol.VideoPlayPause();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Play" + Environment.NewLine);
                                }));
                                break;
                            case "PAUSE":
                                powerpointcontrol.VideoPlayPause();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Pause" + Environment.NewLine);
                                }));
                                break;
                            case "EXIT":
                                powerpointcontrol.Exit();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Exit" + Environment.NewLine);
                                }));
                                break;
                            case "STOP":
                                powerpointcontrol.VideoPlayPause();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Stop" + Environment.NewLine);
                                }));
                                break;
                            case "VOLUME UP":
                                powerpointcontrol.VolumeUp();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Volume Up" + Environment.NewLine);
                                }));
                                break;
                            case "VOLUME DOWN":
                                powerpointcontrol.VolumeDown();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Volume Down" + Environment.NewLine);
                                }));
                                break;
                            default:
                                Console.WriteLine("No action to powerpoint...");
                                break;
                        }    
                    }

                    if (audioVoiceFlag == true)
                    {
                        switch (data.scores[0].sentence.ToUpper())
                        {
                            case "PLAY":
                                audiocontrol.WMI_Play_Pause();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Play" + Environment.NewLine);
                                }));
                                break;
                            case "STOP":
                                audiocontrol.WMI_Play_Pause();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Stop" + Environment.NewLine);
                                }));
                                break;
                            case "VOLUME UP":
                                audiocontrol.WMI_Volume_Up();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Volume Up" + Environment.NewLine);
                                }));
                                break;
                            case "VOLUME DOWN":
                                audiocontrol.WMI_Volume_Down();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Volume Down" + Environment.NewLine);
                                }));
                                break;
                            case "MUTE":
                                audiocontrol.WMI_Mute();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Mute" + Environment.NewLine);
                                }));
                                break;
                            default:
                                Console.WriteLine("No action to powerpoint...");
                                break;
                        }
                    }

//"Select", "Wiki", "Up", "Down", "FullScreen","Pervious","Last","Back", "Forward", "Next", "Start", "Open", "Begin", "Exit", "Class Companion", "Play", "Stop", "Volume Up", "Volume Down", "Mute" 


                    if (imageVoiceFlag == true)
                    {
                        switch (data.scores[0].sentence.ToUpper())
                        {
                            case "NEXT":
                                imagecontrol.Next_Picture();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Next Picture" + Environment.NewLine);
                                }));
                                break;
                            case "PERVIOUS":
                                imagecontrol.Next_Picture();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Net Picture" + Environment.NewLine);
                                }));
                                break;
                            case "BACK":
                                imagecontrol.Last_Picture();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Last Picture" + Environment.NewLine);
                                }));
                                break;
                            case "LAST":
                                imagecontrol.Last_Picture();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Last Picture" + Environment.NewLine);
                                }));
                                break;
                            default:
                                Console.WriteLine("No action to powerpoint...");
                                break;
                        }
                    }

                    
                    if (videoVoiceFlag == true)
                    {
                        switch (data.scores[0].sentence.ToUpper())
                        {
                            case "PLAY":
                                videocontrol.WMI_Play_Pause();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Play" + Environment.NewLine);
                                }));
                                break;
                            case "STOP":
                                videocontrol.WMI_Stop_Restart();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Stop" + Environment.NewLine);
                                }));
                                break;
                            case "VOLUME UP":
                                videocontrol.WMI_Volume_Up();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Volume Up" + Environment.NewLine);
                                }));
                                break;
                            case "VOLUME DOWN":
                                videocontrol.WMI_Volume_Down();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Volume Down" + Environment.NewLine);
                                }));
                                break;
                            case "MUTE":
                                videocontrol.WMI_Mute();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Mute" + Environment.NewLine);
                                }));
                                break;
                            default:
                                Console.WriteLine("No action to powerpoint...");
                                break;
                        }
                    }

//"Select", "Wiki", "Up", "Down", "FullScreen","Pervious","Last","Back", "Forward", "Next", "Start", "Open", "Begin", "Exit", "Class Companion", "Play", "Stop", "Volume Up", "Volume Down", "Mute" 
                   
                    if (documentVoiceFlag == true)
                    {
                        switch (data.scores[0].sentence.ToUpper())
                        {
                            case "NEXT":
                                documentcontrol.ScrollDown();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Scroll Down" + Environment.NewLine);
                                }));
                                break;
                            case "DOWN":
                                documentcontrol.ScrollDown();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Scroll Down" + Environment.NewLine);
                                }));
                                break;
                            case "UP":
                                documentcontrol.ScrollUp();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Scrol Up" + Environment.NewLine);
                                }));
                                break;
                            case "LAST":
                                documentcontrol.ScrollUp();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Scroll Up" + Environment.NewLine);
                                }));
                                break;
                            case "PERVIOUS":
                                documentcontrol.ScrollUp();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Scroll Up" + Environment.NewLine);
                                }));
                                break;
                            case "BACK":
                                documentcontrol.ScrollUp();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Scroll Up" + Environment.NewLine);
                                }));
                                break;
                            default:
                                Console.WriteLine("No action to powerpoint...");
                                break;
                        }
                    }


                    if (webpageVoiceFlag == true)
                    {
                        switch (data.scores[0].sentence.ToUpper())
                        {
                            case "NEXT":
                                webpagecontrol.ScrollDown();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Scroll Down" + Environment.NewLine);
                                }));
                                break;
                            case "DOWN":
                                webpagecontrol.ScrollDown();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Scroll Down" + Environment.NewLine);
                                }));
                                break;
                            case "UP":
                                webpagecontrol.ScrollUp();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Scroll Up" + Environment.NewLine);
                                }));
                                break;
                            case "LAST":
                                webpagecontrol.ScrollUp();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Scroll Up" + Environment.NewLine);
                                }));
                                break;
                            case "PERVIOUS":
                                webpagecontrol.ScrollUp();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Scroll Up" + Environment.NewLine);
                                }));
                                break;
                            case "BACK":
                                webpagecontrol.ScrollUp();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Scroll Up" + Environment.NewLine);
                                }));
                                break;
                            case "FULLSCREEN":
                                webpagecontrol.FullScreen();
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("FullScreen" + Environment.NewLine);
                                }));
                                break;
                            default:
                                Console.WriteLine("No action to powerpoint...");
                                break;
                        }
                    }
//"Select", "Wiki", "Up", "Down", "FullScreen","Pervious","Last","Back", "Forward", "Next", "Start", "Open", "Begin", "Exit", "Class Companion", "Play", "Stop", "Volume Up", "Volume Down", "Mute" 


                    if (classcompanionVoiceFlag == true)
                    {
                        if (backgroundMode == false)
                        {
                            switch (data.scores[0].sentence.ToUpper())
                            {
                                case "EXIT":
                                    classcompanioncontrol.ExitControl();
                                    this.StopAll();
                                    form.Dispatcher.Invoke((Action)(() =>
                                    {
                                        form.Console_TextBox.AppendText("Exit" + Environment.NewLine);
                                    }));
                                    break;
                                case "NEXT":
                                    classcompanioncontrol.NextItem();
                                    form.Dispatcher.Invoke((Action)(() =>
                                    {
                                        form.Console_TextBox.AppendText("Next Item" + Environment.NewLine);
                                    }));
                                    break;
                                case "LAST":
                                    classcompanioncontrol.LastItem();
                                    form.Dispatcher.Invoke((Action)(() =>
                                    {
                                        form.Console_TextBox.AppendText("Pervious Item" + Environment.NewLine);
                                    }));
                                    break;
                                case "BACK":
                                    classcompanioncontrol.LastItem();
                                    form.Dispatcher.Invoke((Action)(() =>
                                    {
                                        form.Console_TextBox.AppendText("Pervious Item" + Environment.NewLine);
                                    }));
                                    break;
                                case "PERVIOUS":
                                    classcompanioncontrol.LastItem();
                                    form.Dispatcher.Invoke((Action)(() =>
                                    {
                                        form.Console_TextBox.AppendText("Pervious Item" + Environment.NewLine);
                                    }));
                                    break;
                                case "START":
                                    classcompanioncontrol.StartItem();
                                    form.Dispatcher.Invoke((Action)(() =>
                                    {
                                        form.Console_TextBox.AppendText("Start Content" + Environment.NewLine);
                                    }));
                                    break;
                                case "BEGIN":
                                    classcompanioncontrol.StartItem();
                                    form.Dispatcher.Invoke((Action)(() =>
                                    {
                                        form.Console_TextBox.AppendText("Start Content" + Environment.NewLine);
                                    }));
                                    break;
                                case "OPEN":
                                    classcompanioncontrol.StartItem();
                                    form.Dispatcher.Invoke((Action)(() =>
                                    {
                                        form.Console_TextBox.AppendText("Start Content" + Environment.NewLine);
                                    }));
                                    break;
                                case "CLASS COMPANION":
                                    classcompanioncontrol.ActivateWindow();
                                    backgroundMode = true;
                                    form.Dispatcher.Invoke((Action)(() =>
                                    {
                                        form.Console_TextBox.AppendText("Class Companion" + Environment.NewLine);
                                    }));
                                    break;
                                case "WIKI":
                                    handler.onRecognition = OnRecognitionDictation;
                                    sr.SetDictation();
                                    wikiTextFlag = true;
                                    playSound();
                                    break;
                                default:
                                    Console.WriteLine("No action to powerpoint...");
                                    break;
                            }
                        }
                        else
                        {
                            switch (data.scores[0].sentence.ToUpper())
                            {
                                case "CLASS COMPANION":
                                    classcompanioncontrol.ActivateWindow();
                                    backgroundMode = false;
                                    form.Dispatcher.Invoke((Action)(() =>
                                    {
                                        form.Console_TextBox.AppendText("Class Companion" + Environment.NewLine);
                                    }));
                                    break;
                                //case "WIKI":
                                //    handler.onRecognition = OnRecognitionDictation;
                                //    sr.SetDictation();
                                //    wikiTextFlag = true;
                                //    playSound();
                                //    break;
                                case "SELECT":
                                    MouseEvent(MouseEventFlags.LeftDown);
                                    MouseEvent(MouseEventFlags.LeftUp);
                                    break;
                                default:
                                    Console.WriteLine("No action to powerpoint...");
                                    break;
                            }
                        }
                    }




                    Console.WriteLine(data.scores[0].sentence);
                    if (data.scores[0].tags.Length > 0)
                        Console.WriteLine(data.scores[0].tags);
                }
                else
                {
                    //form.ClearScores();
                    for (int i = 0; i < PXCMSpeechRecognition.NBEST_SIZE; i++)
                    {
                        int label = data.scores[i].label;
                        int confidence = data.scores[i].confidence;
                        if (label < 0 || confidence == 0) continue;
                        //form.SetScore(label, confidence);
                    }
                    if (data.scores[0].tags.Length > 0)
                        Console.WriteLine(data.scores[0].tags);
                }

            }

            void OnRecognitionDictation(PXCMSpeechRecognition.RecognitionData data)
            {
               Console.WriteLine(data.scores[0].sentence.ToUpper());

                //Open web browser and navigate to the wiki site

               websiteURL = "http://en.wikipedia.org/wiki/" + data.scores[0].sentence.Replace(" ", "_");

               backgroundMode = false;
               form.Dispatcher.Invoke((Action)(() =>
               {
                   form.status.Content = websiteURL;
               }));

               System.Diagnostics.Process.Start(websiteURL);

                
               sr.BuildGrammarFromStringList(1, cmds, null);
               sr.SetGrammar(1);

               handler.onRecognition = OnRecognition;

            }

            public void SetClassCompanionBackgrounMode(Boolean mode)
            {
                backgroundMode = mode;
            }

            void OnAlert(PXCMSpeechRecognition.AlertData data)
            {
                Console.WriteLine(form.AlertToString(data.label));

                form.Dispatcher.Invoke((Action)(() =>
                {
                    form.status.Content = data.label;
                    //form.Console_TextBox.AppendText(data.label  + Environment.NewLine);
                }));
            }

            void CleanUp()
            {
                if (sr != null)
                {
                    sr.Dispose();
                    sr = null;
                }
                if (source != null)
                {
                    source.Dispose();
                    source = null;
                }
            }

            public bool SetVocabularyFromFile(String VocFilename)
            {

                pxcmStatus sts = sr.AddVocabToDictation(PXCMSpeechRecognition.VocabFileType.VFT_LIST, VocFilename);
                if (sts < pxcmStatus.PXCM_STATUS_NO_ERROR) return false;

                return true;
            }

            bool SetGrammarFromFile(String GrammarFilename)
            {

                Int32 grammar = 1;

                pxcmStatus sts = sr.BuildGrammarFromFile(grammar, PXCMSpeechRecognition.GrammarFileType.GFT_NONE, GrammarFilename);
                if (sts < pxcmStatus.PXCM_STATUS_NO_ERROR)
                {
                    Console.WriteLine("Grammar Compile Errors:");
                    Console.WriteLine(sr.GetGrammarCompileErrors(grammar));
                    return false;
                }

                sts = sr.SetGrammar(grammar);
                if (sts < pxcmStatus.PXCM_STATUS_NO_ERROR) return false;


                return true;
            }

            public void SetUpPowerPointVoice()
            {
                powerpointVoiceFlag = true;

                powerpointcontrol = new PowerPointControl();

            }

            public void StopPowerPointVoice()
            {
                powerpointVoiceFlag = false;
            }

            public void SetUpAudioVoice()
            {
                audioVoiceFlag = true;

                audiocontrol = new AudioControl();

            }

            public void StopAudioVoice()
            {
                audioVoiceFlag = false;
            }

            public void SetUpVideoVoice()
            {
                videoVoiceFlag = true;

                videocontrol = new VideoControl();

            }

            public void StopVideoVoice()
            {
                videoVoiceFlag = false;
            }

            public void SetUpDocumentVoice()
            {
                documentVoiceFlag = true;

                documentcontrol = new DocumentControl();

            }

            public void StopDocumentVoice()
            {
                documentVoiceFlag = false;
            }

            public void SetUpImageVoice()
            {
                imageVoiceFlag = true;

                imagecontrol = new ImageControl();

            }

            public void StopImageVoice()
            {
                imageVoiceFlag = false;
            }

            public void SetUpWebpageVoice()
            {
                webpageVoiceFlag = true;

                webpagecontrol = new WebsiteControl();

            }

            public void StopWebpageVoice()
            {
                webpageVoiceFlag = false;
            }

            public void SetUpClassCompanionVoice(MainWindow form1)
            {
                classcompanionVoiceFlag = true;

                classcompanioncontrol = new ClassCompanionControl(form1);

            }

            public void StopClassCompanionVoice()
            {
                classcompanionVoiceFlag = false;
            }

            public void StopAll()
            {
                StopAudioVoice();
                StopDocumentVoice();
                StopPowerPointVoice();
                StopVideoVoice();
                StopImageVoice();
                StopClassCompanionVoice();
            }

            public void StopAllButClassCompanion()
            {
                StopAudioVoice();
                StopDocumentVoice();
                StopPowerPointVoice();
                StopVideoVoice();
                StopImageVoice();
            }

            public void playSoundDefault()
            {
                //player.Play();
                SystemSounds.Beep.Play();
            }

            public void playSound()
            {
                sp.PlaySync();
                //sp.Play();
            }
            
            public void SetUp(MainWindow form1, PXCMSession session)
            {
                form = form1;

                sp = new System.Media.SoundPlayer();
                sp.Stream = Properties.Resources.beep_07;
                sp.LoadAsync();

                /* Create the AudioSource instance */
                source = session.CreateAudioSource();

                if (source == null)
                {
                    CleanUp();
                    Console.WriteLine("Stopped");
                    form.Dispatcher.Invoke((Action)(() =>
                    {
                        form.Console_TextBox.AppendText("Stopped Voice" + Environment.NewLine);
                    }));
                    return;
                }

                /* Set audio volume to 0.2 */
                source.SetVolume(0.2f);

                /* Set Audio Source */
                // May need to set Decive. In the future will be able to select a device in the options menu.
               // source.SetDevice(form.GetCheckedSource());
               
                // Scan and Enumerate audio devices
                source.ScanDevices();
                PXCMAudioSource.DeviceInfo dinfo = null;
                for (int d=source.QueryDeviceNum()-1;d>=0;d--) {
                    
                    source.QueryDeviceInfo(d, out dinfo);
                    // Select one and break out of the loop
                    break;
                    
                }

                // Set the active device
                source.SetDevice(dinfo);


                
                /* Set Module */
                //session.CreateImpl<PXCMSpeechRecognition>(out sr);
                //PXCMSession.ImplDesc mdesc = new PXCMSession.ImplDesc();
                //mdesc.iuid = form.GetCheckedModule();

                pxcmStatus sts = session.CreateImpl<PXCMSpeechRecognition>(out sr);
                if (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR)
                {
                    /* Configure */
                    PXCMSpeechRecognition.ProfileInfo pinfo;
                    sr.QueryProfile(0,out pinfo);
                    //Set Launguage to English for now...
                    pinfo.language = PXCMSpeechRecognition.LanguageType.LANGUAGE_US_ENGLISH;
                    sr.SetProfile(pinfo);

                    /////////////////////////////////////////////////////////////////////////////////////////////

                    ////////////////////////////////////////////////////////////////////////////////////////////

                    // Define all possible commands that can be used and then build a grammar file with that shorten vocablary
                    cmds = new String[20] {"Select", "Wiki", "Up", "Down", "FullScreen","Pervious","Last","Back", "Forward", "Next", "Start", "Open", "Begin", "Exit", "Class Companion", "Play", "Stop", "Volume Up", "Volume Down", "Mute" };
                    // voice commands available, use them
                    sr.BuildGrammarFromStringList(1, cmds, null); 
                    sr.SetGrammar(1);

                    /* Set to Dictation */
                    //sr.SetDictation();
                    
                    /* Initialization */
                    Console.WriteLine("Init Started");
                    form.Dispatcher.Invoke((Action)(() =>
                    {
                        form.Console_TextBox.AppendText("Init Started" + Environment.NewLine);
                    }));
                    handler = new PXCMSpeechRecognition.Handler();
                    handler.onRecognition = OnRecognition;
                    handler.onAlert = OnAlert;

                    sts = sr.StartRec(source, handler);
                    if (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR)
                    {
                        Console.WriteLine("Init OK");
                        form.Dispatcher.Invoke((Action)(() =>
                        {
                            form.Console_TextBox.AppendText("Init Start" + Environment.NewLine);
                        }));

                        /* Wait until the stop button is clicked */
                        while (kill == false)
                        {
                            System.Threading.Thread.Sleep(5);
                        }

                        sr.StopRec();
                    }
                    else
                    {
                        Console.WriteLine("Failed to initialize");
                        form.Dispatcher.Invoke((Action)(() =>
                        {
                            form.Console_TextBox.AppendText("Failed Init" + Environment.NewLine);
                        }));
                    }
                }
                else
                {
                    Console.WriteLine("Init Failed");
                    form.Dispatcher.Invoke((Action)(() =>
                    {
                        form.Console_TextBox.AppendText("Init Failed" + Environment.NewLine);
                    }));
                }

                CleanUp();
                Console.WriteLine("Stopped");
                form.Dispatcher.Invoke((Action)(() =>
                {
                    form.Console_TextBox.AppendText("Stopped" + Environment.NewLine);
                }));
            }

    }
}
