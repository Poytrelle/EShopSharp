namespace ShopSharp.Infrastructure.Exceptions;

public class BasketNotFoundException(int basketId) : Exception($"No basket found with id {basketId}")
{
}
