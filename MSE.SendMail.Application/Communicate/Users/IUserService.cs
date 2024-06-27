using MSE.SendMail.Domain.Models;

namespace MSE.SendMail.Application.Communicate
{
    public interface IUserService
    {
        Task<UserInfoDto> GetUserInfoByIdAsync(int userId);
    }
}
