using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject.Models
{
    public partial class User
    {
        public User()
        {
            ChatMessages = new HashSet<ChatMessage>();
            Comments = new HashSet<Comment>();
            GroupMembers = new HashSet<GroupMember>();
            Groups = new HashSet<Group>();
            JoinRequests = new HashSet<JoinRequest>();
            Posts = new HashSet<Post>();
            Tasks = new HashSet<Task>();
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public virtual ICollection<ChatMessage> ChatMessages { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<GroupMember> GroupMembers { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<JoinRequest> JoinRequests { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
