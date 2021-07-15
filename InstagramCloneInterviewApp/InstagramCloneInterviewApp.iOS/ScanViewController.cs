using Foundation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using UIKit;
using VisionKit;
using Xamarin.Forms;

namespace InstagramCloneInterviewApp.iOS
{
    [Register("UniversalView")]
    public class UniversalView : UIView
    {
        public UniversalView()
        {
            Initialize();
        }

        public UniversalView(RectangleF bounds) : base(bounds)
        {
            Initialize();
        }

        void Initialize()
        {
            BackgroundColor = UIColor.Blue;

        }
    }
    [Register("ScanViewController")]
    public class ScanViewController : UIViewController
    {
        public ScanViewController()
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            //View = new UniversalView();
            base.ViewDidLoad();
            // Perform any additional setup after loading the view
        }

        public override void ViewDidAppear(bool animated)
        {
            var documentCameraController = new VNDocumentCameraViewController();
            var documentscanDelegate = new DocumentScanDelegate();
            documentscanDelegate.OnScanTaken += (VNDocumentCameraScan scan) =>
            {
                documentCameraController.DismissViewController(true, null);
                Debug.WriteLine($"{scan.PageCount} Pages!");
            };

            documentscanDelegate.OnCanceled += () =>
            {
                documentCameraController.DismissViewController(true, null);
            };

            documentCameraController.Delegate = documentscanDelegate;
            documentCameraController.ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen;
            PresentViewController(documentCameraController, true, null);
        }
    }
    class DocumentScanDelegate : VNDocumentCameraViewControllerDelegate
    {
        public delegate void ScanTakenEventHandler(VNDocumentCameraScan scan);
        public event ScanTakenEventHandler OnScanTaken;

        public delegate void ScanCanceledEventHandler();
        public event ScanCanceledEventHandler OnCanceled;

        public override void DidCancel(VNDocumentCameraViewController controller)
        {
            Debug.WriteLine("DocumentScanDelegate:DidCancel");
            OnCanceled();
        }

        public override void DidFinish(VNDocumentCameraViewController controller, VNDocumentCameraScan scan)
        {
            try
            {
                Debug.WriteLine("DocumentScanDelegate:DidFinish");
                OnScanTaken(scan);
                ObservableCollection<Stream> images = new ObservableCollection<Stream>();
                for (int i = 0; i < Convert.ToInt32(scan.PageCount); i++)
                {
                    images.Add(scan.GetImage((uint)i).AsPNG().AsStream());
                }
                MessagingCenter.Send<Object, ObservableCollection<Stream>>(this, "ScannedDocs", images);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        public override void DidFail(VNDocumentCameraViewController controller, NSError error)
        {
            Debug.WriteLine("Failed scanning photo");
        }
    }
}