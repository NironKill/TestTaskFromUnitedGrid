using System.ComponentModel.DataAnnotations;

namespace Chat.Domain
{
    public class Member
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }

        public ICollection<Chat> Chats { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<Membership> Memberships { get; set; }
    }
}
