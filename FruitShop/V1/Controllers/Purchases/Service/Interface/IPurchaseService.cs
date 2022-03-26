using FruitShop.V1.Controllers.Purchases.Request;
using FruitShop.V1.Controllers.Purchases.Responses;
using System.Collections.Generic;

namespace FruitShop.V1.Controllers.Customers.Service.Interface
{
    public interface IPurchaseService
    {
        PurchaseResponse Submit(PurchaseRequest purchaseRequest);

        IEnumerable<PurchaseResponse> GetAll();

        PurchaseResponse GetPurchaseResponse(int purchaseId);

        bool Remove(int purchaseId);
    }
}
