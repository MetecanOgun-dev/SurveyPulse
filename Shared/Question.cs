using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyPulse.Shared
{
    public class Question
    {
        public Guid QuestionId { get; set; } = Guid.NewGuid();
        public Guid? SurveyId { get; set; }
        public string QuestionText { get; set; }
        public int? Order { get; set; }
        public bool IsOptional { get; set; } 
        public ICollection<QuestionOption>? QuestionOptions { get; set; }
        public Survey? Survey { get; set; }
    }
}
