using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyPulse.Shared
{
    public enum SurveyCategories
    {
        Personal,
    }
    public enum SurveyType
    {
        ClosedEnded,
        OpenEnded
    }
    public class Survey 
    {
        public Guid SurveyId { get; set; } = Guid.NewGuid();
        public Guid SurveyDetailId { get; set; }
        public string Owner { get; set; }
        public SurveyCategories Category { get; set; }
        public SurveyType Type { get; set; }
        public bool IsActive { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsPublic { get; set; }
        public string Link { get; set; }
        public ICollection<Question>? Questions { get; set; }
        public ICollection<Respondent>? Respondents { get; set; }
        //public ICollection<SurveyResponse>? SurveyResponses { get; set; }

        public SurveyDetail? SurveyDetail { get; set; }
    }

}
