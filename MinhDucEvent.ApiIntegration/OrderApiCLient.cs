using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MinhDucEvent.ViewModels.Common;
using MinhDucEvent.ViewModels.Sales;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MinhDucEvent.ApiIntegration
{
    public class OrderApiCLient : BaseApiClient, IOrderApiCLient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public OrderApiCLient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> CreateOrder(CheckoutRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/orders/", httpContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<CheckoutRequest> GetById(int id)
        {
            var data = await GetAsync<CheckoutRequest>($"/api/orders/{id}");

            return data;
        }

        public async Task<PagedResult<CheckoutRequest>> GetPagings(GetManageOrderPagingRequest request)
        {
            var data = await GetAsync<PagedResult<CheckoutRequest>>(
                $"/api/orders/paging?pageIndex={request.PageIndex}" +
                $"&pageSize={request.PageSize}" +
                $"&keyword={request.Keyword}");

            return data;
        }
    }
}