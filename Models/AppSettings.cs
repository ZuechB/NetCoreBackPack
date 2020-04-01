using NetCoreBackPack.Models.Mail;

namespace NetCoreBackPack.Models
{
    public class AppSettings
    {
        public SendGridAppSettings SendGrid { get; set; }
    }

    public class SendGridAppSettings
    {
        public string FromName { get; set; }
        public string APIKey { get; set; }
        public string FromEmail { get; set; }
        public bool IsDebugEnabed { get; set; }
        public EmailTemplate[] Templates { get; set; }
    }
}