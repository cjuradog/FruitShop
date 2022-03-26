using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Models;
using Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;


namespace Infraestructure.Test.Repositories
{
    public class ArticleRepositoryTests
    {
        private IRepository<Article> _articleRespositorySut;
        private FruitShopDbContext myContext;

        [SetUp]
        public void SetUp()
        {
            myContext = new FruitShopDbContext(new DbContextOptionsBuilder<FruitShopDbContext>().UseInMemoryDatabase("myDatabase").Options);
        }

        [Test]
        public void Add_WhenHasData_ThenArticleAdded()
        {
            using (var myContext = new FruitShopDbContext(new DbContextOptionsBuilder<FruitShopDbContext>().UseInMemoryDatabase("myDatabase").Options))
            {
                //arrange
                var article = new Article()
                {
                    ArticleId = 1,
                    ArticleTypeId = 1,
                    UnitPrice = 3,
                };
                _articleRespositorySut = new ArticleRepository(myContext);
                //act
                var result = _articleRespositorySut.Add(article);
                myContext.SaveChanges();
                //assert
                result = myContext.Article.First(x => x.ArticleId == article.ArticleId);
                Assert.IsNotNull(result);
                Assert.AreEqual(article.ArticleId, result.ArticleId);
                Assert.AreEqual(article.ArticleType, result.ArticleType);
                Assert.AreEqual(article.UnitPrice, result.UnitPrice);
            }
        }

        [Test]
        public void Delete_WhenHasData_ThenArticleDeleted()
        {
            using (var myContext = new FruitShopDbContext(new DbContextOptionsBuilder<FruitShopDbContext>().UseInMemoryDatabase("myDatabase").Options))
            {
                //arrange
                var article = new Article()
                {
                    ArticleId = 1,
                    ArticleTypeId = 1,
                    UnitPrice = 3,
                };
                _articleRespositorySut = new ArticleRepository(myContext);

                var result = _articleRespositorySut.Add(article);
                myContext.SaveChanges();
                var articleId = 1;
                var currentArticle = myContext.Article.FirstOrDefault(x => x.ArticleId == articleId);

                //act
                _articleRespositorySut.Delete(currentArticle);
                myContext.SaveChanges();

                //assert
                CollectionAssert.DoesNotContain(_articleRespositorySut.GetAll().ToList(), currentArticle);
            }
        }

        [Test]
        public void Get_WhenIdExist_ThenGetArticle()
        {
            using (var myContext = new FruitShopDbContext(new DbContextOptionsBuilder<FruitShopDbContext>().UseInMemoryDatabase("myDatabase").Options))
            {
                //arrange
                var article = new Article()
                {
                    ArticleId = 1,
                    ArticleTypeId = 1,
                    UnitPrice = 3,
                };
                _articleRespositorySut = new ArticleRepository(myContext);
                var idArticle = 1;
                var result = _articleRespositorySut.Add(article);
                myContext.SaveChanges();

                //act
                var currentArticle = myContext.Article.FirstOrDefault(x => x.ArticleId == idArticle);
                result = _articleRespositorySut.Get(currentArticle.ArticleId);

                //assert
                Assert.IsNotNull(result);
                Assert.AreEqual(currentArticle.ArticleId, result.ArticleId);
            }
        }

        [Test]
        public void Get_WhenOneArticleExist_ThenGetAllArticles()
        {
            using (var myContext = new FruitShopDbContext(new DbContextOptionsBuilder<FruitShopDbContext>().UseInMemoryDatabase("myDatabase").Options))
            {
                //arrange
                var article = new Article()
                {
                    ArticleId = 1,
                    ArticleTypeId = 1,
                    UnitPrice = 3,
                };
                var articleList = new List<Article>()
                {
                    article
                };

                //assert
                Assert.IsNotEmpty(articleList);
            }
        }

        [Test]
        public void GetAll_WhenNoData_ThenEmptyList()
        {
            using (myContext)
            {
                //arrange
                IRepository<Article> articleRespositorySut = new ArticleRepository(myContext);
                foreach (var article in articleRespositorySut.GetAll())
                {
                    articleRespositorySut.Delete(article);
                }

                //act
                var articleList = articleRespositorySut.GetAll();
                myContext.SaveChanges();

                //assert
                Assert.IsEmpty(articleList);
            }
        }

        [Test]
        public void GetById_WhenNoData_ThenRetrieveNullObject()
        {
            using (var myContext = new FruitShopDbContext(new DbContextOptionsBuilder<FruitShopDbContext>().UseInMemoryDatabase("myDatabase").Options))
            {
                //arrange
                var articleId = -1;
                IRepository<Article> articleRespositorySut = new ArticleRepository(myContext);

                //act
                var articleFound = articleRespositorySut.Get(articleId);
                myContext.SaveChanges();

                //assert
                Assert.IsNull(articleFound);
            }
        }
    }
}
