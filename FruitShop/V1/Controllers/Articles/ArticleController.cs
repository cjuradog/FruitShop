using FruitShop.V1.Controllers.Articles.Request;
using FruitShop.V1.Controllers.Articles.Response;
using FruitShop.V1.Controllers.Articles.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FruitShop.V1.Controllers.Articles
{
    [Route("api/v1/articles")]
    [ApiController]
    public class ArticleController
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<ArticleResponse> Submit([FromBody] ArticleRequest articleRequest)
        {
            var result = _articleService.Submit(articleRequest);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ArticleRequest>> GetAll()
        {
            var result = _articleService.GetAll();
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpGet("{articleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ArticleResponse> GetArticle(int articleId)
        {
            var result = _articleService.GetArticleResponse(articleId);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpDelete("{articleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ArticleResponse> Remove(int articleId)
        {
            var result = _articleService.Remove(articleId);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }
    }
}
