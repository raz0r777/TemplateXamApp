using Foundation;
using InstagramCloneInterviewApp.Interfaces;
using InstagramCloneInterviewApp.iOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(CameraScanner))]
namespace InstagramCloneInterviewApp.iOS
{
    class CameraScanner : ICameraScanner
    {
        public void OpenScanCamera()
        {
            ScanViewController viewController = new ScanViewController();
            UIApplication.SharedApplication.KeyWindow.RootViewController.
              PresentViewController(viewController, false, null);
        }
    }
}