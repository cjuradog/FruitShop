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
    public class CustomerRepositoryTests
    {
        private IRepository<Customer> _customerRespositorySut;
        private FruitShopDbContext myContext;

        [SetUp]
        public void SetUp()
        {
            myContext = new FruitShopDbContext(new DbContextOptionsBuilder<FruitShopDbContext>().UseInMemoryDatabase("myDatabase").Options);
        }

        [Test]
        public void Add_WhenHasData_ThenCustomerAdded()
        {
            using (var myContext = new FruitShopDbContext(new DbContextOptionsBuilder<FruitShopDbContext>().UseInMemoryDatabase("myDatabase").Options))
            {
                //arrange
                var customer = new Customer()
                {
                    CustomerId = 5,
                    Name = "Pablo",
                    LastName = "Perez",
                    Dni = "45369755S"
                };
                _customerRespositorySut = new CustomerRepository(myContext);

                //act
                var result = _customerRespositorySut.Add(customer);
                myContext.SaveChanges();
                //assert
                result = myContext.Customer.First(x => x.CustomerId == customer.CustomerId);
                Assert.IsNotNull(result);
                Assert.AreEqual(customer.CustomerId, result.CustomerId);
                Assert.AreEqual(customer.Name, result.Name);
                Assert.AreEqual(customer.LastName, result.LastName);
                Assert.AreEqual(customer.Dni, result.Dni);
            }
        }

        [Test]
        public void Delete_WhenHasData_ThenCustomerDeleted()
        {
            using (var myContext = new FruitShopDbContext(new DbContextOptionsBuilder<FruitShopDbContext>().UseInMemoryDatabase("myDatabase").Options))
            {
                //arrange
                var customer = new Customer()
                {
                    CustomerId = 4,
                    Name = "Raul",
                    LastName = "Gomez",
                    Dni = "45333755S"
                };
                _customerRespositorySut = new CustomerRepository(myContext);

                var result = _customerRespositorySut.Add(customer);
                myContext.SaveChanges();
                var customerId = 4;
                var currentCustomer = myContext.Customer.FirstOrDefault(x => x.CustomerId == customerId);

                //act
                _customerRespositorySut.Delete(currentCustomer);
                myContext.SaveChanges();

                //assert
                CollectionAssert.DoesNotContain(_customerRespositorySut.GetAll().ToList(), currentCustomer);
            }
        }

        [Test]
        public void Get_WhenIdExist_ThenGetCustomer()
        {
            using (var myContext = new FruitShopDbContext(new DbContextOptionsBuilder<FruitShopDbContext>().UseInMemoryDatabase("myDatabase").Options))
            {
                //arrange
                var customer = new Customer()
                {
                    CustomerId = 6,
                    Name = "Sara",
                    LastName = "Mendez",
                    Dni = "45334565S"
                };
                _customerRespositorySut = new CustomerRepository(myContext);

                var customerId = 6;
                var result = _customerRespositorySut.Add(customer);
                myContext.SaveChanges();

                //act
                var currentCustomer = myContext.Customer.FirstOrDefault(x => x.CustomerId == customerId);
                result = _customerRespositorySut.Get(currentCustomer.CustomerId);

                //assert
                Assert.IsNotNull(result);
                Assert.AreEqual(currentCustomer.CustomerId, result.CustomerId);
            }
        }

        [Test]
        public void Get_WhenOneArticleExist_ThenGetAllArticles()
        {
            using (var myContext = new FruitShopDbContext(new DbContextOptionsBuilder<FruitShopDbContext>().UseInMemoryDatabase("myDatabase").Options))
            {
                //arrange
                var customer = new Customer()
                {
                    CustomerId = 7,
                    Name = "Sara",
                    LastName = "Mendez",
                    Dni = "45334565S"
                };
                var customerList = new List<Customer>()
                {
                    customer
                };

                //assert
                Assert.IsNotEmpty(customerList);
            }
        }

        [Test]
        public void GetAll_WhenNoData_ThenEmptyList()
        {
            using (myContext)
            {
                //arrange
                IRepository<Customer> customerRespositorySut = new CustomerRepository(myContext);
                foreach (var customer in customerRespositorySut.GetAll())
                {
                    customerRespositorySut.Delete(customer);
                }

                //act
                var customerList = customerRespositorySut.GetAll();
                myContext.SaveChanges();

                //assert
                Assert.IsEmpty(customerList);
            }
        }

        [Test]
        public void GetById_WhenNoData_ThenRetrieveNullObject()
        {
            using (var myContext = new FruitShopDbContext(new DbContextOptionsBuilder<FruitShopDbContext>().UseInMemoryDatabase("myDatabase").Options))
            {
                //arrange
                var customerId = -1;
                IRepository<Customer> customerRespositorySut = new CustomerRepository(myContext);

                //act
                var customerFound = customerRespositorySut.Get(customerId);
                myContext.SaveChanges();

                //assert
                Assert.IsNull(customerFound);
            }
        }
    }
}
