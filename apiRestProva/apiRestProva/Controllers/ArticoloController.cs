
using apiRestProva.Entities;
using apiRestProva.Services;
using Microsoft.AspNetCore.Mvc;

namespace apiRestProva.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticoloController : ControllerBase
    {
        

        private readonly IArticoloService articoloService;

        public ArticoloController(IArticoloService _articoloService)
        {
            articoloService = _articoloService;
        }

        [HttpGet("Articoli")]
        public async Task<IActionResult> GetArticoli()
        {
            var articoli = await articoloService.GetArticoli().ConfigureAwait(false);

            return Ok(articoli);
        }
    }
}