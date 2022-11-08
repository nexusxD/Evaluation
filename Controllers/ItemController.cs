using System;
using System.Collections.Generic;
using System.Linq;
using Evaluation.Dtos.Item;
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
    public async Task<ActionResult<ServiceResponse<List<GetItemDto>>>> GetAllItems()
    {
      return Ok(await _itemService.GetAllItems());
    }

    [HttpGet("Single/{id}")]
    public async Task<ActionResult<ServiceResponse<GetItemDto>>> GetSingleItem(int id)
    {
      return Ok(await _itemService.GetItemById(id));
    }

    [HttpPost("Add")]
    public async Task<ActionResult<ServiceResponse<List<GetItemDto>>>> AddItem(AddItemDto newItem)
    {
      return Ok(await _itemService.AddItem(newItem));
    }
    [HttpDelete("Delete")]
    public async Task<ActionResult<ServiceResponse<List<GetItemDto>>>> DeleteItem(int id)
    {
      var response = await _itemService.DeleteItem(id);
      if (response.Data == null)
      {
        return NotFound(response);
      }
      return Ok(response);
    }
    [HttpPut("Update")]
    public async Task<ActionResult<ServiceResponse<GetItemDto>>> UpdateItem(EditItemDto updatedItem)
    {
      var response = await _itemService.EditItem(updatedItem);
      if (response.Data == null)
      {
        return NotFound(response);
      }
      return Ok(response);
    }
    [HttpGet("ItemsByUserId/{userId}")]
    public async Task<ActionResult<ServiceResponse<GetItemDto>>> GetItemByUserId(int userId)
    {
      return Ok(await _itemService.GetItemByUserId(userId));
    }
    [HttpGet("Trade/{userIdFrom}/{userIdTo}/{idFrom}/{quantity}")]
    public async Task<ActionResult<ServiceResponse<List<GetItemDto>>>> Trade(int userIdFrom, int userIdTo, int idFrom, int quantity)
    {
      return Ok(await _itemService.Trade(userIdFrom, userIdTo, idFrom,quantity));
    }
  }
}