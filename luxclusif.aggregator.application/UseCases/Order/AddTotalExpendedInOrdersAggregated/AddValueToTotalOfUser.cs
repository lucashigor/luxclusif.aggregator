using Hangfire;
using luxclusif.aggregator.domain.Repository;
using MediatR;

namespace luxclusif.aggregator.application.UseCases.Order.AddTotalExpendedInOrdersAggregated
{
    public class AddValueToTotalOfUser :
    IRequestHandler<AddValueToTotalOfUserInput>
    {
        public IOrderRepository repository;

        public AddValueToTotalOfUser(IOrderRepository repository)
        {
            this.repository = repository;
        }

        public Task<Unit> Handle(AddValueToTotalOfUserInput request, CancellationToken cancellationToken)
        {
            BackgroundJob.Enqueue(() => HandleEnqueued(request));

            return Task.FromResult(Unit.Value);
        }

        public async Task HandleEnqueued(AddValueToTotalOfUserInput request)
        {
            var item = await repository.GetByUserIdSynchronous(request.UserId, CancellationToken.None);
            
            if(item == null)
            {
                return;
            }

            await repository.UpdateSynchronous(item, CancellationToken.None);
        }
    }
}
