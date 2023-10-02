using apiRestProva.Entities;
using apiRestProva.Models;
using apiRestProva.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apiRestProva.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        
        public AuthController(IAuthService _authService)
        {
            authService = _authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]      
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var loginResponse = await authService.LoginAsync(request);
            if (loginResponse != null)
            {
                return Ok(loginResponse);
            }

            return new OutputError(400,"password errata");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(string token)
        {
            var logoutResponse = await authService.LogoutAsync(new AuthToken() { AccessToken=token});
            if (logoutResponse == true)
            {
                return Ok(logoutResponse);
            }

            return BadRequest("password errata");
        }
    }
}
