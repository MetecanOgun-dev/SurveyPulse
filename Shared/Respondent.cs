using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyPulse.Shared
{
    public class Respondent
    {
        public Guid RespondentId { get; set; } = Guid.NewGuid();
        public Guid SurveyId { get; set; }
        public string Email { get; set; }
        public Survey? Survey { get; set; }
    }
}
