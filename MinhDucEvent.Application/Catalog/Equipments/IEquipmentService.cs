﻿using MinhDucEvent.ViewModels.Catalog.EquipmentImages;
using MinhDucEvent.ViewModels.Catalog.Equipments;
using MinhDucEvent.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhDucEvent.Application.Catalog.Equipments
{
    public interface IEquipmentService
    {
        Task<int> Create(EquipmentCreateRequest request);

        Task<int> Update(EquipmentUpdateRequest request);

        Task<int> Delete(int equipmentId);

        Task<EquipmentVm> GetById(int equipmentId, string languageId);

        Task<bool> UpdateStock(int equipmentId, int addedQuantity);

        Task<PagedResult<EquipmentVm>> GetAllPaging(GetManageEquipmentsPagingRequest request);

        Task<int> AddImage(int equipmentId, EquipmentImageCreateRequest request);

        Task<int> RemoveImage(int imageId);

        Task<int> UpdateImage(int imageId, EquipmentImageUpdateRequest request);

        Task<EquipmentImageViewModel> GetImageById(int imageId);

        Task<List<EquipmentImageViewModel>> GetListImages(int equipmentId);

        Task<ApiResult<bool>> CategoryAssign(int id, EquipmentCategoryAssignRequest request);

        Task<List<EquipmentVm>> GetAll(string languageId);
    }
}