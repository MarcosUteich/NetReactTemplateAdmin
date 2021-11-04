using System.Threading.Tasks;

namespace Business.Dashboard.Interfaces
{
    public interface IOrderDashboardService
    {
        Task<object> GetOrdersHistory();
    }
}
