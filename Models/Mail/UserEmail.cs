using SendGrid;

namespace NetCoreBackPack.Models.Mail
{
    public class UserEmail
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }


        public Response Response { get; set; }
    }
}