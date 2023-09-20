using apiRestProva.Db;
using apiRestProva.Entities;
using Microsoft.EntityFrameworkCore;

namespace apiRestProva.Services
{
    public class ArticoloService : IArticoloService
    {
        private readonly ProvaDbContext dbContext;
        public ArticoloService(ProvaDbContext _dbContext)
        {
            dbContext = _dbContext;
            dbContext.FillDB();
        }
        public async Task<List<Articolo>> GetArticoli()
        {
            return dbContext.Articoli.ToList();
        }
    }
}
