using Ardalis.Specification;
using ShopSharp.Infrastructure.Entity;

namespace ShopSharp.Infrastructure.Specifications;

public class ProductItemNameSpecification : Specification<ProductItem>
{
    public ProductItemNameSpecification(string catalogItemName)
    {
        Query
            .Where(item => catalogItemName == item.Name);
    }
}
