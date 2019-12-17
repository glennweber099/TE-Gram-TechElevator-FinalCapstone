using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Models
{
    /// <summary>
    /// Model used to change the "favorite" button
    /// </summary>
    public class FavoritedByUser
    {
        /// <summary>
        /// Returns true if the photo is favorited by the specific user
        /// </summary>
        public bool Favorited { get; set; }

        /// <summary>
        /// The id of the photo of the "like" button
        /// </summary>
        public int PhotoId { get; set; }
    }
}
