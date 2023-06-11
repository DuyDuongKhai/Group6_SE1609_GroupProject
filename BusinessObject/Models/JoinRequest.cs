using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject.Models
{
    public partial class JoinRequest
    {
        public int RequestId { get; set; }
        public int? GroupId { get; set; }
        public int? UserId { get; set; }
        public string Status { get; set; }

        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
    }
}
