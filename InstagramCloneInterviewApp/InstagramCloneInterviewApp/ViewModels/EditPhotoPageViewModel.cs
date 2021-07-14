using InstagramCloneInterviewApp.Models;
using InstagramCloneInterviewApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InstagramCloneInterviewApp.ViewModels
{
    public class EditPhotoPageViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public Command LoadEditPhotoSaveCommand { get; set; }
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
        public EditPhotoPageViewModel()
        {
            SelectedPhoto = new Photo();
            LoadEditPhotoSaveCommand = new Command(execute: async () => await ExecuteSaveEditItem());
        }
        //Save user's edited photo
        async Task ExecuteSaveEditItem()
        {
            if (IsBusy)
                return;
            try
            {
                if(SelectedPhoto.Title ==null || SelectedPhoto.Title.Length == 0)
                {
                    ToastMessage.LongAlert("All fields are required!");
                    return;
                }
                var update_photo_status = await InstagramCloneDataStore.SaveEditedPhoto(SelectedPhoto);
                if (update_photo_status.Status_Code == 200)
                {
                    ToastMessage.LongAlert("Your image was successfully edited!");
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else if (update_photo_status.Status_Code == 1)
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
