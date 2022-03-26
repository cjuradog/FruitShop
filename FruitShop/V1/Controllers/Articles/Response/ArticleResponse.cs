using Domain.Entities;
using Domain.Enums;

namespace FruitShop.V1.Controllers.Articles.Response
{
    public class ArticleResponse
    {
        public ArticleResponse()
        {

        }
        public ArticleResponse(Article article)
        {
            ArticleId = article.ArticleId;
            ArticleType = (ArticleTypeEnum)article.ArticleTypeId;
        }

        public int ArticleId { get; set; }

        public ArticleTypeEnum ArticleType { get; set; }
    }
}
