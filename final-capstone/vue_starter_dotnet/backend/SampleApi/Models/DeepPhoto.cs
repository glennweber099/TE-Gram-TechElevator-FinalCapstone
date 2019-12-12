using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Models
{
    /// <summary>
    /// Models a photo with additional properties not included in the main "photo" class, specifically a list of likes and a list of comments
    /// </summary>
    public class DeepPhoto: Photo
    {
        public int totalLikes { get; set; }
        public List<Comment> AllComments { get; set; }
        public List<Like> AllLikes { get; set; }

        public DeepPhoto()
        {
            this.AllComments = new List<Comment>();
        }
    }
}
