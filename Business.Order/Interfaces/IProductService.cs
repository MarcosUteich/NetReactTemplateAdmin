using Business.Sales.Request.Products;
using Business.Sales.Response.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Sales.Interfaces
{
    public interface IProductService
    {
        Task<bool> Add(ProductRequest customer);
        Task<bool> Update(ProductRequest customer);
        Task<bool> Delete(string id);
        Task<ProductResponse> GetById(string id);
        Task<List<ProductResponse>> GetAll();
        Task<List<ProductResponse>> GetByName(string name);
    }
}
