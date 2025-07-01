namespace Chat.Application.DTOs.Member
{
    public class MemberGetDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
    }
}
