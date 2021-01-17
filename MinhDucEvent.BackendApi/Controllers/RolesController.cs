using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhDucEvent.Application.System.Roles;
using MinhDucEvent.ViewModels.System.Roles;
using System;
using System.Threading.Tasks;

namespace MinhDucEvent.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleService.GetAll();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var res = await _roleService.GetById(id);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _roleService.Delete(id);
            return Ok(result);
        }

        [HttpGet("paging")]
        [Authorize]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetManageRolePagingRequest request)
        {
            var result = await _roleService.GetAllPaging(request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] RoleUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            request.Id = id;
            var result = await _roleService.Update(request);
            if (!result)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] RoleCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var roleId = await _roleService.Create(request);
            if (roleId == null)
            {
                return BadRequest();
            }
            var role = await _roleService.GetById(roleId);
            return CreatedAtAction(nameof(GetById), new { id = roleId }, role);
        }
    }
}