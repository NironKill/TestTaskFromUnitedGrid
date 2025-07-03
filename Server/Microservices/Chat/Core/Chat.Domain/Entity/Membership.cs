using Chat.Domain.Base;

namespace Chat.Domain.Entity
{
    public class Membership : BaseEntity
    {
        public Guid MemderId { get; set; }
        public Guid ChatId { get; set; }
        public DateTime JoinedAt { get; set; }
        public bool IsBlocked { get; set; } = false;
        public DateTime? BlockedAt { get; set; }
        public string MemberCustomName { get; set; }

        public Member Member { get; set; }
        public Chat Chat { get; set; }
    }
}
