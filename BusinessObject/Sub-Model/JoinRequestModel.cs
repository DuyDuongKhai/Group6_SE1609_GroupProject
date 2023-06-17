using System;
using System.Linq;
using System.Text;
using BusinessObject.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BusinessObject.Sub_Model
{
    public class JoinRequestModel
    {
        public int RequestId { get; set; }
        public int? GroupId { get; set; }
        public int? UserId { get; set; }
        public string Status { get; set; }

        public virtual GroupModel Group { get; set; }
        public virtual UserModel User { get; set; }
    }
}
