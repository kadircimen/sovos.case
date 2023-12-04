using Core.Mailing;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sovos.CaseStudy.Application.Features.Invoice.Rules;
using Sovos.CaseStudy.Application.Services;
namespace Sovos.CaseStudy.Application.Features.Invoice.Jobs;
public class SendInvoiceMailJob : IRequest<SendInvoiceMailResponse>
{
    public class SendInvoiceMailRequestHandler : IRequestHandler<SendInvoiceMailJob, SendInvoiceMailResponse>
    {
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly IMailService _mailService;
        private readonly InvoiceBusinessRules _rules;
        public SendInvoiceMailRequestHandler(IInvoiceRepository invoiceRepo, IMailService mailService, InvoiceBusinessRules rules)
        {
            _invoiceRepo = invoiceRepo;
            _mailService = mailService;
            _rules = rules;
        }
        public async Task<SendInvoiceMailResponse> Handle(SendInvoiceMailJob request, CancellationToken cancelToken)
        {
            var data = await _invoiceRepo.GetListAsync(predicate: x => x.IsMailSended == false, include: c => c.Include(i => i.InvoiceLine), size: 100);
            if (data.Items.Count > 0)
            {
                var sendMail = await _mailService.SendMailAsync(_rules.SetInvoiceDataToMailModel(data));
                var updatedInvoiceHeaders = data.Items.Where(x => sendMail.Contains(x.Id)).Select(x => { x.IsMailSended = true; return x; }).ToList();
                var updated = await _invoiceRepo.UpdateRangeAsync(updatedInvoiceHeaders);
            }
            return new SendInvoiceMailResponse { };
        }
    }
}
