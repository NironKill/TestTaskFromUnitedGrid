using Chat.Domain.Base;

namespace Chat.Domain.Entity
{
    public class Attachment : BaseEntity
    {
        public string ContentURL { get; set; }
        public int Type { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
