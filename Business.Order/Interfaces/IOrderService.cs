using Business.Sales.Response.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Sales.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderResponse>> GetOrders();
        Task AddOrder(OrderRequest orderRequest);
        Task<bool> UpdateOrder(OrderRequest orderRequest);
        Task<bool> RemoveOrder(string id);
        Task<object> GetStatus();
        Task<object> GetCustomers();
    }
}
