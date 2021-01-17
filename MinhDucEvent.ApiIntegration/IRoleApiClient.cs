using MinhDucEvent.ViewModels.Common;
using MinhDucEvent.ViewModels.System.Roles;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhDucEvent.ApiIntegration
{
    public interface IRoleApiClient
    {
        Task<ApiResult<List<RoleVm>>> GetAll();

        Task<bool> DeleteRole(Guid id);

        Task<PagedResult<RoleVm>> GetPagings(GetManageRolePagingRequest request);

        Task<bool> CreateRole(RoleCreateRequest request);

        Task<RoleVm> GetById(Guid id);

        Task<bool> UpdateRole(RoleUpdateRequest request);
    }
}