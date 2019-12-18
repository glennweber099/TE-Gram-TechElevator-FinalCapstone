using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleApi.DAL;
using SampleApi.Models;
using SampleApi.Models.Account;
using SampleApi.Providers.Security;

namespace SampleApi.Controllers
{
    /// <summary>
    /// Controls actions related to photos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : Controller
    {
        private IPhotoDAO photoDAO;
        private IUserDAO userDAO;
        private ICommentDAO commentDAO;

        /// <summary>
        /// 
        /// Takes in the 2 DAOs to use in the Controller
        /// </summary>
        /// <param name="dao"></param>
        /// <param name="udao"></param>
        public PhotoController(IPhotoDAO dao, IUserDAO udao, ICommentDAO cdao)
        {
            this.photoDAO = dao;
            this.userDAO = udao;
            this.commentDAO = cdao;
        }

        /// <summary>
        /// Displays all photos sorted by upload date to home page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            User user = null;
            if (User.Identity.Name != null)
            {
                user = userDAO.GetUser(User.Identity.Name);
            }

            List<Photo> photos = photoDAO.GetPhotosByRecent(user);
            return Ok(photos);
        }
        /// <summary>
        /// Displays all photos that have been uploaded by a given user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("search/{userId}")]
        public IActionResult GetPhotosByUser(int userId)
        {
            List<Photo> photos = photoDAO.GetPhotosByUser(userId);
            return Ok(photos);
        }

        /// <summary>
        /// Allows user to upload a new photo
        /// </summary>
        /// <param name="photo"></param>
        /// <returns></returns>
        [HttpPost("upload")]
        public IActionResult Upload(Photo photo)
        {
            User user = userDAO.GetUser(User.Identity.Name);
            photo.UserId = user.Id;
            photoDAO.UploadPhoto(photo);
            return Ok();
        }
        /// <summary>
        /// Allows the user to delete their own photos
        /// </summary>
        /// <param name="photo"></param>
        /// <returns></returns>
        [HttpPut("delete-photo")]
        public IActionResult Delete(Photo photo)
        {
            //Verifies username to confirm person deleting photo is the photo owner
           User user = userDAO.GetUser(User.Identity.Name);
            if (photo.UserId != user.Id)
            {
                return Unauthorized(new
                {
                    message = "You do not have permission to remove this photo."
                });
            }
            photoDAO.DeletePhoto(photo);
            return Ok(new {
                message = "Photo removed."
            });
        }

        /// <summary>
        /// Get's all the details and comments of a photo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("detail/{id}")]
        public IActionResult GetPhotoById(int id, int userId)
        {
            User user = userDAO.GetUser(User.Identity.Name);
            userId = user.Id;
            DeepPhoto photo = photoDAO.GetDeepPhotoById(id, userId);
            return Ok(photo);
        }
        /// <summary>
        /// Add a comment to a photo
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="photoId"></param>
        /// <returns></returns>
        [HttpPost("comment/{photoId}")]
        public IActionResult AddAComment(Comment comment, int photoId)
        {
            User user = userDAO.GetUser(User.Identity.Name);
            comment.CommenterId = user.Id;
            commentDAO.AddAComment(comment.CommentString, photoId, comment.CommenterId);
            return Ok();
        }
    }
}