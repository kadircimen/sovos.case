namespace Core.Mailing;
public class MailSettings
{
    public string Server { get; set; }
    public int Port { get; set; }
    public string Sender { get; set; }
    public string SenderMail { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

    public MailSettings()
    {
    }

    public MailSettings(string server, int port, string sender, string senderMail, string userName,
                        string password)
    {
        Server = server;
        Port = port;
        Sender = sender;
        SenderMail = senderMail;
        UserName = userName;
        Password = password;
    }
}
