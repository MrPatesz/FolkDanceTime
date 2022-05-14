using FolkDanceTime.Bll.Services;
using FolkDanceTime.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FolkDanceTime.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class ItemSetTransactionController : ControllerBase
    {
        private readonly ItemSetTransactionService _itemSetTransactionService;

        public ItemSetTransactionController(ItemSetTransactionService itemSetTransactionService)
        {
            _itemSetTransactionService = itemSetTransactionService;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ItemSetTransactionDto>>> GetItemSetTransactionsAsync()
        {
            return Ok(await _itemSetTransactionService.GetItemSetTransactionsAsync());
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ItemSetTransactionDto>>> GetIncomingItemSetTransactionsAsync()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;
            return Ok(await _itemSetTransactionService.GetIncomingItemSetTransactionsAsync(userId));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ItemSetTransactionDto>>> GetOutgoingItemSetTransactionsAsync()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;
            return Ok(await _itemSetTransactionService.GetOutgoingItemSetTransactionsAsync(userId));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ItemSetTransactionDto>> CreateItemSetTransactionAsync([FromQuery] int itemId, [FromQuery] string receiverUserId)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;
            return Ok(await _itemSetTransactionService.CreateItemSetTransactionAsync(itemId, userId, receiverUserId));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> RevokeItemSetTransactionAsync(int id)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;
            return Ok(await _itemSetTransactionService.RevokeItemSetTransactionAsync(id, userId));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> DeclineItemSetTransactionAsync(int id)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;
            return Ok(await _itemSetTransactionService.DeclineItemSetTransactionAsync(id, userId));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> AcceptItemSetTransactionAsync(int id)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;
            return Ok(await _itemSetTransactionService.AcceptItemSetTransactionAsync(id, userId));
        }
    }
}
