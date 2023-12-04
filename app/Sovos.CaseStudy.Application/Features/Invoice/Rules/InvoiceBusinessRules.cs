using Core.CrossCuttingConcerns.Exceptions;
using Core.Mailing;
using Core.Persistence.Paging;
using MimeKit;
using Sovos.CaseStudy.Application.Features.Invoice.Constants;
using Sovos.CaseStudy.Application.Services;
using Sovos.CaseStudy.Domain.Entites;

namespace Sovos.CaseStudy.Application.Features.Invoice.Rules;
public class InvoiceBusinessRules
{
    private readonly IInvoiceRepository _invoiceRepo;
    public InvoiceBusinessRules(IInvoiceRepository invoiceRepo)
    {
        _invoiceRepo = invoiceRepo;
    }
    public async Task InvoiceIdCannotDuplicated(string Id)
    {
        InvoiceHeader result = await _invoiceRepo.GetAsync(x => x.InvoiceId == Id);
        if (result != null)
            throw new BusinessException(InvoiceMessages.InvoiceIdExists);
    }
    public List<MailModel> SetInvoiceDataToMailModel(IPaginate<InvoiceHeader> data)
    {
        var model = data.Items.ToList().ConvertAll(x => new MailModel
        {
            Id = x.Id,
            To = new List<MailboxAddress> { new MailboxAddress(name: x.SenderTitle, address: x.Email) },
            Subject = $"{x.InvoiceId} Numaralı Faturanız Hk.",
            Body = $"{x.InvoiceLine.Where(c => c.IsActive).Count()} kalem ürün içeren {x.InvoiceId} nolu faturanız başarıyla işlenmiştir"
        });
        return model;
    }
}

