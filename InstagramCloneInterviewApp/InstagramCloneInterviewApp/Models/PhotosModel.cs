using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramCloneInterviewApp.Models
{
   public class PhotosModel: StatusCode
    {
        public IList<Photo> Photos { get; set; }
    }
}
