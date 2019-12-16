using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.DAL
{
    /// <summary>
    /// An interface for likes
    /// </summary>
    public interface ILikeDAO
    {
        /// <summary>
        /// A method to toggle between whether a photo with a given Id is "liked" or not
        /// </summary>
        /// <param name="photoId"></param>
        /// <param name="userId"></param>
        int ToggleLike(int photoId, int userId);

    }
}
