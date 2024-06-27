namespace MSE.User.Application.Services
{
    public interface IProducerService
    {
        Task SendAsync(string queueName, object data);
    }
}
