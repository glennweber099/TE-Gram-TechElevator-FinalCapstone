using SampleApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.DAL
{
    /// <summary>
    /// A SQL DAO for Photo objects
    /// </summary>
    public class PhotoSQLDAO: IPhotoDAO
    {
        private readonly string connectionString;

        /// <summary>
        /// Creates a new sql dao for user objects.
        /// </summary>
        /// <param name="connectionString">the database connection string</param>
        public PhotoSQLDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }
        /// <summary>
        /// allows a user to upload a photo
        /// </summary>
        /// <param name="photo"></param>
        public void UploadPhoto(Photo photo)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand(@"insert into photos(caption, userId, imgUrl) VALUES(@caption, @userId, @imgUrl)", conn);
                    command.Parameters.AddWithValue("@caption", photo.Caption);
                    command.Parameters.AddWithValue("@userId", photo.UserId);
                    command.Parameters.AddWithValue("@imgUrl", photo.ImgUrl);

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }
        /// <summary>
        /// allows a user to delete a photo they have uploaded
        /// </summary>
        /// <param name="photo"></param>
        public void DeletePhoto(Photo photo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand(@"UPDATE photos SET isVisible = 0 WHERE id = @id", conn);
                    command.Parameters.AddWithValue("@id", photo.Id);

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }
        /// <summary>
        /// returns a list of photos, starting with the most recent
        /// </summary>
        /// <returns></returns>
        public List<Photo> GetPhotosByRecent()
        {
            return null;
        }
        /// <summary>
        /// returns a list of all photos that have been uploaded by a given user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Photo> GetPhotosByUser(int userId)
        {
            return null;
        }

        // TBD if we want to show a photo detail page that scrolls when a single photo is selected OR just the photo that was selected
        public Photo GetPhoto(int photoId)
        {
            return null;
        }

    }
}
