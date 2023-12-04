using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;
using Sovos.CaseStudy.Application.Features.Invoice.Commands.CreateInvoiceCommand;
using Sovos.CaseStudy.Application.Features.Invoice.Jobs;
using Sovos.CaseStudy.Application.Features.Invoice.Queries.GetById;
using Sovos.CaseStudy.Application.Features.Invoice.Queries.GetInvoices;

namespace Sovos.CaseStudy.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class InvoiceController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateInvoiceRequest request) => Ok(await Mediator.Send(request));
    [HttpGet("list")]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest) => Ok(await Mediator.Send(new GetInvoiceListQuery
    {
        PageRequest = pageRequest,
    }));
    [HttpGet("get")]
    public async Task<IActionResult> GetById([FromQuery] GetInvoiceByIdQuery request) => Ok(await Mediator.Send(request));
}