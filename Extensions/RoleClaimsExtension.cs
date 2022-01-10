using System.Security.Claims;
using Control_Dossier.Models;

namespace Control_Dossier.Extensions;

public static class RoleClaimsExtension
{
    public static IEnumerable<Claim> GetClaims(this User user)
    {
        var result = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email)
        };

        result.AddRange(
            user.Roles.Select(role=> new Claim(ClaimTypes.Role, role.Title))
        );

        return result;
    }
}