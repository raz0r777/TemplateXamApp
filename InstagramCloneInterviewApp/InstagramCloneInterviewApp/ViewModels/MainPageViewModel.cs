using InstagramCloneInterviewApp.Models;
using InstagramCloneInterviewApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InstagramCloneInterviewApp.ViewModels
{
    public class MainPageViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public Command LoadPhotosCommand { get; set; }
        public Command LoadRefreshCommand { get; set; }
        public Command LoadDocumentScanning { get; set; }
        public Command LoadNewPhotosCommand{get;set;}
        public Command LoadSelectedPhotoCommand { get; set; }
        public Command LoadAddNewPhotoCommand { get; set; }
        public ObservableCollection<Photo> photos;
        public ObservableCollection<Photo> Photos
        {
            get { return photos; }
            set
            {
                if (photos != value)
                {
                    photos = value;
                    SetProperty(ref photos, value);
                    OnPropertyChanged("Photos");
                }
            }
        }
        public ObservableCollection<Photo> photosAll;
        public ObservableCollection<Photo> PhotosAll
        {
            get { return photosAll; }
            set
            {
                if (photosAll != value)
                {
                    photosAll = value;
                    SetProperty(ref photosAll, value);
                    OnPropertyChanged("PhotosAll");
                }
            }
        }
        public PhotosModel photosModel;
        public PhotosModel PhotosModel
        {
            get { return photosModel; }
            set
            {
                if (photosModel != value)
                {
                    photosModel = value;
                    SetProperty(ref photosModel, value);
                    OnPropertyChanged("PhotosModel");
                }
            }
        }
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
        public int counterData;
        public int CounterData
        {
            get { return counterData; }
            set
            {
                if (counterData != value)
                {
                    counterData = value;
                    SetProperty(ref counterData, value);
                    OnPropertyChanged("CounterData");
                }
            }
        }
        public int standartCounter;
        public int StandartCounter
        {
            get { return standartCounter; }
            set
            {
                if (standartCounter != value)
                {
                    standartCounter = value;
                    SetProperty(ref standartCounter, value);
                    OnPropertyChanged("StandartCounter");
                }
            }
        }
        public int railCounter;
        public int RailCounter
        {
            get { return railCounter; }
            set
            {
                if (railCounter != value)
                {
                    railCounter = value;
                    SetProperty(ref railCounter, value);
                    OnPropertyChanged("RailtCounter");
                }
            }
        }
        public MainPageViewModel()
        {
            CounterData = 10;
            StandartCounter = 10;
            RailCounter = 10;
            LoadPhotosCommand = new Command(execute: async () => await ExecuteGetAllPhotos());
            LoadRefreshCommand = new Command(execute: async () => await ExecuteRefreshGetAllPhotos());
            LoadNewPhotosCommand = new Command(execute: async () => await ExecuteGetNewPhotos());
            LoadSelectedPhotoCommand = new Command(execute: async () => await ExecuteGetDetailsSelectedPhoto());
            LoadAddNewPhotoCommand = new Command(execute: async () => await ExecuteGoAddNewPhoto());
            LoadDocumentScanning = new Command(execute: async () => await ExecuteGoToScan());
            Photos = new ObservableCollection<Photo>();
            PhotosAll = new ObservableCollection<Photo>();
            PhotosModel = new PhotosModel();
            SelectedPhoto = new Photo();
        }
        //Get List of Photos
        async Task ExecuteGetAllPhotos()
        {
            if (IsBusy)
                return;      
            try
            {
                IsBusy = true;
                Photos.Clear();
                PhotosAll.Clear();
                PhotosModel = await InstagramCloneDataStore.GetAllPhotos();
                if (PhotosModel != null)
                {
                    foreach (var photo in PhotosModel.Photos)
                    {

                        PhotosAll.Add(photo);
                    }

                    foreach (var el in PhotosAll.Skip(0).Take(StandartCounter))
                    {
                        Photos.Add(el);
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
        //Refresh list of photos
        async Task ExecuteRefreshGetAllPhotos()
        {
            if (IsRefreshing)
                return;
            try
            {
                IsRefreshing = true;
                Photos.Clear();
                PhotosAll.Clear();
                PhotosModel = await InstagramCloneDataStore.GetAllPhotos();
                if (PhotosModel != null)
                {
                    if (PhotosModel.Photos != null && PhotosModel.Photos.Count > 0)
                    {
                        foreach (var photo in PhotosModel.Photos)
                        {

                            PhotosAll.Add(photo);
                        }

                        foreach (var el in PhotosAll.Skip(0).Take(StandartCounter))
                        {
                            Photos.Add(el);
                        }
                    }
                    else if (PhotosModel.Status_Code == 1)
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
                IsRefreshing = false;
            }
        }
         //How user scroll in every iteration get 10 new photos 
         async Task ExecuteGetNewPhotos()
         {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                await Task.Delay(500);
                foreach (var el in PhotosAll.Skip(RailCounter).Take(StandartCounter))
                {
                    Photos.Add(el);
                }
                RailCounter += StandartCounter;
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
        //Route to Details with photo user selected
        async Task ExecuteGetDetailsSelectedPhoto()
        {
            if (IsBusy)
                return;
            try
            {
                if (SelectedPhoto.Id > 0)
                    await Application.Current.MainPage.Navigation.PushAsync(new PhotoDetailsPage(SelectedPhoto));
                else
                    return;                               
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
        //Route to New to add new photo
        async Task ExecuteGoAddNewPhoto()
        {
            if (IsBusy)
                return;
            try
            {
              await Application.Current.MainPage.Navigation.PushAsync(new AddNewPhotoPage());
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
        //Get List of Photos
        async Task ExecuteGoToScan()
        {
            if (IsBusy)
                return;
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new DocumentScanningPage());
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
