using MinhDucEvent.ApiIntegration;
using MinhDucEvent.ViewModels.Catalog.EquipmentCategories;
using MinhDucEvent.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MinhDucEvent.Utilities.Constants;
using System.Net.Http.Headers;

namespace MinhDucEvent.ApiIntegration
{
    public class EquipmentCategoryApiClient : BaseApiClient, IEquipmentCategoryApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public EquipmentCategoryApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<EquipmentCategoryVm>> GetAll(string languageId)
        {
            return await GetListAsync<EquipmentCategoryVm>("/api/equipmentcategories?languageId=" + languageId);
        }

        public async Task<EquipmentCategoryVm> GetById(string languageId, int id)
        {
            return await GetAsync<EquipmentCategoryVm>($"/api/equipmentcategories/{id}/{languageId}");
        }

        public async Task<bool> CreateEquipmentCategory(EquipmentCategoryCreateRequest request)
        {
            var sessions = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.Token);

            var languageId = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Name) ? "" : request.Name.ToString()), "name");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.SeoDescription) ? "" : request.SeoDescription.ToString()), "seoDescription");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.SeoTitle) ? "" : request.SeoTitle.ToString()), "seoTitle");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.SeoAlias) ? "" : request.SeoAlias.ToString()), "seoAlias");
            requestContent.Add(new StringContent(languageId), "languageId");

            var response = await client.PostAsync($"/api/EquipmentCategories/", requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateEquipmentCategory(EquipmentCategoryUpdateRequest request)
        {
            var sessions = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.Token);

            var languageId = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Name) ? "" : request.Name.ToString()), "name");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.SeoDescription) ? "" : request.SeoDescription.ToString()), "seoDescription");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.SeoTitle) ? "" : request.SeoTitle.ToString()), "seoTitle");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.SeoAlias) ? "" : request.SeoAlias.ToString()), "seoAlias");
            requestContent.Add(new StringContent(languageId), "languageId");

            var response = await client.PutAsync($"/api/EquipmentCategories/" + request.Id, requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteEquipmentCategory(int id)
        {
            return await Delete($"/api/EquipmentCategories/" + id);
        }

        public async Task<PagedResult<EquipmentCategoryVm>> GetPagings(GetManageEqCaPagingRequest request)
        {
            var data = await GetAsync<PagedResult<EquipmentCategoryVm>>(
                $"/api/EquipmentCategories/paging?pageIndex={request.PageIndex}" +
                $"&pageSize={request.PageSize}" +
                $"&keyword={request.Keyword}&languageId={request.LanguageId}");

            return data;
        }
    }
}