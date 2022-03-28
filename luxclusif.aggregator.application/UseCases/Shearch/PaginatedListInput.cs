using luxclusif.aggregator.domain.SeedWork.ShearchableRepository;
using MediatR;

namespace luxclusif.aggregator.application.UseCases.Shearch
{
    public class PaginatedListInput : IRequest<PaginatedListOutput>
    {
        public PaginatedListInput(int? page = null, int? perPage = null, string? search = null, string? sort = null, SearchOrder? dir = null)
        {
            Page = page;
            PerPage = perPage;
            Search = search;
            Sort = sort;
            Dir = dir;
        }

        public int? Page { get; set; }
        public int? PerPage { get; set; }
        public string? Search { get; set; }
        public string? Sort { get; set; }
        public SearchOrder? Dir { get; set; }

    }
}
