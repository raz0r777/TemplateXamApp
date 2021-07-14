using InstagramCloneInterviewApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace InstagramCloneInterviewApp.ViewModels
{
    public class DocumentScanningPageViewModel : BaseViewModel, INotifyPropertyChanged
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
        public DocumentScanningPageViewModel()
        {
            ScannedImages = new ObservableCollection<ScannedImage>();
        }
    }
}
