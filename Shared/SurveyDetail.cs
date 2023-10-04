using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyPulse.Shared
{
    public class SurveyDetail
    {
        public Guid SurveyDetailId { get; set; } = Guid.NewGuid();
        public Guid SurveyId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Owner { get; set; }
        public bool DisplayRespondents { get; set; }
        //public int ResponsesDone { get; set; }

        public Survey? Survey { get; set; }
    }
}
