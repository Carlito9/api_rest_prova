using apiRestProva.Entities;

namespace apiRestProva.Services
{
    public interface IAuthService
    {
        Task<AuthToken> LoginAsync(LoginRequest request);
        Task<bool> LogoutAsync(AuthToken authToken);
    }
}

