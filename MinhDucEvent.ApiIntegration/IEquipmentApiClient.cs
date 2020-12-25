using MinhDucEvent.ViewModels.Catalog.Equipments;
using MinhDucEvent.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MinhDucEvent.ApiIntegration
{
    public interface IEquipmentApiClient
    {
        Task<PagedResult<EquipmentVm>> GetPagings(GetManageEquipmentsPagingRequest request);

        Task<bool> CreateEquipment(EquipmentCreateRequest request);

        Task<bool> UpdateEquipment(EquipmentUpdateRequest request);

        Task<ApiResult<bool>> CategoryAssign(int id, EquipmentCategoryAssignRequest request);

        Task<EquipmentVm> GetById(int id, string languageId);

        Task<bool> DeleteEquipment(int id);
    }
}