using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject.Models
{
    public partial class Group
    {
        public Group()
        {
            ChatMessages = new HashSet<ChatMessage>();
            GroupMembers = new HashSet<GroupMember>();
            JoinRequests = new HashSet<JoinRequest>();
            Meetings = new HashSet<Meeting>();
            Posts = new HashSet<Post>();
            Tasks = new HashSet<Task>();
        }

        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int? GroupAdminId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Description { get; set; }

        public virtual User GroupAdmin { get; set; }
        public virtual ICollection<ChatMessage> ChatMessages { get; set; }
        public virtual ICollection<GroupMember> GroupMembers { get; set; }
        public virtual ICollection<JoinRequest> JoinRequests { get; set; }
        public virtual ICollection<Meeting> Meetings { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
