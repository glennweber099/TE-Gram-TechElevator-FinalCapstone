using SampleApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.DAL
{
    /// <summary>
    /// An interface associated with the Comments data
    /// </summary>
    public interface ICommentDAO
    {
        /// <summary>
        /// A method to get all comments on a particular photo
        /// </summary>
        /// <param name="photoId"></param>
        List<Comment> GetAllCommentsByPhotoId(int photoId);
        /// <summary>
        /// A method to get the most recent photo on a particular photo
        /// </summary>
        /// <param name="photoId"></param>
        Comment GetMostRecentCommentByPhotoId(int photoId);
        /// <summary>
        /// A method that allows a user to add a comment
        /// </summary>
        /// <param name="Comment"></param>
        void AddAComment(Comment Comment);
    }
}
