using Domain.Entities;

namespace FruitShop.V1.Controllers.Customers.Response
{
    public class CustomerResponse
    {
        public CustomerResponse()
        {

        }
        public CustomerResponse(Customer customer)
        {
            Name = customer.Name;
            LastName = customer.LastName;
        }
        public string Name { get; set; }

        public string LastName { get; set; }
    }
}
