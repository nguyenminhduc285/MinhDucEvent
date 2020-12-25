using MinhDucEvent.ViewModels.Common;

namespace MinhDucEvent.ViewModels.Catalog.Equipments
{
    public class GetManageEquipmentsPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }

        public string LanguageId { get; set; }

        public int? EquipmentCategoryId { get; set; }
    }
}