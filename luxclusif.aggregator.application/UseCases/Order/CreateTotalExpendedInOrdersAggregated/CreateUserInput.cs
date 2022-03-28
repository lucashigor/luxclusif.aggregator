using MediatR;

namespace luxclusif.aggregator.application.UseCases.Order.CreateTotalExpendedInOrdersAggregated
{
    public class CreateUserInput : IRequest
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }

        public CreateUserInput(Guid userId, string name)
        {
            UserId = userId;
            Name = name;
        }
    }
}
