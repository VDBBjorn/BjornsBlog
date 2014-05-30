using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BjornsBlog.Entities;

namespace BjornsBlog.Mappers
{
    class TopicMapper: EntityTypeConfiguration<Topic>
    {
        public TopicMapper()
        {
            this.ToTable("Topics");

            this.HasKey(t => t.Id);
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Id).IsRequired();
            this.Property(t => t.Title).HasMaxLength(100).IsRequired();
            this.Property(t => t.Body).IsRequired().HasMaxLength(500);
            this.Property(t => t.CreationDate).IsRequired();

            this.HasMany(t => t.Replies).WithRequired(t => t.Topic).HasForeignKey(t => t.TopicId);
        }
    }
}
