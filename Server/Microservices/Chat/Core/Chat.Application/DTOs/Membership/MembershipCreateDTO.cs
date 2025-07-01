namespace Chat.Application.DTOs.Membership
{
    public class MembershipCreateDTO
    {
        public Guid MemderId { get; set; }
        public Guid ChatId { get; set; }
        public DateTime JoinedAt { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime? BlockedAt { get; set; }
        public string MemberCustomName { get; set; }
    }
}
