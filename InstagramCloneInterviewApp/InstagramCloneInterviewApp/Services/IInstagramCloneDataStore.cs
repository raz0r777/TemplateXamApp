using InstagramCloneInterviewApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstagramCloneInterviewApp.Services
{
    public interface IInstagramCloneDataStore <T>
    {
        Task<PhotosModel> GetAllPhotos();
        Task<StatusCode> DeleteSelectedPhoto(int photo_id);
        Task<StatusCode> SaveEditedPhoto(Photo photo);
        Task<StatusCode> SaveNewPhoto(Photo photo);
    }
}
