using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BusinessObject.Sub_Model
{
    public partial class GroupMemberModel
    {
        public int GroupMemberId { get; set; }
        public int? GroupId { get; set; }
        public int? UserId { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public DateTime? JoinedAt { get; set; }

    }
}
