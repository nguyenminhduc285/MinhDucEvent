using MinhDucEvent.ViewModels.Catalog.Categories;
using MinhDucEvent.ViewModels.Common;
using MinhDucEvent.ViewModels.Sales;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MinhDucEvent.Application.Catalog.Orders
{
    public interface IOrderService
    {
        Task<CheckoutRequest> GetById(int id);

        Task<int> Create(CheckoutRequest request);

        Task<PagedResult<CheckoutRequest>> GetAllPaging(GetManageOrderPagingRequest request);
    }
}