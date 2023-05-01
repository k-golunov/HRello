using MimeKit;
using Serilog;
using Serilog.Events;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace HRelloApi.Notification;

/// <summary>
/// Отправка сообщение по smtp
/// </summary>
public static class EmailSender
{
    /// <summary>
    /// Отправка сообщений
    /// </summary>
    /// <param name="message">сообщение для отправки</param>
    /// <param name="mailTo">почта получателя</param>
    public static void SendEmail(string message, string mailTo)
    {
        try
        {
            var Mailmessage = new MimeMessage();
            Mailmessage.From.Add(new MailboxAddress("Моя компания",
                "intervalgd@mail.ru")); //отправитель сообщения поменять адрес
            Mailmessage.To.Add(new MailboxAddress("Моя компания", mailTo)); //адресат сообщения поменять адрес
            Mailmessage.Subject = "Сообщение от HRello"; //тема сообщения 
            Mailmessage.Body = new BodyBuilder() { HtmlBody = $"<div style=\"color: orange;\">{message}</div>" }
                .ToMessageBody(); //тело сообщения (так же в формате HTML)

            using var client = new SmtpClient();
            client.Connect("smtp.mail.ru", 465, true);
            client.Authenticate("intervalgd@mail.ru", "jhmcg8hfpFJidgD3hvM2");
            client.Send(Mailmessage);
            client.Disconnect(true);
            Log.Logger.Write(LogEventLevel.Information, "Сообщение успешно отправлено");
        }
        catch (Exception e)
        {
            Log.Logger.Write(LogEventLevel.Error,$"Ошибка в отправке сообщения {e.Message}");
        }
    }
}