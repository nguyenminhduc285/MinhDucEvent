using MinhDucEvent.ViewModels.Common;
using MinhDucEvent.ViewModels.System.Roles;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhDucEvent.Application.System.Roles
{
    public interface IRoleService
    {
        Task<List<RoleVm>> GetAll();

        Task<RoleVm> GetById(Guid id);

        Task<Guid> Create(RoleCreateRequest request);

        Task<bool> Update(RoleUpdateRequest request);

        Task<bool> Delete(Guid RoleId);

        Task<PagedResult<RoleVm>> GetAllPaging(GetManageRolePagingRequest request);
    }
}