using Microsoft.AspNetCore.Mvc;
using SurveyPulse.Server.Data;
using SurveyPulse.Service.Models;
using SurveyPulse.Service.Services.EmailService;
using SurveyPulse.Service.Services.EmailService.SystemMailGenerators;
using SurveyPulse.Shared.ResponseModels;
using Survey = SurveyPulse.Shared.Survey;

namespace SurveyPulse.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _EmailService;
        public SurveyController(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _EmailService = emailService;
        }

        [HttpPost("AddSurvey")]
        public async Task<IActionResult> NewSurvey([FromBody] Survey survey)
        {
            if(survey is not null)
            {
                try
                {
                    _context.Surveys.Add(survey);
                    var res = _context.SaveChanges();
                }
                catch (Exception ex) { throw ex; }
            }

            if(survey.Respondents != null && survey.Respondents.Count > 0)
            {
                var surveyLink = survey.Link;
                var emailContentVariables = new Dictionary<int, string> { { 0, survey.Owner }, { 1, surveyLink } };
                var mailTo = survey.Respondents.Select(respondent => respondent.Email).ToArray();
                var message = new Message(mailTo, "Welcome to SurveyPulse", new SystemMailGenerator(SystemEmailTemplates.SurveyInvitation, emailContentVariables).MailContent);
                await _EmailService.SendEmail(message);
            }

            var response = new ApiResponse { Success = true, AdditionalInfo = "Survey saved and invitation links has been sent successful." };
            return StatusCode(StatusCodes.Status201Created, response);
        }
    }
}
