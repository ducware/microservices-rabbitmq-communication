using MSE.SendMail.Domain.Models;

namespace MSE.SendMail.Application.Services
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
