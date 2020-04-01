using NetCoreBackPack.Models.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBackPack.Services
{
    public interface IMailService
    {
        Task<List<UserEmail>> Send(List<UserEmail> users, string templateId, BaseAccount substitutions);
        Task<UserEmail> Send(UserEmail user, string templateId, BaseAccount substitutions);
    }

    public class MailService : IMailService
    {
        private string apiKey = "";
        private string fromEmail = "";
        private string fromName = "";
        public MailService(string apiKey, string fromEmail, string fromName)
        {
            this.apiKey = apiKey;
            this.fromEmail = fromEmail;
            this.fromName = fromName;
        }

        public async Task<List<UserEmail>> Send(List<UserEmail> users, string templateId, BaseAccount substitutions)
        {
            return await SendMail(users, templateId, substitutions: substitutions);
        }

        public async Task<UserEmail> Send(UserEmail user, string templateId, BaseAccount substitutions)
        {
            var users = new List<UserEmail>() { user };
            var userMail = await SendMail(users, templateId, substitutions: substitutions);
            return userMail.FirstOrDefault();
        }

        private async Task<List<UserEmail>> SendMail(List<UserEmail> users, string templateId, BaseAccount substitutions = null)
        {
            var userEmails = new List<UserEmail>();
            foreach (var to in users)
            {
                substitutions.email = to.Email;
                substitutions.firstname = to.FirstName;

                var email = new Email(apiKey)
                {
                    From = new From
                    {
                        Name = fromName
                    },
                    Substitutions = substitutions,
                    To = new EmailAddress()
                    {
                        Email = to.Email,
                        Name = to.FirstName
                    },
                    HtmlContent = " ",
                    TemplateId = templateId
                };

                email.From.Email = fromEmail;

                to.Response = await SendToSendGrid(email);
                userEmails.Add(to);
            }

            return userEmails;
        }

        private async Task<Response> SendToSendGrid(Email email)
        {
            var client = new SendGridClient(email.ApiKey);

            var from = new EmailAddress(email.From.Email, email.From.Name);
            var msg = MailHelper.CreateSingleTemplateEmail(from,
                                                          email.To,
                                                          email.TemplateId,
                                                          email.Substitutions
                                                        );

            return await client.SendEmailAsync(msg);
        }
    }
}
