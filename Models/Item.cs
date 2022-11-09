using System;
using System.Collections.Generic;
using System.Linq;

namespace Evaluation.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; } = "Galletas";
        public string Description { get; set; } = "Galletas Ricas";
        public int Quantity { get; set; } = 15;
        public int UserId {get; set; }
        public User? User {get; set;}
    }
}