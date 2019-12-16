using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleApi.DAL;
using SampleApi.Models;

namespace SampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : Controller
    {
        private IUserDAO userDAO;
        private ILikeDAO likeDAO;

        /// <summary>
        /// A controller for likes
        /// </summary>
        /// <param name="likeDAO"></param>
        public LikeController(ILikeDAO likeDAO, IUserDAO userDAO)
        {
            this.likeDAO = likeDAO;
            this.userDAO = userDAO;

        }

        [HttpPost("togglelike")]
        public IActionResult ToggleLike(Like like)
        {
            User user = userDAO.GetUser(User.Identity.Name);
            like.UserId = user.Id;
            int totalLikes = likeDAO.ToggleLike(like.PhotoId, like.UserId);
            return Ok(totalLikes);
        }
    }
}