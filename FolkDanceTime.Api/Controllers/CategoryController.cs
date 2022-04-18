using FolkDanceTime.Bll.Services;
using FolkDanceTime.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FolkDanceTime.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]//(Roles = "Admin")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CategoryDto>>> GetCategoriesAsync()
        {
            // TODO create CategoryHeaderDto and use that here
            return Ok(await _categoryService.GetCategoriesAsync());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDto>> GetCategoryAsync(int id)
        {
            return Ok(await _categoryService.GetCategoryAsync(id));
        }

        // TODO AddPropertyToCategoryAsync
        // TODO RemovePropertyFromCategoryAsync

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoryDto>> AddCategoryAsync([FromBody] CategoryDto category)
        {
            return Ok(await _categoryService.AddCategoryAsync(category));
            // TODO use CategoryHeaderDto here
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoryDto>> EditCategoryAsync([FromBody] CategoryDto category)
        {
            return Ok(await _categoryService.EditCategoryAsync(category));
            // TODO rename to RenameCategory, use CategoryHeaderDto here
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteCategoryAsync(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return Ok();
        }
    }
}
