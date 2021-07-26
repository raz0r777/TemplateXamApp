using Foundation;
using InstagramCloneInterviewApp.Interfaces;
using InstagramCloneInterviewApp.iOS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class CameraScanner : ICameraScanner
    {
        private TaskCompletionSource<ObservableCollection<ImageSource>> taskCompletionSource;

        public Task<ObservableCollection<ImageSource>> OpenScanCamera()
        {
            taskCompletionSource = new TaskCompletionSource<ObservableCollection<ImageSource>>();
            var documentCameraViewController = new VNDocumentCameraViewController();

            var documentscanDelegate = new DocumentDelegate();

            documentscanDelegate.OnScanTaken += (VNDocumentCameraScan scan) =>
            {
                ObservableCollection<ImageSource> images = new ObservableCollection<ImageSource>();

                for (int i = 0; i < Convert.ToInt32(scan.PageCount); i++)
                {
                    var stream = scan.GetImage((uint)i).AsPNG().AsStream();
                    stream.Position = 0;
                    images.Add(ImageSource.FromStream(() => stream));
                }

                documentCameraViewController.DismissViewController(true, null);
                Debug.WriteLine($"{scan.PageCount} Pages!");

                taskCompletionSource.SetResult(images);
            };

            documentscanDelegate.OnCanceled += () =>
            {
                documentCameraViewController.DismissViewController(true, null);
            };

            documentCameraViewController.ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen;
            documentCameraViewController.Delegate = documentscanDelegate;
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(documentCameraViewController, true, null);
            return taskCompletionSource.Task;
        }
    }

    public class DocumentDelegate : VNDocumentCameraViewControllerDelegate
    {
        public delegate void ScanTakenEventHandler(VNDocumentCameraScan scan);
        public event ScanTakenEventHandler OnScanTaken;

        public delegate void ScanCanceledEventHandler();
        public event ScanCanceledEventHandler OnCanceled;
        private readonly ObservableCollection<Stream> images = new ObservableCollection<Stream>();
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