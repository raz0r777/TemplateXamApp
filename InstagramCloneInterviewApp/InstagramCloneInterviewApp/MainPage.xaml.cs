using InstagramCloneInterviewApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InstagramCloneInterviewApp
{
    public partial class MainPage : ContentPage
    {
        MainPageViewModel viewModel = new MainPageViewModel();
        public MainPage()
        {
            InitializeComponent();
            BindingContext = viewModel;

        }
        protected override void OnAppearing()
        {
            viewModel.LoadPhotosCommand.Execute(null);
            base.OnAppearing();
        }
    }
}
