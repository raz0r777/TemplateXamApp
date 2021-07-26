using InstagramCloneInterviewApp.Models;
using InstagramCloneInterviewApp.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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
        }
        private async void ScanDocumentButtonClicked(object sender, EventArgs e)
        {
            try
            { 
                if (Device.RuntimePlatform == Device.Android)
                    await Application.Current.MainPage.DisplayAlert("", "AT THIS MOMENT WE DONT SUPPORT THIS FEATURE FOR ANDROID", "OK");
                else
                {
                    var images = await DependencyService.Get<Interfaces.ICameraScanner>().OpenScanCamera();

                    var a = images.Select((x) => new ScannedImage { ImageData = x }).ToList();

                    foreach (var image in a)
                    {
                        viewModel.ScannedImages.Add(image);
                    }
                    

                    viewModel.Image = a[0].ImageData;
                }                 
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }
    }
}