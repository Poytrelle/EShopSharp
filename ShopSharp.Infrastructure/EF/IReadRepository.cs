using Ardalis.Specification;

namespace ShopSharp.Infrastructure.EF;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}
