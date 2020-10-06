using BuyABit.Extensions;

namespace BuyABit.Interfaces
{
    public interface IIdentityService
    {        string GenerateJwtToken(string userId, string userName, AppSettings appSettings);
    }
}
