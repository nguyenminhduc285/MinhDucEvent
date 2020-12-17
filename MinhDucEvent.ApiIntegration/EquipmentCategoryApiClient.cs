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

namespace MinhDucEvent.ApiIntegration
{
    public class EquipmentCategoryApiClient : BaseApiClient, IEquipmentCategoryApiClient
    {
        public EquipmentCategoryApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
        }

        public async Task<List<EquipmentCategoryVm>> GetAll(string languageId)
        {
            return await GetListAsync<EquipmentCategoryVm>("/api/categories?languageId=" + languageId);
        }

        public async Task<EquipmentCategoryVm> GetById(string languageId, int id)
        {
            return await GetAsync<EquipmentCategoryVm>($"/api/categories/{id}/{languageId}");
        }
    }
}