using FolkDanceTime.Bll.Services;
using FolkDanceTime.Dal.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FolkDanceTime.Web.Server.Controllers
{
    [Route("api/categories")]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService categoryService;

        public CategoryController(CategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Category>> GetCategories()
        {
            return Ok(categoryService.GetCategories());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Category> GetCategoryById(int id)
        {
            var album = categoryService.GetCategoryById(id);
            if (album == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(album);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Category>> AddCategory([FromBody] Category category)
        {
            var newCategory = await categoryService.AddCategory(category);
            if (newCategory == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(newCategory);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Category>> EditAlbum([FromBody] Category category)
        {
            var editedCategory = await categoryService.EditCategory(category);
            if (editedCategory == null)
                return BadRequest();
            else
                return Ok(editedCategory);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteCategoryById(int id)
        {
            if (await categoryService.DeleteCategoryById(id))
                return Ok();
            else
                return BadRequest();
        }
    }
}
