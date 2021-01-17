using MinhDucEvent.Data.Enums;

namespace MinhDucEvent.ViewModels.Catalog.Categories
{
    public class CategoryVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SeoDescription { get; set; }
        public string SeoTitle { get; set; }
        public string SeoAlias { get; set; }
        public int? ParentId { get; set; }
        public Status Status { get; set; }
        public int SortOrder { get; set; }
    }
}