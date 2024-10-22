namespace ShopSharp.Infrastructure.EF;

public abstract class BaseEntity
{
    public virtual int Id { get; protected set; }
}
