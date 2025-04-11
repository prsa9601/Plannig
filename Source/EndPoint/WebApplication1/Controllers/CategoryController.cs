using Application.Category.Create;
using Application.Category.Edit;
using Application.Category.Remove;
using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Facade.Category;
using Query.Category.DTOs;

namespace Planning.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ApiController
    {
        private readonly ICategoryFacade _facade;

        public CategoryController(ICategoryFacade facade)
        {
            _facade = facade;
        }

        [Authorize]
        [HttpPost("CreateCategory")]
        public async Task<ApiResult> CreateCategory(CreateCategoryCommand command)
        {
            return CommandResult(await _facade.Create(command));
        }
        [Authorize]
        [HttpPatch("EditCategory")]
        public async Task<ApiResult> EditCategory(EditCategoryCommand command)
        {
            return CommandResult(await _facade.Edit(command));
        }
        [Authorize]
        [HttpDelete("DeleteCategory")]
        public async Task<ApiResult> DeleteCategory(long CategoryId)
        {
            return CommandResult(await _facade.Remove(new RemoveCategoryCommand(CategoryId)));
        }
        [Authorize]
        [HttpGet("GetCategoryById")]
        public async Task<ApiResult<CategoryDto?>> GetCategoryById(long CategoryId)
        {
            return QueryResult(await _facade.GetCategoryById(CategoryId));
        }
        [Authorize]
        [HttpGet("GetListCategory")]
        public async Task<ApiResult<List<CategoryDto?>>> GetListCategory()
        {
            return QueryResult(await _facade.GetListCategory());
        }
    }
}
