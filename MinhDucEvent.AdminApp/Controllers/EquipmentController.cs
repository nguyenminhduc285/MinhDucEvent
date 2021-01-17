using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using MinhDucEvent.ApiIntegration;
using MinhDucEvent.Utilities.Constants;
using MinhDucEvent.ViewModels.Catalog.Equipments;
using MinhDucEvent.ViewModels.Common;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace MinhDucEvent.AdminApp.Controllers
{
    public class EquipmentController : BaseController
    {
        private readonly IEquipmentApiClient _equipmentApiClient;
        private readonly IConfiguration _configuration;

        private readonly IEquipmentCategoryApiClient _equipmentCategoryApi;

        public EquipmentController(IEquipmentApiClient equipmentApiClient,
            IConfiguration configuration,
            IEquipmentCategoryApiClient equipmentCategoryApiClient)
        {
            _configuration = configuration;
            _equipmentApiClient = equipmentApiClient;
            _equipmentCategoryApi = equipmentCategoryApiClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string keyword, int? equipmentcategoryId, int pageIndex = 1, int pageSize = 5)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var request = new GetManageEquipmentsPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                LanguageId = languageId,
                EquipmentCategoryId = equipmentcategoryId
            };
            var data = await _equipmentApiClient.GetPagings(request);
            ViewBag.Keyword = keyword;

            var categories = await _equipmentCategoryApi.GetAll(languageId);
            ViewBag.Categories = categories.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = equipmentcategoryId.HasValue && equipmentcategoryId.Value == x.Id
            });

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
        public async Task<IActionResult> Create([FromForm] EquipmentCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _equipmentApiClient.CreateEquipment(request);
            if (result)
            {
                TempData["result"] = "Thêm mới sản phẩm thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm sản phẩm thất bại");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> CategoryAssign(int id)
        {
            var roleAssignRequest = await GetEquipmentCategoryAssignRequest(id);
            return View(roleAssignRequest);
        }

        [HttpPost]
        public async Task<IActionResult> EquipmentCategoryAssign(EquipmentCategoryAssignRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _equipmentApiClient.CategoryAssign(request.Id, request);

            if (result.IsSuccessed)
            {
                TempData["result"] = "Cập nhật danh mục thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            var roleAssignRequest = await GetEquipmentCategoryAssignRequest(request.Id);

            return View(roleAssignRequest);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var Equipment = await _equipmentApiClient.GetById(id, languageId);
            var editVm = new EquipmentUpdateRequest()
            {
                Id = Equipment.Id,
                Description = Equipment.Description,
                Details = Equipment.Details,
                Name = Equipment.Name,
                SeoAlias = Equipment.SeoAlias,
                SeoDescription = Equipment.SeoDescription,
                SeoTitle = Equipment.SeoTitle
            };
            return View(editVm);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Edit([FromForm] EquipmentUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _equipmentApiClient.UpdateEquipment(request);
            if (result)
            {
                TempData["result"] = "Cập nhật sản phẩm thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Cập nhật sản phẩm thất bại");
            return View(request);
        }

        private async Task<EquipmentCategoryAssignRequest> GetEquipmentCategoryAssignRequest(int id)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var EquipmentObj = await _equipmentApiClient.GetById(id, languageId);
            var equipmentcategories = await _equipmentCategoryApi.GetAll(languageId);
            var categoryAssignRequest = new EquipmentCategoryAssignRequest();
            foreach (var role in equipmentcategories)
            {
                categoryAssignRequest.Categories.Add(new SelectItem()
                {
                    Id = role.Id.ToString(),
                    Name = role.Name,
                    Selected = EquipmentObj.EquipmentCategories.Contains(role.Name)
                });
            }
            return categoryAssignRequest;
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(new EquipmentDeleteRequest()
            {
                Id = id
            });
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var result = await _equipmentApiClient.GetById(id, languageId);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EquipmentDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _equipmentApiClient.DeleteEquipment(request.Id);
            if (result)
            {
                TempData["result"] = "Xóa sản phẩm thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa không thành công");
            return View(request);
        }
    }
}