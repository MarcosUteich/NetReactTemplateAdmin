using Business.Sales.Interfaces;
using Business.Sales.Request.Products;
using Business.Sales.Response.Products;
using Domain.Data;
using Domain.Data.ContextRepository;
using Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Sales.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<bool> Add(ProductRequest productRequest)
        {
            try
            {
                await _productRepository.InsertAsync(new Product { 
                    Name = productRequest.Name,
                    Price = productRequest.Price,
                    CreatedOnUtc =DateTime.UtcNow
                });

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> Delete(string id)
        {
            try
            {
                await _productRepository.DeleteManyAsync(w => w.Id.Equals(id));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<ProductResponse>> GetAll()
        {
            try
            {
                var products = await _productRepository.GetAllAsync();
                return products.Select(s => new ProductResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                    Price = s.Price,
                    DateCreated = s.CreatedOnUtc.ToString("dd/MM/yyyy")
                }).ToList();
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new List<ProductResponse>());
            }
        }

        public async Task<ProductResponse> GetById(string id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                DateCreated = product.CreatedOnUtc.ToString("dd/MM/yyyy")
            };
        }

        public async Task<List<ProductResponse>> GetByName(string name)
        {
            try
            {
                var product = await _productRepository.GetAllAsync(w =>
                    w.Name.ToLower().Contains(name.ToLower()));

                return product.Select(s => new ProductResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                    Price = s.Price,
                    DateCreated = s.CreatedOnUtc.ToString("dd/MM/yyyy")
                }).ToList();
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new List<ProductResponse>());
            }
        }

        public async Task<bool> Update(ProductRequest product)
        {
            try
            {
                if (product == null)
                    throw new ArgumentNullException(nameof(product));

                var update = UpdateBuilder<Product>.Create()
                    .Set(x => x.Name, product.Name)
                    .Set(x => x.Price, product.Price);

                await _productRepository.UpdateOneAsync(x => x.Id == product.Id, update);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
