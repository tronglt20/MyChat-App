using Domain.Base;

namespace Domain.Entities
{
    public class Message : Entity
    {
        public Message()
        {

        }

        public int SenderId { get; set; }
        public int RoomId { get; set; }
        public string Content { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual Room Room { get; set; }
        public virtual User Sender { get; set; }
    }
}
