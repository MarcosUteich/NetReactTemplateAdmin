using Business.Customers.Interfaces;
using Business.Customers.Requests;
using Domain.Data;
using Domain.Data.ContextRepository;
using Domain.Entities.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Customers.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;

        public CustomerService(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> Add(CustomerRequest customer)
        {
            try
            {
                await _customerRepository.InsertAsync(
                    new Customer
                    {
                        Name = customer.Name,
                        Email = customer.Email,
                        Active = true,
                        CreatedOnUtc = DateTime.UtcNow
                    });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(string id)
        {
            try
            {
                await _customerRepository.DeleteManyAsync(w => w.Id.Equals(id));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<CustomerResponse>> GetAll()
        {
            try
            {
                var customers = await _customerRepository.GetAllAsync();
                return customers.Select(s => new CustomerResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                    Email = s.Email,
                    Active = s.Active,
                    DateCreated = s.CreatedOnUtc.ToString("dd/MM/yyyy")
                }).ToList();
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new List<CustomerResponse>());
            }
        }

        public async Task<List<CustomerResponse>> GetByName(string name)
        {
            try
            {
                var customers = await _customerRepository.GetAllAsync(w =>
                    w.Name.ToLower().Contains(name.ToLower()));

                return customers.Select(s => new CustomerResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                    Email = s.Email,
                    Active = s.Active,
                    DateCreated = s.CreatedOnUtc.ToString("dd/MM/yyyy")
                }).ToList();
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new List<CustomerResponse>());
            }
        }

        public async Task<CustomerResponse> GetById(string id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            return new CustomerResponse
            {
                Id = customer.Id,
                Email = customer.Email,
                Name = customer.Name,
                Active = customer.Active,
                DateCreated = customer.CreatedOnUtc.ToString("dd/MM/yyyy")
            };
        }

        public async Task<bool> Update(CustomerRequest customer)
        {
            try
            {
                if (customer == null)
                    throw new ArgumentNullException(nameof(customer));

                var update = UpdateBuilder<Customer>.Create()
                    .Set(x => x.Name, customer.Name)
                    .Set(x => x.Email, customer.Email)
                    .Set(x => x.Active, customer.Active);

                await _customerRepository.UpdateOneAsync(x => x.Id == customer.Id, update);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
