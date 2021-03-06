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
    /// <summary>
    /// Creates a new account controller used to authenticate the user.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private ITokenGenerator tokenGenerator;
        private IPasswordHasher passwordHasher;
        private IUserDAO userDao;

        /// <summary>
        /// Creates a new account controller.
        /// </summary>
        /// <param name="tokenGenerator">A token generator used when creating auth tokens.</param>
        /// <param name="passwordHasher">A password hasher used when hashing passwords.</param>
        /// <param name="userDao">A data access object to store user data.</param>
        public AccountController(ITokenGenerator tokenGenerator, IPasswordHasher passwordHasher, IUserDAO userDao)
        {
            this.tokenGenerator = tokenGenerator;
            this.passwordHasher = passwordHasher;
            this.userDao = userDao;
        }

        /// <summary>
        /// Registers a user and provides a bearer token.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public IActionResult Register(User model)
        {            
            // Does user already exist
            if (userDao.GetUser(model.Username) != null)
            {
                return BadRequest(new
                {
                    Message = "Username has already been taken."
                });
            }

            // Generate a password hash
            var passwordHash = passwordHasher.ComputeHash(model.Password);

            // Create a user object
            var user = new User { Password = passwordHash.Password, Salt = passwordHash.Salt, Role = "User", Username = model.Username, Email = model.Email};

            // Save the user
            userDao.CreateUser(user);

            // Generate a token
            var token = tokenGenerator.GenerateToken(user.Username, user.Role);

            // Return the token
            return Ok(token);
        }

        /// <summary>
        /// allow a user to update their username
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("update-username")]
        public IActionResult UpdateUsername(User user)
        {
            
            if (userDao.GetUser(user.Username) != null)
            {
                return BadRequest(new
                {
                    Message = "Username has already been taken."
                });
            }
            else
            {                                
                userDao.UpdateUsername(user);
                
                return Ok(new {
                    Message = "Username updated successfully!"
                });
            }
        }

        /// <summary>
        /// Allows the user to update their password
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("update-password")]
        public IActionResult UpdatePassword(User user)
        {
            var passwordHash = passwordHasher.ComputeHash(user.Password);
            User updatedUser = new User { Password = passwordHash.Password, Salt = passwordHash.Salt, Role = "User", Username = user.Username, Email = user.Email, Id = user.Id };
            userDao.UpdatePassword(updatedUser);

            return Ok(new {
                Message = "Password updated successfully!"
            });
        }


        /// <summary>
        /// Authenticates the user and provides a bearer token.
        /// </summary>
        /// <param name="model">An object including the user's credentials.</param> 
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Login(AuthenticationModel model)
        {
            // Assume the user is not authorized
            IActionResult result = Unauthorized();

            // Get the user by username
            var user = userDao.GetUser(model.Username);

            // If we found a user and the password has matches
            if (user != null && passwordHasher.VerifyHashMatch(user.Password, model.Password, user.Salt))
            {
                // Create an authentication token
                var token = tokenGenerator.GenerateToken(user.Username, user.Role);

                // Switch to 200 OK
                result = Ok(token);
            }

            return result;
        }

        /// <summary>
        /// Allows a user to "deactivate" their account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("delete")]
        public IActionResult DeactivateAccount(AuthenticationModel model)
        {
            // Assume the user is not authorized
            IActionResult result = Unauthorized();

            // Get the user by username
            var user = userDao.GetUser(model.Username);

            // If we found a user and the password has matches
            if (user != null && passwordHasher.VerifyHashMatch(user.Password, model.Password, user.Salt))
            {
                userDao.DeleteUser(user);
                // Generate a token
                var token = tokenGenerator.GenerateToken(user.Username, user.Role);

                // Return the token
                result = Ok(token);
            }
            return result;
        }
    }
}