using System;
using System.Collections.Generic;
using System.Linq;
using Evaluation.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace Evaluation.Controllers
{
    [ApiController]
    [Route("api/user/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("All")]
        public ActionResult<List<User>> Get()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpGet("Single/{id}")]
        public ActionResult<User> GetSingle(int id)
        {
            return Ok(_userService.GetUserById(id));
        }

        [HttpPost("Add")]
        public ActionResult<List<User>> AddUser(User newUser)
        {
            return Ok(_userService.AddUser(newUser));
        }
        [HttpDelete("delete")]
        public ActionResult<List<User>> DeleteUser(int id)
        {
            return Ok(_userService.DeleteUser(id));
        }
        [HttpPut("edit")]
        public ActionResult<User> EditUser (User editedUser)
        {
            return Ok(_userService.EditUser(editedUser));
        }

    }
}