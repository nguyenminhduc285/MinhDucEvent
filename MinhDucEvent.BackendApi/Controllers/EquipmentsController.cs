using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhDucEvent.Application.Catalog.Equipments;
using MinhDucEvent.ViewModels.Catalog.EquipmentImages;
using MinhDucEvent.ViewModels.Catalog.Equipments;
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
            var equipments = await _equipmentService.GetAllPaging(request);
            return Ok(equipments);
        }

        [HttpGet("{equipmentId}/{languageId}")]
        public async Task<IActionResult> GetById(int equipmentId, string languageId)
        {
            var Equipment = await _equipmentService.GetById(equipmentId, languageId);
            if (Equipment == null)
                return BadRequest("Cannot find Equipment");
            return Ok(Equipment);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] EquipmentCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var equipmentId = await _equipmentService.Create(request);
            if (equipmentId == 0)
                return BadRequest();

            var Equipment = await _equipmentService.GetById(equipmentId, request.LanguageId);
            return CreatedAtAction(nameof(GetById), new { id = equipmentId }, Equipment);
        }

        [HttpPut("{equipmentId}")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int equipmentId, [FromForm] EquipmentUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.Id = equipmentId;
            var affectedResult = await _equipmentService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{equipmentId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int EquipmentId)
        {
            var affectedResult = await _equipmentService.Delete(EquipmentId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        //Images
        [HttpPost("{equipmentId}/images")]
        public async Task<IActionResult> CreateImage(int equipmentId, [FromForm] EquipmentImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = await _equipmentService.AddImage(equipmentId, request);
            if (imageId == 0)
                return BadRequest();

            var image = await _equipmentService.GetImageById(imageId);

            return CreatedAtAction(nameof(GetImageById), new { id = imageId }, image);
        }

        [HttpPut("{equipmentId}/images/{imageId}")]
        [Authorize]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm] EquipmentImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _equipmentService.UpdateImage(imageId, request);
            if (result == 0)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{equipmentId}/images/{imageId}")]
        [Authorize]
        public async Task<IActionResult> RemoveImage(int imageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _equipmentService.RemoveImage(imageId);
            if (result == 0)
                return BadRequest();

            return Ok();
        }

        [HttpGet("{equipmentId}/images/{imageId}")]
        public async Task<IActionResult> GetImageById(int imageId)
        {
            var image = await _equipmentService.GetImageById(imageId);
            if (image == null)
                return BadRequest("Cannot find Equipment");
            return Ok(image);
        }

        [HttpPut("{id}/categories")]
        [Authorize]
        public async Task<IActionResult> CategoryAssign(int id, [FromBody] EquipmentCategoryAssignRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _equipmentService.CategoryAssign(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}