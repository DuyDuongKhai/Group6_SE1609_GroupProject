using System;
using System.Linq;
using System.Text;
using BusinessObject.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BusinessObject.Sub_Model
{
    public class PostModel
    {
        public int PostId { get; set; }
        public int? GroupId { get; set; }
        public int? UserId { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual GroupModel Group { get; set; }
        public virtual UserModel User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

    }
}
