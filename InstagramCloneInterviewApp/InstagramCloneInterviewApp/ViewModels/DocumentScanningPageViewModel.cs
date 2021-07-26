using InstagramCloneInterviewApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace InstagramCloneInterviewApp.ViewModels
{
    public class DocumentScanningPageViewModel : BaseViewModel
    {
        private ObservableCollection<ScannedImage> scannedImages;
        public ObservableCollection<ScannedImage> ScannedImages
        {
            get { return scannedImages; }
            set
            {
                if (scannedImages != value)
                {
                    scannedImages = value;

                    SetProperty(ref scannedImages, value);                  
                }
            }
        }

        private ImageSource _image;
        public ImageSource Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        public DocumentScanningPageViewModel()
        {
            ScannedImages = new ObservableCollection<ScannedImage>();
        }
    }
}
