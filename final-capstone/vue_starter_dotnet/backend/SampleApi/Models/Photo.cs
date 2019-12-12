using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Models
{
    /// <summary>
    /// Represents a photo in the system.
    /// </summary>
    public class Photo
    {
        /// <summary>
        /// the photo's id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// the photo's caption
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// the id of the user who uploaded the photo
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// the name of the user who uploaded the photo
        /// </summary>
        public string PhotoOwner { get; set; }

        /// <summary>
        /// the url of the photo
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// when the photo was uploaded
        /// </summary>
        public DateTime DateAdded { get; set; }

        /// <summary>
        /// tells us if the photo is visible (and it won't be if it was "deleted")
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// gives a count of the total likes associated with the photo
        /// </summary>
        public int totalLikes { get; set; }
    }
}
