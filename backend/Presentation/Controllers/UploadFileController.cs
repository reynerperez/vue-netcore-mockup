using Application.Invoices.Commands.UploadFile;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/upload")]
    public class UploadFileController : Controller
    {

        private readonly IMediator _mediator;

        public UploadFileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile([FromForm] UploadFileCommand command)
        {
            
            return Ok(await _mediator.Send(command));
        }
    }
}
