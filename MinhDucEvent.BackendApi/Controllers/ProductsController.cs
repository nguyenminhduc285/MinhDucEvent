using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhDucEvent.Application.Catalog.Products;
using MinhDucEvent.ViewModels.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhDucEvent.BackendApi.Controllers
{
    //api/products
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productservice;

        public ProductsController(IProductService productService)
        {
            _productservice = productService;
        }

        [HttpGet("{productId}/{languageId}")]
        public async Task<IActionResult> GetById(int productId, string languageId)
        {
            var Equipment = await _productservice.GetById(productId, languageId);
            if (Equipment == null)
                return BadRequest("Cannot find Product");
            return Ok(Equipment);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var equipmentId = await _productservice.Create(request);
            if (equipmentId == 0)
                return BadRequest();

            var Equipment = await _productservice.GetById(equipmentId, request.LanguageId);
            return CreatedAtAction(nameof(GetById), new { id = equipmentId }, Equipment);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetManageProductPagingRequest request)
        {
            var equipments = await _productservice.GetAllPaging(request);
            return Ok(equipments);
        }
    }
}