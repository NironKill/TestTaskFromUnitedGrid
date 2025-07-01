using System.ComponentModel.DataAnnotations;

namespace Chat.Domain
{
    public class Attachment
    {
        [Key]
        public Guid Id { get; set; }
        public string ContentURL { get; set; }
        public int Type { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
