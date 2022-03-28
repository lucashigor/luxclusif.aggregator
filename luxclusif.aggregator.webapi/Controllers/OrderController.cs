using luxclusif.aggregator.application.Models;
using luxclusif.aggregator.application.UseCases.Shearch;
using luxclusif.aggregator.webapi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace luxclusif.aggregator.webapi.Controllers
{
    [Route("[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class OrderController : BaseController
    {
        private readonly IMediator mediator;
        public OrderController(IMediator mediator, Notifier notifier) : base(notifier)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [SwaggerOperation(
            OperationId = "Post_Shearch",
            Summary = "Post_Shearch")]
        [SwaggerResponse(200, Type = typeof(DefaultResponseDto<PaginatedListInput>), Description = "Post_Shearch")]
        [SwaggerResponse(400, Type = typeof(DefaultResponseDto<object>), Description = "Error")]
        [SwaggerResponse(500, Type = typeof(DefaultResponseDto<object>), Description = "Error")]
        public async Task<IActionResult> CreateConfiguration([FromBody] PaginatedListInput model)
        {
            var ret = await mediator.Send<PaginatedListOutput>(model);

            return Result(ret);
        }
    }
}
