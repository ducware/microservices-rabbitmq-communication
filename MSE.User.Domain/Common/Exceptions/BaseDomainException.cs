using Newtonsoft.Json;
using System.Net;

namespace MSE.User.Domain.Common.Exceptions
{
    public class BaseDomainException : Exception
    {
        public string Code { get; set; }

        public int Status { get; set; }

        public BaseDomainException(string code, string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message)
        {
            this.Code = code;
            this.Status = (int)statusCode;
        }

        public virtual dynamic GetMessage() => new { Code, message = Message };

        public override string ToString()
        {
            return JsonConvert.SerializeObject(GetMessage(), new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
