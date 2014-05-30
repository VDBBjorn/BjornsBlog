using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BjornsBlog;
using BjornsBlog.Entities;
using Blog.Web.Models;

namespace Blog.Web.Controllers
{
    public class RepliesController : BaseApiController
    {
        public RepliesController(IBlogRepository blogRepository) : base(blogRepository){}

        public IEnumerable<ReplyModel> Get(int topicId)
        {
            var query = TheRepository.GetReplies(topicId);
            var results = query.ToList().Select(s => TheModelFactory.Create(s));
            return results;
        }

        public HttpResponseMessage Post(int topicId, [FromBody] ReplyModel replyModel)
        {
            try
            {
                var entity = TheModelFactory.Parse(replyModel);
                if (entity == null)
                {
                    Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not read reply from body");
                }
                else
                {
                    entity.CreationDate = DateTime.Now;
                    entity.TopicId = topicId;
                }

                if (TheRepository.AddReply(entity,topicId) && TheRepository.SaveAll())
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

        [HttpPatch]
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody] ReplyModel replyModel)
        {
            try
            {
                var updatedReply = TheModelFactory.Parse(replyModel);

                if (updatedReply == null) Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not read reply from body");

                var originalReply = TheRepository.GetReply(replyModel.Id);

                if (originalReply == null || originalReply.Id != id)
                {
                    return Request.CreateResponse(HttpStatusCode.NotModified, "Reply is not found");
                }
                updatedReply.Id = id;

                if (TheRepository.Update(originalReply, updatedReply) && TheRepository.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, TheModelFactory.Create(updatedReply));
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
                var reply = TheRepository.GetReply(id);

                if (reply == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                if (TheRepository.DeleteReply(id) && TheRepository.SaveAll())
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
