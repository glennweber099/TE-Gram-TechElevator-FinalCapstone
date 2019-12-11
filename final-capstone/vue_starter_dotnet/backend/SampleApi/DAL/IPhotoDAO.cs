using SampleApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.DAL
{
    public interface IPhotoDAO
    {
        void UploadPhoto(Photo photo);
        void DeletePhoto(Photo photo);
        List<Photo> GetPhotosByRecent();
        List<Photo> GetPhotosByUser(int userId);
        Photo GetPhoto(int photoId);
    }
}
