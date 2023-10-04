using SurveyPulse.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyPulse.Service.Services.EmailService.SystemMailGenerators
{
    public enum SystemEmailTemplates
    {
        Test,
        Welcome, //0=> validationLink
        SurveyInvitation, //0 => Owner // 1 => link
    }
    public class SystemMailGenerator
    {
        public string MailContent { get; set; }
        public SystemMailGenerator(SystemEmailTemplates emailTemplate , Dictionary<int, string> emailVariables)
        {
            switch (emailTemplate)
            {
                case SystemEmailTemplates.Test:
                    {
                        MailContent = "Test";
                    }
                    break;
                    case SystemEmailTemplates.Welcome:
                    {
                        MailContent = GenerateWelcomeEmail(emailVariables);
                    }
                    break;
                    case SystemEmailTemplates.SurveyInvitation:
                    {
                        MailContent = GenerateSurveyInvitationEmail(emailVariables);
                    }
                    break;
            }
        }

        private string GenerateWelcomeEmail(Dictionary<int, string> emailVariables)
        {
            //emailVariables : 
            //{0}=> username
            //{1}=> validationLink
            string mailContent = @"    
            <table width=""100%"" cellpadding=""0"" cellspacing=""0"">
                <tr>
                    <td align=""center"">
                        <table width=""600"" cellpadding=""0"" cellspacing=""0"">
                            <tr>
                                <td style=""background: rgb(164,33,194);
                                    background: linear-gradient(0deg, rgba(164,33,194,0.6750350652956495) 0%, rgba(162,0,255,1) 100%);
                                    padding: 20px 0; text-align: center;"">
                                    <h1 style=""color: #fff;"">Welcome to SurveyPulse</h1>
                                </td>
                            </tr>
                            <tr>
                                <td style=""padding: 20px;"">
                                    <p>Hello {0},</p>
                                    <p>Thank you for joining our survey community! 📋</p>
                                    <p>To ensure that your account is active and you can start participating in surveys, please verify your email address within the next 7 days by clicking the link below:</p>
                                    <p><a href=""{1}"">Verify Your Email</a></p>
                                    <p>By confirming your email, you'll unlock exciting opportunities to share your opinions, earn rewards, and make your voice heard on a wide range of topics. Your valuable insights will help shape the future!</p>
                                    <p>If you have any questions or need assistance, our dedicated support team is here to help. Feel free to reach out to us at <a href=""mailto:support@surveypulse.com"">support@surveypulse.com</a>.</p>
                                    <p>We look forward to having you as an active member of our survey community. Thank you for joining us on this exciting journey!</p>
                                    <p>Best regards,<br>SurveyPulse</p>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>";

            foreach (var variable in emailVariables)
            {
                mailContent = mailContent.Replace("{" + variable.Key + "}", variable.Value);
            }
            return mailContent;
        }
        private string GenerateSurveyInvitationEmail(Dictionary<int, string> emailVariables)
        {
            //emailVariables : 
            //{0}=> validationLink
            string mailContent = @"    
            <table width=""100%"" cellpadding=""0"" cellspacing=""0"">
                <tr>
                    <td align=""center"">
                        <table width=""600"" cellpadding=""0"" cellspacing=""0"">
                            <tr>
                                <td style=""background: rgb(164,33,194);
                                    background: linear-gradient(0deg, rgba(164,33,194,0.6750350652956495) 0%, rgba(162,0,255,1) 100%);
                                    padding: 20px 0; text-align: center;"">
                                    <h1 style=""color: #fff;"">Survey Invitation - SurveyPulse</h1>
                                </td>
                            </tr>
                            <tr>
                                <td style=""padding: 20px;"">
                                    <p>Dear Mr/Mrs,</p>
                                    <p>We are delighted to invite you to participate in {0}'s survey! Your opinions and thoughts are essential to us. </p>
                                    <p>By participating in this survey, you can share your thoughts. </p>
                                    <p>To complete the survey, please click on the following link:</p>
                                    <a href=""{1}"">Take the Survey</a>
                                    <p>Your participation is highly valuable to us. Thank you in advance for your participation.</p>
                                    <p>Best regards,<br>SurveyPulse</p>
                                </td>
                            </tr>
                        </table>
                       </td>
                    </tr>
                </table>";

            foreach (var variable in emailVariables)
            {
                mailContent = mailContent.Replace("{" + variable.Key + "}", variable.Value);
            }
            return mailContent;
        }
    }
}
