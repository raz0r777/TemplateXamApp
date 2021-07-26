using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InstagramCloneInterviewApp.Interfaces
{
    public interface ICameraScanner
    {
        Task<ObservableCollection<ImageSource>> OpenScanCamera();
    }
}
