using System;
using System.Collections.Generic;
using System.Linq;
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
    [Route("api/[controller]")]
    [ApiController]
    public class CameraController : Controller
    {
        public IPhotoDAO photoDAO;
        public IUserDAO userDAO;

        public CameraController(IPhotoDAO dao, IUserDAO udao)
        {
            this.photoDAO = dao;
            this.userDAO = udao;
        }
        [HttpPost]
        public IActionResult Upload(Photo photo)
        {
            User user = userDAO.GetUser(User.Identity.Name);
            photo.UserId = user.Id;
            photoDAO.UploadPhoto(photo);
            return Ok();
        }
    }
}