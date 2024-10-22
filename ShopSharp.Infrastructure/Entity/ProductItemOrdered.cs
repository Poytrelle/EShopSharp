namespace ShopSharp.Infrastructure.Entity;

/// <summary>
/// Represents a snapshot of the item that was ordered. If catalog item details change, details of
/// the item that was part of a completed order should not change.
/// </summary>
public class ProductItemOrdered
{
    public required int CatalogItemId { get; init; }
    public required string ProductName { get; init; }
    public required string PictureUri { get; init; }
}
