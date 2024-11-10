using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyMvc.Entities.Dto
{
    public class ResultBayboss<T>
    {
        public T R { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Value { get; set; }
        public string Value2 { get; set; }
        public string Value3 { get; set; }
        public int Logicalref { get; set; }
        public byte[] Bytes { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }

        public static ResultBayboss<T> Success(T iData, int iStatusCode, string iMessage)
        {
            return new ResultBayboss<T>
            {
                R = iData,
                StatusCode = iStatusCode,
                Message = iMessage,
            };
        }

        public static ResultBayboss<T> Fail(T Data, int iStatusCode, string iMessage)
        {
            return new ResultBayboss<T>
            {
                R = Data,
                StatusCode = iStatusCode,
                Message = iMessage
            };
        }

        public static ResultBayboss<T> DownloadFileSuccess(int iStatusCode, byte[] iBytes, string iFileName, string iContentType)
        {
            return new ResultBayboss<T>
            {
                StatusCode = iStatusCode,
                Bytes = iBytes,
                FileName = iFileName,
                ContentType = iContentType
            };
        }
    }
}
