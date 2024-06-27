using MSE.User.Domain.Common.Commands;
using MSE.User.Domain.Enums;
using MSE.User.Domain.Models;

namespace MSE.User.Application.Commands.Users
{
    public class CreateUserCommand : ICommand<BaseCommandResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; } = Gender.Unknown;
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
