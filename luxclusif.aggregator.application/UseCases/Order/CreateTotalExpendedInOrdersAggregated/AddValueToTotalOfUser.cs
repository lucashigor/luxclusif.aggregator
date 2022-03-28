using luxclusif.aggregator.application.Interfaces;
using luxclusif.aggregator.domain.Repository;
using MediatR;

namespace luxclusif.aggregator.application.UseCases.Order.CreateTotalExpendedInOrdersAggregated
{
    public class AddValueToTotalOfUser :
    IRequestHandler<AddValueToTotalOfUserInput>
    {
        public IOrderRepository repository;
        public IUnitOfWork unitOfWork;

        public AddValueToTotalOfUser(IOrderRepository repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(AddValueToTotalOfUserInput request, CancellationToken cancellationToken)
        {
            var item = await repository.GetByUserId(request.UserId, cancellationToken);

            item.AddValue(request.Value);

            await repository.Update(item, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
