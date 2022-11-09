using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Evaluation.Data;
using Evaluation.Dtos.Item;
using Microsoft.EntityFrameworkCore;

namespace Evaluation.Services.ItemService
{
  public class ItemService : IItemService
  {
    static class Counter
    {
      public static int counter = 0;
    }
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public ItemService(IMapper mapper, DataContext context)
    {
      _mapper = mapper;
      _context = context;
    }
    public async Task<ServiceResponse<List<GetItemDto>>> AddItem(AddItemDto newItem)
    {
      var serviceResponse = new ServiceResponse<List<GetItemDto>>();
      Item item = _mapper.Map<Item>(newItem);
      _context.Items.Add(item);
      await _context.SaveChangesAsync();
      serviceResponse.Data = await _context.Items.Select(i => _mapper.Map<GetItemDto>(i)).ToListAsync();
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetItemDto>>> DeleteItem(int id)
    {
      var serviceResponse = new ServiceResponse<List<GetItemDto>>();
      try
      {
        var itemDelete = await  _context.Items.FirstAsync(u => u.ItemId == id);
        _context.Items.Remove(itemDelete);
        await _context.SaveChangesAsync();
        serviceResponse.Data = _context.Items.Select(i => _mapper.Map<GetItemDto>(i)).ToList();
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
        var editedArticulo = await _context.Items.FirstOrDefaultAsync(i => i.ItemId == editedItem.ItemId);

        editedArticulo.Name = editedItem.Name;
        editedArticulo.Description = editedItem.Description;
        editedArticulo.Quantity = editedItem.Quantity;
        editedArticulo.UserId = editedItem.UserId;

        await _context.SaveChangesAsync();

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
      var dbItems = await _context.Items.ToListAsync();
      serviceResponse.Data = dbItems.Select(i => _mapper.Map<GetItemDto>(i)).ToList();
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetItemDto>> GetItemById(int id)
    {
      var serviceResponse = new ServiceResponse<GetItemDto>();
      var dbItems = await _context.Items.FirstOrDefaultAsync(i => i.ItemId == id);
      serviceResponse.Data = _mapper.Map<GetItemDto>(dbItems);
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetItemDto>>> GetItemByUserId(int userId)
    {
      var serviceResponse = new ServiceResponse<List<GetItemDto>>();
      var dbItems = await _context.Items.ToListAsync();
      List<Item> useritem = dbItems.FindAll(
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
        serviceResponse.Message = "No items found";
        return serviceResponse;
      }

    }
    public async Task<ServiceResponse<List<GetItemDto>>> Trade(int userIdFrom, int userIdTo, int idFrom, int quantity)
    {
      Counter.counter=0;
      var serviceResponse = new ServiceResponse<List<GetItemDto>>();
      //viejo item
      var oldItem = await _context.Items.FirstOrDefaultAsync(i => i.ItemId == idFrom);

      oldItem.Name = oldItem.Name;
      oldItem.Description = oldItem.Description;
      Counter.counter = Counter.counter + quantity;
      oldItem.Quantity = oldItem.Quantity-quantity;
      oldItem.UserId = userIdFrom;
      await _context.SaveChangesAsync();

      //Nuevo item
      Item itemAux = new Item {Name=oldItem.Name, Description=oldItem.Description, Quantity=Counter.counter, UserId=userIdTo};
      Item item = _mapper.Map<Item>(itemAux);
      _context.Items.Add(item);      
      await _context.SaveChangesAsync();

      serviceResponse.Data = await _context.Items.Select(i => _mapper.Map<GetItemDto>(i)).ToListAsync();
      
      return serviceResponse;

    }

  }
}