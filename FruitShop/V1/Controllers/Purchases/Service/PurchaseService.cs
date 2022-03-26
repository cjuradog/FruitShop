using Domain.Entities;
using Domain.Interfaces;
using FruitShop.V1.Controllers.Customers.Service.Interface;
using FruitShop.V1.Controllers.Purchases.Request;
using FruitShop.V1.Controllers.Purchases.Responses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FruitShop.V1.Controllers.Customers.Service
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IRepository<Purchase> _purchaseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseService(IRepository<Purchase> purchaseRepository,
                           IUnitOfWork unitOfWork)
        {
            _purchaseRepository = purchaseRepository;
            _unitOfWork = unitOfWork;
        }
        public PurchaseResponse Submit(PurchaseRequest purchaseRequest)
        {
            try
            {
                var purchase = new Purchase()
                {
                    ArticleId = purchaseRequest.ArticleId,
                    CustomerId = purchaseRequest.CustomerId,
                    Quantity = purchaseRequest.Quantity
                };
                _purchaseRepository.Add(purchase);
                _unitOfWork.SaveChanges();

                return new PurchaseResponse(purchase);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<PurchaseResponse> GetAll()
        {
            try
            {
                return from purchase in _purchaseRepository.GetAll()
                       select new PurchaseResponse(purchase);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PurchaseResponse GetPurchaseResponse(int purchaseId)
        {
            var currentArticle = _purchaseRepository.Get(purchaseId);
            return new PurchaseResponse(currentArticle);
        }

        public bool Remove(int purchaseId)
        {
            var currentPurchase = _purchaseRepository.Get(purchaseId);
            _purchaseRepository.Delete(currentPurchase);

            if (_unitOfWork.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
