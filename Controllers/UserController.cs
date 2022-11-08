using System;
using System.Collections.Generic;
using System.Linq;
using Evaluation.Dtos.User;
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
    public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> Get()
    {
      return Ok(await _userService.GetAllUsers());
    }

    [HttpGet("Single/{id}")]
    public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetSingle(int id)
    {
      return Ok(await _userService.GetUserById(id));
    }

    [HttpPost("Add")]
    public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> AddUser(AddUserDto newUser)
    {
      return Ok(await _userService.AddUser(newUser));
    }
    [HttpDelete("delete")]
    public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> DeleteUser(int id)
    {
      var response = await _userService.DeleteUser(id);
      if (response.Data == null)
      {
        return NotFound(response);
      }
      return Ok(response);
    }
    [HttpPut("edit")]
    public async Task<ActionResult<ServiceResponse<GetUserDto>>> EditUser(EditUserDto editedUser)
    {
      var response = await _userService.EditUser(editedUser);
      if (response.Data == null)
      {
        return NotFound(response);
      }
      return Ok(response);
    }

  }
}