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

                    SqlCommand cmd = new SqlCommand("SELECT count(likes.id) as 'Total Likes', users.id  as 'userId', users.username, photos.caption, photos.dateAdded, photos.id as 'photoId', photos.imageUrl, photos.isVisible FROM photos join users on photos.userId = users.id left join likes on likes.photoId = photos.id where isVisible = 1 group by likes.photoId, users.id, users.username, photos.caption, photos.dateAdded, photos.id, photos.imageUrl, photos.isVisible ORDER BY dateAdded DESC", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        {
                            Photo photo = new Photo();

                            photo.Id = (Convert.ToInt32(reader["photoId"]));
                            photo.Caption = (Convert.ToString(reader["caption"]));
                            photo.UserId = (Convert.ToInt32(reader["userId"]));
                            photo.PhotoOwner = (Convert.ToString(reader["username"]));
                            photo.ImageUrl = (Convert.ToString(reader["imageUrl"]));
                            photo.DateAdded = (Convert.ToDateTime(reader["dateAdded"]));
                            photo.IsVisible = (Convert.ToBoolean(reader["isVisible"]));
                            photo.totalLikes = (Convert.ToInt32(reader["Total Likes"]));
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

        public List<Photo> GetPhotosByUser(int userId)
        {
            List<Photo> output = new List<Photo>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT count(likes.id) as 'Total Likes', photos.caption, photos.dateAdded, photos.id as 'photoId', photos.imageUrl, photos.isVisible FROM photos join users on photos.userId = users.id left join likes on likes.photoId = photos.id where isVisible = 1 AND photos.userId = @userId group by likes.photoId, users.id, users.username, photos.caption, photos.dateAdded, photos.id, photos.imageUrl, photos.isVisible ORDER BY dateAdded DESC", conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        {
                            Photo photo = new Photo();

                            photo.Id = (Convert.ToInt32(reader["photoId"]));
                            photo.Caption = (Convert.ToString(reader["caption"]));
                            photo.ImageUrl = (Convert.ToString(reader["imageUrl"]));
                            photo.DateAdded = (Convert.ToDateTime(reader["dateAdded"]));
                            photo.IsVisible = (Convert.ToBoolean(reader["isVisible"]));
                            photo.totalLikes = (Convert.ToInt32(reader["Total Likes"]));
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
        public DeepPhoto GetDeepPhotoById(int id)
        {
            DeepPhoto deepPhoto = new DeepPhoto();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from photos where Id = @id select * from comments where photoId = @id select count(*) as 'Total Likes' from likes where likes.photoId = @id select username from users join photos on photos.userId = users.id where photos.id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())  // THere will be zero or one
                    {
                        // Create the deepPhoto object
                        deepPhoto.Id = (Convert.ToInt32(reader["id"]));
                        deepPhoto.Caption = (Convert.ToString(reader["caption"]));
                        deepPhoto.UserId = (Convert.ToInt32(reader["userId"]));
                        deepPhoto.ImageUrl = (Convert.ToString(reader["imageUrl"]));
                        deepPhoto.DateAdded = (Convert.ToDateTime(reader["dateAdded"]));
                        deepPhoto.IsVisible = (Convert.ToBoolean(reader["isVisible"]));
                    }
                    else
                    {
                        // Not found - return NULL
                        return deepPhoto;
                    }

                    /***
                     * Get the comments. reader.NextResult moves us to the second rowset returned 
                     * from our query (the comments)
                     * ***/
                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            Comment comment = new Comment();
                            {
                                comment.CommentString = Convert.ToString(reader["comment"]);
                                comment.Id = (Convert.ToInt32(reader["id"]));
                                comment.PhotoId = (Convert.ToInt32(reader["photoId"]));
                                comment.CommenterId = Convert.ToInt32(reader["commenterId"]);
                                comment.DateCommented = Convert.ToDateTime(reader["dateCommented"]);
                            };
                            deepPhoto.AllComments.Add(comment);
                        }
                    }
                    /***
                     * Get the comments. reader.NextResult moves us to the second rowset returned 
                     * from our query (the likes)
                     * ***/
                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                                deepPhoto.totalLikes = (Convert.ToInt32(reader["Total Likes"]));
                        }
                    }
                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            deepPhoto.PhotoOwner = (Convert.ToString(reader["username"]));
                        }
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return deepPhoto;
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
