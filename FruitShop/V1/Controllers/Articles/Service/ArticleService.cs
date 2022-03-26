using Domain.Entities;
using Domain.Interfaces;
using FruitShop.V1.Controllers.Articles.Request;
using FruitShop.V1.Controllers.Articles.Response;
using FruitShop.V1.Controllers.Articles.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FruitShop.V1.Controllers.Articles.Service
{
    public class ArticleService : IArticleService
    {
        private readonly IRepository<Article> _articleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ArticleService(IRepository<Article> articleRepository, IUnitOfWork unitOfWork)
        {
            _articleRepository = articleRepository;
            _unitOfWork = unitOfWork;
        }

        public ArticleResponse Submit(ArticleRequest articleRequest)
        {
            try
            {
                var article = new Article()
                {
                    ArticleTypeId = (short)articleRequest.ArticleType,
                    UnitPrice = articleRequest.UnitPrice
                };
                article = _articleRepository.Add(article);
                _unitOfWork.SaveChanges();

                return new ArticleResponse();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<ArticleResponse> GetAll()
        {
            try
            {
                return from article in _articleRepository.GetAll()
                       select new ArticleResponse(article);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ArticleResponse GetArticleResponse(int articleId)
        {
            var currentArticle = _articleRepository.Get(articleId);
            return new ArticleResponse(currentArticle);
        }

        public bool Remove(int articleId)
        {
            var currentArticle = _articleRepository.Get(articleId);
            _articleRepository.Delete(currentArticle);

            if (_unitOfWork.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
