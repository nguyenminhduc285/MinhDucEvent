using Microsoft.EntityFrameworkCore;
using MinhDucEvent.Data.EF;
using MinhDucEvent.Data.Entities;
using MinhDucEvent.Utilities.Constants;
using MinhDucEvent.Utilities.Exceptions;
using MinhDucEvent.ViewModels.Catalog.Categories;
using MinhDucEvent.ViewModels.Common;
using MinhDucEvent.ViewModels.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhDucEvent.Application.Catalog.Orders
{
    public class OrderService : IOrderService
    {
        private readonly MinhDucEventDbContext _context;

        public OrderService(MinhDucEventDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(CheckoutRequest request)
        {
            var orderdetails = new List<OrderDetail>();
            foreach (var i in request.OrderDetails)
            {
                orderdetails.Add(new OrderDetail()
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Price = i.Price
                });
            }

            var order = new Order()
            {
                OrderDate = request.OrderDate,
                ShipName = request.Name,
                ShipAddress = request.Address,
                ShipEmail = request.Email,
                ShipPhoneNumber = request.PhoneNumber,
                UserId = request.UserId,
                OrderDetails = orderdetails,
                Status = Data.Enums.OrderStatus.Success,
            };
            var res = await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            Console.WriteLine("order ----------------------------------------------- ", res);

            return order.Id;
        }

        public async Task<PagedResult<CheckoutRequest>> GetAllPaging(GetManageOrderPagingRequest request)
        {
            //1. select join
            var query = from oder in _context.Orders
                        join oderdetails in _context.OrderDetails on oder.Id equals oderdetails.OrderId
                        select new { oder, oderdetails };

            //2. filter
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.oder.ShipName.Contains(request.Keyword));
            //3. Paging
            int totalRow = await _context.Orders.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new CheckoutRequest()
                {
                    Id = x.oder.Id,
                    Name = x.oder.ShipName,
                    Address = x.oder.ShipAddress,
                    Email = x.oder.ShipEmail,
                    PhoneNumber = x.oder.ShipPhoneNumber,
                    UserId = x.oder.UserId,
                    OrderDate = x.oder.OrderDate,
                    OrderDetails = (List<OrderDetailVm>)x.oder.OrderDetails.Select(y => new OrderDetailVm
                    {
                        ProductId = y.ProductId,
                        Quantity = y.Quantity
                    })
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<CheckoutRequest>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
        }

        public async Task<CheckoutRequest> GetById(int id)
        {
            var query = from oder in _context.Orders
                        join oderdetails in _context.OrderDetails on oder.Id equals oderdetails.OrderId
                        where oder.Id == id
                        select new { oder, oderdetails };
            return await query.Select(x => new CheckoutRequest()
            {
                Name = x.oder.ShipName,
                Address = x.oder.ShipAddress,
                Email = x.oder.ShipEmail,
                PhoneNumber = x.oder.ShipPhoneNumber,
                UserId = x.oder.UserId,
                OrderDate = x.oder.OrderDate,
                OrderDetails = (List<OrderDetailVm>)x.oder.OrderDetails.Select(y => new OrderDetailVm
                {
                    ProductId = y.ProductId,
                    Quantity = y.Quantity
                })
            }).FirstOrDefaultAsync();
        }
    }
}