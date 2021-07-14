﻿using InstagramCloneInterviewApp.Models;
using InstagramCloneInterviewApp.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InstagramCloneInterviewApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPhotoPage : ContentPage
    {
        EditPhotoPageViewModel viewModel = new EditPhotoPageViewModel();
        public EditPhotoPage(Photo photo)
        {
            InitializeComponent();
            viewModel.SelectedPhoto = photo;
            BindingContext = viewModel;

        }
        private async void SelectImageFromLibraryButton(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("", "Your device does not currently support this functionality", "OK");
                return;
            }
            var mediaOptions = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Small
            };
            var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);
            if (imageOriginal == null)
            {
                await DisplayAlert("", "Could not get the image, please try again.", "Ok");
                return;
            }
            imageOriginal.Source = ImageSource.FromStream(() => selectedImageFile.GetStream());
        }
    }
}