using Control_Dossier.Data;
using Control_Dossier.Extension;
using Control_Dossier.Models;
using Control_Dossier.ViewModel;
using Control_Dossier.ViewModel.Dossier;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Control_Dossier.Controller;

[ApiController]
public class DossierController : ControllerBase
{
    [HttpPost("v1/dossiers")]
    public async Task<IActionResult> PostAsync(
        [FromServices] AppDbContext context,
        [FromBody] CreateDossierViewModel model
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResultViewModel<Dossier>(ModelState.GetErrors()));
        }

        try
        {
            var user = await context.Users.FirstOrDefaultAsync();
            var dossier = new Dossier
            {
                Id = 0,
                Title = model.Title,
                Code = model.Code,
                Content = model.Content,
                Country = model.Country,
                CreateDate = DateTime.UtcNow,
                LastUpdateDate = DateTime.UtcNow,
                Author = user
                
            };

            await context.Dossiers.AddAsync(dossier);
            await context.SaveChangesAsync();
            return Created($"v1/dossiers/{dossier.Id}", dossier);
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, new ResultViewModel<Dossier>("01E01 - Não foi possivel criar o Dossiê."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<Dossier>("01E02 - Falha interna do servidor."));
            
        }
    }


    [HttpGet("v1/dossiers")]
    public async Task<IActionResult> GetAsync(
        [FromServices] AppDbContext context
    )
    {
        try
        {
            var dossiers = await context
                .Dossiers
                .AsNoTracking()
                .OrderByDescending(x=>x.CreateDate)
                .ToListAsync();

            return Ok(new ResultViewModel<List<Dossier>>(dossiers));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<string>("01E03 - Falha interna do servidor"));
        } 
    }

    [HttpGet("v1/dossiers/{id:int}")]
    public async Task<IActionResult> GetAsyncById(
        [FromServices] AppDbContext context,
        [FromRoute] int id
    )
    {
        try
        {
            var dossier = await context
                .Dossiers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (dossier == null)
            {
                return NotFound(new ResultViewModel<string>("Dossiê não encontrado"));
            }

            return Ok(new ResultViewModel<Dossier>(dossier));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<string>("01E04 - Falha interna do servidor"));
        }  
    }

    [HttpPut("v1/dossiers/{id:int}")]
    public async Task<IActionResult> PutAsync(
        [FromServices] AppDbContext context,
        [FromBody] UpdateDossierViewModel model,
        [FromRoute] int id
        )
    {
        try
        {
            var dossier = await context
                .Dossiers
                .FirstOrDefaultAsync(x => x.Id == id);

            if (dossier == null)
            {
                return NotFound(new ResultViewModel<string>("01E07 - Dossiê não encontrado"));
            }

            dossier.Title = model.Title;
            dossier.Code = model.Code;
            dossier.Country = model.Country;
            dossier.Content = model.Content;
            dossier.LastUpdateDate = DateTime.Now.ToUniversalTime();

            context.Dossiers.Update(dossier);
            await context.SaveChangesAsync();
            return Ok(new ResultViewModel<Dossier>(dossier));
            
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, new ResultViewModel<Dossier>("01E05 - Não foi possivel alterar o Dossiê."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<Dossier>("01E06 - Falha interna do servidor."));
            
        }
    }


    [HttpDelete("v1/dossiers/{id:int}")]
    public async Task<IActionResult> DeleteAsync(
        [FromServices] AppDbContext context,
        [FromRoute] int id
    )
    {
        try
        {
            var dossier = await context
                .Dossiers
                .FirstOrDefaultAsync(x => x.Id == id);

            if (dossier == null)
            {
                return NotFound(new ResultViewModel<string>("01E09 - Dossiê não encontrado"));
            }

            context.Dossiers.Remove(dossier);
            await context.SaveChangesAsync();
            return Ok(new ResultViewModel<Dossier>(dossier));
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, new ResultViewModel<Dossier>("01E10 - Não foi possivel excluir o Dossiê."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<Dossier>("01E08 - Falha interna do servidor."));
            
        }
    }
}