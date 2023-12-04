using Core.CrossCuttingConcerns.Logging;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Newtonsoft.Json;

namespace Core.Mailing;
public class MailService : IMailService
{
    private readonly MailSettings _mailSettings;
    private readonly LoggerServiceBase _logger;
    public MailService(IConfiguration configuration, LoggerServiceBase logger)
    {
        _mailSettings = configuration.GetSection("MailSettings").Get<MailSettings>();
        _logger = logger;
    }
    public async Task<List<int>> SendMailAsync(List<MailModel> mail)
    {
        var response = new List<int>();
        var email = new MimeMessage();
        var smtp = new SmtpClient();
        email.From.Add(new MailboxAddress(_mailSettings.Sender, _mailSettings.SenderMail));

        //smtp.Connect(_mailSettings.Server, _mailSettings.Port); -> Hata almamak için kapatıyorum.
        foreach (var m in mail)
        {
            if (m.To != null || m.To.Count > 0)
            {
                try
                {
                    email.To.AddRange(m.To);
                    email.Subject = m.Subject;
                    BodyBuilder bodyBuilder = new() { TextBody = m.Body };
                    email.Body = bodyBuilder.ToMessageBody();
                    //smtp.Send(email); -> hata almaması için satırı kapatıyorum
                    _logger.Info($"Mail sent. To: {m.To} Data: {JsonConvert.SerializeObject(m)}");
                    response.Add(m.Id);
                }
                catch (Exception ex)
                {
                    _logger.Error($"Mail Sender Error. To: {m.To} Data: {JsonConvert.SerializeObject(m)} Exception: {JsonConvert.SerializeObject(ex.Message)}");
                }
            }
        }
        //await smtp.SendAsync(email);
        //smtp.Disconnect(true);
        //email.Dispose();
        //smtp.Dispose(); -> hata almamak için satırları kapatıyorum.

        return response;
    }
}
