using System.Net.Mail;
using System.Net;

namespace Service.Users;

public class EmailService
{
    public void SendPasswordResetEmail(string recipientEmail)
    {
        string resetLink = "";
        string senderEmail = "your_email@example.com";
        string senderPassword = "your_email_password";
        string subject = "Password Reset";
        string body = $"Dear User,\n\nYou have requested to reset your password. Please click on the following link to reset your password:\n\n{resetLink}";

        MailMessage mail = new MailMessage(senderEmail, recipientEmail, subject, body);
        mail.IsBodyHtml = false;

        SmtpClient client = new SmtpClient("smtp.yourmailserver.com", 587);
        client.UseDefaultCredentials = false;
        client.Credentials = new NetworkCredential(senderEmail, senderPassword);
        client.EnableSsl = true;

        try
        {
            client.Send(mail);
            Console.WriteLine("Email sent successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
}
