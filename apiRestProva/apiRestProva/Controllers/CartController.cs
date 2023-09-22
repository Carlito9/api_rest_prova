
using apiRestProva.Entities;
using apiRestProva.Models;
using apiRestProva.Services;
using Microsoft.AspNetCore.Mvc;

namespace apiRestProva.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {


        private readonly ICartService cartService;


        public CartController(ICartService _cartService)
        {
            cartService = _cartService;
        }

        [HttpGet("GetArticles")]
        public async Task<IActionResult> GetArticles()
        {
            var articoli = await cartService.GetArticles().ConfigureAwait(false);

            return Ok(articoli);
        }

        [HttpPost("CreateCart")]
        public async Task<IActionResult> CreateCart(string username, string deviceId)
        {
            var cartId = await cartService.CreateCart(username, deviceId).ConfigureAwait(false);

            return Ok(cartId);
        }

        [HttpPost("UpdateCart")]
        public async Task<IActionResult> UpdateCart(string cartId, ArticleCartDTO article)
        {
            await cartService.UpdateCart(cartId, article).ConfigureAwait(false);

            return Ok();
        }

        //stesso nome non da errore
        [HttpGet("GetCart")]
        public async Task<IActionResult> GetCart(string cartId)
        {
            var carrello = await cartService.GetCart(cartId).ConfigureAwait(false);
            return Ok(carrello);
        }
    }
}