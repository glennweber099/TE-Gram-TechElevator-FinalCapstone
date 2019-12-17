using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleApi.DAL;
using SampleApi.Models;

namespace SampleApi.Controllers
{
    /// <summary>
    /// Controls actions related to favorites
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : Controller
    {
        private IFavoriteDAO favoriteDAO;
        private IUserDAO userDAO;

        /// <summary>
        /// Creates a new controller to handle favorites
        /// </summary>
        /// <param name="favoriteDAO"></param>
        public FavoriteController(IFavoriteDAO favoriteDAO, IUserDAO userDAO)
        {
            this.favoriteDAO = favoriteDAO;
            this.userDAO = userDAO;
        }

        /// <summary>
        /// Allows a user to add a photo to their favorites list, or remove a photo they had previously favorited from the list
        /// </summary>
        /// <param name="fav"></param>
        /// <returns></returns>
        [HttpPost("togglefavorite")]
        public IActionResult ToggleFavorite(Favorite fav)
        {
            User user = userDAO.GetUser(User.Identity.Name);
            fav.UserId = user.Id;
            FavoritedByUser output = favoriteDAO.ToggleFavorite(fav.PhotoId, fav.UserId);
            return Ok(output);
        }

        /// <summary>
        /// Retrieves a list of all photos that have been favorited by a given user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("getallfavoritesbyuser")]
        public IActionResult GetAllFavoritesByUser(User user)
        {
            User user2 = userDAO.GetUser(User.Identity.Name);
            user2.Id = user.Id;
            List<Photo> favPhotos = favoriteDAO.GetAllFavoritesByUser(user.Id);
            return Ok(favPhotos);
        }
    }
}