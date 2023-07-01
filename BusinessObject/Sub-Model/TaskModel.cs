using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using BusinessObject.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BusinessObject.Sub_Model
{
    public class TaskModel
    {

        public int? GroupId { get; set; }
        public int? AssignedToUserId { get; set; }
        public string TaskTitle { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual UserModel AssignedToUser { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual GroupModel Group { get; set; }
    }
}
