using System;
using System.Collections.Generic;
using System.Linq;

namespace Evaluation.Services.UserService
{
    public class UserService : IUserService
    {
        private static List<User> usuarios = new List<User>
        {
            new User(),
            new User{ UserId=1, Name="Pata", Email="pata@patamail.com", Phone=9876543210, Type=UserType.Vendedor}
        };
        public List<User> AddUser(User newUser)
        {
            usuarios.Add(newUser);
            return usuarios;
        }

        public List<User> DeleteUser(int id)
        {
            var usuarioDelete = usuarios.FirstOrDefault(u => u.UserId == id);
            usuarios.Remove(usuarioDelete);
            return usuarios;
        }

        public User EditUser(User editedUser)
        {
            var editedUsuario = usuarios.Where(u => u.UserId == editedUser.UserId).FirstOrDefault();
            editedUsuario.UserId = editedUser.UserId;
            editedUsuario.Name = editedUser.Name;
            editedUsuario.Email = editedUser.Email;
            editedUsuario.Phone = editedUser.Phone;
            editedUsuario.Type = editedUser.Type;
            var usuarioDelete = usuarios.Where(u => u.UserId == editedUser.UserId).FirstOrDefault();
            usuarios.Remove(usuarioDelete);
            usuarios.Add(editedUsuario);
            return editedUsuario;
        }

        public List<User> GetAllUsers()
        {
            return usuarios;
        }

        public User GetUserById(int id)
        {
            return usuarios.FirstOrDefault(u => u.UserId == id);
        }
    }
}