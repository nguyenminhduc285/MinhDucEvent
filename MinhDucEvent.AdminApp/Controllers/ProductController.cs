﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using MinhDucEvent.ApiIntegration;
using MinhDucEvent.Utilities.Constants;
using MinhDucEvent.ViewModels.Catalog.Equipments;
using MinhDucEvent.ViewModels.Catalog.Products;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace MinhDucEvent.AdminApp.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductApiClient _productApiClient;
        private readonly IConfiguration _configuration;

        private readonly ICategoryApiClient _categoryApiClient;
        private readonly IEquipmentApiClient _equipmentApiClient;
        private readonly IWebHostEnvironment _hostingE;
        
        public ProductController(IProductApiClient productApiClient,
            IConfiguration configuration,
            ICategoryApiClient categoryApiClient,
            IEquipmentApiClient equipmentApiClient,
            IWebHostEnvironment hostingE
            )
        {
            _configuration = configuration;
            _productApiClient = productApiClient;
            _categoryApiClient = categoryApiClient;
            _equipmentApiClient = equipmentApiClient;
            _hostingE = hostingE;
        }

        public async Task<IActionResult> Index(string keyword, int? categoryId, int pageIndex = 1, int pageSize = 3)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var request = new GetManageProductPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                LanguageId = languageId,
                CategoryId = categoryId
            };
            var data = await _productApiClient.GetPagings(request);
            ViewBag.Keyword = keyword;

            var categories = await _categoryApiClient.GetAll(languageId);
            ViewBag.Categories = categories.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = categoryId.HasValue && categoryId.Value == x.Id
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
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var leq = _equipmentApiClient.GetPagings(new GetManageEquipmentsPagingRequest()
            {
                EquipmentCategoryId = null,
                Keyword = null,
                LanguageId = languageId,
                PageIndex = 1,
                PageSize = 80
            }); 
            var obj = new ProductCreateRequest();
              obj.eqm = leq.Result.Items.FindAll(s=> s.Id > 0);
              return View(obj);
        }
        [HttpPost]
        public IActionResult upload_detail_image(IFormFile upload)
        {
            var filename = DateTime.Now.ToString("yyyyMMddHHmmss") + upload.FileName;
            var path = Path.Combine(Directory.GetCurrentDirectory(), _hostingE.WebRootPath, "upload", filename);
            var stream = new FileStream(path,FileMode.Create);
            upload.CopyToAsync(stream);
            return new JsonResult(new {path = "https://localhost:5011" +  "/upload/" + filename});
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _productApiClient.CreateProduct(request);
            if (result)
            {
                TempData["result"] = "Thêm mới sản phẩm thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm sản phẩm thất bại");
            return View(request);
        }
        [HttpGet, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productApiClient.DeleteProduct(id);
            if (result)
            {
                TempData["result"] = "Xóa sản phẩm thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa sản phẩm thất bại");
            return  View("~/Views/Product/Index.cshtml");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var leq = _equipmentApiClient.GetPagings(new GetManageEquipmentsPagingRequest()
            {
                EquipmentCategoryId = null,
                Keyword = null,
                LanguageId = languageId,
                PageIndex = 1,
                PageSize = 80
            }); 
            
            var product = _productApiClient.GetById(id,languageId).Result;
            var resultProduction = new ProductEdit()
            {
                Name = product.Name,
                Price = product.Price,
                OriginalPrice = product.OriginalPrice,
                Description = product.Description,
                Details= product.Details,
                SeoDescription=product.SeoDescription,
                SeoTitle = product.SeoTitle,
                SeoAlias=product.SeoAlias,
                ThumbnailImage=product.ThumbnailImage,
                eqm = leq.Result.Items
            };
         
            return View(resultProduction);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View("~/Views/Product/Index.cshtml");

            var result = await _productApiClient.UpdateProduct(request);
            if (result)
            {
                TempData["result"] = "Cập nhật người dùng thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Fail Update");
            return  View("~/Views/Product/Index.cshtml");
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);
            var leq = _equipmentApiClient.GetPagings(new GetManageEquipmentsPagingRequest()
            {
                EquipmentCategoryId = null,
                Keyword = null,
                LanguageId = languageId,
                PageIndex = 1,
                PageSize = 80
            }); 
            var product = _productApiClient.GetById(id,languageId).Result;
            var resultProduction = new ProductEdit()
            {
                Name = product.Name,
                Price = product.Price,
                OriginalPrice = product.OriginalPrice,
                Description = product.Description,
                Details= product.Details,
                SeoDescription=product.SeoDescription,
                SeoTitle = product.SeoTitle,
                SeoAlias=product.SeoAlias,
                ThumbnailImage=product.ThumbnailImage,
                eqm = leq.Result.Items
            };
         
            return View(resultProduction);
        }
    }
}