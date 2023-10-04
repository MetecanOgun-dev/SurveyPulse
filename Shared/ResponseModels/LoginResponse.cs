using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyPulse.Shared.ResponseModels
{
    public class LoginResponse: ApiResponse
    {
        public string? Token { get; set; }
    }
}
