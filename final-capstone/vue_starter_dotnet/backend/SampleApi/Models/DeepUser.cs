using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Models
{
    public class DeepUser: User
    {
        public List<Photo> FavoritedPhotos { get; set; }
        public DeepUser()
        {
            this.FavoritedPhotos = new List<Photo>();
        }
    }
}
