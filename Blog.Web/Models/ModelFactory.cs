using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using BjornsBlog;
using BjornsBlog.Entities;

namespace Blog.Web.Models
{
    public class ModelFactory
    {
        private UrlHelper _urlHelper;
        private IBlogRepository _repository;
        public ModelFactory(HttpRequestMessage request, IBlogRepository repository)
        {
            _urlHelper = new UrlHelper(request);
            _repository = repository;
        }

        public TopicModel Create(Topic topic)
        {
            return new TopicModel
            {
                Url = _urlHelper.Link("Topics", new {id = topic.Id}),
                Id = topic.Id,
                Title = topic.Title,
                Body = topic.Body,
                CreationDate = topic.CreationDate
            };
        }

        public ReplyModel Create(Reply reply)
        {
            return new ReplyModel
            {
                Id = reply.Id,
                Body = reply.Body,
                CreationDate = reply.CreationDate
            };
        }

        public Topic Parse(TopicModel model)
        {
            try
            {
                var topic = new Topic
                {
                    Title = model.Title,
                    Body = model.Body,
                    CreationDate = DateTime.Now
                };
                return topic;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Reply Parse(ReplyModel model)
        {
            try
            {
                var reply = new Reply
                {
                    Body = model.Body
                };
                return reply;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}