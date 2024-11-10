using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyMvc.Entities.Model.Result
{
    public class ResultApi<T>
    {
        public T R { get; set; }
        public int OperationStatusCode { get; set; }
        public bool OperationStatus { get; set; }
        public string OperationMessage { get; set; }
        public DateTime Date { get; set; }
        public string Service { get; set; }
        public string Method { get; set; }
        public bool ErrorStatus { get; set; }
        public int ErrorCode { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }

    }
}
