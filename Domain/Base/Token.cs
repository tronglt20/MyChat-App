using Domain.Entities;

namespace Domain.Base
{
    public class Token : Entity
    {
        public Token()
        {

        }

        public Token(string token, DateTime dateTime)
        {
            Content = token;
            Expiration = dateTime;
        }

        public string Content { get; set; }
        public DateTime? Expiration { get; set; }

        public virtual ICollection<UserRegistration> UserRegistrations { get; set; } = new HashSet<UserRegistration>();
    }
}
