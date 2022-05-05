using FolkDanceTime.Bll.Services;
using FolkDanceTime.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FolkDanceTime.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]//(Roles = "Admin,Dancer")]
    public class ItemTransactionController : ControllerBase
    {
        private readonly ItemTransactionService _itemTransactionService;

        public ItemTransactionController(ItemTransactionService itemTransactionService)
        {
            _itemTransactionService = itemTransactionService;
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ItemTransactionDto>>> GetItemTransactionsAsync()
        {
            return Ok(await _itemTransactionService.GetItemTransactionsAsync());
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ItemTransactionDto>>> GetIncomingItemTransactionsAsync()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier).Value;
            return Ok(await _itemTransactionService.GetIncomingItemTransactionsAsync(userId));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ItemTransactionDto>>> GetOutgoingItemTransactionsAsync()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier).Value;
            return Ok(await _itemTransactionService.GetOutgoingItemTransactionsAsync(userId));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ItemTransactionDto>> CreateTransactionAsync([FromQuery] int itemId, [FromQuery] string receiverUserId)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier).Value;
            return Ok(await _itemTransactionService.CreateTransactionAsync(itemId, userId, receiverUserId));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> RevokeTransactionAsync(int id)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier).Value;
            return Ok(await _itemTransactionService.RevokeTransactionAsync(id, userId));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> DeclineTransactionAsync(int id)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier).Value;
            return Ok(await _itemTransactionService.DeclineTransactionAsync(id, userId));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> AcceptTransactionAsync(int id)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier).Value;
            return Ok(await _itemTransactionService.AcceptTransactionAsync(id, userId));
        }
    }
}
