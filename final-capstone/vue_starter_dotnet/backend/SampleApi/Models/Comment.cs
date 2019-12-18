using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Models
{
    /// <summary>
    /// Models a comment
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// The actual comment that a site user has inputted
        /// </summary>
        public string CommentString { get; set; }
        /// <summary>
        /// The comment's id in the database
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The id of the photo the comment is associated with
        /// </summary>
        public int PhotoId { get; set; }
        /// <summary>
        /// Name of person who commented on the photo
        /// </summary>
        public string CommenterName { get; set; }
        /// <summary>
        /// The id of the user who left the comment
        /// </summary>
        public int CommenterId { get; set; }
        /// <summary>
        /// The date and time when the comment was left
        /// </summary>
        public DateTime DateCommented { get; set; }
    }
}
