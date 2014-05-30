using System.Data.Entity.Migrations;
using BjornsBlog.Mappers;

namespace BjornsBlog
{
    public class BlogContextMigrationConfiguration: DbMigrationsConfiguration<BlogContext>
    {
        public BlogContextMigrationConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

#if DEBUG
        protected override void Seed(BlogContext context)
        {
            new BlogDataSeeder(context).Seed();
        }
#endif
    }
}
