using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Evaluation.Data;
using Evaluation.Dtos.User;
using Microsoft.EntityFrameworkCore;

namespace Evaluation.Services.UserService
{
  public class UserService : IUserService
  {
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public UserService(IMapper mapper, DataContext context)
    {
      _mapper = mapper;
      _context = context;
    }
    public async Task<ServiceResponse<List<GetUserDto>>> AddUser(AddUserDto newUser)
    {
      var serviceResponse = new ServiceResponse<List<GetUserDto>>();
      User user = _mapper.Map<User>(newUser);
      _context.Users.Add(user);
      await _context.SaveChangesAsync();
      serviceResponse.Data = await _context.Users.Select(u => _mapper.Map<GetUserDto>(u)).ToListAsync();
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id)
    {
      var serviceResponse = new ServiceResponse<List<GetUserDto>>();
      try
      {
        var usuarioDelete = await _context.Users.FirstAsync(u => u.UserId == id);
        _context.Users.Remove(usuarioDelete);
        await _context.SaveChangesAsync();
        serviceResponse.Data = _context.Users.Select(u => _mapper.Map<GetUserDto>(u)).ToList();
      }
      catch (Exception ex)
      {
        serviceResponse.Succes = false;
        serviceResponse.Message = ex.Message;
      }
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetUserDto>> EditUser(EditUserDto editedUser)
    {
      var serviceResponse = new ServiceResponse<GetUserDto>();
      try
      {
        var editedUsuario = await _context.Users.FirstOrDefaultAsync(u => u.UserId == editedUser.UserId);

        editedUsuario.Name = editedUser.Name;
        editedUsuario.Email = editedUser.Email;
        editedUsuario.Phone = editedUser.Phone;
        editedUsuario.Type = editedUser.Type;

        await _context.SaveChangesAsync();

        serviceResponse.Data = _mapper.Map<GetUserDto>(editedUsuario);
      }
      catch (Exception ex)
      {
        serviceResponse.Succes = false;
        serviceResponse.Message = ex.Message;
      }
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetUserDto>>> GetAllUsers()
    {
      var serviceResponse = new ServiceResponse<List<GetUserDto>>();
      var dbUsers = await _context.Users.ToListAsync();
      serviceResponse.Data = dbUsers.Select(u => _mapper.Map<GetUserDto>(u)).ToList();
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetUserDto>> GetUserById(int id)
    {
      var serviceResponse = new ServiceResponse<GetUserDto>();
      var dbUsers = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
      serviceResponse.Data = _mapper.Map<GetUserDto>(dbUsers);
      return serviceResponse;
    }

  }
}