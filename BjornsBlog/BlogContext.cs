using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using BjornsBlog.Entities;
using BjornsBlog.Mappers;

namespace BjornsBlog
{
    public class BlogContext: DbContext
    {
        public BlogContext(): base("bjornsBlogConnection")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BlogContext, BlogContextMigrationConfiguration>());
        }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Reply> Replies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new TopicMapper()); 
            modelBuilder.Configurations.Add(new ReplyMapper());
            base.OnModelCreating(modelBuilder);
        }
    }
}
