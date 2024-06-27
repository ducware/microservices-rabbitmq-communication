using MSE.User.Domain.Common.Queries;
using MSE.User.Domain.Models;

namespace MSE.User.Application.Queries.Users
{
    public class GetUserByIdQuery : IQuery<UserInfoDto>
    {
        public int Id { get; set; }

        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
