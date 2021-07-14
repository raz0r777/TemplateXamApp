using InstagramCloneInterviewApp.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InstagramCloneInterviewApp
{

    public partial class App : Application
    {
        public static string FakeApiUrlAdress = "https://jsonplaceholder.typicode.com";
        public App()
        {
            InitializeComponent();
            DependencyService.Register<InstagramCloneDataStore>();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
