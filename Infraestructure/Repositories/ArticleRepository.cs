using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Models;
using System.Collections.Generic;
using System.Linq;

namespace Infraestructure.Repositories
{
    public class ArticleRepository : IRepository<Article>
    {
        private readonly FruitShopDbContext _fruitStoreDbContext;

        public ArticleRepository(FruitShopDbContext fruitStoreDbContext)
        {
            _fruitStoreDbContext = fruitStoreDbContext;
        }

        public Article Add(Article entity)
        {
            return _fruitStoreDbContext.Article.Add(entity).Entity;
        }

        public Article Delete(Article entity)
        {
            return _fruitStoreDbContext.Article.Remove(entity).Entity;
        }

        public Article Get(int id)
        {
            return _fruitStoreDbContext.Article.FirstOrDefault(x => x.ArticleId == id);
        }

        public IList<Article> GetAll()
        {
            return _fruitStoreDbContext.Article.ToList();
        }
    }
}
