using Microsoft.AspNetCore.Mvc;
using MinhDucEvent.ApiIntegration;
using MinhDucEvent.ViewModels.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhDucEvent.AdminApp.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderApiCLient _orderApi;

        public OrderController(
            IOrderApiCLient orderApiCLient)
        {
            _orderApi = orderApiCLient;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 5)
        {
            var request = new GetManageOrderPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _orderApi.GetPagings(request);
            ViewBag.Keyword = keyword;

            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _orderApi.GetById(id);
            return View(result);
        }
    }
}