using Foundation;
using InstagramCloneInterviewApp.Interfaces;
using InstagramCloneInterviewApp.iOS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using VisionKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(CameraScanner))]
namespace InstagramCloneInterviewApp.iOS
{
    class CameraScanner : ICameraScanner
    {
        TaskCompletionSource<List<Stream>> taskCompletionSource;

        public void OpenScanCamera()
        {
            ScanViewController viewController = new ScanViewController();
            UIApplication.SharedApplication.KeyWindow.RootViewController.
              PresentViewController(viewController, false, null);

            //var documentCameraViewController = new VNDocumentCameraViewController();
            //documentCameraViewController.Delegate = new DocumentDelegate();
            //UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(documentCameraViewController, true, null);
        }
    }

    public class DocumentDelegate : VNDocumentCameraViewControllerDelegate
    {
        public override void DidCancel(VNDocumentCameraViewController controller)
        {
            Debug.WriteLine("DocumentScanDelegate:DidCancel");
        }

        public override void DidFinish(VNDocumentCameraViewController controller, VNDocumentCameraScan scan)
        {
            Debug.WriteLine(scan);
        }

        public override void DidFail(VNDocumentCameraViewController controller, NSError error)
        {
            Debug.WriteLine("Failed scanning photo");
        }
    }
}