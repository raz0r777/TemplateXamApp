using InstagramCloneInterviewApp.Models;
using InstagramCloneInterviewApp.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InstagramCloneInterviewApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DocumentScanningPage : ContentPage
    {
        DocumentScanningPageViewModel viewModel = new DocumentScanningPageViewModel();
        public DocumentScanningPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
            MessagingCenter.Subscribe<Object, ObservableCollection<Stream>>(this, "ScannedDocs", (args, images) =>
            {
                foreach (var element in images)
                {
                    ScannedImage img = new ScannedImage();
                    element.Position = 0;
                    img.ImageData = element;
                    viewModel.ScannedImages.Add(img);
                }
            });
        }
        private void ScanDocumentButtonClicked(object sender, EventArgs e)
        {
            try
            { 
                if (Device.RuntimePlatform == Device.Android)
                    Application.Current.MainPage.DisplayAlert("", "AT THIS MOMENT WE DONT SUPPORT THIS FEATURE FOR ANDROID", "OK");
                else
                    DependencyService.Get<Interfaces.ICameraScanner>().OpenScanCamera();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }
    }
}