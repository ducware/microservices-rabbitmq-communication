using MSE.SendMail.Domain.Models;
using System.Net.Http.Json;

namespace MSE.SendMail.Application.Communicate.Users
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("UserServiceClient");
        }

        public async Task<UserInfoDto> GetUserInfoByIdAsync(int userId)
        {
            var response = await _httpClient.GetFromJsonAsync<UserInfoDto>($"api/v1/users/{userId}");
            return response;
        }
    }
}
