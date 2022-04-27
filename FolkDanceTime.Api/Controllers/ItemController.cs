using FolkDanceTime.Bll.Services;
using FolkDanceTime.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FolkDanceTime.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]//(Roles = "Admin,Dancer")]
    public class ItemController : ControllerBase
    {
        private readonly ItemService _itemService;

        public ItemController(ItemService itemService)
        {
            _itemService = itemService;
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ItemDto>>> GetItemsAsync()
        {
            // TODO create ItemHeaderDto and use that here
            return Ok(await _itemService.GetItemsAsync());
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ItemDto>>> GetMyItemsAsync()
        {
            // TODO create ItemHeaderDto and use that here
            var userId = HttpContext.User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier).Value;
            return Ok(await _itemService.GetMyItemsAsync(userId));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ItemDto>> GetItemAsync(int id)
        {
            return Ok(await _itemService.GetItemAsync(id));
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ItemDto>> AddItemAsync([FromBody] ItemDto item, [FromQuery] int categoryId)
        {
            // TODO add empty PropertyValue records upon creation
            var userId = HttpContext.User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier).Value;
            return Ok(await _itemService.AddItemAsync(item, categoryId, userId));
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ItemDto>> EditItemAsync([FromBody] ItemDto item)
        {
            // TODO also edit all the property values
            return Ok(await _itemService.EditItemAsync(item));
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteItemAsync(int id)
        {
            // TODO only soft delete!
            await _itemService.DeleteItemAsync(id);
            return Ok();
        }

        // TODO
        //[HttpPut("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult> HandOverItemAsync(int id, [FromQuery] int toUserId)
        //{
        //    return Ok(await _itemService.HandOverItemAsync(id, toUserId));
        //}
    }
}
