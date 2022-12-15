using Domain.Base;

namespace Domain.Entities
{
    public class Room : Entity
    {
        public Room()
        {

        }

        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public bool IsDelete { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual ICollection<Message> Message { get; set; } = new HashSet<Message>();

        public virtual ICollection<User> User { get; set; } = new HashSet<User>();
    }
}
