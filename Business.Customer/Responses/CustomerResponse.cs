using System;

namespace Business.Customers.Requests
{
    public class CustomerResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public string DateCreated { get; set; }
    }
}
