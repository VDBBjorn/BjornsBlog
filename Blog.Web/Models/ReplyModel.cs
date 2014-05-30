using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Web.Models
{
    public class ReplyModel
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime CreationDate { get; set; }
    }
}