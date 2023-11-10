using Api.Client.MarquetStore.DTO;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Api.Client.MarquetStore.Service.Imp
{
    public class ImpEmailService : ISend
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ImpEmailService> _logger;

        public ImpEmailService(IConfiguration configuration, ILogger<ImpEmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }


        public async Task<bool> SendEmail(EmailDTO model)
        {
            MimeMessage email = new MimeMessage();
            try
            {
                
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

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al enviar el correo.");
                return false;
            }
        }
    }
}
