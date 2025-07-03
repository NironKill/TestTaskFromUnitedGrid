using Chat.Domain.Base;

namespace Chat.Domain.Entity
{
    public class Chat : BaseEntity
    {
        public string Name { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; } = false;

        public ICollection<Member> Members { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<Membership> Memberships { get; set; }
    }
}
