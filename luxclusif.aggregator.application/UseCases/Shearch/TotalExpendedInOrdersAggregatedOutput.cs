using luxclusif.aggregator.domain.Entity;

namespace luxclusif.aggregator.application.UseCases.Shearch
{
    public class TotalExpendedInOrdersAggregatedOutput
    {
        public TotalExpendedInOrdersAggregatedOutput(Guid userId, string name, decimal value)
        {
            UserId = userId;
            Name = name;
            Value = value;
        }

        public Guid UserId { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }

        public static TotalExpendedInOrdersAggregatedOutput FromTotalExpendedInOrdersAggregated(TotalExpendedInOrdersAggregated entity)
            => new(entity.UserId, entity.Name, entity.Value);   
    }
}
