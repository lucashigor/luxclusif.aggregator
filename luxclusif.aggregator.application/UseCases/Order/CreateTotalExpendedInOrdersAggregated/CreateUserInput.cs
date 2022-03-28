using MediatR;

namespace luxclusif.aggregator.application.UseCases.Order.CreateTotalExpendedInOrdersAggregated
{
    public class CreateUserInput : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public CreateUserInput(Guid userId, string name)
        {
            Id = userId;
            Name = name;
        }
    }
}
