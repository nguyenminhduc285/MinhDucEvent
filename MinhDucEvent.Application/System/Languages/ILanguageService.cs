using MinhDucEvent.ViewModels.Common;
using MinhDucEvent.ViewModels.System.Languages;
using MinhDucEvent.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MinhDucEvent.Application.System.Languages
{
    public interface ILanguageService
    {
        Task<ApiResult<List<LanguageVm>>> GetAll();
    }
}