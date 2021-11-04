using Domain.Data;

namespace Domain.Entities.Products
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
