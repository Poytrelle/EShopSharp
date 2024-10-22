using Ardalis.Specification;
using ShopSharp.Infrastructure.Entity;

namespace ShopSharp.Infrastructure.Specifications;

public class ProductFilterPaginatedSpecification : Specification<ProductItem>
{
    public ProductFilterPaginatedSpecification(int skip, int take, int? brandId, int? typeId)
        : base()
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }
        Query
            .Where(i => (!brandId.HasValue || i.BrandId == brandId) &&
            (!typeId.HasValue || i.TypeId == typeId))
            .Skip(skip).Take(take);
    }
}
