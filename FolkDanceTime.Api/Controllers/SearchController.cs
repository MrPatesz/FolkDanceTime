using FolkDanceTime.Bll.Services;
using FolkDanceTime.Shared.Dtos;
using FolkDanceTime.Shared.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FolkDanceTime.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize(Policy = "AdminOnly")]
    public class SearchController : ControllerBase
    {
        private readonly SearchService _searchService;

        public SearchController(SearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<SearchResultDto>>> SearchItemsAsync([FromQuery] string term, [FromQuery] SearchBy searchBy)
        {
            return Ok(await _searchService.SearchItemsAsync(term, searchBy));
        }
    }
}
