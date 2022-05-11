using FolkDanceTime.Bll.Services;
using FolkDanceTime.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FolkDanceTime.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class ItemSetController : ControllerBase
    {
        private readonly ItemSetService _itemSetService;

        public ItemSetController(ItemSetService itemSetService)
        {
            _itemSetService = itemSetService;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ItemSetDto>>> GetItemSetsAsync()
        {
            return Ok(await _itemSetService.GetItemSetsAsync());
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ItemSetDto>>> GetMyItemSetsAsync()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;
            return Ok(await _itemSetService.GetMyItemSetsAsync(userId));
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ItemSetDto>> AddItemSetAsync([FromBody] ItemSetDto itemSet)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;
            return Ok(await _itemSetService.AddItemSetAsync(itemSet, userId));
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ItemSetDto>> EditItemSetAsync([FromBody] ItemSetDto itemSet)
        {
            return Ok(await _itemSetService.EditItemSetAsync(itemSet));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteItemSetAsync(int id)
        {
            // TODO only soft delete!
            await _itemSetService.DeleteItemSetAsync(id);
            return Ok();
        }
    }
}
