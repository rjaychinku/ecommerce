namespace myShop.Interfaces
{
    public interface IIdentityService
    {
        string GenerateJwtToken(string userId, string userName, string secret);
    }
}
