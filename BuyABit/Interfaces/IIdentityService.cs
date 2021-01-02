using BuyABit.Extensions;
using System.Security.Claims;

namespace BuyABit.Interfaces
{
    public interface IIdentityService
    {
        string GenerateJwtToken(string userId, string userName, AppSettings appSettings);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token, AppSettings appSettings);
    }
}
