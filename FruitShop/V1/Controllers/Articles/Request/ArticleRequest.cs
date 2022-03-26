using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FruitShop.V1.Controllers.Articles.Request
{
    public class ArticleRequest
    {
        public ArticleTypeEnum ArticleType { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public bool AvaiableStock { get; set; }
    }
}
