using InstagramCloneInterviewApp.Models;
using InstagramCloneInterviewApp.ViewModels;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InstagramCloneInterviewApp.Services
{
    public class InstagramCloneDataStore : BaseViewModel, IInstagramCloneDataStore<object>
    {
        HttpClient instagramCloneClient;
        public InstagramCloneDataStore()
        {
            instagramCloneClient = new HttpClient()
            {
                BaseAddress = new Uri($"{App.FakeApiUrlAdress}/")
            };
        }
        public async Task<PhotosModel> GetAllPhotos()
        {
            PhotosModel photoCall = new PhotosModel();
            try
            {           
                if (CrossConnectivity.Current.IsConnected)
                {
                    instagramCloneClient.DefaultRequestHeaders.Clear();
                    instagramCloneClient.DefaultRequestHeaders.Add("Accept", "application/json");                  
                    var response = await instagramCloneClient.GetAsync($"/photos");
                    var status_code = (int)response.StatusCode;
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonMessage;
                        using (Stream responseStream = await response.Content.ReadAsStreamAsync())
                        {
                            jsonMessage = new StreamReader(responseStream).ReadToEnd();
                        }                   
                        IList<Photo> photos = JsonConvert.DeserializeObject<IList<Photo>>(jsonMessage);
                        photoCall.Photos = photos;
                        photoCall.Status_Code = status_code;
                        return photoCall;
                    }
                    else
                    {
                        photoCall.Status_Code = status_code;
                        return photoCall;
                    }
                }
                else
                {
                    photoCall.Status_Code = 1;
                    return photoCall;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                string error = ex.Message;
                photoCall.Status_Code = 1;
                photoCall.Error_Message = error;
                return photoCall;
            }
        }
        public async Task<StatusCode> DeleteSelectedPhoto(int photo_id)
        {
            StatusCode statusCode = new StatusCode();
            try
            {          
                if (CrossConnectivity.Current.IsConnected)
                {
                 
                    instagramCloneClient.DefaultRequestHeaders.Clear();
                    instagramCloneClient.DefaultRequestHeaders.Add("Accept", "application/json");
                    var response = await instagramCloneClient.DeleteAsync($"/photos/"+ photo_id);
                    statusCode.Status_Code = (int)response.StatusCode;
                    return statusCode;
                }
                else
                {
                    statusCode.Status_Code = 1;
                    return statusCode;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                string error = ex.Message;
                statusCode.Status_Code = 2;
                statusCode.Error_Message = error;
                return statusCode;
            }
        }
        public async Task<StatusCode> SaveEditedPhoto(Photo photo)
        {
            StatusCode statusCode = new StatusCode();
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    instagramCloneClient.DefaultRequestHeaders.Clear();
                    instagramCloneClient.DefaultRequestHeaders.Add("Accept", "application/json");
                    var postData = JsonConvert.SerializeObject(photo);   
                    var response = await instagramCloneClient.PutAsync($"/photos/" + photo.Id, new StringContent(postData, Encoding.UTF8, "application/json"));
                    statusCode.Status_Code = (int)response.StatusCode;
                    return statusCode;
                }
                else
                {
                    statusCode.Status_Code = 1;
                    return statusCode;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                string error = ex.Message;
                statusCode.Status_Code = 2;
                statusCode.Error_Message = error;
                return statusCode;
            }
        }
        public async Task<StatusCode> SaveNewPhoto(Photo photo)
        {
            StatusCode statusCode = new StatusCode();
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    instagramCloneClient.DefaultRequestHeaders.Clear();
                    instagramCloneClient.DefaultRequestHeaders.Add("Accept", "application/json");
                    var postData = JsonConvert.SerializeObject(photo);
                    var response = await instagramCloneClient.PostAsync($"/photos", new StringContent(postData, Encoding.UTF8, "application/json"));
                    statusCode.Status_Code = (int)response.StatusCode;
                    return statusCode;
                }
                else
                {
                    statusCode.Status_Code = 1;
                    return statusCode;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                string error = ex.Message;
                statusCode.Status_Code = 2;
                statusCode.Error_Message = error;
                return statusCode;
            }
        }
    }
}
