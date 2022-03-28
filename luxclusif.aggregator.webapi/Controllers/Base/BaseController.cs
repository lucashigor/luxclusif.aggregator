using luxclusif.aggregator.application.Models;
using Microsoft.AspNetCore.Mvc;

namespace luxclusif.aggregator.webapi.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        private readonly Notifier notifier;

        public BaseController(Notifier notifier)
        {
            this.notifier = notifier;
        }

        public IActionResult Result(object model)
        {
            var responseDto = new DefaultResponseDto<object>();

            responseDto.Data = model;

            if (notifier.Erros.Any())
            {
                responseDto.Success = false;
                responseDto.Errors.AddRange(notifier.Erros);
            }

            if (notifier.Warnings.Any())
            {
                responseDto.Warnings.AddRange(notifier.Warnings);
            }

            return Ok(responseDto);
        }
    }
}
