using luxclusif.aggregator.domain.SeedWork;
using luxclusif.aggregator.domain.Validation;

namespace luxclusif.aggregator.domain.Entity;
public class TotalExpendedInOrdersAggregated : AgregateRoot
{
    public Guid UserId { get; private set; }
    public decimal Value { get; private set; }
    public string Name { get; private set; }

    public TotalExpendedInOrdersAggregated(string name, Guid userId, decimal value) : base ()
    {
        this.UserId = userId;
        this.Value = value;
        this.Name = name;

        this.Validate();
    }

    protected override void Validate()
    {
        Name.NotNull();
        Name.NotNullOrEmptyOrWhiteSpace();

        UserId.NotNull();
        Value.NotNull();

        base.Validate();
    }

    public void AddValue(decimal value)
    {
        Value += value;
        LastUpdateAt = DateTime.UtcNow;

        Validate();
    }
}
