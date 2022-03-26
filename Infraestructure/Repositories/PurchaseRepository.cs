using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Models;
using System.Collections.Generic;
using System.Linq;

namespace Infraestructure.Repositories
{
    public class PurchaseRepository : IRepository<Purchase>
    {
        private readonly FruitShopDbContext _fruitStoreDbContext;

        public PurchaseRepository(FruitShopDbContext fruitStoreDbContext)
        {
            _fruitStoreDbContext = fruitStoreDbContext;
        }

        public Purchase Add(Purchase entity)
        {
            return _fruitStoreDbContext.Purchase.Add(entity).Entity;
        }

        public Purchase Delete(Purchase entity)
        {
            return _fruitStoreDbContext.Purchase.Remove(entity).Entity;
        }

        public Purchase Get(int id)
        {
            return _fruitStoreDbContext.Purchase.FirstOrDefault(x => x.PurchaseId == id);
        }

        public IList<Purchase> GetAll()
        {
            return _fruitStoreDbContext.Purchase.ToList();
        }
    }
}
