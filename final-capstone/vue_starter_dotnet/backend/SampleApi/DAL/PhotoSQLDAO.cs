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
                    int userId = photo.UserId;
                    SqlCommand command = new SqlCommand("insert into photos(caption, userId, imageUrl) VALUES(@caption, @userId, @imageUrl)", conn);
                    command.Parameters.AddWithValue("@caption", photo.Caption);
                    command.Parameters.AddWithValue("@userId", photo.UserId);
                    command.Parameters.AddWithValue("@imageUrl", photo.ImageUrl);
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

                    SqlCommand command = new SqlCommand("UPDATE photos SET isVisible = 0 WHERE id = @id", conn);
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
            List<Photo> output = new List<Photo>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT users.id  as 'userId', users.username, photos.caption, photos.dateAdded, photos.id as 'photoId', photos.imageUrl, photos.isVisible FROM photos join users on photos.userId = users.id where isVisible = 1 ORDER BY dateAdded DESC ", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        {
                            Photo photo = new Photo();

                            photo.photoOwner = Convert.ToString(reader["username"]);
                            photo.Id = (Convert.ToInt32(reader["photoId"]));
                            photo.Caption = (Convert.ToString(reader["caption"]));
                            photo.UserId = (Convert.ToInt32(reader["userId"]));
                            photo.ImageUrl = (Convert.ToString(reader["imageUrl"]));
                            photo.DateAdded = (Convert.ToDateTime(reader["dateAdded"]));
                            photo.IsVisible = (Convert.ToBoolean(reader["isVisible"]));
                            output.Add(photo);
                        };
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return output;
        }

        /// <summary>
        /// This gets a photo with a given ID as well as data about the user that submitted the photo, and all comments and likes associated with that photo
        /// </summary>
        /// <param name="photo"></param>
        /// <returns></returns>
        public DeepPhoto GetDeepPhotoById(Photo photo)
        {
            DeepPhoto deepPhoto = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from photos where photoId = @id select * from comments where photoId = @id select * from likes where photoId = @id", conn);
                    cmd.Parameters.AddWithValue("@id", photo.Id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        {
                            DeepPhoto newPhoto = new DeepPhoto();
                            List<Comment> comments = new List<Comment>();
                            List<Like> likes = new List<Like>();

                            newPhoto.photoOwner = Convert.ToString(reader["username"]);
                            newPhoto.Id = (Convert.ToInt32(reader["photoId"]));
                            newPhoto.Caption = (Convert.ToString(reader["caption"]));
                            newPhoto.UserId = (Convert.ToInt32(reader["userId"]));
                            newPhoto.ImageUrl = (Convert.ToString(reader["imageUrl"]));
                            newPhoto.DateAdded = (Convert.ToDateTime(reader["dateAdded"]));
                            newPhoto.IsVisible = (Convert.ToBoolean(reader["isVisible"]));

                            

                            deepPhoto = newPhoto;

                            
                        };
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return topComment;
        }

        //public List<Photo> GetPhotosByUser(int userId)
        //{
        //    return null;
        //}

        //// TBD if we want to show a photo detail page that scrolls when a single photo is selected OR just the photo that was selected

        //public Photo GetPhoto(int photoId)
        //{
        //    return null;
        //}
    }
}
