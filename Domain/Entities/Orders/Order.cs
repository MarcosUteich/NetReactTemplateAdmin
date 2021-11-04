using Domain.Data;
using Domain.Entities.Customers;
using Domain.Entities.Products;
using System.Collections.Generic;

namespace Domain.Entities.Orders
{
    public class Order : BaseEntity
    {
        public OrderStatusEnum Status { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public double Total { get; set; }
    }
}