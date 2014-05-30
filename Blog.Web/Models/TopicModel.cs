using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Blog.Web.Models
{
    [DataContract]
    public class TopicModel
    {
        [DataMember(Name = "id", IsRequired = false)]
        public int Id { get; set; }
        [DataMember(Name= "title", IsRequired = true)] 
        public string Title { get; set; }
        [DataMember(Name = "body", IsRequired = true)]
        public string Body { get; set; }
        [DataMember(IsRequired = false)]
        public DateTime CreationDate { get; set; }
        [DataMember(IsRequired = false)]
        public string Url { get; set; }
    }
}