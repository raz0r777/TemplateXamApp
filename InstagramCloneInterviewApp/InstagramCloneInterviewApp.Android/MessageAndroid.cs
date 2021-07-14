using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using InstagramCloneInterviewApp.Droid;
using InstagramCloneInterviewApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace InstagramCloneInterviewApp.Droid
{
    public class MessageAndroid : IMessage
    {
        public MessageAndroid()
        {
        }
        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }
    }
}