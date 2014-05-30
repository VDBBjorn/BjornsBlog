using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BjornsBlog.Entities;
using BjornsBlog.Mappers;

namespace BjornsBlog
{
    public class BlogRepository: IBlogRepository
    {
        private BlogContext _context;

        public BlogRepository(BlogContext context)
        {
            this._context = context;
        }

        public IQueryable<Topic> GetAllTopics()
        {
            return _context.Topics.AsQueryable();
        }

        public Topic GetTopic(int topicId, bool includeReplies = true)
        {
            //if(!includeReplies)
            //    return _context.Topics.SingleOrDefault(t => t.Id == topicId);
            return _context.Topics.Include("Replies").SingleOrDefault(t => t.Id == topicId);
        }

        public IQueryable<Reply> GetReplies(int topicId)
        {
            var topic = GetTopic(topicId);
            var replies =  _context.Topics.Find(topicId).Replies.AsQueryable();
            return replies;
        }

        public Reply GetReply(int replyId)
        {
            return _context.Replies.SingleOrDefault(t => t.Id == replyId);
        }

        public bool AddTopic(Topic topic)
        {
            try
            {
                _context.Topics.Add(topic);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddReply(Reply reply, int topicId)
        {
            try
            {
                _context.Topics.Find(topicId).Replies.Add(reply);
                //SaveAll(); ??
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Topic originalTopic, Topic updatedTopic)
        {
            _context.Entry(originalTopic).CurrentValues.SetValues(updatedTopic);
            return true;
        }

        public bool Update(Reply originalReply, Reply updatedReply)
        {
            _context.Entry(originalReply).CurrentValues.SetValues(updatedReply);
            return true;
        }

        public bool DeleteTopic(int topicId)
        {
            try
            {
                var entity = _context.Topics.Find(topicId);
                if (entity != null)
                {
                    _context.Topics.Remove(entity);
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteReply(int replyId)
        {
            try
            {
                var entity = _context.Replies.Find(replyId);
                if (entity != null)
                {
                    _context.Replies.Remove(entity);
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
