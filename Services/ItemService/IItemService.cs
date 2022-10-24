using System;
using System.Collections.Generic;
using System.Linq;

namespace Evaluation.Services.ItemService
{
    public interface IItemService
    {
        List<Item> GetAllItems();
        Item GetItemById(int id);
        List<Item> AddItem(Item newItem);
        Item EditItem(Item editedItem);
        List<Item> DeleteItem(int id);
    }
}