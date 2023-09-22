using apiRestProva.Db;
using apiRestProva.Entities;
using apiRestProva.Models;
using Microsoft.EntityFrameworkCore;

namespace apiRestProva.Services
{
    public class ArticleService : IArticleService
    {
        private readonly ProvaDbContext dbContext;
        public ArticleService(ProvaDbContext _dbContext)
        {
            dbContext = _dbContext;
            dbContext.FillDB();
        }
        public async Task<List<ArticleDTO>> GetArticoli()
        {
            var articles = await dbContext.Articles.ToListAsync();
            return MapToDto(articles);
        }
        public List<ArticleDTO> MapToDto(List<Article> Articles)
        {
            List<ArticleDTO> articlesDTO = new List<ArticleDTO>();
            foreach (Article art in Articles)
            {
                var artDTO = new ArticleDTO
                {
                    articleCode = art.articleCode,
                    articleDescription = art.articleDescription,
                    carrierCode = art.carrierCode,
                    carrierName = art.carrierName,
                    price = art.price,
                    maxQuantity = art.maxQuantity

                };
                articlesDTO.Add(artDTO);
            }
            return articlesDTO;
        }
    }
}
