using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhDucEvent.Application.Catalog.Categories;
using MinhDucEvent.ViewModels.Catalog.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhDucEvent.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(
            ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string languageId)
        {
            var categories = await _categoryService.GetAll(languageId);
            return Ok(categories);
        }

        [HttpGet("{id}/{languageId}")]
        public async Task<IActionResult> GetById(string languageId, int id)
        {
            var category = await _categoryService.GetById(languageId, id);
            return Ok(category);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetManageCategoryPagingRequest request)
        {
            var categories = await _categoryService.GetAllPaging(request);
            return Ok(categories);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] CategoryCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var caId = await _categoryService.Create(request);
            if (caId == 0)
            {
                return BadRequest();
            }
            var ca = await _categoryService.GetById(request.LanguageId, caId);
            return CreatedAtAction(nameof(GetById), new { id = caId }, ca);
        }

        [HttpPut("{caId}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int caId, [FromForm] CategoryUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.Id = caId;
            var affectedResult = await _categoryService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{caId}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int caId)
        {
            var affectedResult = await _categoryService.Delete(caId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }
    }
}