using Ardalis.Specification.EntityFrameworkCore;
using ShopSharp.Infrastructure.EF;

namespace ShopSharp.Domain.Data;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(CatalogContext dbContext) : base(dbContext)
    {
    }
}
