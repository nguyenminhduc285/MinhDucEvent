using MinhDucEvent.ViewModels.Common;
using MinhDucEvent.ViewModels.Sales;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MinhDucEvent.ApiIntegration
{
    public interface IOrderApiCLient
    {
        Task<CheckoutRequest> GetById(int id);

        Task<bool> CreateOrder(CheckoutRequest request);

        Task<PagedResult<CheckoutRequest>> GetPagings(GetManageOrderPagingRequest request);
    }
}