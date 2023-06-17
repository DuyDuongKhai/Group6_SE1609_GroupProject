using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using BusinessObject.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BusinessObject.Sub_Model
{
    public class GroupModel
    {
        public string GroupName { get; set; }
        public int? GroupAdminId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Description { get; set; }

       
    }
}
