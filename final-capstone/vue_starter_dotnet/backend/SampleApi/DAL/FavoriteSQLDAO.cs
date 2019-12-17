using SampleApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.DAL
{
    public class FavoriteSQLDAO: IFavoriteDAO
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
        public FavoritedByUser ToggleFavorite(int photoId, int userId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                FavoritedByUser output = new FavoritedByUser();
                // Find out if photo is already favorited by user
                SqlCommand AddOrDeleteFavorites = new SqlCommand("SELECT * FROM favorites WHERE photoId = @photoId AND userId = @userId", conn);
                AddOrDeleteFavorites.Parameters.AddWithValue("@photoId", photoId);
                AddOrDeleteFavorites.Parameters.AddWithValue("@userId", userId);

                if (AddOrDeleteFavorites.ExecuteScalar() != null)
                {
                    AddOrDeleteFavorites = new SqlCommand("DELETE favorites WHERE photoId = @photoId AND userId = @userId", conn);
                    AddOrDeleteFavorites.Parameters.AddWithValue("@photoId", photoId);
                    AddOrDeleteFavorites.Parameters.AddWithValue("@userId", userId);

                    AddOrDeleteFavorites.ExecuteNonQuery();
                }
                else
                {
                    AddOrDeleteFavorites = new SqlCommand("INSERT favorites (photoId, userId) VALUES (@photoId, @userId)", conn);
                    AddOrDeleteFavorites.Parameters.AddWithValue("@photoId", photoId);
                    AddOrDeleteFavorites.Parameters.AddWithValue("@userId", userId);

                    AddOrDeleteFavorites.ExecuteNonQuery();
                }
                output.PhotoId = photoId;

                SqlCommand Favorited = new SqlCommand("SELECT * FROM favorites WHERE photoId = @photoId and userId = @userId", conn);
                Favorited.Parameters.AddWithValue("@photoId", photoId);
                Favorited.Parameters.AddWithValue("@userId", userId);
                output.Favorited = Convert.ToBoolean(Favorited.ExecuteScalar());

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

                    SqlCommand cmd = new SqlCommand(@"SELECT count(likes.id) as 'Total Likes', users.id  as 'userId', users.username, photos.caption, photos.dateAdded, photos.id as 'photoId', photos.imageUrl, photos.isVisible, isLikedByUser = CASE WHEN EXISTS(SELECT * FROM likes WHERE photoId = photos.id and userId = @userId) THEN 1 ELSE 0 END, isFavoritedByUser = CASE WHEN EXISTS(SELECT * FROM favorites WHERE photoId = photos.id and userId = @userId) THEN 1 ELSE 0 END
                                                        FROM photos
                                                        join users on photos.userId = users.id
                                                        left join likes on likes.photoId = photos.id           
                                                        where isVisible = 1
                                                        group by likes.photoId, users.id, users.username, photos.caption, photos.dateAdded, photos.id, photos.imageUrl, photos.isVisible
                                                        ORDER BY dateAdded DESC", conn);
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
