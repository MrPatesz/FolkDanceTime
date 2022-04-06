using FolkDanceTime.Bll.Services;
using FolkDanceTime.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FolkDanceTime.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class ItemController : ControllerBase
    {
        private readonly ItemService _itemService;

        public ItemController(ItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ItemDto>>> GetItemsAsync()
        {
            return Ok(await _itemService.GetItemsAsync());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ItemDto>> GetItemAsync(int id)
        {
            return Ok(await _itemService.GetItemAsync(id));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ItemDto>> AddItemAsync([FromBody] ItemDto item, [FromQuery] int categoryId)
        {
            // var userId = HttpContext.User.FindFirst("ide a claim kulcsa kell"); // TODO
            return Ok(await _itemService.AddItemAsync(item, categoryId, userId));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ItemDto>> EditItemAsync([FromBody] ItemDto item)
        {
            return Ok(await _itemService.EditItemAsync(item));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteItemAsync(int id)
        {
            await _itemService.DeleteItemAsync(id);
            return Ok();
        }
    }
}
