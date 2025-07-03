namespace Chat.Application.DTOs.Attached
{
    public class AttachmentGetDTO
    {
        public Guid Id { get; set; }
        public string ContentURL { get; set; }
        public int Type { get; set; }
    }
}
