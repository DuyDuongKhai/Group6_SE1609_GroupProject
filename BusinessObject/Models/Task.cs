using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace BusinessObject.Models
{
    public partial class Task
    {
        public int TaskId { get; set; }
        public int? GroupId { get; set; }
        public int? AssignedToUserId { get; set; }
        public string TaskTitle { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public virtual User AssignedToUser { get; set; }
        public virtual Group Group { get; set; }
    }
}
