using Microsoft.AspNetCore.Mvc;
using MinhDucEvent.Application.Catalog.Equipments;
using MinhDucEvent.ViewModels.Catalog.Equipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhDucEvent.BackendApi.Controllers
{
    //api/products
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentsController : ControllerBase
    {
        private readonly IEquipmentService _equipmentService;

        public EquipmentsController(
            IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetManageEquipmentPagingRequest request)
        {
            var products = await _equipmentService.GetAllPaging(request);
            return Ok(products);
        }
    }
}