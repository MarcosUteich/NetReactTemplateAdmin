using Business.Customers.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Customers.Interfaces
{
    public interface ICustomerService
    {
        Task<bool> Add(CustomerRequest customer);
        Task<bool> Update(CustomerRequest customer);
        Task<bool> Delete(string id);
        Task<CustomerResponse> GetById(string id);
        Task<List<CustomerResponse>> GetAll();
        Task<List<CustomerResponse>> GetByName(string name);
    }
}
