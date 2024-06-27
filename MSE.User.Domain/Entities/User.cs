using MSE.User.Domain.Common;
using MSE.User.Domain.Enums;

namespace MSE.User.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; } = Gender.Unknown;
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
