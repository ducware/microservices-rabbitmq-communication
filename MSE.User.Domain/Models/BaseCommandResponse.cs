namespace MSE.User.Domain.Models
{
    public class BaseCommandResponse
    {
        public string Code { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public BaseCommandResponse()
        {
            
        }

        public BaseCommandResponse(string code, string message)
        {
            this.Code = code;
            this.Message = message;
        }
    }
}
