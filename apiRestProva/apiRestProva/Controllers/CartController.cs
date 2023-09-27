
using apiRestProva.Entities;
using apiRestProva.Models;
using apiRestProva.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apiRestProva.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : MyController
    {


        private readonly ICartService cartService;


        public CartController(ICartService _cartService)
        {
            cartService = _cartService;
        }

        [HttpGet("GetArticles")]
        
        public async Task<object> GetArticles()
        {
            var articoli = await cartService.GetArticles().ConfigureAwait(false);

            if (articoli == null)
            {
                return new OutputError(500, "Impossibile visualizzare gli articoli");
            }
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
            try
            {
                await cartService.UpdateCart(cartId, article).ConfigureAwait(false);
            }
            catch (OutputException oerr)
            {
                return oerr.error;
            }

            return Ok();
        }

        [HttpGet("GetCart")]
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