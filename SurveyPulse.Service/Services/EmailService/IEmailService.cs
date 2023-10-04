using MimeKit;
using SurveyPulse.Service.Models;

namespace SurveyPulse.Service.Services.EmailService
{
    public interface IEmailService
    {
        public Task SendEmail(Message message) => throw new NotImplementedException();
    }
}
