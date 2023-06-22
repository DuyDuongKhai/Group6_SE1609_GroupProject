using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class TaskDto
    {
        [Required]
        public string TaskTitle { get; set; }

        public string Description { get; set; }

        [Required]
        public int AssignedToUserId { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
