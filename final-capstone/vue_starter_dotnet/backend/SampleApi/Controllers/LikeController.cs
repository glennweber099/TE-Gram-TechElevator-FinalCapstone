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
        private ILikeDAO likeDAO;

        public LikeController(ILikeDAO likeDAO)
        {
            this.likeDAO = likeDAO;
        }

        [HttpPost("togglelike")]
        public IActionResult ToggleLike(Like like)
        {
            likeDAO.ToggleLike(like.PhotoId, like.UserId);
            return Ok();
        }
    }
}