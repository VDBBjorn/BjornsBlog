﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BjornsBlog.Entities
{
    public class Reply
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime CreationDate { get; set; }
        public int TopicId { get; set; }
        public virtual Topic Topic { get; set; }

    }
}
