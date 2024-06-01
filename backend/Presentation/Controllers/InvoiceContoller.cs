using Application.Invoices;
using Application.Invoices.Commands.Create;
using Application.Invoices.Commands.Delete;
using Application.Invoices.Commands.DownlaodFile;
using Application.Invoices.Commands.Update;
using Application.Invoices.Queries.Get;
using Application.Invoices.Queries.List;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> ListInvoices() {
            List<InvoiceDTO> list = await _mediator.Send(new ListInvoiceQuery());
            return Ok(list);
        }

        [HttpGet("{InvoiceId}")]
        public async  Task<IActionResult> GetProductById([FromRoute] GetInvoiceQuery query) {
            InvoiceDTO invoice = await _mediator.Send(query);
            return invoice != null ? Ok(invoice) : NotFound();
        } 

        [HttpPost]
        public async Task<IActionResult> CreateInvoices([FromBody] CreateInvoiceCommand command)
        {
            await _mediator.Send(command);
            return Created();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateInvoice([FromBody] UpdateInvoiceCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{InvoiceId}")]
        public async Task<IActionResult> DeleteInvoice([FromRoute] DeleteInvoiceCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("download")]
        public async Task<IActionResult> GetBlobDownload([FromQuery] DownloadFileCommand command)
        {

            byte[]? file = await _mediator.Send(command);


            return file != null ? File(file, "application/octet-stream") : NotFound();
        }


    }
}
