using InstagramCloneInterviewApp.Interfaces;
using InstagramCloneInterviewApp.Models;
using InstagramCloneInterviewApp.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InstagramCloneInterviewApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhotoDetailsPage : ContentPage
    {
        PhotoDetailsPageViewModel viewModel = new PhotoDetailsPageViewModel();
        public PhotoDetailsPage(Photo photo)
        {
            InitializeComponent();
            viewModel.SelectedPhoto = photo;
            BindingContext = viewModel;           
        }
    
        private void TapGestureRecognizerImageFullSize(object sender, EventArgs e)
        {
            var imageSource = (Image)sender;
            var selectedImage = imageSource.Source;
            viewModel.LoadImageFullSizeCommand.Execute(selectedImage);      
        }

        private void Edit_Clicked(object sender, EventArgs e)
        {
             Application.Current.MainPage.Navigation.PushAsync((new EditPhotoPage(viewModel.SelectedPhoto)));
        }
    }
}