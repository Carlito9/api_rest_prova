using apiRestProva.Entities;
using apiRestProva.Models;

namespace apiRestProva.Services
{
    public interface ICartService
    {
        Task<List<ArticleDTO>> GetArticles();

        Task<string> CreateCart(string username, string devideId);
        Task<CartDTO> GetCart(string cartId);
        Task UpdateCart(string cartId, ArticleCartDTO article);

        Task<Cart> Preview(string cartId);
        Task<string> Buy(string cartId, decimal totalAmount);
    }
}
