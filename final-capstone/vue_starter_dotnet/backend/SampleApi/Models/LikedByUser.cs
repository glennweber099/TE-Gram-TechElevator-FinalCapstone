using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Models
{
    /// <summary>
    /// Model used to change the "like" button
    /// </summary>
    public class LikedByUser
    {
        /// <summary>
        /// Returns true if the photo is liked by the specific user
        /// </summary>
        public bool Liked { get; set; }
        
        /// <summary>
        /// The id of the photo of the "like" button
        /// </summary>
        public int PhotoId { get; set; }
        
        /// <summary>
        /// Total number of likes
        /// </summary>
        public int TotalLikes { get; set;}
    }
}
