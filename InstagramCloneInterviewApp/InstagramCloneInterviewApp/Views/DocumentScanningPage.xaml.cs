using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InstagramCloneInterviewApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DocumentScanningPage : ContentPage
    {
        public DocumentScanningPage()
        {
            InitializeComponent();
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