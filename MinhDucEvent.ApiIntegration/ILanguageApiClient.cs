using MinhDucEvent.ViewModels.Common;
using MinhDucEvent.ViewModels.System.Languages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhDucEvent.ApiIntegration
{
    public interface ILanguageApiClient
    {
        Task<ApiResult<List<LanguageVm>>> GetAll();
    }
}