using System;
using System.Collections.Generic;
using System.Linq;
using BjornsBlog.Entities;

namespace BjornsBlog
{
    public class BlogDataSeeder
    {
        private readonly BlogContext _context;

        public BlogDataSeeder(BlogContext context)
        {
            this._context = context;
        }

        public void Seed()
        {
            if (_context.Topics.Any())
                return;

            try
            {
                var reply1 = new Reply
                {
                    Body = "Awesome!!",
                    CreationDate = DateTime.Now
                };
                var reply2 = new Reply
                {
                    Body = "Cool!!",
                    CreationDate = DateTime.Now
                };
                var reply3 = new Reply
                {
                    Body = "Nice!!",
                    CreationDate = DateTime.Now
                };

                var replies = new List<Reply>
                {
                    reply1, reply2, reply3
                };

                for (var i = 0; i < 5; i++)
                {
                    var topic = new Topic
                    {
                        Title = "First post!",
                        Body = "So exiting!",
                        CreationDate = DateTime.Now,
                        Replies = replies.ToList()
                    };
                    _context.Topics.Add(topic);
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                string message = ex.ToString();
                throw ex;
            }
        }
    }
}
