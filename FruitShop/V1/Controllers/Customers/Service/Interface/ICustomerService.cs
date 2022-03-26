using FruitShop.V1.Controllers.Customers.Request;
using FruitShop.V1.Controllers.Customers.Response;
using System.Collections.Generic;

namespace FruitShop.V1.Controllers.Customers.Service.Interface
{
    public interface ICustomerService
    {
        CustomerResponse Submit(CustomerRequest customerRequest);

        IEnumerable<CustomerResponse> GetAll();

        CustomerResponse GetCustomerResponse(int customerId);

        bool Remove(int customerId);
    }
}
