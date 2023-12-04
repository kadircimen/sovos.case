using MimeKit;
namespace Core.Mailing;
public class MailModel
{
    public int Id { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public List<MailboxAddress> To { get; set; }
    public MailModel() { }
    public MailModel(
        string subject,
        string body,
        List<MailboxAddress> to
    )
    {
        Subject = subject;
        Body = body;
        To = to;
    }
}
