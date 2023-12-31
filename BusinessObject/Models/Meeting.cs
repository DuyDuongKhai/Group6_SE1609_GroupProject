﻿using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject.Models
{
    public partial class Meeting
    {
        public int MeetingId { get; set; }
        public int? GroupId { get; set; }
        public string MeetingTitle { get; set; }
        public DateTime? MeetingDateTime { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        public virtual Group Group { get; set; }
    }
}
