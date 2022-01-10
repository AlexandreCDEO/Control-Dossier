using Control_Dossier.Data;
using Control_Dossier.Extensions;
using Control_Dossier.Models;
using Control_Dossier.Services;
using Control_Dossier.ViewModels;
using Control_Dossier.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace Control_Dossier.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    [HttpPost("v1/accounts")]
    public async Task<IActionResult> CreateAccount(
        [FromBody] RegisterViewModel model,
        [FromServices] AppDbContext context
        )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
        }

        var user = new User
        {
            Name = model.Name,
            Email = model.Email,
            Password = PasswordHasher.Hash(model.Password)
        };

        try
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return Ok(new ResultViewModel<dynamic>(
                new
                {
                    Name = user.Name,
                    Email = user.Email
                }));

        }
        catch (DbUpdateException e)
        {
            return StatusCode(500, new ResultViewModel<string>("02E01 - E-mail já cadastrado"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<string>("02E02 - Falha interna do servidor"));

        }

    }



    [HttpPost("v1/accounts/login")]
    public async Task<IActionResult> LoginAsync(
        [FromServices] AppDbContext context,
        [FromBody] LoginViewModel model,
        [FromServices] TokenService tokenService
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
        }

        var user = await context
            .Users
            .AsNoTracking()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Email == model.Email);

        if (user == null)
        {
            return StatusCode(401, new ResultViewModel<string>("02E03 - Usuario ou senha inválidos"));
        }

        if (!PasswordHasher.Verify(user.Password, model.Password))
        {
            return StatusCode(401, new ResultViewModel<string>("02E04 - Usuario ou senha inválidos"));

        }

        try
        {
            var token = tokenService.GenerateToken(user);
            return Ok(new ResultViewModel<string>(token, null));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<string>("02E05 - Falha interna do servidor"));
        }
    }
    
    
}