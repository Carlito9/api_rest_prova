
using apiRestProva.Entities;
using apiRestProva.Models;
using apiRestProva.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apiRestProva.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {


        private readonly ICartService cartService;

        private readonly ILogger<CartController> logger;

        public CartController(ICartService _cartService, ILogger<CartController> _logger)
        {
            cartService = _cartService;
            logger = _logger;
        }

        [HttpGet("GetArticles")]
        
        public async Task<object> GetArticles()
        {
            var articoli = await cartService.GetArticles().ConfigureAwait(false);
            logger.LogInformation("Recupero Articoli...");
            if (articoli == null)
            {
                return new OutputError(500, "Impossibile visualizzare gli articoli");
            }
            return Ok(articoli);
        }
        
        [HttpPost("CreateCart")]
       
        public async Task<IActionResult> CreateCart(string username, string deviceId)
        {
            var cart = await cartService.CreateCart(username, deviceId).ConfigureAwait(false);
            logger.LogInformation("carrello aggiunto...");
            return CreatedAtRoute("GetCart",new { cartId = cart },cart);
        }

        [HttpPost("UpdateCart")]
        public async Task<IActionResult> UpdateCart(string cartId, ArticleCartDTO article)
        {
            try
            {
                await cartService.UpdateCart(cartId, article).ConfigureAwait(false);
            }
            catch (OutputException oerr)
            {
                return oerr.error;
            }

            return NoContent();
        }
        [AllowAnonymous]
        [HttpGet("{cartId}", Name ="GetCart")]
        public async Task<IActionResult> GetCart(string cartId)
        {
            var carrello = await cartService.GetCart(cartId).ConfigureAwait(false);
            return Ok(carrello);
        }
        //stesso nome non da errore
        [HttpGet("Preview")]
        public async Task<IActionResult> Preview(string cartId)
        {
            var carrello = await cartService.Preview(cartId).ConfigureAwait(false);
            return Ok(carrello);
        }
        [HttpGet("Buy")]
        public async Task<IActionResult> Buy(string cartId, decimal total)
        {
            var transaction = await cartService.Buy(cartId,total).ConfigureAwait(false);
            return Ok(transaction);
        }
    }
}