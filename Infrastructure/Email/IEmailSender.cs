namespace EcommerceInfrastructure.Email;

public interface IEmailSender
{
    void SendEmail(string to, string subject, string body);
}