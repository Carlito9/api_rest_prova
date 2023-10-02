using apiRestProva.Db;
using apiRestProva.Entities;
using apiRestProva.Models;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text.Json;
namespace apiRestProva.Services
{
    public class CartService : ICartService
    {
        private readonly HttpClientService httpClient;
        private readonly ProvaDbContext dbContext;
        private readonly ILogger<CartService> logger;
        private decimal money;
        public CartService(HttpClientService _httpClient, ProvaDbContext _dbContext, ILogger<CartService> _logger)
        {
            httpClient = _httpClient;
            dbContext = _dbContext;
            logger = _logger;
        }

        public async Task<string> Buy(string cartId, decimal totalAmount)
        {
            var cart = await dbContext.Carts.Include(c => c.articles).FirstOrDefaultAsync(c => c.cartId == cartId);
            if (cart == null)
            {
                logger.LogError($"id carrello: {cartId} inesistente");
                throw new OutputException(new OutputError(400, "il carrello non esiste"));
            }
            logger.LogInformation("pagamento in corso");
            Pay(totalAmount);
            logger.LogInformation("pagamento effettuato");
            Random random = new Random();
            return random.Next((int)Math.Pow(10,8), (int)Math.Pow(10, 9)).ToString();

        }

        private void Pay(decimal totalAmount)
        {
            money -= totalAmount;
        }

        public async Task<string> CreateCart(string username, string deviceId)
        {
            logger.LogInformation("creazione carrello " + deviceId);
            List<char> charsToRemove = new List<char>() { ' ', '/', ':' };
            var cId = (deviceId + DateTime.UtcNow.ToString()).ToString();
            foreach (char c in charsToRemove)
            {
                cId = cId.Replace(c.ToString(), "");
            }

            var cart = new Cart
            {
                
                articles = new List<ArticleCart>(),
                expireCartDatetime = DateTime.UtcNow.AddMinutes(10),
                
                cartId = cId,
                totalAmount = 0
            };
            logger.LogInformation("carrello creato, sto aggiungendo al db");
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
            if (cart == null)
            {
                logger.LogError($"id carrello: {cartId} inesistente");
                throw new OutputException(new OutputError(400, "il carrello non esiste"));
            }
            return Task.FromResult(cart.Result.MapToDTO());
        }

        public Task<PreviewDTO> Preview(string cartId)
        {
            var cart = dbContext.Carts.Include(c => c.articles).FirstOrDefaultAsync(c => c.cartId == cartId);

            if (cart == null)
            {
                logger.LogError($"id carrello: {cartId} inesistente");
                throw new OutputException(new OutputError(400, "il carrello non esiste"));
            }
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
            if (cart == null)
            {
                logger.LogError($"id carrello: {cartId} inesistente");
                throw new OutputException(new OutputError(400, "il carrello non esiste"));
            }
            if (cart.expireCartDatetime > DateTime.UtcNow)
            {
                cart.articles.Add(aCart);
                cart.totalAmount += aCart.Quantity * aCart.Price;

                logger.LogInformation($"carrello: {cart.cartId} in aggiornamento nel db");
                await dbContext.AddArticleCart(aCart);
                dbContext.SaveChanges();
                logger.LogInformation($"carrello: {cart.cartId} in aggiornato");
            }
            else
            {
                logger.LogError("il tempo per l'acquisto è scaduto");
                throw new OutputException(new OutputError(400, "il tempo per l'acquisto è scaduto"));
            }
        }
    }
}
