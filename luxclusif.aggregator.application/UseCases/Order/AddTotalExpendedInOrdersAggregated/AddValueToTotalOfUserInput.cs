using MediatR;

namespace luxclusif.aggregator.application.UseCases.Order.AddTotalExpendedInOrdersAggregated
{
    public class AddValueToTotalOfUserInput : IRequest
    {
        public Guid UserId { get; set; }
        public decimal Value { get; set; }
    }
}
