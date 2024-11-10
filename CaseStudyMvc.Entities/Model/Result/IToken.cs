using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyMvc.Entities.Model.Result
{
    public class IToken
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
