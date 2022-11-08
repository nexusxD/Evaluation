using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Evaluation.Dtos.User;

namespace Evaluation.Services.UserService
{
  public class UserService : IUserService
  {
    private static List<User> usuarios = new List<User>
        {
            new User(),
            new User{ UserId=1, Name="Pata", Email="pata@patamail.com", Phone=9876543210, Type=UserType.Vendedor}
        };
    private readonly IMapper _mapper;

    public UserService(IMapper mapper)
    {
      _mapper = mapper;
    }
    public async Task<ServiceResponse<List<GetUserDto>>> AddUser(AddUserDto newUser)
    {
      var serviceResponse = new ServiceResponse<List<GetUserDto>>();
      User user = _mapper.Map<User>(newUser);
      user.UserId = usuarios.Max(u => u.UserId) + 1;
      usuarios.Add(user);
      serviceResponse.Data = usuarios.Select(u => _mapper.Map<GetUserDto>(u)).ToList();
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id)
    {
      var serviceResponse = new ServiceResponse<List<GetUserDto>>();
      try
      {
        var usuarioDelete = usuarios.First(u => u.UserId == id);
        usuarios.Remove(usuarioDelete);
        var usuariosOrder = usuarios.OrderBy(u => u.UserId).ToList();
        serviceResponse.Data = usuariosOrder.Select(u => _mapper.Map<GetUserDto>(u)).ToList();
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
        var editedUsuario = usuarios.FirstOrDefault(u => u.UserId == editedUser.UserId);

        editedUsuario.Name = editedUser.Name;
        editedUsuario.Email = editedUser.Email;
        editedUsuario.Phone = editedUser.Phone;
        editedUsuario.Type = editedUser.Type;

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
      var usuariosOrder = usuarios.OrderBy(u => u.UserId).ToList();
      serviceResponse.Data = usuariosOrder.Select(u => _mapper.Map<GetUserDto>(u)).ToList();
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetUserDto>> GetUserById(int id)
    {
      var serviceResponse = new ServiceResponse<GetUserDto>();
      var usuario = usuarios.FirstOrDefault(u => u.UserId == id);
      serviceResponse.Data = _mapper.Map<GetUserDto>(usuario);
      return serviceResponse;
    }

  }
}