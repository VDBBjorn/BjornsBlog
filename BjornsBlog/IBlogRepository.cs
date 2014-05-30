using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BjornsBlog.Entities;

namespace BjornsBlog
{
    public interface IBlogRepository
    {
        IQueryable<Topic> GetAllTopics();
        Topic GetTopic(int topicId, bool includeReplies = true);
        IQueryable<Reply> GetReplies(int topicId);
        Reply GetReply(int replyId);

        bool AddTopic(Topic topic);
        bool AddReply(Reply reply, int topicId);

        bool Update(Topic originalTopic, Topic updatedTopic);
        bool Update(Reply originalReply, Reply updatedReply);

        bool DeleteTopic(int topicId);
        bool DeleteReply(int replyId);

        bool SaveAll();
    }
}
