using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MinhDucEvent.ApiIntegration;
using MinhDucEvent.Utilities.Constants;
using MinhDucEvent.ViewModels.Catalog.EquipmentCategories;
using MinhDucEvent.ViewModels.Catalog.Equipments;
using System.Threading.Tasks;

namespace MinhDucEvent.AdminApp.Controllers
{
    public class EquipmentCategoryController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IEquipmentCategoryApiClient _equipmentCategoryApi;

        public EquipmentCategoryController(
            IConfiguration configuration,
            IEquipmentCategoryApiClient equipmentCategoryApiClient)
        {
            _configuration = configuration;
            _equipmentCategoryApi = equipmentCategoryApiClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 5)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var request = new GetManageEqCaPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                LanguageId = languageId,
            };
            var data = await _equipmentCategoryApi.GetPagings(request);
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
        public async Task<IActionResult> Create([FromForm] EquipmentCategoryCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _equipmentCategoryApi.CreateEquipmentCategory(request);
            if (result)
            {
                TempData["result"] = "Thêm mới sản phẩm thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm sản phẩm thất bại");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var eqca = await _equipmentCategoryApi.GetById(languageId, id);
            var editVm = new EquipmentCategoryUpdateRequest()
            {
                Id = eqca.Id,

                Name = eqca.Name,
                SeoAlias = eqca.SeoAlias,
                SeoDescription = eqca.SeoDescription,
                SeoTitle = eqca.SeoTitle
            };
            return View(editVm);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Edit([FromForm] EquipmentCategoryUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _equipmentCategoryApi.UpdateEquipmentCategory(request);
            if (result)
            {
                TempData["result"] = "Cập nhật sản phẩm thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Cập nhật sản phẩm thất bại");
            return View(request);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(new EquipmentCategoryDeleteRequest()
            {
                Id = id
            });
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var result = await _equipmentCategoryApi.GetById(languageId, id);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EquipmentCategoryDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _equipmentCategoryApi.DeleteEquipmentCategory(request.Id);
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