using SampleApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.DAL
{
    /// <summary>
    /// The DAO interface for working with data related to Photos
    /// </summary>
    public interface IPhotoDAO
    {
        /// <summary>
        /// Allows a user to send the data of a newly uploaded photo to the database
        /// </summary>
        /// <param name="photo"></param>
        void UploadPhoto(Photo photo);
        /// <summary>
        /// Allows a user to set "is visible" (isVisible) to false (0) for a photo they had previously uploaded to the database
        /// </summary>
        /// <param name="photo"></param>
        void DeletePhoto(Photo photo);
        /// <summary>
        /// Retrieves data for all photos from the database, ordering by most recently uploaded first
        /// </summary>
        /// <returns></returns>
        List<Photo> GetPhotosByRecent(User user);

        /// <summary>
        /// Get all photo details by Id
        /// </summary>
        DeepPhoto GetDeepPhotoById(int id);
        /// <summary>
        /// Queries the database and retrieves all photos that have been uploaded by the given user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<Photo> GetPhotosByUser(int userId);
    }
}
