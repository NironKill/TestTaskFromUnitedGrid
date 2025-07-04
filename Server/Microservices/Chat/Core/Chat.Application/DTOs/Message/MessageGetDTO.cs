﻿namespace Chat.Application.DTOs.Message
{
    public class MessageGetDTO
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public Guid ChatId { get; set; }
        public Guid? AttachedId { get; set; }
        public string Text { get; set; }
        public DateTime SentAt { get; set; }
        public DateTime? EditedAt { get; set; }
        public bool IsRead { get; set; } 
    }
}
