namespace ShopSharp.Infrastructure.Services;

public interface ITokenClaimsService
{
    Task<string> GetTokenAsync(string userName);
}
