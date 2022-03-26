using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Models;
using System.Collections.Generic;
using System.Linq;

namespace Infraestructure.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly FruitShopDbContext _fruitStoreDbContext;

        public CustomerRepository(FruitShopDbContext fruitStoreDbContext)
        {
            _fruitStoreDbContext = fruitStoreDbContext;
        }

        public Customer Add(Customer entity)
        {
            return _fruitStoreDbContext.Customer.Add(entity).Entity;
        }

        public Customer Delete(Customer entity)
        {
            return _fruitStoreDbContext.Customer.Remove(entity).Entity;
        }

        public Customer Get(int id)
        {
            return _fruitStoreDbContext.Customer.FirstOrDefault(x => x.CustomerId == id);
        }

        public IList<Customer> GetAll()
        {
            return _fruitStoreDbContext.Customer.ToList();
        }
    }
}
