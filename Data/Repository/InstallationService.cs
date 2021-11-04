using Domain.Data;
using Domain.Data.Context;
using Domain.Data.ContextRepository;
using Domain.Entities.Customers;
using Domain.Entities.Orders;
using Domain.Entities.Products;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class InstallationService : IInstallationService
    {
        private readonly IDatabaseContext _dbContext;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Product> _productRepository;

        public InstallationService(
            IDatabaseContext dbContext,
            IRepository<Customer> customerRepository,
            IRepository<Order> orderRepository,
            IRepository<Product> productRepository)
        {
            _dbContext = dbContext;
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;

        }
        private async Task CreateIndexes()
        {
            await Task.CompletedTask;

            //Product
            //await dbContext.CreateIndex(_productRepository, OrderBuilder<Product>.Create().Ascending(x => x.MarkAsNew).Ascending(x => x.CreatedOnUtc), "MarkAsNew_1_CreatedOnUtc_1");
            //await dbContext.CreateIndex(_productRepository, OrderBuilder<Product>.Create().Ascending(x => x.Published).Ascending(x => x.ShowOnHomePage).Ascending(x => x.DisplayOrder).Ascending(x => x.Name), "ShowOnHomePage_1_Published_1");
            //await dbContext.CreateIndex(_productRepository, OrderBuilder<Product>.Create().Ascending(x => x.Published).Ascending(x => x.BestSeller).Ascending(x => x.DisplayOrder).Ascending(x => x.Name), "ShowOnBestSeller_1_Published_1");
            //await dbContext.CreateIndex(_productRepository, OrderBuilder<Product>.Create().Ascending(x => x.ParentGroupedProductId).Ascending(x => x.DisplayOrder), "ParentGroupedProductId_1_DisplayOrder_1");
            //await dbContext.CreateIndex(_productRepository, OrderBuilder<Product>.Create().Ascending(x => x.ProductTags).Ascending(x => x.Published).Ascending(x => x.VisibleIndividually).Ascending(x => x.Name), "ProductTags._id_1_Name_1");
            //await dbContext.CreateIndex(_productRepository, OrderBuilder<Product>.Create().Ascending(x => x.Name), "Name_1");
            //await dbContext.CreateIndex(_productRepository, OrderBuilder<Product>.Create().Ascending("ProductCategories.DisplayOrder"), "CategoryId_1_DisplayOrder_1");
            //await dbContext.CreateIndex(_productRepository, OrderBuilder<Product>.Create().Ascending(x => x.Published).Ascending(x => x.VisibleIndividually).Ascending(x => x.DisplayOrderCategory).Ascending("ProductCategories.CategoryId"), "ProductCategories.CategoryId_1_OrderCategory_1");
            //await dbContext.CreateIndex(_productRepository, OrderBuilder<Product>.Create().Ascending(x => x.Published).Ascending(x => x.VisibleIndividually).Ascending(x => x.Name).Ascending("ProductCategories.CategoryId"), "ProductCategories.CategoryId_1_Name_1");
            //await dbContext.CreateIndex(_productRepository, OrderBuilder<Product>.Create().Ascending(x => x.Published).Ascending(x => x.VisibleIndividually).Ascending(x => x.Price).Ascending("ProductCategories.CategoryId"), "ProductCategories.CategoryId_1_Price_1");
            //await dbContext.CreateIndex(_productRepository, OrderBuilder<Product>.Create().Ascending(x => x.Published).Ascending(x => x.VisibleIndividually).Ascending(x => x.Sold).Ascending("ProductCategories.CategoryId"), "ProductCategories.CategoryId_1_Sold_1");
            //await dbContext.CreateIndex(_productRepository, OrderBuilder<Product>.Create().Ascending(x => x.Published).Ascending(x => x.VisibleIndividually).Ascending("ProductCategories.CategoryId").Ascending("ProductCategories.IsFeaturedProduct"), "ProductCategories.CategoryId_1_IsFeaturedProduct_1");
            //await dbContext.CreateIndex(_productRepository, OrderBuilder<Product>.Create().Ascending("ProductCollections.CollectionId").Ascending(x => x.Published).Ascending(x => x.VisibleIndividually).Ascending(x => x.DisplayOrderCollection), "ProductCollections.CollectionId_1_OrderCategory_1");
            //await dbContext.CreateIndex(_productRepository, OrderBuilder<Product>.Create().Ascending("ProductCollections.CollectionId").Ascending(x => x.Published).Ascending(x => x.VisibleIndividually).Ascending(x => x.Name), "ProductCollections.CollectionId_1_Name_1");
            //await dbContext.CreateIndex(_productRepository, OrderBuilder<Product>.Create().Ascending("ProductCollections.CollectionId").Ascending(x => x.Published).Ascending(x => x.VisibleIndividually).Ascending(x => x.Sold), "ProductCollections.CollectionId_1_Sold_1");
            //await dbContext.CreateIndex(_productRepository, OrderBuilder<Product>.Create().Ascending("ProductCollections.CollectionId").Ascending("ProductCollections.IsFeaturedProduct").Ascending(x => x.Published).Ascending(x => x.VisibleIndividually), "ProductCollections.CollectionId_1_IsFeaturedProduct_1");
            //await dbContext.CreateIndex(_productRepository, OrderBuilder<Product>.Create().Ascending(x => x.Published).Ascending(x => x.VisibleIndividually).Ascending(x => x.BrandId).Ascending(x => x.DisplayOrderBrand), "ProductBrand");
            //await dbContext.CreateIndex(_productRepository, OrderBuilder<Product>.Create().Ascending(x => x.Published).Ascending(x => x.VisibleIndividually).Ascending("ProductSpecificationAttributes.SpecificationAttributeOptionId").Ascending("ProductSpecificationAttributes.AllowFiltering"), "ProductSpecificationAttributes");
            //await dbContext.CreateIndex(_productRepository, OrderBuilder<Product>.Create().Ascending(x => x.DisplayOrder).Ascending(x => x.Name), "DisplayOrder_Name");
        }
        private async Task CreateTables(string collation)
        {
            try
            {
                var classes = Assembly.Load("Domain").GetTypes();
                foreach (var c in classes)
                {
                    if (c.BaseType != null && c.IsClass && c.BaseType == typeof(BaseEntity))
                        await _dbContext.CreateTable(c.Name, collation);
                }

                await CreateIndexes();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InstallData(string collation)
        {
            await CreateTables(collation);
            await PopulateData();
        }
        private async Task PopulateData()
        {
            //await PopulateClients();
            //await PopulateProducts();
            //await PopulateOrders();
        }

        private async Task PopulateClients()
        {
            await _customerRepository.InsertManyAsync(
                new List<Customer> {
                    new Customer
                    {
                        Name = "Paulo Miranda",
                        Active = true,
                        Email = "paulomirando@gmail.com",
                        CreatedOnUtc = DateTime.UtcNow
                    },
                    new Customer
                    {
                        Name = "Magda Rodrigues",
                        Active = true,
                        Email = "magda@gmail.com",
                        CreatedOnUtc = DateTime.UtcNow
                    },
                    new Customer
                    {
                        Name = "Carlos Eduardo",
                        Active = true,
                        Email = "carlos@gmail.com",
                        CreatedOnUtc = DateTime.UtcNow
                    },
                    new Customer
                    {
                        Name = "Maria Fernandes",
                        Active = true,
                        Email = "maria@gmail.com",
                        CreatedOnUtc = DateTime.UtcNow
                    },
                    new Customer
                    {
                        Name = "Renato Fagundes",
                        Active = true,
                        Email = "renato@gmail.com",
                        CreatedOnUtc = DateTime.UtcNow
                    },
                    new Customer
                    {
                        Name = "Carlos Eduardo",
                        Active = true,
                        Email = "carlos@gmail.com",
                        CreatedOnUtc = DateTime.UtcNow
                    },
                    new Customer
                    {
                        Name = "Marcio Rodrigues",
                        Active = true,
                        Email = "marcio@gmail.com",
                        CreatedOnUtc = DateTime.UtcNow
                    },
                    new Customer
                    {
                        Name = "Palo Ernanes",
                        Active = true,
                        Email = "paulo@gmail.com",
                        CreatedOnUtc = DateTime.UtcNow
                    },
                    new Customer
                    {
                        Name = "Marcos Uteich",
                        Active = true,
                        Email = "marcos.uteich@gmail.com",
                        CreatedOnUtc = DateTime.UtcNow
                    },
                });
        }

        private async Task PopulateProducts()
        {
            await _productRepository.InsertManyAsync(new List<Product> { 
                new Product
                {
                    Name = "Caneca de Procelana",
                    Price = 10.00,
                    CreatedOnUtc = DateTime.UtcNow
                },
            new Product
                {
                    Name = "Tv Smart 55",
                    Price = 4500.00,
                    CreatedOnUtc = DateTime.UtcNow
                },
            new Product
                {
                    Name = "XBox One",
                    Price = 1899.00,
                    CreatedOnUtc = DateTime.UtcNow
                },
            new Product
                {
                    Name = "Sofa 4 Lugares",
                    Price = 1999.00,
                    CreatedOnUtc = DateTime.UtcNow
                },
            new Product
                {
                    Name = "Fogão 4 Bocas",
                    Price = 487.00,
                    CreatedOnUtc = DateTime.UtcNow
                },
            new Product
                {
                    Name = "Lampada",
                    Price = 13.00,
                    CreatedOnUtc = DateTime.UtcNow
                },
            new Product
                {
                    Name = "Bicicleta Aro 15",
                    Price = 745.00,
                    CreatedOnUtc = DateTime.UtcNow
                },
            new Product
                {
                    Name = "Ar Condicionado",
                    Price = 999.00,
                    CreatedOnUtc = DateTime.UtcNow
                },
            new Product
                {
                    Name = "Liquitificador",
                    Price = 79.00,
                    CreatedOnUtc = DateTime.UtcNow
                },
            new Product
                {
                    Name = "Radio",
                    Price = 387.00,
                    CreatedOnUtc = DateTime.UtcNow
                },
            new Product
                {
                    Name = "Notebook",
                    Price = 7200.00,
                    CreatedOnUtc = DateTime.UtcNow
                },
            new Product
                {
                    Name = "Geladeira",
                    Price = 1599.00,
                    CreatedOnUtc = DateTime.UtcNow
                },
            });
        }

        private async Task PopulateOrders()
        {
            
        }
    }
}
