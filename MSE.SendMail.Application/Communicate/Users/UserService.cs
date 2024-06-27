using MSE.Common.Constants;
using MSE.SendMail.Domain.Models;
using System.Net.Http.Json;

namespace MSE.SendMail.Application.Communicate.Users
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(UtilConst.ApiGatewayClient);
        }

        public async Task<UserInfoDto> GetUserInfoByIdAsync(int userId)
        {
            var response = await _httpClient.GetFromJsonAsync<UserInfoDto>($"{UtilConst.UserServiceEndpoint}/api/v1/users/{userId}");
            return response;
        }
    }
}
