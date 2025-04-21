using System.Net;
using System.Net.Mail;
using System.Text;

public class ForecastEmail
{
    public ForecastEmail()
    {

    }

    public static void EmailSend(string SenderEmail, string SenderPassword, string RecieverEmail)
    {
        StringBuilder FilePath = new StringBuilder();
        FilePath.Append(@"\Output\SpaceMission.csv");

        using SmtpClient smtpServer = new SmtpClient
        {
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            EnableSsl = true,
            Host = "smtp.gmail.com",
            Port = 587,
            Credentials = new NetworkCredential(SenderEmail, SenderPassword)
        };

        MailMessage mail = new MailMessage();
        try
        {
            mail.From = new MailAddress(SenderEmail);
        }
        catch (Exception)
        {
            Console.WriteLine("There is no such sender email or the email is not in the right format.");
        }

        try
        {
            mail.To.Add(RecieverEmail);
        }
        catch (Exception)
        {
            Console.WriteLine("There is no such reciever email or the email is not in the right format.");
        }

        mail.Subject = "The shortest path to the station";
        mail.Body = "This email contains the shortest path to the station";

        System.Net.Mail.Attachment attachment;
        try
        {
            attachment = new System.Net.Mail.Attachment(FilePath.ToString());
            mail.Attachments.Add(attachment);
        }
        catch (Exception)
        {
            Console.WriteLine("No file with best launch data is located in this folder.");
        }

        try
        {
            smtpServer.Credentials = new System.Net.NetworkCredential(SenderEmail, SenderPassword);
        }
        catch (Exception)
        {
            Console.WriteLine("Email name or password is incorrect.");
        }

        try
        {
            smtpServer.Send(mail);
            Console.WriteLine("The email with the best route is successfully sent.");
        }
        catch (Exception)
        {
            Console.WriteLine("Email is not sent.");
        }
    }
}