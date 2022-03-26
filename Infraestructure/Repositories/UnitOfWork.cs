using Domain.Interfaces;
using Infraestructure.Models;

namespace Infraestructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FruitShopDbContext _fruitStoreDbContext;

        public UnitOfWork(FruitShopDbContext fruitStoreDbContext)
        {
            _fruitStoreDbContext = fruitStoreDbContext;
        }

        public int SaveChanges()
        {
            return _fruitStoreDbContext.SaveChanges();
        }
    }
}
