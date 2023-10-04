using MimeKit;

namespace SurveyPulse.Service.Models
{
    public enum SystemEmailTemplate
    {
        Test,
        Welcome, //0=> userName, 1=>validationLink
        PasswordReset,
        NotAuthenticatedSurvey
    }
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public Message(IEnumerable<string> to, string subject,  string content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(e1 => new MailboxAddress("SurveyPulse", e1)));
            Subject = subject;
            Content = content;
            
        }
    }
}

