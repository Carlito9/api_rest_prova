using apiRestProva.Db;
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
            return null;
        }
    }
}
