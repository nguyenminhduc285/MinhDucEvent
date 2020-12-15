using MinhDucEvent.ViewModels.Common;

namespace MinhDucEvent.ViewModels.System.Users
{
    public class GetUserPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}