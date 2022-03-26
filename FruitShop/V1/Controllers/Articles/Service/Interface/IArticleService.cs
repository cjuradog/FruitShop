using FruitShop.V1.Controllers.Articles.Request;
using FruitShop.V1.Controllers.Articles.Response;
using System.Collections.Generic;

namespace FruitShop.V1.Controllers.Articles.Service.Interface
{
    public interface IArticleService
    {
        ArticleResponse Submit(ArticleRequest articleRequest);

        IEnumerable<ArticleResponse> GetAll();

        ArticleResponse GetArticleResponse(int articleId);

        bool Remove(int articleId);
    }
}
