using SignalRAssignment.Domain.Models;

namespace SignalRAssignment.Domain.DTO
{
    public class LoginUser
    {
        public int AccountId { get; set; }
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? Type { get; set; }
    }
}
