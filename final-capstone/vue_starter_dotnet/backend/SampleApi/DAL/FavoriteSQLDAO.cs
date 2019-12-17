using SampleApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.DAL
{
    public class FavoriteSQLDAO: IFavoriteSQLDAO
    {
        private readonly string connectionString;

        /// <summary>
        /// Creates a new sql dao for handling data related to favorites.
        /// </summary>
        /// <param name="connectionString">the database connection string</param>
        public FavoriteSQLDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }


        /// <summary>
        /// Allows a user with a given userId to add a photo with a given photoId to their favorites list, or delete a previously favorited photo from the list
        /// </summary>
        /// <param name="photoId"></param>
        /// <param name="userId"></param>
        public FavoritedByUser ToggleAFavorite(int photoId, int userId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                FavoritedByUser output = new FavoritedByUser();
                // Find out if photo is already favorited by user
                SqlCommand cmd = new SqlCommand("SELECT id FROM favorites WHERE photoId = @photoId AND userId = @userId", conn);
                cmd.Parameters.AddWithValue("@photoId", photoId);
                cmd.Parameters.AddWithValue("@userId", userId);

                if (cmd.ExecuteScalar() != null)
                {
                    cmd = new SqlCommand("DELETE favorites WHERE photoId = @photoId AND userId = @userId", conn);
                    cmd.Parameters.AddWithValue("@photoId", photoId);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd = new SqlCommand("INSERT favorites (photoId, userId) VALUES (@photoId, @userId)", conn);
                    cmd.Parameters.AddWithValue("@photoId", photoId);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    cmd.ExecuteNonQuery();
                }
                output.PhotoId = photoId;
                SqlCommand commd = new SqlCommand("SELECT* FROM likes WHERE photoId = @photoId and userId = @userId) THEN 1 ELSE 0 END", conn);
                commd.Parameters.AddWithValue("@photoId", photoId);
                commd.Parameters.AddWithValue("@userId", userId);
                output.Favorited = Convert.ToBoolean(commd.ExecuteScalar());

                return output;
            }
        }

        /// <summary>
        /// Allows a user with a given userId to view all photos in their favorites list
        /// </summary>
        /// <param name="userId"></param>
        public List<Photo> GetAllFavoritesByUser(int userId)
        {
            List<Photo> output = new List<Photo>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT photos.id, photos.caption, photos.dateAdded, photos.isVisible, photos.userId, photos.imageUrl FROM photos JOIN favorites ON favorites.photoId = photos.Id WHERE favorites.userId = @userId ORDER BY dateAdded DESC", conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        {
                            Photo photo = new Photo();

                            photo.Id = (Convert.ToInt32(reader["photoId"]));
                            photo.Caption = (Convert.ToString(reader["caption"]));
                            photo.DateAdded = (Convert.ToDateTime(reader["dateAdded"]));
                            photo.IsVisible = (Convert.ToBoolean(reader["isVisible"]));
                            photo.ImageUrl = (Convert.ToString(reader["imageUrl"]));
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
    }
}
