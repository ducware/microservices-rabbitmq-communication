namespace MSE.Common.Models
{
    public class UserAccountDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
