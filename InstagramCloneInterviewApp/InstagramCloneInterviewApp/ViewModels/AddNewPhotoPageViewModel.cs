using InstagramCloneInterviewApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InstagramCloneInterviewApp.ViewModels
{
    public class AddNewPhotoPageViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public Command LoadSaveNewPhotoCommand { get; set; }
        public Photo selectedPhoto;
        public Photo SelectedPhoto
        {
            get { return selectedPhoto; }
            set
            {
                if (selectedPhoto != value)
                {
                    selectedPhoto = value;
                    SetProperty(ref selectedPhoto, value);
                    OnPropertyChanged("SelectedPhoto");
                }
            }
        }
        public AddNewPhotoPageViewModel()
        {
            SelectedPhoto = new Photo()
            {
            AlbumId=1,
            ThumbnailUrl= "https://via.placeholder.com/150/771796",
            Url= "https://via.placeholder.com/600/771796",
            Title= ""
            };
            LoadSaveNewPhotoCommand = new Command(execute: async () => await ExecuteSaveEditItem());
        }
        //Save user's photo
        async Task ExecuteSaveEditItem()
        {
            if (IsBusy)
                return;
            try
            {
                if (SelectedPhoto.Title == null || SelectedPhoto.Title.Length == 0)
                {
                    ToastMessage.LongAlert("All fields are required!");
                    return;
                }
                var save_photo_status = await InstagramCloneDataStore.SaveNewPhoto(SelectedPhoto);
                if (save_photo_status.Status_Code == 201)
                {
                    ToastMessage.LongAlert("Your image was successfully saved!");
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else if (save_photo_status.Status_Code == 1)
                {
                    await Application.Current.MainPage.DisplayAlert("", "Check your internet connection and try again later...", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("", "We have some Server Error, please try again later...", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                var msg = ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
