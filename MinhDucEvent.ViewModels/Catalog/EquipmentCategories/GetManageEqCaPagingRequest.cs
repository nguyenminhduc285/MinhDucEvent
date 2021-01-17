using MinhDucEvent.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinhDucEvent.ViewModels.Catalog.EquipmentCategories
{
    public class GetManageEqCaPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }

        public string LanguageId { get; set; }
    }
}