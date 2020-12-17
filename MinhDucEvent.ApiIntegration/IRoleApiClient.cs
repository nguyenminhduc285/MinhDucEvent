using MinhDucEvent.ViewModels.Common;
using MinhDucEvent.ViewModels.System.Roles;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhDucEvent.ApiIntegration
{
    public interface IRoleApiClient
    {
        Task<ApiResult<List<RoleVm>>> GetAll();
    }
}