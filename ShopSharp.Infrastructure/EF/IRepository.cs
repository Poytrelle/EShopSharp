using Ardalis.Specification;

namespace ShopSharp.Infrastructure.EF;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}
