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
    public class PhotoDetailsPageViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public Command LoadImageFullSizeCommand { get; set; }
        public Command LoadDeleteImageCommand { get; set; }
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
        public PhotoDetailsPageViewModel()
        {
            LoadImageFullSizeCommand = new Command(execute: async (image) => await ExecuteGetFullSizeImage(image));
            LoadDeleteImageCommand = new Command(execute: async () => await ExecuteDeleteImage());
        }
        //Load Image in fullsize
        async Task ExecuteGetFullSizeImage(object img)
        {
            if (IsBusy)
                return;
            try
            {
                var image_source = img as ImageSource;
                if(image_source!= null)
                await Application.Current.MainPage.Navigation.PushAsync(new FullSizeImagePage(image_source));
                else
                await Application.Current.MainPage.DisplayAlert("", "Image Source is empty...", "OK");
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
        //Delete choosed image
        async Task ExecuteDeleteImage()
        {
            if (IsBusy)
                return;
            try
            {
                var check_user_answer = await Application.Current.MainPage.DisplayAlert("", "Are you sure you want to delete this photo?" , "Yes", "No");
                if (check_user_answer)
                {
                    var deleted_photo_status = await InstagramCloneDataStore.DeleteSelectedPhoto(SelectedPhoto.Id);
                    if (deleted_photo_status.Status_Code == 200)
                    {
                        ToastMessage.LongAlert("Your image was successfully deleted!");
                        Application.Current.MainPage = new NavigationPage(new MainPage());                
                    }
                    else if (deleted_photo_status.Status_Code == 1)
                    {
                        await Application.Current.MainPage.DisplayAlert("", "Check your internet connection and try again later...", "OK");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("", "We have some Server Error, please try again later...", "OK");
                    }
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
