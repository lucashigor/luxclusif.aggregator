using luxclusif.aggregator.domain.Repository;
using MediatR;

namespace luxclusif.aggregator.application.UseCases.Shearch
{
    public class ListTotalExpendedInOrdersAggregated : IRequestHandler<PaginatedListInput, PaginatedListOutput>
    {
        private readonly IOrderRepository orderRepository;

        public ListTotalExpendedInOrdersAggregated(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<PaginatedListOutput> Handle(PaginatedListInput request, CancellationToken cancellationToken)
        {
            var shearchOutup = await orderRepository.Search(
                new(
                    request.Page,
                    request.PerPage,
                    request.Search,
                    request.Sort,
                    request.Dir
                    ), cancellationToken);

            return new PaginatedListOutput(
                shearchOutup.CurrentPage,
                shearchOutup.PerPage,
                shearchOutup.Total,
                shearchOutup.Items.Select(TotalExpendedInOrdersAggregatedOutput.FromTotalExpendedInOrdersAggregated).ToList()
                );
        }
    }
}
