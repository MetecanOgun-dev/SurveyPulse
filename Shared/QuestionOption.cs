using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyPulse.Shared
{
    public class QuestionOption
    {
        public Guid QuestionOptionId { get; set; } = Guid.NewGuid();
        public Guid QuestionId { get; set; }
        public string OptionText { get; set; }
        public bool IsCorrectOption { get; set; }

        public Question? Question { get; set; }
    }
}
