using Api.Client.MarquetStore.DTO;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Api.Client.MarquetStore.Service.Imp
{
    public class ImpEmailService : ISend
    {
        private readonly IConfiguration _configuration;

        public ImpEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task SendEmail(EmailDTO model)
        {
            MimeMessage email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("Email:UserName").Value));
            email.To.Add(MailboxAddress.Parse(model.For));
            email.Subject = model.Affair;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = model.Content
            };

            using (SmtpClient smtp = new SmtpClient())
            {
               await smtp.ConnectAsync(
                    _configuration.GetSection("Email:Host").Value,
                    Convert.ToInt32(_configuration.GetSection("Email:Port").Value),
                    SecureSocketOptions.StartTls
                    );

                await smtp.AuthenticateAsync(_configuration.GetSection("Email:UserName").Value,
                        _configuration.GetSection("Email:Password").Value);

               await smtp.SendAsync(email);
               await smtp.DisconnectAsync(true);
            }
        }
    }
}
