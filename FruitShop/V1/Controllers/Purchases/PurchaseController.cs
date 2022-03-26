using FruitShop.V1.Controllers.Customers.Service.Interface;
using FruitShop.V1.Controllers.Purchases.Request;
using FruitShop.V1.Controllers.Purchases.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FruitShop.V1.Controllers.Purchases
{
    [Route("api/v1/purchases")]
    [ApiController]
    public class PurchaseController
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<PurchaseResponse> Submit([FromBody] PurchaseRequest purchaseRequest)
        {
            var result = _purchaseService.Submit(purchaseRequest);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<PurchaseResponse>> GetAll()
        {
            var result = _purchaseService.GetAll();
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpGet("{purchaseId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<PurchaseResponse> GetArticle(int purchaseId)
        {
            var result = _purchaseService.GetPurchaseResponse(purchaseId);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpDelete("{purchaseId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<PurchaseResponse> Remove(int articleId)
        {
            var result = _purchaseService.Remove(articleId);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }
    }
}
