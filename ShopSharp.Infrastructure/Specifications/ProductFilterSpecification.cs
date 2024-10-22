using Ardalis.Specification;
using ShopSharp.Infrastructure.Entity;

namespace ShopSharp.Infrastructure.Specifications;

public class ProductFilterSpecification : Specification<ProductItem>
{
    public ProductFilterSpecification(int? brandId, int? typeId)
    {
        Query
            .Where(i =>
                (!brandId.HasValue || i.BrandId == brandId) &&
                (!typeId.HasValue || i.TypeId == typeId));
    }
}
