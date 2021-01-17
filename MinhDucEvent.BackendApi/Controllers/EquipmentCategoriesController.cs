using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MinhDucEvent.Application.Catalog.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinhDucEvent.ViewModels.Catalog.EquipmentCategories;
using Microsoft.AspNetCore.Authorization;

namespace MinhDucEvent.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentCategoriesController : ControllerBase
    {
        private readonly IEquipmentCategoryService _equipmentCategoryService;

        public EquipmentCategoriesController(
            IEquipmentCategoryService categoryService)
        {
            _equipmentCategoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string languageId)
        {
            var equipmentCategories = await _equipmentCategoryService.GetAll(languageId);
            return Ok(equipmentCategories);
        }

        [HttpGet("{id}/{languageId}")]
        public async Task<IActionResult> GetById(string languageId, int id)
        {
            var equipmentCategory = await _equipmentCategoryService.GetById(languageId, id);
            return Ok(equipmentCategory);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetManageEqCaPagingRequest request)
        {
            var eqcas = await _equipmentCategoryService.GetAllPaging(request);
            return Ok(eqcas);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] EquipmentCategoryCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var eqcaId = await _equipmentCategoryService.Create(request);
            if (eqcaId == 0)
            {
                return BadRequest();
            }
            var Eqca = await _equipmentCategoryService.GetById(request.LanguageId, eqcaId);
            return CreatedAtAction(nameof(GetById), new { id = eqcaId }, Eqca);
        }

        [HttpPut("{eqcaId}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int eqcaId, [FromForm] EquipmentCategoryUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.Id = eqcaId;
            var affectedResult = await _equipmentCategoryService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{eqcaId}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int eqcaId)
        {
            var affectedResult = await _equipmentCategoryService.Delete(eqcaId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }
    }
}