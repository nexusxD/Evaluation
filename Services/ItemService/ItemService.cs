using System;
using System.Collections.Generic;
using System.Linq;

namespace Evaluation.Services.ItemService
{
    public class ItemService : IItemService
    {
        private static List<Item> items = new List<Item>
        {
            new Item(),
            new Item{ItemId=1, Name="Mandarinas", Description="Muy jugosas", Quantity=100, UserId=0}
        };
        public List<Item> AddItem(Item newItem)
        {
            items.Add(newItem);
            return items;
        }

        public List<Item> DeleteItem(int id)
        {
            var itemDelete = items.FirstOrDefault(i => i.ItemId == id);
            items.Remove(itemDelete);
            return items;
        }

        public Item EditItem(Item editedItem)
        {
            var editadoitem = items.Where(i => i.ItemId == editedItem.ItemId).FirstOrDefault();
            editadoitem.ItemId = editedItem.ItemId;
            editadoitem.Name = editedItem.Name;
            editadoitem.Description = editedItem.Description;
            editadoitem.Quantity = editedItem.Quantity;
            editadoitem.UserId = editedItem.UserId;
            var itemDelete = items.Where(i => i.ItemId == editedItem.ItemId).FirstOrDefault();
            items.Remove(itemDelete);
            items.Add(editadoitem);
            return editadoitem;
        }

        public List<Item> GetAllItems()
        {
            return items;
        }

        public Item GetItemById(int id)
        {
            return items.FirstOrDefault(i => i.ItemId == id);
        }
    }
}