using MSE.User.Domain.Common.Commands;
using MSE.User.Domain.Models;

namespace MSE.User.Application.Commands.UserAccounts
{
    public class CreateUserAccountCommand : ICommand<BaseCommandResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }
    }
}
