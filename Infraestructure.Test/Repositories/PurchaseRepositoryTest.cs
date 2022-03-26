using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Models;
using Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infraestructure.Test.Repositories
{
    public class PurchaseRepositoryTests
    {
        private IRepository<Purchase> _purchaseRespositorySut;
        private FruitShopDbContext myContext;

        [SetUp]
        public void SetUp()
        {
            myContext = new FruitShopDbContext(new DbContextOptionsBuilder<FruitShopDbContext>().UseInMemoryDatabase("myDatabase").Options);
        }

        [Test]
        public void Add_WhenHasData_ThenPurchaseAdded()
        {
            using (var myContext = new FruitShopDbContext(new DbContextOptionsBuilder<FruitShopDbContext>().UseInMemoryDatabase("myDatabase").Options))
            {
                //arrange
                var purchase = new Purchase()
                {
                    PurchaseId = 1,
                    ArticleId =1,
                    CustomerId =1
                };
                _purchaseRespositorySut = new PurchaseRepository(myContext);

                //act
                var result = _purchaseRespositorySut.Add(purchase);
                myContext.SaveChanges();
                //assert
                result = myContext.Purchase.First(x => x.PurchaseId == purchase.PurchaseId);
                Assert.IsNotNull(result);
                Assert.AreEqual(purchase.CustomerId, result.CustomerId);
                Assert.AreEqual(purchase.PurchaseId, result.PurchaseId);
                Assert.AreEqual(purchase.ArticleId, result.ArticleId);
            }
        }

        [Test]
        public void Delete_WhenHasData_ThenPurchaseDeleted()
        {
            using (var myContext = new FruitShopDbContext(new DbContextOptionsBuilder<FruitShopDbContext>().UseInMemoryDatabase("myDatabase").Options))
            {
                //arrange
                var purchase = new Purchase()
                {
                    PurchaseId = 2,
                    ArticleId = 2,
                    CustomerId = 2
                };
                _purchaseRespositorySut = new PurchaseRepository(myContext);

                var result = _purchaseRespositorySut.Add(purchase);
                myContext.SaveChanges();
                var purchaseId = 2;
                var currentPurchase = myContext.Purchase.FirstOrDefault(x => x.PurchaseId == purchaseId);

                //act
                _purchaseRespositorySut.Delete(currentPurchase);
                myContext.SaveChanges();

                //assert
                CollectionAssert.DoesNotContain(_purchaseRespositorySut.GetAll().ToList(), currentPurchase);
            }
        }

        [Test]
        public void Get_WhenIdExist_ThenGetPurchase()
        {
            using (var myContext = new FruitShopDbContext(new DbContextOptionsBuilder<FruitShopDbContext>().UseInMemoryDatabase("myDatabase").Options))
            {
                //arrange
                var purchase = new Purchase()
                {
                    PurchaseId = 3,
                    ArticleId = 3,
                    CustomerId = 3
                };
                _purchaseRespositorySut = new PurchaseRepository(myContext);

                var purchaseId = 3;
                var result = _purchaseRespositorySut.Add(purchase);
                myContext.SaveChanges();

                //act
                var currentPurchase = myContext.Purchase.FirstOrDefault(x => x.PurchaseId == purchaseId);
                result = _purchaseRespositorySut.Get(currentPurchase.PurchaseId);

                //assert
                Assert.IsNotNull(result);
                Assert.AreEqual(currentPurchase.PurchaseId, result.PurchaseId);
            }
        }

        [Test]
        public void Get_WhenOnePurchaseExist_ThenGetAllPurchase()
        {
            using (var myContext = new FruitShopDbContext(new DbContextOptionsBuilder<FruitShopDbContext>().UseInMemoryDatabase("myDatabase").Options))
            {
                //arrange
                var purchase = new Purchase()
                {
                    PurchaseId = 3,
                    ArticleId = 3,
                    CustomerId = 3
                };
                var purchaseList = new List<Purchase>()
                {
                    purchase
                };

                //assert
                Assert.IsNotEmpty(purchaseList);
            }
        }

        [Test]
        public void GetAll_WhenNoData_ThenEmptyList()
        {
            using (var myContext = new FruitShopDbContext(new DbContextOptionsBuilder<FruitShopDbContext>().UseInMemoryDatabase("myDatabase").Options))
            {
                //arrange
                IRepository<Purchase> purchaseRespositorySut = new PurchaseRepository(myContext);
                foreach (var purchase in purchaseRespositorySut.GetAll())
                {
                    purchaseRespositorySut.Delete(purchase);
                }

                //act
                var purchsaeList = purchaseRespositorySut.GetAll();
                myContext.SaveChanges();

                //assert
                Assert.IsEmpty(purchsaeList);
            }
        }

        [Test]
        public void GetById_WhenNoData_ThenRetrieveNullObject()
        {
            using (var myContext = new FruitShopDbContext(new DbContextOptionsBuilder<FruitShopDbContext>().UseInMemoryDatabase("myDatabase").Options))
            {
                //arrange
                var purchaseId = -1;
                IRepository<Purchase> purchaseRespositorySut = new PurchaseRepository(myContext);

                //act
                var purchaseFound = purchaseRespositorySut.Get(purchaseId);
                myContext.SaveChanges();

                //assert
                Assert.IsNull(purchaseFound);
            }
        }
    }
}
