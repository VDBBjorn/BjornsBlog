using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BjornsBlog;
using Blog.Web.Models;

namespace Blog.Web.Controllers
{
    public class BaseApiController : ApiController
    {
        private readonly IBlogRepository _repository;
        private ModelFactory _modelFactory;

        public BaseApiController(IBlogRepository blogRepository)
        {
            this._repository = blogRepository;
        }

        protected IBlogRepository TheRepository
        {
            get
            {
                return _repository;
            }
        }

        protected ModelFactory TheModelFactory
        {
            get { return _modelFactory ?? (_modelFactory = new ModelFactory(Request, TheRepository)); }
        }
    }
}
