namespace Chat.Application.DTOs.Membership
{
    public class MembershipUpdateDTO
    {
        public Guid Id { get; set; }
        public bool? IsBlocked { get; set; }
        public DateTime? BlockedAt { get; set; }
        public string? MemberCustomName { get; set; }
    }
}
