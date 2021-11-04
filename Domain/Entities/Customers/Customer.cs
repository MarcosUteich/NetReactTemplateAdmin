using Domain.Data;

namespace Domain.Entities.Customers
{
    public partial class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
    }
}
