using Chat.Domain.Base;

namespace Chat.Domain.Entity
{
    public class Member : BaseEntity
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }

        public ICollection<Chat> Chats { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<Membership> Memberships { get; set; }
    }
}
