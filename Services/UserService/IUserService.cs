using System;
using System.Collections.Generic;
using System.Linq;
using Evaluation.Dtos.User;

namespace Evaluation.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<List<GetUserDto>>> GetAllUsers();
        Task<ServiceResponse<GetUserDto>> GetUserById(int id);
        Task<ServiceResponse<List<GetUserDto>>> AddUser(AddUserDto newUser);
        Task<ServiceResponse<GetUserDto>> EditUser(EditUserDto editedUser);
        Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id);

    }
}