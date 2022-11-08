using System;
using System.Collections.Generic;
using System.Linq;
using Evaluation.Dtos.Item;

namespace Evaluation.Services.ItemService
{
    public interface IItemService
    {
        Task<ServiceResponse<List<GetItemDto>>> GetAllItems();
        Task<ServiceResponse<GetItemDto>> GetItemById(int id);
        Task<ServiceResponse<List<GetItemDto>>> AddItem(AddItemDto newItem);
        Task<ServiceResponse<GetItemDto>> EditItem(EditItemDto editedItem);
        Task<ServiceResponse<List<GetItemDto>>> DeleteItem(int id);
        Task<ServiceResponse<List<GetItemDto>>> GetItemByUserId(int userId);
        Task<ServiceResponse<List<GetItemDto>>> Trade(int userIdFrom, int userIdTo, int idFrom, int idTo, int quantity);
    }
}