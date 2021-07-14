using InstagramCloneInterviewApp.Models;
using InstagramCloneInterviewApp.ViewModels;
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
    public partial class FullSizeImagePage : ContentPage
    {    
        public FullSizeImagePage(ImageSource photo)
        {        
            InitializeComponent();
            try
            {
                imageFullSize.Source = photo;
            }
            catch(Exception ex)
            {
                var error = ex.Message;
            }      
        }
    }
}