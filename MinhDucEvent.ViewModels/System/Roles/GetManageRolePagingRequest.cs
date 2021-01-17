using MinhDucEvent.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinhDucEvent.ViewModels.System.Roles
{
    public class GetManageRolePagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}