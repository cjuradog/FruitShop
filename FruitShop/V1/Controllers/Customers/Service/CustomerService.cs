using Domain.Entities;
using Domain.Interfaces;
using FruitShop.V1.Controllers.Customers.Request;
using FruitShop.V1.Controllers.Customers.Response;
using FruitShop.V1.Controllers.Customers.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FruitShop.V1.Controllers.Customers.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CustomerService(IRepository<Customer> customerRepository,
                                  IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public CustomerResponse Submit(CustomerRequest customerRequest)
        {
            try
            {
                var customer = new Customer()
                {
                    Name = customerRequest.Name,
                    LastName = customerRequest.LastName,
                    Dni = customerRequest.Dni,
                };
                _customerRepository.Add(customer);
                _unitOfWork.SaveChanges();

                return new CustomerResponse(customer);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<CustomerResponse> GetAll()
        {
            try
            {
                return from customer in _customerRepository.GetAll()
                       select new CustomerResponse(customer);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CustomerResponse GetCustomerResponse(int customerId)
        {
            var currentCustomer = _customerRepository.Get(customerId);
            return new CustomerResponse(currentCustomer);
        }

        public bool Remove(int customerId)
        {
            var currentCustomer = _customerRepository.Get(customerId);
            _customerRepository.Delete(currentCustomer);

            if (_unitOfWork.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
