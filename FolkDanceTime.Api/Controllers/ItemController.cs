using FolkDanceTime.Bll.Services;
using FolkDanceTime.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FolkDanceTime.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class ItemController : ControllerBase
    {
        private readonly ItemService _itemService;
        private readonly PictureService _pictureService;

        public ItemController(ItemService itemService, PictureService pictureService)
        {
            _itemService = itemService;
            _pictureService = pictureService;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ItemDto>>> GetItemsAsync()
        {
            return Ok(await _itemService.GetItemsAsync());
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ItemDto>>> GetMyItemsAsync()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;
            return Ok(await _itemService.GetMyItemsAsync(userId));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ItemDto>> GetItemAsync(int id)
        {
            return Ok(await _itemService.GetItemAsync(id));
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ItemDto>> AddItemAsync([FromBody] ItemDto item, [FromQuery] int categoryId)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;
            return Ok(await _itemService.AddItemAsync(item, categoryId, userId));
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ItemDto>> EditItemAsync([FromBody] ItemDto item)
        {
            return Ok(await _itemService.EditItemAsync(item));
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteItemAsync(int id)
        {
            // TODO only soft delete!
            await _itemService.DeleteItemAsync(id);
            return Ok();
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> PostPicture([FromForm(Name = "image")] IFormFile file)
        {
            return Ok(await _pictureService.SavePicture(file));
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult DeletePicture([FromQuery] string fileName)
        {
            _pictureService.DeletePicture(fileName);
            return Ok();
        }
    }
}
