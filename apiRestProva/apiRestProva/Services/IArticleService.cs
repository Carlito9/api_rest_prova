
using apiRestProva.Models;

namespace apiRestProva.Services
{
    public interface IArticleService
    {
        Task<List<ArticleDTO>> GetArticoli();
    }
}
