using SampleApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.DAL
{
    /// <summary>
    /// Queries the database for data related to comments
    /// </summary>
    public class CommentSQLDAO: ICommentDAO
    {
        private readonly string connectionString;

        /// <summary>
        /// Creates a new sql dao for comment objects.
        /// </summary>
        /// <param name="connectionString">the database connection string</param>
        public CommentSQLDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }
        /// <summary>
        /// A method to get all comments on a particular photo
        /// </summary>
        /// <param name="photoId"></param>
        public List<Comment> GetAllCommentsByPhotoId(int photoId)
        {
            List<Comment> output = new List<Comment>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM comments WHERE photoId = @photoId ORDER BY dateAdded DESC ", conn);
                    cmd.Parameters.AddWithValue("@photoId", photoId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        {
                            Comment comment = new Comment();

                            comment.CommentString = Convert.ToString(reader["comment"]);
                            comment.Id = (Convert.ToInt32(reader["Id"]));
                            comment.PhotoId = (Convert.ToInt32(reader["photoId"]));
                            comment.CommenterId = (Convert.ToInt32(reader["commenterId"]));
                            comment.DateCommented = (Convert.ToDateTime(reader["dateCommented"]));
                            output.Add(comment);
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
        /// A method to get the most recent comment on a particular photo
        /// </summary>
        /// <param name="photoId"></param>
        public Comment GetMostRecentCommentByPhotoId(int photoId)
        {
            Comment topComment = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM comments WHERE photoId = @photoId ORDER BY dateAdded DESC ", conn);
                    cmd.Parameters.AddWithValue("@photoId", photoId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        {
                            Comment comment = new Comment();

                            comment.CommentString = Convert.ToString(reader["comment"]);
                            comment.Id = (Convert.ToInt32(reader["Id"]));
                            comment.PhotoId = (Convert.ToInt32(reader["photoId"]));
                            comment.CommenterId = (Convert.ToInt32(reader["commenterId"]));
                            comment.DateCommented = (Convert.ToDateTime(reader["dateCommented"]));

                            topComment = comment;
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
        /// <summary>
        /// A method that allows a user to add a comment to a photo
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="photoId"></param>
        public void AddAComment(string comment, int photoId, int commenterId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("insert into comments (comment, photoId, commenterId) Values(@comment, @photoId, @commenterId)", conn);
                    command.Parameters.AddWithValue("@comment", comment);
                    command.Parameters.AddWithValue("@photoId", photoId);
                    command.Parameters.AddWithValue("@commenterId", commenterId);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }
    }
}
