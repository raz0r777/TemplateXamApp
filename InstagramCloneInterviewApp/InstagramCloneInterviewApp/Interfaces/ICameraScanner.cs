using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace InstagramCloneInterviewApp.Interfaces
{
    public interface ICameraScanner
    {
        Task<ObservableCollection<Stream>> OpenScanCamera();
    }
}
