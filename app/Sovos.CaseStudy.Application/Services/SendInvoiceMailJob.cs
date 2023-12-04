using MediatR;
using Sovos.CaseStudy.Application.Features.Invoice.Jobs;

namespace Sovos.CaseStudy.Application.Services;
public class SendInvoiceMailJob
{
    private readonly IMediator _mediator;
    public SendInvoiceMailJob(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task Execute()
    {
        await _mediator.Send(new Features.Invoice.Jobs.SendInvoiceMailJob());
    }
}
