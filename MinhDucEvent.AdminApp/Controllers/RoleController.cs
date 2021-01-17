using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MinhDucEvent.ApiIntegration;
using MinhDucEvent.Utilities.Constants;
using MinhDucEvent.ViewModels.System.Roles;
using System;
using System.Threading.Tasks;

namespace MinhDucEvent.AdminApp.Controllers
{
    public class RoleController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IRoleApiClient _roleApi;

        public RoleController(
            IConfiguration configuration,
            IRoleApiClient roleApiClient)
        {
            _configuration = configuration;
            _roleApi = roleApiClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 5)
        {
            var request = new GetManageRolePagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
            };
            var data = await _roleApi.GetPagings(request);
            ViewBag.Keyword = keyword;

            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] RoleCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _roleApi.CreateRole(request);
            if (result)
            {
                TempData["result"] = "Thêm mới Danh mục người dùng thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm Danh mục người dùng thất bại");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var role = await _roleApi.GetById(id);
            var roletVm = new RoleUpdateRequest()
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            };
            return View(roletVm);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Edit([FromForm] RoleUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _roleApi.UpdateRole(request);
            if (result)
            {
                TempData["result"] = "Cập nhật Danh mục người dùng thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Cập nhật Danh mục người dùng thất bại");
            return View(request);
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            return View(new RoleDeleteRequest()
            {
                Id = id
            });
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _roleApi.GetById(id);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RoleDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _roleApi.DeleteRole(request.Id);
            if (result)
            {
                TempData["result"] = "Xóa Danh mục người dùng thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa Danh mục người dùng không thành công");
            return View(request);
        }
    }
}