using MassTransit;
using MSE.Common.Models;
using MSE.SendMail.Application.Communicate;
using MSE.SendMail.Domain.Models;

namespace MSE.SendMail.Application.Services
{
    public class UserAccountConsumerService : IConsumer<UserAccountDto>
    {
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;

        public UserAccountConsumerService(IEmailService emailService, IUserService userService)
        {
            _emailService = emailService;
            _userService = userService;
        }

        public async Task Consume(ConsumeContext<UserAccountDto> context)
        {
            var data = context.Message;

            var userInfo = await _userService.GetUserInfoByIdAsync(data.UserId);

            var message = new Message(new string[] { userInfo.Email }, $"Thank you {userInfo.FirstName} Your account has been created", $"Username: {data.Username} - Password: {data.Password}");
            _emailService.SendEmail(message);
            await Task.CompletedTask;
        }
    }
}
