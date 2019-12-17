using SampleApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.DAL
{
    /// <summary>
    /// An interface to handle data related to Favorites
    /// </summary>
    public interface IFavoriteSQLDAO
    {

        /// <summary>
        /// Allows a user with a given userId to add a photo with a given photoId to their favorites list, or delete a previously favorited photo from the list
        /// </summary>
        /// <param name="photoId"></param>
        /// <param name="userId"></param>
        FavoritedByUser ToggleAFavorite(int photoId, int userId);

        /// <summary>
        /// Allows a user with a given userId to view all photos in their favorites list
        /// </summary>
        /// <param name="userId"></param>
        List<Photo> GetAllFavoritesByUser(int userId);
    }
}
