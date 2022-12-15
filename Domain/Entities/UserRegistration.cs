using Domain.Base;
using Domain.Enums;

namespace Domain.Entities
{
    public class UserRegistration : EntityBase
    {
        public UserRegistration()
        {

        }

        public UserRegistration(string email, string token)
        {
            Email = email;
            Status = UserRegistrationStatusEnum.Open;
            CreatedOn = DateTime.UtcNow;
            
            Id = Guid.NewGuid();
            Token = new Token(token, DateTime.UtcNow.AddDays(2));
        }

        public Guid Id { get; set; }
        public string Email { get; set; }
        public UserRegistrationStatusEnum Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public int TokenId { get; set; }

        public virtual Token Token { get; set; }
    }
}
