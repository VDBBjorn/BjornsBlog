namespace BjornsBlog
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTask : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Body = c.String(nullable: false, maxLength: 500),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Replies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(nullable: false, maxLength: 500),
                        CreationDate = c.DateTime(nullable: false),
                        TopicId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Topics", t => t.TopicId)
                .Index(t => t.TopicId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Replies", "TopicId", "dbo.Topics");
            DropIndex("dbo.Replies", new[] { "TopicId" });
            DropTable("dbo.Replies");
            DropTable("dbo.Topics");
        }
    }
}
