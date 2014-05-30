using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BjornsBlog.Entities
{
    public sealed class Topic
    {
        public Topic()
        {
            Replies = new List<Reply>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<Reply> Replies { get; set; }
    }
}
