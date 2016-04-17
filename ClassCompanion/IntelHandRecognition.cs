using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ClassCompanion
{
    class IntelHandRecognition
    {

        PowerPointControl powerpointcontrol;
        AudioControl audiocontrol;
        VideoControl videocontrol;
        ClassCompanionControl classcompanioncontrol;
        DocumentControl documentcontrol;
        ImageControl imagecontrol;
        WebsiteControl webpagecontrol;

        Boolean powerpointVoiceFlag = false;
        Boolean audioVoiceFlag = false;
        Boolean videoVoiceFlag = false;
        Boolean classcompanionVoiceFlag = false;
        Boolean documentVoiceFlag = false;
        Boolean imageVoiceFlag = false;
        Boolean webpageVoiceFlag = false;

        Boolean backgroundMode = false;

        private MainWindow form;
        private bool _disconnected = false;
        //Queue containing depth image - for synchronization purposes
        private const int NumberOfFramesToDelay = 3;
        private float _maxRange;
       


        /* Checking if sensor device connect or not */
        private bool DisplayDeviceConnection(bool state)
        {
            if (state)
            {
                if (!_disconnected) Console.WriteLine("Device Disconnected");
                _disconnected = true;
            }
            else
            {
                if (_disconnected) Console.WriteLine("Device Reconnected");
                _disconnected = false;
            }
            return _disconnected;
        }

        /* Displaying current frame gestures */
        private void DisplayGesture(PXCMHandData handAnalysis,int frameNumber)
        {

            int firedGesturesNumber = handAnalysis.QueryFiredGesturesNumber();
            string gestureStatusLeft = string.Empty;
            string gestureStatusRight = string.Empty;

            string gestureLeftHand = string.Empty;
            string gestureRightHand = string.Empty;
            string bothHands = string.Empty;

            if (firedGesturesNumber == 0)
            {
              
                return;
            }

            //Give the thread a rest and also helps with misread hand gestures.
            Thread.Sleep(1000);
            if (firedGesturesNumber > 1)
            {
                // 2 Hands of gestures detected
                for (int i = 0; i < firedGesturesNumber; i++)
                {

                    PXCMHandData.GestureData gestureData;


                    if (handAnalysis.QueryFiredGestureData(i, out gestureData) == pxcmStatus.PXCM_STATUS_NO_ERROR)
                    {
                        PXCMHandData.IHand handData;
                        if (handAnalysis.QueryHandDataById(gestureData.handId, out handData) != pxcmStatus.PXCM_STATUS_NO_ERROR)
                            return;


                        PXCMHandData.BodySideType bodySideType = handData.QueryBodySide();
                        if (bodySideType == PXCMHandData.BodySideType.BODY_SIDE_LEFT)
                        {

                            gestureLeftHand = "L:" + gestureData.name;
                        }
                        else if (bodySideType == PXCMHandData.BodySideType.BODY_SIDE_RIGHT)
                        {

                            gestureRightHand = "R:" + gestureData.name;
                        }

                    }

                }

            }
            else
            {
                //One hand of gestures detected

                    PXCMHandData.GestureData gestureData;


                    if (handAnalysis.QueryFiredGestureData(0, out gestureData) == pxcmStatus.PXCM_STATUS_NO_ERROR)
                    {
                        PXCMHandData.IHand handData;
                        if (handAnalysis.QueryHandDataById(gestureData.handId, out handData) != pxcmStatus.PXCM_STATUS_NO_ERROR)
                            return;


                        PXCMHandData.BodySideType bodySideType = handData.QueryBodySide();
                        if (bodySideType == PXCMHandData.BodySideType.BODY_SIDE_LEFT)
                        {

                            gestureLeftHand = "L:" + gestureData.name;
                        }
                        else if (bodySideType == PXCMHandData.BodySideType.BODY_SIDE_RIGHT)
                        {

                            gestureRightHand = "R:" + gestureData.name;
                        }

                    }

            }

            bothHands = gestureLeftHand + gestureRightHand;

            //Comands

            if (powerpointVoiceFlag == true)
            {
                switch (bothHands)
                {
                    case "R:tap":
                        powerpointcontrol.NextSlide();
                        break;
                    case "L:tap":
                        powerpointcontrol.PerviousSlide();
                        break;
                    case "R:v_sign":
                        powerpointcontrol.FullScreen();
                        break;
                    case "L:v_sign":
                        powerpointcontrol.Exit();
                        break;
                    //case "R:tapL:tap":
                    //    powerpointcontrol.FullScreen();
                    //    break;
                    default:
                        Console.WriteLine("No action to powerpoint...");
                        break;
                }
            }

            if (audioVoiceFlag == true)
            {
                switch (bothHands)
                {
                    case "R:tap":
                        audiocontrol.WMI_Play_Pause();
                        audiocontrol.WMI_Play_Pause();
                        break;
                    case "L:tap":
                        audiocontrol.WMI_Play_Pause();
                        audiocontrol.WMI_Play_Pause();
                        break;
                    case "R:v_sign":
                        audiocontrol.WMI_Volume_Up();
                        audiocontrol.QT_Volume_Up();
                        break;
                    case "L:v_sign":
                        audiocontrol.WMI_Volume_Down();
                        audiocontrol.QT_Volume_Down();
                        break;
                    default:
                        Console.WriteLine("No action to audio...");
                        break;
                }
            }


            if (imageVoiceFlag == true)
            {
                switch (bothHands)
                {
                    case "R:tap":
                        imagecontrol.Next_Picture();
                        break;
                    case "L:tap":
                        imagecontrol.Last_Picture();
                        break;
                    default:
                        Console.WriteLine("No action to image...");
                        break;
                }
            }


            if (videoVoiceFlag == true)
            {
                switch (bothHands)
                {
                    case "R:tap":
                        videocontrol.WMI_Play_Pause();
                        videocontrol.QT_Play_Pause();
                        break;
                    case "L:tap":
                        videocontrol.WMI_Play_Pause();
                        videocontrol.WMI_Play_Pause();
                        break;
                    case "R:v_sign":
                        videocontrol.WMI_Volume_Up();
                        break;
                    case "L:v_sign":
                        videocontrol.WMI_Volume_Down();
                        break;
                    default:
                        Console.WriteLine("No action to video...");
                        break;
                }
            }


            if (documentVoiceFlag == true)
            {
                switch (bothHands)
                {
                    case "R:tap":
                        documentcontrol.ScrollDown();
                        break;
                    case "L:tap":
                        documentcontrol.ScrollUp();
                        break;
                    case "R:v_sign":
                        documentcontrol.ScrollDown();
                        break;
                    case "L:v_sign":
                        documentcontrol.ScrollUp();
                        break;
                    default:
                        Console.WriteLine("No action to document...");
                        break;
                }
            }

            if (webpageVoiceFlag == true)
            {
                switch (bothHands)
                {
                    case "R:tap":
                        webpagecontrol.ScrollDown();
                        break;
                    case "L:tap":
                        webpagecontrol.ScrollUp();
                        break;
                    case "R:v_sign":
                        webpagecontrol.FullScreen();
                        break;
                    case "L:v_sign":
                        webpagecontrol.FullScreen();
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
                    switch (bothHands)
                    {
                        case "R:tap":
                            classcompanioncontrol.NextItem();
                            break;
                        case "L:tap":
                            classcompanioncontrol.LastItem();
                            break;
                        case "L:v_sign":
                            classcompanioncontrol.ExitControl();
                            this.StopAll();
                            break;
                        case "R:v_sign":
                            classcompanioncontrol.StartItem();
                            break;
                        default:
                            Console.WriteLine("No action to classcompanion...");
                            break;
                    }
                }
                else
                {
                    switch (bothHands)
                    {
                        case "L:v_signR:v_sign":
                            classcompanioncontrol.ActivateWindow();
                            backgroundMode = false;
                            break;
                        case "R:v_signL:v_sign":
                            classcompanioncontrol.ActivateWindow();
                            backgroundMode = false;
                            break;
                        default:
                            Console.WriteLine("No action to classcompanion...");
                            break;
                    }
                }
            }

                //Right Hand Only
                Console.WriteLine("Frame " + frameNumber + ") " + bothHands + "\n", Color.SeaGreen);
        }

        /* Displaying current frame alerts */
        private void DisplayAlerts(PXCMHandData handAnalysis, int frameNumber)
        {
            bool isChanged = false;
            string sAlert = "Alert: ";
            for (int i = 0; i < handAnalysis.QueryFiredAlertsNumber(); i++)
            {
                PXCMHandData.AlertData alertData;
                if (handAnalysis.QueryFiredAlertData(i, out alertData) != pxcmStatus.PXCM_STATUS_NO_ERROR)
                    continue;

                //See PXCMHandAnalysis.AlertData.AlertType for all available alerts
                switch (alertData.label)
                {
                    case PXCMHandData.AlertType.ALERT_HAND_DETECTED:
                        {

                            sAlert += "Hand Detected, ";
                            isChanged = true;
                            break;
                        }
                    case PXCMHandData.AlertType.ALERT_HAND_NOT_DETECTED:
                        {

                            sAlert += "Hand Not Detected, ";
                            isChanged = true;
                            break;
                        }
                    case PXCMHandData.AlertType.ALERT_HAND_CALIBRATED:
                        {

                            sAlert += "Hand Calibrated, ";
                            isChanged = true;
                            break;
                        }
                    case PXCMHandData.AlertType.ALERT_HAND_NOT_CALIBRATED:
                        {

                            sAlert += "Hand Not Calibrated, ";
                            isChanged = true;
                            break;
                        }
                    case PXCMHandData.AlertType.ALERT_HAND_INSIDE_BORDERS:
                        {

                            sAlert += "Hand Inside Border, ";
                            isChanged = true;
                            break;
                        }
                    case PXCMHandData.AlertType.ALERT_HAND_OUT_OF_BORDERS:
                        {

                            sAlert += "Hand Out Of Borders, ";
                            isChanged = true;
                            break;
                        }
                }
            }
            if (isChanged)
            {
                Console.WriteLine("Frame " + frameNumber + ") " + sAlert + "\n", Color.RoyalBlue);
            }


        }

        public static pxcmStatus OnNewFrame(Int32 mid, PXCMBase module, PXCMCapture.Sample sample)
        {
            return pxcmStatus.PXCM_STATUS_NO_ERROR;
        }

        public void SetClassCompanionBackgrounMode(Boolean mode)
        {
            backgroundMode = mode;
        }

        public void SetUpPowerPointGesture()
        {
            powerpointVoiceFlag = true;

            powerpointcontrol = new PowerPointControl();

        }

        public void StopPowerPointGesture()
        {
            powerpointVoiceFlag = false;
        }

        public void SetUpAudioGesture()
        {
            audioVoiceFlag = true;

            audiocontrol = new AudioControl();

        }

        public void StopAudioGesture()
        {
            audioVoiceFlag = false;
        }

        public void SetUpVideoGesture()
        {
            videoVoiceFlag = true;

            videocontrol = new VideoControl();

        }

        public void StopVideoGesture()
        {
            videoVoiceFlag = false;
        }

        public void SetUpDocumentGesture()
        {
            documentVoiceFlag = true;

            documentcontrol = new DocumentControl();

        }

        public void StopDocumentGesture()
        {
            documentVoiceFlag = false;
        }

        public void SetUpImageGesture()
        {
            imageVoiceFlag = true;

            imagecontrol = new ImageControl();

        }

        public void StopImageGesture()
        {
            imageVoiceFlag = false;
        }

        public void SetUpWebpageGesture()
        {
            webpageVoiceFlag = true;

            webpagecontrol = new WebsiteControl();

        }

        public void StopWebpageGesture()
        {
            webpageVoiceFlag = false;
        }

        public void SetUpClassCompanionGesture(MainWindow form1)
        {
            classcompanionVoiceFlag = true;

            classcompanioncontrol = new ClassCompanionControl(form1);

        }

        public void StopClassCompanionGesture()
        {
            classcompanionVoiceFlag = false;
        }

        public void StopAll()
        {
            StopAudioGesture();
            StopDocumentGesture();
            StopPowerPointGesture();
            StopVideoGesture();
            StopImageGesture();
            StopClassCompanionGesture();
        }

        public void StopAllButClassCompanion()
        {
            StopAudioGesture();
            StopDocumentGesture();
            StopPowerPointGesture();
            StopVideoGesture();
            StopImageGesture();
        }

        /* Using PXCMSenseManager to handle data */
        public void SimplePipeline(MainWindow form1, PXCMSenseManager session)
        {   
            Console.WriteLine(String.Empty,Color.Black);
            bool liveCamera = false;

            bool flag = true;
            PXCMSenseManager instance = session;
            _disconnected = false;
            //instance = session.CreateSenseManager();
            if (instance == null)
            {
                Console.WriteLine("Failed creating SenseManager");
                return;
            }




            PXCMCapture.DeviceInfo info;

            liveCamera = true;

            /* Set Module */
            pxcmStatus status = instance.EnableHand();
            PXCMHandModule handAnalysis = instance.QueryHand();


            if (status != pxcmStatus.PXCM_STATUS_NO_ERROR || handAnalysis == null)
            {
                Console.WriteLine("Failed Loading Module");
                return;
            }

            PXCMSenseManager.Handler handler = new PXCMSenseManager.Handler();
            handler.onModuleProcessedFrame = new PXCMSenseManager.Handler.OnModuleProcessedFrameDelegate(OnNewFrame);


            PXCMHandConfiguration handConfiguration = handAnalysis.CreateActiveConfiguration();
            PXCMHandData handData = handAnalysis.CreateOutput();

            if (handConfiguration == null)
            {
                Console.WriteLine("Failed Create Configuration");
                return;
            }
            if (handData==null)
            {
                Console.WriteLine("Failed Create Output");
                return;
            }


            Console.WriteLine("Init Started");
            if (handAnalysis != null && instance.Init(handler) == pxcmStatus.PXCM_STATUS_NO_ERROR)
            {

                PXCMCapture.DeviceInfo dinfo;

                PXCMCapture.Device device = instance.captureManager.device;
                if (device != null)
                {
                    pxcmStatus result = device.QueryDeviceInfo(out dinfo);
                    if (result==pxcmStatus.PXCM_STATUS_NO_ERROR && dinfo != null && dinfo.model == PXCMCapture.DeviceModel.DEVICE_MODEL_IVCAM)
                    {
                        device.SetDepthConfidenceThreshold(1);
                        device.SetMirrorMode(PXCMCapture.Device.MirrorMode.MIRROR_MODE_DISABLED);
                        device.SetIVCAMFilterOption(6);
                    }

                    _maxRange = device.QueryDepthSensorRange().max;

                }
               

                if (handConfiguration != null)
                {
                    handConfiguration.EnableAllAlerts();
                    handConfiguration.EnableSegmentationImage(true);

                    handConfiguration.ApplyChanges();
                    handConfiguration.Update();
                }

                Console.WriteLine("Streaming");
                int frameCounter = 0;
                int frameNumber = 0;

                // Stream data

                handConfiguration.DisableAllGestures();
                
                //Load gestures depending on the module you are in:
                //handConfiguration.EnableGesture("spreadfingers", true);
                //handConfiguration.EnableGesture("swipe", true);
                handConfiguration.EnableGesture("tap", true);
                //handConfiguration.EnableGesture("thumb_down", true);
                //handConfiguration.EnableGesture("thumb_up", true);
                //handConfiguration.EnableGesture("two_fingers_pinch_open ", true);
                handConfiguration.EnableGesture("v_sign", true);
                //handConfiguration.EnableGesture("full_pinch", true);
                //handConfiguration.EnableGesture("wave", true);

                //Apply Changes
                handConfiguration.ApplyChanges();

                while (true)
                {


                    if (instance.AcquireFrame(true) < pxcmStatus.PXCM_STATUS_NO_ERROR)
                    {
                        break;
                    }

                    frameCounter++;

                    if (!DisplayDeviceConnection(!instance.IsConnected()))
                    {

                        if (handData != null)
                        {
                            handData.Update();
                        }


                            if (handData != null)
                            {
                                frameNumber = liveCamera ? frameCounter : instance.captureManager.QueryFrameIndex();

                                DisplayGesture(handData, frameNumber);
                                DisplayAlerts(handData, frameNumber);
                            }
                    }
                    instance.ReleaseFrame();
                }


                // Clean Up
                if (handData != null) handData.Dispose();
            }
            else
            {
                Console.WriteLine("Init Failed");
                flag = false;
            }


            if (handConfiguration != null) handConfiguration.Dispose();
            instance.Close();
            instance.Dispose();
            if (flag)
            {
                Console.WriteLine("Stopped");
            }
        }
    }
}
