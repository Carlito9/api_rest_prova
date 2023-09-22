using apiRestProva.Db;
using apiRestProva.Entities;
using apiRestProva.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Text.Json;
namespace apiRestProva.Services
{
    public class CartService : ICartService
    {
        private readonly HttpClientService httpClient;
        private readonly ProvaDbContext dbContext;

        public CartService(HttpClientService _httpClient, ProvaDbContext _dbContext)
        {
            httpClient = _httpClient;
            dbContext = _dbContext;
        }

        public Task<string> Buy(string cartId, decimal totalAmount)
        {
            Random random = new Random();
            return Task.FromResult(random.Next((int)Math.Pow(10,8), (int)Math.Pow(10, 9)).ToString());

        }

        public async Task<string> CreateCart(string username, string deviceId)
        {
            var cart = new Cart
            {
                
                articles = new List<ArticleCart>(),
                expireCartDatetime = DateTime.UtcNow.AddMinutes(10),
                cartId = deviceId + DateTime.UtcNow.ToString().Trim(),
                totalAmount = 0
            };
            
            await dbContext.AddCart(cart);
            return cart.cartId;

        }

        public async Task<List<ArticleDTO>> GetArticles()
        {

            HttpResponseMessage response = await httpClient.Get("Articoli");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<ArticleDTO>>(jsonString);

            }
            return null;
        }
        public Task<CartDTO> GetCart(string cartId)
        {
            var cart = dbContext.Carts.Include(c => c.articles).FirstOrDefaultAsync(c => c.cartId == cartId);
            return Task.FromResult(cart.Result.MapToDTO());
        }

        public Task<PreviewDTO> Preview(string cartId)
        {
            var cart = dbContext.Carts.Include(c => c.articles).FirstOrDefaultAsync(c => c.cartId == cartId);
            return Task.FromResult(cart.Result.MapToPreview());
        }

        public async Task UpdateCart(string cartId, ArticleCartDTO article)
        {
           
            var aCart = new ArticleCart
            {
                CartId = cartId,
                ArticleCode = article.ArticleCode,
                Price = article.Price,
                Quantity = article.Quantity
            };
            
            var cart = await dbContext.Carts.Include(c=>c.articles).FirstOrDefaultAsync(c=>c.cartId == cartId);
            cart.articles.Add(aCart);
            cart.totalAmount += aCart.Quantity * aCart.Price; 
       
            await dbContext.AddArticleCart(aCart);
            dbContext.SaveChanges();
        }
    }
}
