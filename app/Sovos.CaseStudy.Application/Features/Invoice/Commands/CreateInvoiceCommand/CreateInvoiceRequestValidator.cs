using FluentValidation;

namespace Sovos.CaseStudy.Application.Features.Invoice.Commands.CreateInvoiceCommand;
public class CreateInvoiceRequestValidator : AbstractValidator<CreateInvoiceRequest>
{
    public CreateInvoiceRequestValidator()
    {
        RuleFor(x => x.SenderTitle).NotEmpty();
        RuleFor(x => x.InvoiceId).NotEmpty();
        RuleFor(x => x.ReceiverTitle).NotEmpty();
        //RuleForEach(x => x.InvoiceLines).ChildRules(l =>
        //{
        //    l.RuleFor(c => c.Name).NotEmpty();
        //    l.RuleFor(c => c.Quantity).GreaterThan(0);
        //    l.RuleFor(c => c.UnitCode).NotEmpty();
        //    l.RuleFor(c => c.UnitPrice).GreaterThan(0);
        //});
    }
}
