namespace luxclusif.aggregator.application.UseCases.Shearch
{
    public class PaginatedListOutput
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
        public IReadOnlyList<TotalExpendedInOrdersAggregatedOutput> Items { get; set; }
        public PaginatedListOutput(int currentPage,
            int perPage,
            int total,
            IReadOnlyList<TotalExpendedInOrdersAggregatedOutput> items)
        {
            Page = currentPage;
            PerPage = perPage;
            Total = total;
            Items = items;
        }
    }
}
