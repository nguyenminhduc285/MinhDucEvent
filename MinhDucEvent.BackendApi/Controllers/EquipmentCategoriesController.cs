using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MinhDucEvent.Application.Catalog.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}