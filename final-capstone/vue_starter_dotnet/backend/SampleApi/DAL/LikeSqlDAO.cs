﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.DAL
{
    public class LikeSqlDAO : ILikeDAO
    {
        private readonly string connectionString;

        public LikeSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void ToggleLike(int photoId, int userId)
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
            }
        }
    }
}