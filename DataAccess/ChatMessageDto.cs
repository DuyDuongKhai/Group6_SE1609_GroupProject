﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ChatMessageDto
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
    }
}
