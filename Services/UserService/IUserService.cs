using System;
using System.Collections.Generic;
using System.Linq;

namespace Evaluation.Services.UserService
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User GetUserById(int id);
        List<User> AddUser(User newUser);
        User EditUser(User editedUser);
        List<User> DeleteUser(int id);

    }
}