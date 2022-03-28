namespace luxclusif.aggregator.domain.SeedWork;
public abstract class Entity
{
    protected Entity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastUpdateAt { get; protected set; }

    protected virtual void Validate()
    {
    }
}
