using Business.Dashboard.Interfaces;
using Domain.Data.ContextRepository;
using Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dashboard.Services
{
    public class OrderDashboardService : IOrderDashboardService
    {
        private readonly IRepository<Order> _orderRespository;

        public OrderDashboardService(IRepository<Order> orderRespository)
        {
            _orderRespository = orderRespository;
        }

        public async Task<object> GetOrdersHistory()
        {
            List<object> result = new List<object>();
            var orders = await _orderRespository.GetAllAsync();

            var data = orders.GroupBy(w => new { w.Status, w.CreatedOnUtc.Month }).Select(s => new
            {
                Status = s.First().Status.ToString(),
                Month = s.First().CreatedOnUtc.Month,
                Total = s.Sum(s => s.Total)
            }).GroupBy(e => e.Status).ToList();

            foreach (var item in data)
            {
                result.Add(new { 
                    name = item.First().Status,
                    data = item.Select(s => s.Total).ToArray()
                });
            }

            return result;
        }
    }
}
