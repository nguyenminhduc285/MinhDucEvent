using MinhDucEvent.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinhDucEvent.ViewModels.Sales
{
    public class GetManageOrderPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}