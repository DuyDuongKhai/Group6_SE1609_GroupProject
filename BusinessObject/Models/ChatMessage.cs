using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject.Models
{
    public partial class ChatMessage
    {
        public int MessageId { get; set; }
        public int? GroupId { get; set; }
        public int? UserId { get; set; }
        public string Content { get; set; }
        public DateTime? SentAt { get; set; }

        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
    }
}
