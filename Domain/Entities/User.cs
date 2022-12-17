using Domain.Base;

namespace Domain.Entities
{
    public class User : Entity
    {
        public User()
        {

        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Message> Messages { get; set; } = new HashSet<Message>();
        public virtual ICollection<Room> Rooms { get; set; } = new HashSet<Room>();

        public virtual ICollection<Room> RoomUsers { get; set; } = new HashSet<Room>();
    }
}
