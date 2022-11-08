using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Evaluation.Dtos.Item
{
  public class GetItemDto
  {
    public int ItemId { get; set; }
    public string Name { get; set; } = "Galletas";
    public string Description { get; set; } = "Galletas Ricas";
    public int Quantity { get; set; } = 15;
    public int UserId { get; set; }
  }
}