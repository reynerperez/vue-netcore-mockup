using Application.Invoices;
using Application.Invoices.Commands.Create;
using Application.Invoices.Commands.Delete;
using Application.Invoices.Commands.Update;
using Application.Invoices.Queries.Downlaod;
using Application.Invoices.Queries.Get;
using Application.Invoices.Queries.List;
using Application.Invoices.Queries.Search;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/invoices")]
    public class InvoiceContoller : ControllerBase
    {
        private readonly IMediator _mediator;

        public InvoiceContoller(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] SearchInvoiceQuery query)
        {
            List<InvoiceDTO> list;
            if (query != null && query.Term != null && query.Term?.Trim() != "")
                list = await _mediator.Send(query);
            else
                list = await _mediator.Send(new ListInvoiceQuery());
            return Ok(list);
        }

        [HttpGet("{InvoiceId}")]
        public async Task<IActionResult> Get([FromRoute] GetInvoiceQuery query)
        {
            InvoiceDTO invoice = await _mediator.Send(query);
            return invoice != null ? Ok(invoice) : NotFound();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateInvoiceCommand command)
        {
            int? id = await _mediator.Send(command);

            return Ok(id);

            // var createdResource = new { InvoiceId = id };
            // var routeValues = new { InvoiceId = createdResource.InvoiceId };
            // return CreatedAtAction(nameof(Get), routeValues, createdResource);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UpdateInvoiceCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{InvoiceId}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] DeleteInvoiceCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("download")]
        [Authorize]
        public async Task<IActionResult> Download([FromQuery] DownloadFileQuery command)
        {
            byte[]? file = await _mediator.Send(command);
            return file != null ? File(file, "application/octet-stream") : NotFound();
        }
    }
}
