using SampleApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.DAL
{
    /// <summary>
    /// Queries the database for likes
    /// </summary>
    public class LikeSqlDAO : ILikeDAO
    {
        private readonly string connectionString;
        /// <summary>
        /// A way to query the database for data relating to "likes"
        /// </summary>
        /// <param name="connectionString"></param>
        public LikeSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Allows a site user to add a "like" to a given photo, or remove a like they have previously added
        /// </summary>
        /// <param name="photoId"></param>
        /// <param name="userId"></param>
        public int ToggleLike(int photoId, int userId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Find out if photo is already liked by user
                SqlCommand cmd = new SqlCommand("SELECT id FROM likes WHERE photoId = @photoId AND userId = @userId", conn);
                cmd.Parameters.AddWithValue("@photoId", photoId);
                cmd.Parameters.AddWithValue("@userId", userId);

                if (cmd.ExecuteScalar() != null)
                {
                    cmd = new SqlCommand("DELETE likes WHERE photoId = @photoId AND userId = @userId", conn);
                    cmd.Parameters.AddWithValue("@photoId", photoId);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd = new SqlCommand("INSERT likes (photoId, userId) VALUES (@photoId, @userId)", conn);
                    cmd.Parameters.AddWithValue("@photoId", photoId);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    cmd.ExecuteNonQuery();
                }

                // Get the number of likes on the photo
                cmd = new SqlCommand("SELECT COUNT(*) FROM likes WHERE photoId = @photoId", conn);
                cmd.Parameters.AddWithValue("@photoId", photoId);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        
    }
}
