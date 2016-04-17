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
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ClassCompanion
{
    /// <summary>
    /// Interaction logic for GestureControlOverlayMenu.xaml
    /// </summary>
    public partial class GestureControlOverlayMenu : Window
    {

        //Start of user interface

        public GestureControlOverlayMenu()
        {
            InitializeComponent();

            CenterWindowOnScreen();

            maximixe();

            changeOpacity(0.0);

            //this.Center_Border.Opacity = 0.0;

        }


        public void maximixe(){
            
            Application.Current.MainWindow.WindowState = WindowState.Maximized;
            //this.Activate();
        
        }

        public void minimize()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        public void changeOpacity(Double value)
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

        private void left_Box_MouseEnter(object sender, MouseEventArgs e)
        {
            //Change Color
            left_Box.Opacity = .5;
        }

        private void left_Box_MouseLeave(object sender, MouseEventArgs e)
        {
            left_Box.Opacity = 0;
        }

        private void right_Box_MouseEnter(object sender, MouseEventArgs e)
        {
            right_Box.Opacity = .5;
        }

        private void right_Box_MouseLeave(object sender, MouseEventArgs e)
        {
            right_Box.Opacity = 0;
        }

        private void down_Box_MouseEnter(object sender, MouseEventArgs e)
        {
            down_Box.Opacity = .5;
        }

        private void down_Box_MouseLeave(object sender, MouseEventArgs e)
        {
            down_Box.Opacity = 0;
        }

        private void up_MouseEnter(object sender, MouseEventArgs e)
        {
            up_Box.Opacity = .5;
        }

        private void up_MouseLeave(object sender, MouseEventArgs e)
        {
            up_Box.Opacity = 0;
        }




        //Start Of Control


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

        MainWindow form;

        GestureControlOverlayMenu gestureControlOverlayMenu;

        float intial_X;
        float intial_Y;

        int difference_Y;
        float difference_Y_Last;

        int current;

        float initialScrollPoint;

        public Boolean Disable = false;

        Boolean powerpointVoiceFlag = false;
        Boolean audioVoiceFlag = false;
        Boolean videoVoiceFlag = false;
        Boolean classcompanionVoiceFlag = false;
        Boolean documentVoiceFlag = false;
        Boolean imageVoiceFlag = false;
        Boolean webpageVoiceFlag = false;

        Boolean backgroundMode = false;

        PowerPointControl powerpointcontrol;
        AudioControl audiocontrol;
        VideoControl videocontrol;
        ClassCompanionControl classcompanioncontrol;
        DocumentControl documentcontrol;
        ImageControl imagecontrol;
        WebsiteControl webpagecontrol;

        double ignoreCommandCount = 1;

        IntPtr handle;

        int scrollSensitivity = 1;

        System.Diagnostics.Process process;

        public Boolean kill = false;

        double thisDpiWidthFactor;
        double thisDpiHeightFactor;
        double height;
        double width;

        //string[] commands;

        public void CalculateDpiFactors()
        {

            gestureControlOverlayMenu.Dispatcher.Invoke((Action)(() =>
            {
                Window MainWindow2 = this;
                PresentationSource MainWindowPresentationSource = PresentationSource.FromVisual(MainWindow2);
                Matrix m = MainWindowPresentationSource.CompositionTarget.TransformToDevice;
                thisDpiWidthFactor = m.M11;
                thisDpiHeightFactor = m.M22;
                height = System.Windows.SystemParameters.PrimaryScreenHeight * thisDpiHeightFactor;
                width = System.Windows.SystemParameters.PrimaryScreenWidth * thisDpiWidthFactor;
            }));
            //System.Windows.SystemParameters.PrimaryScreenHeight
            //System.Windows.SystemParameters.PrimaryScreenWidth
        }

        

        public void OnFiredUXEvent(PXCMTouchlessController.UXEventData data)
        {
            // handle the event….
            switch (data.type)
            {
                case PXCMTouchlessController.UXEventData.UXEventType.UXEvent_CursorVisible:
                    {

                        CalculateDpiFactors();

                        form.Dispatcher.Invoke((Action)(() =>
                        {
                            form.Console_TextBox.AppendText("Cursor Visible" + Environment.NewLine);
                        }));

                        Console.WriteLine("Cursor Visible");
                        intial_X = data.position.x;
                        intial_Y = data.position.y;
                        gestureControlOverlayMenu.Dispatcher.Invoke((Action)(() =>
                        {
                            gestureControlOverlayMenu.maximixe();
                            gestureControlOverlayMenu.Opacity = .7;
                        }));

                    }
                    break;
                case PXCMTouchlessController.UXEventData.UXEventType.UXEvent_CursorNotVisible:
                    {
                        form.Dispatcher.Invoke((Action)(() =>
                        {
                            form.Console_TextBox.AppendText("Cursor Not Visible" + Environment.NewLine);
                        }));
                        Console.WriteLine("Cursor Not Visible");



                        //Check to see if the fire is the second UXEvent
                        if (ignoreCommandCount % 2 != 0)
                        {

                        
                            gestureControlOverlayMenu.Dispatcher.Invoke((Action)(() =>
                            {
                                gestureControlOverlayMenu.minimize();
                                gestureControlOverlayMenu.Opacity = 0;
                            }));

                            if (0.7 < data.position.x && data.position.x < 1.1 && .01 < data.position.y && data.position.y < 1.0)
                            {
                            
                                Console.WriteLine("R:tap");
                                fireCommand("R:tap");
                            }
                            if (-0.1 < data.position.x && data.position.x < 0.11 && .01 < data.position.y && data.position.y < 1.0)
                            {
                                Console.WriteLine("L:tap");
                                fireCommand("L:tap");
                            }

                          
                        }

                        ignoreCommandCount = ignoreCommandCount + 1;
                       
                        //Bottom x = .1  to .74    y= 1.0 to .7
                        //Top y = -.1 to .11   x =.1 to .74
                        //Right x = .7 to 1.1     y = .01 o 1.0
                        //Left    x= .11 to -.1       y = .01 to 1.0


                    }
                    break;
                case PXCMTouchlessController.UXEventData.UXEventType.UXEvent_Select:
                    {
                        if (webpageVoiceFlag == true)
                        {

                            if (ignoreCommandCount % 2 != 0)
                            {

                                gestureControlOverlayMenu.Dispatcher.Invoke((Action)(() =>
                                {
                                    //gestureControlOverlayMenu.Opacity = 0;
                                    gestureControlOverlayMenu.minimize();
                                    System.Threading.Thread.Sleep(1000);
                                }));
                                Console.WriteLine("Select");
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("Select" + Environment.NewLine);
                                }));
                                MouseEvent(MouseEventFlags.LeftDown);
                                MouseEvent(MouseEventFlags.LeftUp);

                                gestureControlOverlayMenu.Dispatcher.Invoke((Action)(() =>
                                {
                                    //gestureControlOverlayMenu.Opacity = 0;
                                    gestureControlOverlayMenu.maximixe();
                                    System.Threading.Thread.Sleep(1000);
                                }));
                            }
                            ignoreCommandCount = ignoreCommandCount + 1;
                        }
                        else
                        {

                        }
                    }
                    break;
                case PXCMTouchlessController.UXEventData.UXEventType.UXEvent_MetaPinch:
                    {
                        Console.WriteLine("Pinch Held");
                    }
                    break;
                case PXCMTouchlessController.UXEventData.UXEventType.UXEvent_StartMetaCounter:
                    {
                        Console.WriteLine("V Detected");


                        if (Disable == true)
                        {
                            Disable = Disable;
                        }

                        else
                        {

                            gestureControlOverlayMenu.Dispatcher.Invoke((Action)(() =>
                            {
                                //gestureControlOverlayMenu.Opacity = 0;
                                gestureControlOverlayMenu.minimize();
                                System.Threading.Thread.Sleep(50);
                            }));

                            if (0.7 < data.position.x && data.position.x < 1.1 && .01 < data.position.y && data.position.y < 1.0)
                            {
                                Console.WriteLine("V_Sign Right Box");
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("V_Sign Right Box" + Environment.NewLine);
                                }));
                                fireCommand("R:v_sign");
                            }
                            if (-0.1 < data.position.x && data.position.x < 0.11 && .01 < data.position.y && data.position.y < 1.0)
                            {
                                Console.WriteLine("V_Sign Left Box");
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("V_Sign Left Box" + Environment.NewLine);
                                }));
                                fireCommand("L:v_sign");
                            }

                            if (0.1 < data.position.x && data.position.x < .74 && -0.1 < data.position.y && data.position.y < .11)
                            {
                                Console.WriteLine("V_Sign Top Box");
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("V_Sign Top Box" + Environment.NewLine);
                                }));
                                //fireCommand("R:tap");
                            }
                            if (0.1 < data.position.x && data.position.x < 0.74 && 0.7 < data.position.y && data.position.y < 1.1)
                            {
                                Console.WriteLine("V_Sign Bottom Box");

                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("V_Sign Bottom Box" + Environment.NewLine);
                                }));
                                //fireCommand("L:tap");
                            }
                            if (0.3 < data.position.x && data.position.x < 0.65 && 0.3 < data.position.y && data.position.y < 0.65)
                            {
                                Console.WriteLine("Center Box");
                                form.Dispatcher.Invoke((Action)(() =>
                                {
                                    form.Console_TextBox.AppendText("V_Sign Center Box" + Environment.NewLine);
                                }));
                                fireCommand("L:v_signR:v_sign");
                            }

                            gestureControlOverlayMenu.Dispatcher.Invoke((Action)(() =>
                            {
                                System.Threading.Thread.Sleep(50);
                                gestureControlOverlayMenu.maximixe();
                                //gestureControlOverlayMenu.Opacity = .5;
                            }));

                        }

                        //Bottom x = .1  to .74    y = 0.7 to 1.1
                        //Top    x =.1 to .74      y = -.1 to .11 
                        //Right  x = .7 to 1.1     y = .01 o 1.0
                        //Left   x = .11 to -.1    y = .01 to 1.0
                        //Center x = 0.3 to 0.65   y = 0.3 to 0.65

                    }
                    break;
                case PXCMTouchlessController.UXEventData.UXEventType.UXEvent_StartScroll:
                    {
                        Console.WriteLine("Start Scroll");
                        form.Dispatcher.Invoke((Action)(() =>
                        {
                            form.Console_TextBox.AppendText("Start Scroll" + Environment.NewLine);
                        }));

                        initialScrollPoint = data.position.y;

                        current = 0;

                        gestureControlOverlayMenu.Dispatcher.Invoke((Action)(() =>
                        {
                            gestureControlOverlayMenu.Opacity = .2;
                        }));

                        gestureControlOverlayMenu.Dispatcher.Invoke((Action)(() =>
                        {
                            gestureControlOverlayMenu.minimize();
                            gestureControlOverlayMenu.Opacity = 0.0;
                        }));


                    }
                    break;
                case PXCMTouchlessController.UXEventData.UXEventType.UXEvent_Scroll:
                    {
                        difference_Y = (int)((data.position.y - initialScrollPoint) * 100);

                        //Goal -50 through 50. Store current value and try to make it to the goal... 
                        //If not at goal then send up or down arrow.
                        if(difference_Y < current + 5 && difference_Y > current - 5)
                        {
                            goto skipIfs;
                        }
                        else if (current > difference_Y)
                        {
                            //Scroll Sensitivty: Lesser values mean more sensitivity
                            System.Windows.Forms.SendKeys.SendWait("{UP}");
                            current = current - scrollSensitivity;
                        }                        
                        else if (current < difference_Y)
                        {
                            System.Windows.Forms.SendKeys.SendWait("{DOWN}");
                            current = current + scrollSensitivity;
                        }
                    skipIfs:
                        Console.WriteLine(difference_Y + " " + current);
                    }
                    break;
                case PXCMTouchlessController.UXEventData.UXEventType.UXEvent_EndScroll:
                    {

                        //Check if on box? If so then fire the command
                        Console.WriteLine("End Scroll");
                        form.Dispatcher.Invoke((Action)(() =>
                        {
                            form.Console_TextBox.AppendText("End Scroll" + Environment.NewLine);
                        }));

                        gestureControlOverlayMenu.Dispatcher.Invoke((Action)(() =>
                        {
                            gestureControlOverlayMenu.maximixe();
                            gestureControlOverlayMenu.Opacity = .7;
                        }));
                    }
                    break;
                case PXCMTouchlessController.UXEventData.UXEventType.UXEvent_CursorMove:
                    {


                        Point point = new Point();
                        point.X = Math.Max(Math.Min(1.0F, data.position.x), 0.0F);
                        point.Y = Math.Max(Math.Min(1.0F, data.position.y), 0.0F);

                        //Point myListBoxPosition = DisplayArea.PointToScreen(new Point(0d, 0d));

                        int mouseX = (int)(point.X * width * 1);
                        int mouseY = (int)(point.Y * height * 1);

                        SetCursorPos(mouseX, mouseY);

                        Console.WriteLine(data.position.x + " " + data.position.y);


                        //System.Drawing.Point point = new System.Drawing.Point();
                        //point.X = (int) (Math.Max(Math.Min(1.0F, data.position.x), 0.0F)*1500);
                        //point.Y = (int) (Math.Max(Math.Min(1.0F, data.position.y), 0.0F)*800);

                        //Console.WriteLine(point.X + " , " + point.Y);

                        //point = pointDamper(point);

                        //System.Windows.Forms.Cursor.Position = point;

                    }
                    break;
            }
        }

        private void fireCommand(String command)
        {
            if (powerpointVoiceFlag == true)
            {
                switch (command)
                {
                    case "R:tap":
                        powerpointcontrol.NextSlide();
                        form.Dispatcher.Invoke((Action)(() =>
                        {
                            form.Console_TextBox.AppendText("Next Slide" + Environment.NewLine);
                        }));
                        break;
                    case "L:tap":
                        powerpointcontrol.PerviousSlide();
                        form.Dispatcher.Invoke((Action)(() =>
                        {
                            form.Console_TextBox.AppendText("Pervious Slide" + Environment.NewLine);
                        }));
                        break;
                    case "R:v_sign":
                        powerpointcontrol.FullScreen();
                        form.Dispatcher.Invoke((Action)(() =>
                        {
                            form.Console_TextBox.AppendText("Fullscreen" + Environment.NewLine);
                        }));
                        break;
                    case "L:v_sign":
                        powerpointcontrol.Exit();
                        form.Dispatcher.Invoke((Action)(() =>
                        {
                            form.Console_TextBox.AppendText("Exit Fullscreen" + Environment.NewLine);
                        }));
                        break;
                    default:
                        Console.WriteLine("No action to powerpoint...");
                        break;
                }
            }

            if (audioVoiceFlag == true)
            {
                switch (command)
                {
                    case "R:tap":
                        form.Dispatcher.Invoke((Action)(() =>
                        {
                            form.Console_TextBox.AppendText("Play" + Environment.NewLine);
                        }));
                        audiocontrol.WMI_Play_Pause();
                        break;
                    case "L:tap":
                        audiocontrol.WMI_Stop_Restart();
                        form.Dispatcher.Invoke((Action)(() =>
                        {
                            form.Console_TextBox.AppendText("Stop" + Environment.NewLine);
                        }));
                        break;
                    case "R:v_sign":
                        audiocontrol.WMI_Volume_Up();
                        form.Dispatcher.Invoke((Action)(() =>
                        {
                            form.Console_TextBox.AppendText("Volume Up" + Environment.NewLine);
                        }));
                        break;
                    case "L:v_sign":
                        audiocontrol.WMI_Mute();
                        form.Dispatcher.Invoke((Action)(() =>
                        {
                            form.Console_TextBox.AppendText("Mute" + Environment.NewLine);
                        }));
                        break;
                    default:
                        Console.WriteLine("No action to audio...");
                        break;
                }
            }


            if (imageVoiceFlag == true)
            {
                switch (command)
                {
                    case "R:tap":
                        imagecontrol.Next_Picture();
                        form.Dispatcher.Invoke((Action)(() =>
                        {
                            form.Console_TextBox.AppendText("Next Picture" + Environment.NewLine);
                        }));
                        break;
                    case "L:tap":
                        imagecontrol.Last_Picture();
                        form.Dispatcher.Invoke((Action)(() =>
                        {
                            form.Console_TextBox.AppendText("Pervious Picture" + Environment.NewLine);
                        }));
                        break;
                    default:
                        Console.WriteLine("No action to image...");
                        break;
                }
            }


            if (videoVoiceFlag == true)
            {
                switch (command)
                {
                    case "R:tap":
                        videocontrol.WMI_Play_Pause();
                        form.Dispatcher.Invoke((Action)(() =>
                        {
                            form.Console_TextBox.AppendText("Play" + Environment.NewLine);
                        }));
                        break;
                    case "L:tap":
                        videocontrol.WMI_Stop_Restart();
                        form.Dispatcher.Invoke((Action)(() =>
                        {
                            form.Console_TextBox.AppendText("Pause" + Environment.NewLine);
                        }));
                        break;
                    case "R:v_sign":
                        videocontrol.WMI_Volume_Up();
                        form.Dispatcher.Invoke((Action)(() =>
                        {
                            form.Console_TextBox.AppendText("Volume Up" + Environment.NewLine);
                        }));
                        break;
                    case "L:v_sign":
                        videocontrol.WMI_Mute();
                        form.Dispatcher.Invoke((Action)(() =>
                        {
                            form.Console_TextBox.AppendText("Volume Down" + Environment.NewLine);
                        }));
                        break;
                    default:
                        Console.WriteLine("No action to video...");
                        break;
                }
            }


            if (documentVoiceFlag == true)
            {
                switch (command)
                {
                    case "R:tap":
                        documentcontrol.ScrollDown();
                        form.Dispatcher.Invoke((Action)(() =>
                        {
                            form.Console_TextBox.AppendText("Next Page" + Environment.NewLine);
                        }));
                        break;
                    case "L:tap":
                        documentcontrol.ScrollUp();
                        form.Dispatcher.Invoke((Action)(() =>
                        {
                            form.Console_TextBox.AppendText("Last Page" + Environment.NewLine);
                        }));
                        break;
                    case "R:v_sign":
                        documentcontrol.ScrollDown();
                        form.Dispatcher.Invoke((Action)(() =>
                        {
                            form.Console_TextBox.AppendText("Last Page" + Environment.NewLine);
                        }));
                        break;
                    case "L:v_sign":
                        documentcontrol.ScrollUp();
                        form.Dispatcher.Invoke((Action)(() =>
                        {
                            form.Console_TextBox.AppendText("Next Page" + Environment.NewLine);
                        }));
                        break;
                    default:
                        Console.WriteLine("No action to document...");
                        break;
                }
            }

            if (webpageVoiceFlag == true)
            {
                switch (command)
                {
                    case "R:tap":
                        webpagecontrol.ScrollDown();
                        form.Dispatcher.Invoke((Action)(() =>
                        {
                            form.Console_TextBox.AppendText("Next Page" + Environment.NewLine);
                        }));
                        break;
                    case "L:tap":
                        webpagecontrol.ScrollUp();
                        form.Dispatcher.Invoke((Action)(() =>
                        {
                            form.Console_TextBox.AppendText("Pervious Page" + Environment.NewLine);
                        }));
                        break;
                    case "R:v_sign":
                        webpagecontrol.FullScreen();
                        form.Dispatcher.Invoke((Action)(() =>
                        {
                            form.Console_TextBox.AppendText("FullScreen" + Environment.NewLine);
                        }));
                        break;
                    case "L:v_sign":
                        webpagecontrol.FullScreen();
                        form.Dispatcher.Invoke((Action)(() =>
                        {
                            form.Console_TextBox.AppendText("FullScreen" + Environment.NewLine);
                        }));
                        break;
                    default:
                        Console.WriteLine("No action to webpage...");
                        break;
                }
            }

            if (classcompanionVoiceFlag == true)
            {
                if (backgroundMode == false)
                {
                    switch (command)
                    {
                        case "R:tap":
                            classcompanioncontrol.NextItem();
                            form.Dispatcher.Invoke((Action)(() =>
                            {
                                form.Console_TextBox.AppendText("Next Item" + Environment.NewLine);
                            }));
                            break;
                        case "L:tap":
                            classcompanioncontrol.LastItem();
                            form.Dispatcher.Invoke((Action)(() =>
                            {
                                form.Console_TextBox.AppendText("Pervious Item" + Environment.NewLine);
                            }));
                            break;
                        case "L:v_sign":
                            classcompanioncontrol.ExitControl();
                            this.StopAll();
                            form.Dispatcher.Invoke((Action)(() =>
                            {
                                form.Console_TextBox.AppendText("Exit Control" + Environment.NewLine);
                            }));
                            break;
                        case "R:v_sign":
                            form.Dispatcher.Invoke((Action)(() =>
                            {
                                form.Hide();
                                form.minimize();
                            }));
                            classcompanioncontrol.StartItem();
                            System.Threading.Thread.Sleep(5000);
                            form.Dispatcher.Invoke((Action)(() =>
                            {
                                form.Console_TextBox.AppendText("Start Item" + Environment.NewLine);
                            }));
                            break;
                        default:
                            Console.WriteLine("No action to classcompanion...");
                            break;
                    }
                }
                else
                {
                    switch (command)
                    {
                        case "L:v_signR:v_sign":
                            gestureControlOverlayMenu.Dispatcher.Invoke((Action)(() =>
                            {
                                currently_Controlling.Content = "Currently Controlling: Class Companion";
                                form.Show();
                                form.Restore();
                            }));
                            classcompanioncontrol.ActivateWindow();
                            backgroundMode = false;
                            gestureControlOverlayMenu.Dispatcher.Invoke((Action)(() =>
                            {
                                StopAllButClassCompanion();
                                setCommands();
                            }));
                            break;
                        case "R:v_signL:v_sign":
                            gestureControlOverlayMenu.Dispatcher.Invoke((Action)(() =>
                            {
                                currently_Controlling.Content = "Currently Controlling: Class Companion";
                                form.Show();
                                form.Restore();
                            }));
                            classcompanioncontrol.ActivateWindow();
                            backgroundMode = false;
                            gestureControlOverlayMenu.Dispatcher.Invoke((Action)(() =>
                            {
                                StopAllButClassCompanion();
                                setCommands();
                            }));
                            break;
                        case "down":
                            //System.Windows.Forms.SendKeys.SendWait("{DOWN}");
                            break;
                        case "up":
                            //System.Windows.Forms.SendKeys.SendWait("{UP}");
                            break;
                        default:
                            Console.WriteLine("No action to classcompanion...");
                            break;
                    }
                }
            }
        }

        public void setCommands()
        {

            if (powerpointVoiceFlag == true)
            {
                Top_Icon.Content = "";
                Bottom_Icon.Content = "";
                Right_Icon.Content = "4";
                Left_Icon.Content = "3";
                Left_V_Command.Content = "r";
                Right_V_Command.Content = "1";
                return;
            }

            if (audioVoiceFlag == true)
            {
                Top_Icon.Content = "";
                Bottom_Icon.Content = "";
                Right_Icon.Content = "4";
                Left_Icon.Content = "<";
                Left_V_Command.Content = "X";
                Right_V_Command.Content = "Xð";
                return;
            }

            if (imageVoiceFlag == true)
            {
                Top_Icon.Content = "";
                Bottom_Icon.Content = "";
                Right_Icon.Content = "4";
                Left_Icon.Content = "3";
                Left_V_Command.Content = "";
                Right_V_Command.Content = "";
                return;
            }

            if (videoVoiceFlag == true)
            {
                Top_Icon.Content = "";
                Bottom_Icon.Content = "";
                Right_Icon.Content = "4";
                Left_Icon.Content = "<";
                Left_V_Command.Content = "X";
                Right_V_Command.Content = "Xð";
                return;
            }

            if (documentVoiceFlag == true)
            {
                Top_Icon.Content = "";
                Bottom_Icon.Content = "";
                Right_Icon.Content = "6";
                Left_Icon.Content = "5";
                Left_V_Command.Content = "";
                Right_V_Command.Content = "";
                return;
            }

            if (webpageVoiceFlag == true)
            {
                Top_Icon.Content = "";
                Bottom_Icon.Content = "";
                Right_Icon.Content = "6";
                Left_Icon.Content = "5";
                Left_V_Command.Content = "";
                Right_V_Command.Content = "";
                return;
            }

            if (classcompanionVoiceFlag == true)
            {
                Top_Icon.Content = "";
                Bottom_Icon.Content = "";
                Right_Icon.Content = "4";
                Left_Icon.Content = "3";
                Left_V_Command.Content = "r";
                Right_V_Command.Content = "2";
                return;
            }

        }

            //    switch (command)
            //    {
            //        case "R:tap":
            //            powerpointcontrol.NextSlide();
            //            form.Dispatcher.Invoke((Action)(() =>
            //            {
            //                form.Console_TextBox.AppendText("Next Slide" + Environment.NewLine);
            //            }));
            //            break;
            //        case "L:tap":
            //            powerpointcontrol.PerviousSlide();
            //            form.Dispatcher.Invoke((Action)(() =>
            //            {
            //                form.Console_TextBox.AppendText("Pervious Slide" + Environment.NewLine);
            //            }));
            //            break;
            //        case "R:v_sign":
            //            powerpointcontrol.FullScreen();
            //            form.Dispatcher.Invoke((Action)(() =>
            //            {
            //                form.Console_TextBox.AppendText("Fullscreen" + Environment.NewLine);
            //            }));
            //            break;
            //        case "L:v_sign":
            //            powerpointcontrol.Exit();
            //            form.Dispatcher.Invoke((Action)(() =>
            //            {
            //                form.Console_TextBox.AppendText("Exit Fullscreen" + Environment.NewLine);
            //            }));
            //            break;
            //        default:
            //            Console.WriteLine("No action to powerpoint...");
            //            break;
            //    }
            //}

            //if (audioVoiceFlag == true)
            //{
            //    switch (command)
            //    {
            //        case "R:tap":
            //            form.Dispatcher.Invoke((Action)(() =>
            //            {
            //                form.Console_TextBox.AppendText("Play" + Environment.NewLine);
            //            }));
            //            audiocontrol.WMI_Play_Pause();
            //            audiocontrol.WMI_Play_Pause();
            //            break;
            //        case "L:tap":
            //            audiocontrol.WMI_Play_Pause();
            //            audiocontrol.WMI_Play_Pause();
            //            form.Dispatcher.Invoke((Action)(() =>
            //            {
            //                form.Console_TextBox.AppendText("Pause" + Environment.NewLine);
            //            }));
            //            break;
            //        case "R:v_sign":
            //            audiocontrol.WMI_Volume_Up();
            //            audiocontrol.QT_Volume_Up();
            //            form.Dispatcher.Invoke((Action)(() =>
            //            {
            //                form.Console_TextBox.AppendText("Volume Up" + Environment.NewLine);
            //            }));
            //            break;
            //        case "L:v_sign":
            //            audiocontrol.WMI_Volume_Down();
            //            audiocontrol.QT_Volume_Down();
            //            form.Dispatcher.Invoke((Action)(() =>
            //            {
            //                form.Console_TextBox.AppendText("Volume Down" + Environment.NewLine);
            //            }));
            //            break;
            //        default:
            //            Console.WriteLine("No action to audio...");
            //            break;
            //    }
            //}


            //if (imageVoiceFlag == true)
            //{
            //    switch (command)
            //    {
            //        case "R:tap":
            //            imagecontrol.Next_Picture();
            //            form.Dispatcher.Invoke((Action)(() =>
            //            {
            //                form.Console_TextBox.AppendText("Next Picture" + Environment.NewLine);
            //            }));
            //            break;
            //        case "L:tap":
            //            imagecontrol.Last_Picture();
            //            form.Dispatcher.Invoke((Action)(() =>
            //            {
            //                form.Console_TextBox.AppendText("Pervious Picture" + Environment.NewLine);
            //            }));
            //            break;
            //        default:
            //            Console.WriteLine("No action to image...");
            //            break;
            //    }
            //}


            //if (videoVoiceFlag == true)
            //{
            //    switch (command)
            //    {
            //        case "R:tap":
            //            videocontrol.WMI_Play_Pause();
            //            videocontrol.QT_Play_Pause();
            //            form.Dispatcher.Invoke((Action)(() =>
            //            {
            //                form.Console_TextBox.AppendText("Play" + Environment.NewLine);
            //            }));
            //            break;
            //        case "L:tap":
            //            videocontrol.WMI_Play_Pause();
            //            videocontrol.WMI_Play_Pause();
            //            form.Dispatcher.Invoke((Action)(() =>
            //            {
            //                form.Console_TextBox.AppendText("Pause" + Environment.NewLine);
            //            }));
            //            break;
            //        case "R:v_sign":
            //            videocontrol.WMI_Volume_Up();
            //            form.Dispatcher.Invoke((Action)(() =>
            //            {
            //                form.Console_TextBox.AppendText("Volume Up" + Environment.NewLine);
            //            }));
            //            break;
            //        case "L:v_sign":
            //            videocontrol.WMI_Volume_Down();
            //            form.Dispatcher.Invoke((Action)(() =>
            //            {
            //                form.Console_TextBox.AppendText("Volume Down" + Environment.NewLine);
            //            }));
            //            break;
            //        default:
            //            Console.WriteLine("No action to video...");
            //            break;
            //    }
            //}


            //if (documentVoiceFlag == true)
            //{
            //    switch (command)
            //    {
            //        case "R:tap":
            //            documentcontrol.ScrollDown();
            //            form.Dispatcher.Invoke((Action)(() =>
            //            {
            //                form.Console_TextBox.AppendText("Next Page" + Environment.NewLine);
            //            }));
            //            break;
            //        case "L:tap":
            //            documentcontrol.ScrollUp();
            //            form.Dispatcher.Invoke((Action)(() =>
            //            {
            //                form.Console_TextBox.AppendText("Last Page" + Environment.NewLine);
            //            }));
            //            break;
            //        case "R:v_sign":
            //            documentcontrol.ScrollDown();
            //            form.Dispatcher.Invoke((Action)(() =>
            //            {
            //                form.Console_TextBox.AppendText("Last Page" + Environment.NewLine);
            //            }));
            //            break;
            //        case "L:v_sign":
            //            documentcontrol.ScrollUp();
            //            form.Dispatcher.Invoke((Action)(() =>
            //            {
            //                form.Console_TextBox.AppendText("Next Page" + Environment.NewLine);
            //            }));
            //            break;
            //        default:
            //            Console.WriteLine("No action to document...");
            //            break;
            //    }
            //}

            //if (webpageVoiceFlag == true)
            //{
            //    switch (command)
            //    {
            //        case "R:tap":
            //            webpagecontrol.ScrollDown();
            //            form.Dispatcher.Invoke((Action)(() =>
            //            {
            //                form.Console_TextBox.AppendText("Next Page" + Environment.NewLine);
            //            }));
            //            break;
            //        case "L:tap":
            //            webpagecontrol.ScrollUp();
            //            form.Dispatcher.Invoke((Action)(() =>
            //            {
            //                form.Console_TextBox.AppendText("Pervious Page" + Environment.NewLine);
            //            }));
            //            break;
            //        case "R:v_sign":
            //            webpagecontrol.FullScreen();
            //            form.Dispatcher.Invoke((Action)(() =>
            //            {
            //                form.Console_TextBox.AppendText("FullScreen" + Environment.NewLine);
            //            }));
            //            break;
            //        case "L:v_sign":
            //            webpagecontrol.FullScreen();
            //            form.Dispatcher.Invoke((Action)(() =>
            //            {
            //                form.Console_TextBox.AppendText("FullScreen" + Environment.NewLine);
            //            }));
            //            break;
            //        default:
            //            Console.WriteLine("No action to webpage...");
            //            break;
            //    }
            //}



        public bool ApplicationIsActivated()
        {
            handle = GetForegroundWindow();

            if (handle == form.process.MainWindowHandle)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void SetClassCompanionBackgrounMode(Boolean mode)
        {
            backgroundMode = mode;

            if (mode == false)
            {
                gestureControlOverlayMenu.Dispatcher.Invoke((Action)(() =>
                {
                    currently_Controlling.Content = "Currently Controlling: Class Companion";
                }));
            }

        }

        public void SetUpPowerPointGesture(System.Diagnostics.Process process1)
        {
            process = process1;

            powerpointVoiceFlag = true;

            setCommands();

            powerpointcontrol = new PowerPointControl();

            gestureControlOverlayMenu.Dispatcher.Invoke((Action)(() =>
            {

                currently_Controlling.Content = "Currently Controlling: PowerPoint";

            }));


        }

        public void StopPowerPointGesture()
        {
            powerpointVoiceFlag = false;
        }

        public void SetUpAudioGesture()
        {
            audioVoiceFlag = true;

            setCommands();

            audiocontrol = new AudioControl();

            gestureControlOverlayMenu.Dispatcher.Invoke((Action)(() =>
            {

                currently_Controlling.Content = "Currently Controlling: Audio";

            }));

        }

        public void StopAudioGesture()
        {
            audioVoiceFlag = false;
        }

        public void SetUpVideoGesture()
        {
            videoVoiceFlag = true;

            setCommands();

            videocontrol = new VideoControl();

            gestureControlOverlayMenu.Dispatcher.Invoke((Action)(() =>
            {

                currently_Controlling.Content = "Currently Controlling: Video";

            }));
        
        }

        public void StopVideoGesture()
        {
            videoVoiceFlag = false;
        }

        public void SetUpDocumentGesture()
        {
            documentVoiceFlag = true;

            setCommands();

            documentcontrol = new DocumentControl();

            gestureControlOverlayMenu.Dispatcher.Invoke((Action)(() =>
            {

                currently_Controlling.Content = "Currently Controlling: Document";

            }));
          
        }

        public void StopDocumentGesture()
        {
            documentVoiceFlag = false;
        }

        public void SetUpImageGesture()
        {
            imageVoiceFlag = true;

            setCommands();

            imagecontrol = new ImageControl();

            gestureControlOverlayMenu.Dispatcher.Invoke((Action)(() =>
            {

                currently_Controlling.Content = "Currently Controlling: Image";

            }));
         
        }

        public void StopImageGesture()
        {
            imageVoiceFlag = false;
        }

        public void SetUpWebpageGesture()
        {
            webpageVoiceFlag = true;

            setCommands();

            webpagecontrol = new WebsiteControl();

            gestureControlOverlayMenu.Dispatcher.Invoke((Action)(() =>
            {

                currently_Controlling.Content = "Currently Controlling: WebPage";

            }));
           
        }

        public void StopWebpageGesture()
        {
            webpageVoiceFlag = false;
        }

        public void SetUpClassCompanionGesture(MainWindow form1)
        {
            classcompanionVoiceFlag = true;

            classcompanioncontrol = new ClassCompanionControl(form1);

            setCommands();

        }

        public void StopClassCompanionGesture()
        {
            classcompanionVoiceFlag = false;

            gestureControlOverlayMenu.Dispatcher.Invoke((Action)(() =>
            {

                currently_Controlling.Content = "Currently Controlling: Class Companion";
                this.Opacity = 0;

            }));
            
        }

        public void StopAll()
        {
            StopAudioGesture();
            StopDocumentGesture();
            StopPowerPointGesture();
            StopVideoGesture();
            StopImageGesture();
            StopWebpageGesture();
            StopClassCompanionGesture();

            gestureControlOverlayMenu.Dispatcher.Invoke((Action)(() =>
            {

                currently_Controlling.Content = "Currently Controlling: None";
                this.Opacity = 0;

            }));

        }

        public void StopAllButClassCompanion()
        {
            StopAudioGesture();
            StopDocumentGesture();
            StopPowerPointGesture();
            StopVideoGesture();
            StopImageGesture();
            StopWebpageGesture();
        }


        public void SetUpTouchlessController(MainWindow form1, GestureControlOverlayMenu gestureControlOverlayMenu1, PXCMSenseManager sm)
        {

            form = form1;

            setCommands();

            gestureControlOverlayMenu = gestureControlOverlayMenu1;

            // Enable the touchless controller feature.
            sm.EnableTouchlessController();

            // Initialize the pipeline
            sm.Init();

            // Get the module instance 
            PXCMTouchlessController tc = sm.QueryTouchlessController();

            pxcmStatus rc;
            PXCMTouchlessController.ProfileInfo pInfo;

            rc = tc.QueryProfile(out pInfo);
            Console.WriteLine("Querying Profile: " + rc.ToString());
            if (rc != pxcmStatus.PXCM_STATUS_NO_ERROR)
                Environment.Exit(-1);

            pInfo.config = PXCMTouchlessController.ProfileInfo.Configuration.Configuration_Scroll_Vertically | PXCMTouchlessController.ProfileInfo.Configuration.Configuration_Meta_Context_Menu;

            rc = tc.SetProfile(pInfo);
            Console.WriteLine("Setting Profile: " + rc.ToString());

            // register for the ux events
            tc.SubscribeEvent(OnFiredUXEvent);

            // Streaming data
            while (sm.AcquireFrame(true) >= pxcmStatus.PXCM_STATUS_NO_ERROR && kill == false)
            {

                sm.ReleaseFrame();

            }

            // Clean up
            sm.Dispose();
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
             gestureControlOverlayMenu.Dispatcher.Invoke((Action)(() =>
             {
                    this.Center_Border.Opacity = 0.5;
             }));
        }

        private void Center_Border_MouseLeave(object sender, MouseEventArgs e)
        {
            gestureControlOverlayMenu.Dispatcher.Invoke((Action)(() =>
            {
                this.Center_Border.Opacity = 0.0;
            }));
        }

    }
}
