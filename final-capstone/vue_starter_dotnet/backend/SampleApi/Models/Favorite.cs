using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Models
{
    /// <summary>
    /// A model of a "favorite"
    /// </summary>
    public class Favorite
    {
        /// <summary>
        /// The id of the favorite
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// the id of the user who created the favorite
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// the id of the photo being favorited
        /// </summary>
        public int PhotoId { get; set; }
    }
}
