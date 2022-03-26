using Domain.Entities;

namespace FruitShop.V1.Controllers.Purchases.Responses
{
    public class PurchaseResponse
    {
        public PurchaseResponse(Purchase purchase)
        {
            PurchaseId = purchase.PurchaseId;
        }

        public int PurchaseId { get; set; }
    }
}
