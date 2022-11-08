using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Evaluation.Dtos.Item;
using Evaluation.Dtos.User;

namespace Evaluation
{
  public class AutoMapperProfile : Profile
  {
    public AutoMapperProfile()
    {
      CreateMap<User, GetUserDto>();
      CreateMap<AddUserDto, User>();
      CreateMap<EditUserDto, User>();
      
      CreateMap<Item, GetItemDto>();
      CreateMap<AddItemDto,Item>();
      CreateMap<EditItemDto, Item>();
    }
  }
}