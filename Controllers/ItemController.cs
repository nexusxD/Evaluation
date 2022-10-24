using System;
using System.Collections.Generic;
using System.Linq;
using Evaluation.Services.ItemService;
using Microsoft.AspNetCore.Mvc;

namespace Evaluation.Controllers
{
    [ApiController]
    [Route("api/item/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }
        [HttpGet("All")]
        public ActionResult<List<Item>> GetAllItems()
        {
            return Ok(_itemService.GetAllItems());
        }

        [HttpGet("Single/{id}")]
        public ActionResult<Item> GetSingleItem(int id)
        {
            return Ok(_itemService.GetItemById(id));
        }

        [HttpPost("Add")]
        public ActionResult<List<Item>> AddItem(Item newItem)
        {
            return Ok(_itemService.AddItem(newItem));
        }
        [HttpDelete("Delete")]
        public ActionResult<List<Item>> DeleteItem(int id)
        {
            return Ok(_itemService.DeleteItem(id));
        }
        [HttpPut("Update")]
        public ActionResult<Item> UpdateItem(Item updatedItem)
        {
            return Ok(_itemService.EditItem(updatedItem));
        }

    }
}