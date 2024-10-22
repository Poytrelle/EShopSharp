using Ardalis.Specification;
using ShopSharp.Infrastructure.Entity;

namespace ShopSharp.Infrastructure.Specifications;

public class ProductItemsSpecification : Specification<ProductItem>
{
    public ProductItemsSpecification(params int[] ids)
    {
        Query
            .Where(c => ids.Contains(c.Id));
    }
}
