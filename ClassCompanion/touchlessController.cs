using System;
using System.Collections.Generic;
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
using System.Threading;
using System.Runtime.InteropServices;


namespace ClassCompanion
{
    class touchlessController
    {

        //[DllImport("User32.dll")]
        //public static extern bool SetCursorPos(int x, int y);

        //MainWindow form;

        //GestureControlOverlayMenu gestureControlOverlayMenu;

        //int point_X_Old;
        //int point_Y_Old;

        //float initialScrollPoint;

        //public void OnFiredUXEvent(PXCMTouchlessController.UXEventData data)
        //{
        //    // handle the event….
        //    switch (data.type)
        //    {
        //        case PXCMTouchlessController.UXEventData.UXEventType.UXEvent_CursorVisible:
        //            {
        //                Console.WriteLine("Cursor Visible");

        //            }
        //            break;
        //        case PXCMTouchlessController.UXEventData.UXEventType.UXEvent_CursorNotVisible:
        //            {
        //                Console.WriteLine("Cursor Not Visible");
        //                gestureControlOverlayMenu.Dispatcher.Invoke((Action)(() =>
        //                {
        //                    gestureControlOverlayMenu.Opacity = 0;
        //                }));
        //            }
        //            break;
        //        case PXCMTouchlessController.UXEventData.UXEventType.UXEvent_Select:
        //            {
        //                Console.WriteLine("Select");

        //            }
        //            break;
        //        case PXCMTouchlessController.UXEventData.UXEventType.UXEvent_MetaPinch:
        //            {
        //                Console.WriteLine("Pinch Detected");
        //            }
        //            break;
        //        case PXCMTouchlessController.UXEventData.UXEventType.UXEvent_ShowMetaMenu:
        //            {
        //                Console.WriteLine("Pinch Detected");
        //            }
        //            break;
        //        case PXCMTouchlessController.UXEventData.UXEventType.UXEvent_StartScroll:
        //            {
        //                Console.WriteLine("Start Scroll");
        //                initialScrollPoint = data.position.y;

        //            }
        //            break;
        //        case PXCMTouchlessController.UXEventData.UXEventType.UXEvent_Scroll:
        //            {
        //                Console.WriteLine("Scroll Event");
        //            }
        //            break;
        //        case PXCMTouchlessController.UXEventData.UXEventType.UXEvent_EndScroll:
        //            {
        //                Console.WriteLine("End Scroll");
        //            }
        //            break;
        //        case PXCMTouchlessController.UXEventData.UXEventType.UXEvent_CursorMove:
        //            {

        //                gestureControlOverlayMenu.Dispatcher.Invoke((Action)(() =>
        //                {
        //                    gestureControlOverlayMenu.Opacity = .5;
        //                }));


        //                Point point = new Point();
        //                point.X = Math.Max(Math.Min(1.0F, data.position.x), 0.0F);
        //                point.Y = Math.Max(Math.Min(1.0F, data.position.y), 0.0F);

        //                //Point myListBoxPosition = DisplayArea.PointToScreen(new Point(0d, 0d));

        //                int mouseX = (int)(point.X * System.Windows.SystemParameters.PrimaryScreenWidth);
        //                int mouseY = (int)(point.Y * System.Windows.SystemParameters.PrimaryScreenHeight);

        //                SetCursorPos(mouseX, mouseY);



        //                //System.Drawing.Point point = new System.Drawing.Point();
        //                //point.X = (int) (Math.Max(Math.Min(1.0F, data.position.x), 0.0F)*1500);
        //                //point.Y = (int) (Math.Max(Math.Min(1.0F, data.position.y), 0.0F)*800);

        //                //Console.WriteLine(point.X + " , " + point.Y);

        //                //point = pointDamper(point);

        //                //System.Windows.Forms.Cursor.Position = point;

        //            }
        //            break;
        //    }
        //}
        //// x = 10 and then x = 30   new - old = delta  If abs(delta) > 10 
        //public System.Drawing.Point pointDamper(System.Drawing.Point point)
        //{
        //    if (Math.Abs(point.X - point_X_Old) > 10)
        //    {
        //        Console.WriteLine("X is greater than 20");
        //        point.X = point_X_Old;
        //    }

        //    if (Math.Abs(point.Y - point_Y_Old) > 10)
        //    {
        //        point.Y = point_Y_Old;
        //    }

        //    point_Y_Old = point.Y;
        //    point_X_Old = point.X;

        //    return point;
        //}

        //public void SetUpTouchlessController(MainWindow form1, GestureControlOverlayMenu gestureControlOverlayMenu1, PXCMSenseManager sm)
        //{

        //    form = form1;

        //    gestureControlOverlayMenu = gestureControlOverlayMenu1;

        //    // Enable the touchless controller feature.
        //    sm.EnableTouchlessController();

        //    // Initialize the pipeline
        //    sm.Init();

        //    // Get the module instance 
        //    PXCMTouchlessController tc = sm.QueryTouchlessController();

        //    // register for the ux events
        //    tc.SubscribeEvent(OnFiredUXEvent);

        //    // Streaming data
        //    while (sm.AcquireFrame(true) >= pxcmStatus.PXCM_STATUS_NO_ERROR)
        //    {

        //        sm.ReleaseFrame();

        //    }

        //    // Clean up
        //    sm.Dispose();
        //}

    }

}
