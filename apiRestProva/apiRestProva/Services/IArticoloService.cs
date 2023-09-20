
using apiRestProva.Entities;

namespace apiRestProva.Services
{
    public interface IArticoloService
    {
        Task<List<Articolo>> GetArticoli();
    }
}
