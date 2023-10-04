using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyPulse.Shared.ResponseModels
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
