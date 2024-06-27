using MSE.User.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSE.User.Domain.Entities
{
    public class UserAccount : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}
