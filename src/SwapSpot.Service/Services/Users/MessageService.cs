using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using SwapSpot.Service.DTOs.Messages;
using SwapSpot.Service.Interfaces.Users;

namespace SwapSpot.Service.Services.Users;

public class MessageService : IMessageService
{
    private readonly IConfiguration _configuration;
    public MessageService(IConfiguration configuration)
    {
        _configuration = configuration.GetSection("Email");
    }
    public async Task SendMessageAsync(MessageForCreationDto dto)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_configuration["EmailAddress"]));

        email.Subject = dto.Subject;

        email.Body = new TextPart("html")
        {
            Text = dto.Body
        };

        var smtp = new SmtpClient();

        await smtp.ConnectAsync(_configuration["Host"], 587, MailKit.Security.SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_configuration["EmailAddress"], _configuration["Password"]);

        await smtp.SendAsync(email);

        await smtp.DisconnectAsync(true);
     }
}
