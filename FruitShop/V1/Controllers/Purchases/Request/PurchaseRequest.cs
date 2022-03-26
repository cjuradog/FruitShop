namespace FruitShop.V1.Controllers.Purchases.Request
{
    public class PurchaseRequest
    {
        public int CustomerId { get; set; }

        public int ArticleId { get; set; }

        public int Quantity { get; set; }
    }
}
