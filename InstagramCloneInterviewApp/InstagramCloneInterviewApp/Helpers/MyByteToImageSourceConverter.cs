using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace InstagramCloneInterviewApp.Helpers
{
    public class MyByteToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                Stream ImageData = (Stream)value;
                ImageData.Position = 0;

                var a = ImageSource.FromStream(() => ImageData);

                return a;
            }
            else
            {
               return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
