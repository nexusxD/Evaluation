using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Evaluation.Dtos.Item;

namespace Evaluation.Services.ItemService
{
  public class ItemService : IItemService
  {
    static class Counter
    {
      public static int counter = 0;
    }
    private static List<Item> items = new List<Item>
        {
            new Item(),
            new Item{ItemId=1, Name="Mandarinas", Description="Muy jugosas", Quantity=100, UserId=1},
            new Item{ItemId=2, Name="Manzanas", Description="Muy jugosas", Quantity=10, UserId=2},
            new Item{ItemId=3, Name="Naranjas", Description="Muy jugosas", Quantity=100, UserId=1}
        };
    private readonly IMapper _mapper;

    public ItemService(IMapper mapper)
    {
      _mapper = mapper;
    }
    public async Task<ServiceResponse<List<GetItemDto>>> AddItem(AddItemDto newItem)
    {
      var serviceResponse = new ServiceResponse<List<GetItemDto>>();
      Item item = _mapper.Map<Item>(newItem);
      item.ItemId = items.Max(i => i.ItemId) + 1;
      items.Add(item);
      serviceResponse.Data = items.Select(i => _mapper.Map<GetItemDto>(i)).ToList();
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetItemDto>>> DeleteItem(int id)
    {
      var serviceResponse = new ServiceResponse<List<GetItemDto>>();
      try
      {
        var itemDelete = items.First(u => u.UserId == id);
        items.Remove(itemDelete);
        var itemsOrder = items.OrderBy(u => u.UserId).ToList();
        serviceResponse.Data = itemsOrder.Select(i => _mapper.Map<GetItemDto>(i)).ToList();
      }
      catch (Exception ex)
      {
        serviceResponse.Succes = false;
        serviceResponse.Message = ex.Message;
      }
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetItemDto>> EditItem(EditItemDto editedItem)
    {
      var serviceResponse = new ServiceResponse<GetItemDto>();

      try
      {
        var editedArticulo = items.FirstOrDefault(i => i.UserId == editedItem.UserId);

        editedArticulo.Name = editedItem.Name;
        editedArticulo.Description = editedItem.Description;
        editedArticulo.Quantity = editedItem.Quantity;
        editedArticulo.UserId = editedItem.UserId;

        serviceResponse.Data = _mapper.Map<GetItemDto>(editedArticulo);
      }
      catch (Exception ex)
      {
        serviceResponse.Succes = false;
        serviceResponse.Message = ex.Message;
      }
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetItemDto>>> GetAllItems()
    {
      var serviceResponse = new ServiceResponse<List<GetItemDto>>();
      var itemsOrder = items.OrderBy(i => i.ItemId).ToList();
      serviceResponse.Data = itemsOrder.Select(i => _mapper.Map<GetItemDto>(i)).ToList();
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetItemDto>> GetItemById(int id)
    {
      var serviceResponse = new ServiceResponse<GetItemDto>();
      var item = items.FirstOrDefault(i => i.ItemId == id);
      serviceResponse.Data = _mapper.Map<GetItemDto>(item);
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetItemDto>>> GetItemByUserId(int userId)
    {
      var serviceResponse = new ServiceResponse<List<GetItemDto>>();
      List<Item> useritem = items.FindAll(
        delegate (Item i)
        {
          return i.UserId == userId;
        }
      );
      if (useritem.Count != 0)
      {
        serviceResponse.Data = useritem.Select(i => _mapper.Map<GetItemDto>(i)).ToList();
        return serviceResponse;
      }
      else
      {
        serviceResponse.Data = items.Select(i => _mapper.Map<GetItemDto>(i)).ToList();
        return serviceResponse;
      }

    }
    public async Task<ServiceResponse<List<GetItemDto>>> Trade(int userIdFrom, int userIdTo, int idFrom, int quantity)
    {
      Counter.counter=0;
      var serviceResponse = new ServiceResponse<List<GetItemDto>>();
      //nuevo item a crear
      var newItem = items.Where(i => i.ItemId == idFrom).FirstOrDefault();
      newItem.ItemId = items.Max(i=>i.ItemId)+1;
      newItem.Name = newItem.Name;
      newItem.Description = newItem.Description;
      var quantityAux = newItem.Quantity;
      Counter.counter = Counter.counter + quantity;
      newItem.Quantity = Counter.counter;
      newItem.UserId = userIdTo;
      
      //Item que ya existe en la lista
      Item item = new Item {ItemId=idFrom, Name=newItem.Name, Description=newItem.Description, Quantity=quantityAux-quantity, UserId=userIdFrom};
      
      items.Add(item);
      var itemsOrder = items.OrderBy(i => i.ItemId).ToList();
      serviceResponse.Data = itemsOrder.Select(i => _mapper.Map<GetItemDto>(i)).ToList();
      return serviceResponse;

    }

  }
}