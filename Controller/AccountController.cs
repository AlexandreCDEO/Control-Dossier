using Control_Dossier.Data;
using Control_Dossier.Service;
using Microsoft.AspNetCore.Mvc;

namespace Control_Dossier.Controller;

[ApiController]
public class AccountController : ControllerBase
{
    [HttpPost("v1/login")]
    public IActionResult Login(
        [FromServices] AppDbContext context
        )
    {
        var tokenService = new TokenService();
        var user = context.Users.FirstOrDefault();
        var token = tokenService.GenerateToken(user);
        return Ok(token);
    }
}