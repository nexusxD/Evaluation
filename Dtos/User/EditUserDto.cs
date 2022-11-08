using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Evaluation.Dtos.User
{
    public class EditUserDto
    {
        public int UserId { get; set; }
        public string Name { get; set; } = "Pato";
        public string Email { get; set; } = "pato@patomail.com";
        public long Phone { get; set; } = 1234567890;
        public UserType Type { get; set; } = UserType.Comprador;  
    }
}