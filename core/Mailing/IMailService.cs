namespace Core.Mailing;
public interface IMailService
{
    Task<List<int>> SendMailAsync(List<MailModel> mail);
}
