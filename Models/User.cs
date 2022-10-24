using System;
using System.Collections.Generic;
using System.Linq;

namespace Evaluation.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; } = "Pato";
        public string Email { get; set; } = "pato@patomail.com";
        public long Phone { get; set; } = 1234567890;
        public UserType Type { get; set; } = UserType.Comprador;
    }
}