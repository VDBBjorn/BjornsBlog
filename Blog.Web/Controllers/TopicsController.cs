using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BjornsBlog;
using BjornsBlog.Entities;
using Blog.Web.Models;

namespace Blog.Web.Controllers
{
    //[Filters.ForceHttps()]
    public class TopicsController : BaseApiController
    {
        public TopicsController(IBlogRepository blogRepository) : base(blogRepository)
        {
        }

        public IEnumerable<TopicModel> GetAll()
        {
            IQueryable<Topic> query = TheRepository.GetAllTopics();
            var results = query.ToList().Select(s => TheModelFactory.Create(s));
            return results;
        }

        public HttpResponseMessage GetTopic(int topicId)
        {
            try
            {
                var topic = TheRepository.GetTopic(topicId);
                return topic != null ? Request.CreateResponse(HttpStatusCode.OK, TheModelFactory.Create(topic)) : Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        
        public HttpResponseMessage Post([FromBody] TopicModel newTopic)
        {
            try
            {
                var entity = TheModelFactory.Parse(newTopic);
                if (entity == null)
                    Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not read topic from body");

                if (TheRepository.AddTopic(entity) && TheRepository.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, TheModelFactory.Create(entity));
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not save to database");
            }
            catch (Exception e)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [System.Web.Http.HttpPatch]
        [System.Web.Http.HttpPut]
        public HttpResponseMessage Put(int id, [FromBody] TopicModel topicModel)
        {
            try
            {
                var updatedTopic = TheModelFactory.Parse(topicModel);

                if (updatedTopic == null) Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not read topic from body");

                var originalTopic = TheRepository.GetTopic(id, false);

                if (originalTopic == null || originalTopic.Id != id)
                {
                    return Request.CreateResponse(HttpStatusCode.NotModified, "Topic is not found");
                }
                updatedTopic.Id = id;

                if (TheRepository.Update(originalTopic, updatedTopic) && TheRepository.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, TheModelFactory.Create(updatedTopic));
                }
                return Request.CreateResponse(HttpStatusCode.NotModified);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var topic = TheRepository.GetTopic(id);

                if (topic == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                if (topic.Replies.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Can not delete topic, replies still exist.");
                }

                if (TheRepository.DeleteTopic(id) && TheRepository.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
