﻿using System;
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
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : Controller
    {
        private IPhotoDAO photoDAO;
        private IUserDAO userDAO;

        public PhotoController(IPhotoDAO dao, IUserDAO udao)
        {
            this.photoDAO = dao;
            this.userDAO = udao;
        }

        /// <summary>
        /// Displays all photos sorted by upload date to home page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
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
        [HttpPut("delete")]
        public IActionResult Delete(Photo photo)
        {
            // Verifies username to confirm person deleting photo is the photo owner
            User user = userDAO.GetUser(User.Identity.Name);
            if(photo.UserId != user.Id)
            {
                return Unauthorized();
            }
            photoDAO.DeletePhoto(photo);
            return NoContent();
        }
    }
}