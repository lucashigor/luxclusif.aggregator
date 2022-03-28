namespace luxclusif.aggregator.domain.SeedWork.ShearchableRepository
{
    public class SearchInput
    {
        public SearchInput(int? page, int? perPage, string? search, string? orderBy, SearchOrder? order)
        {
            Page = page ?? 0;
            PerPage = perPage ?? 0;
            Search = search ?? "";
            OrderBy = orderBy ?? "";
            Order = order ?? SearchOrder.Undefined;

            Validate();
        }

        public int Page { get; set; }
        public int PerPage { get; set; }
        public string Search { get; set; }
        public string OrderBy { get; set; }
        public SearchOrder Order { get; set; }

        public void Validate()
        {
            if (Page == 0)
                Page = 1;
            if (PerPage <= 0)
                PerPage = 15;
            if (PerPage >= 100)
                PerPage = 100;
            if (Search is null)
                Search = "";
            if (OrderBy is null)
                OrderBy = "";
            if (Order == SearchOrder.Undefined)
                Order = SearchOrder.Asc;

        }
    }
}
