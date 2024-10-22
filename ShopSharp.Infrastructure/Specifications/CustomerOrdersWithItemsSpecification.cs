using Ardalis.Specification;
using ShopSharp.Infrastructure.Entity;

namespace ShopSharp.Infrastructure.Specifications;

public class CustomerOrdersWithItemsSpecification : Specification<Order>
{
    public CustomerOrdersWithItemsSpecification(string buyerId)
    {
        Query
            .Where(o => o.BuyerId == buyerId)
            .Include(o => o.OrderItems)
            .ThenInclude(i => i.ItemOrdered);
    }
}
