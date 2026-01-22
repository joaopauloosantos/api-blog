using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleBlog.Domain;

namespace SimpleBlog.Repository.Configurations
{
    public class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
    {
        public void Configure(EntityTypeBuilder<BlogPost> builder)
        {
            builder.ToTable("blog_post");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(p => p.Title)
                .HasColumnName("title")
                .HasMaxLength(100)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(p => p.Content)
                .HasColumnName("content")
                .HasMaxLength(500)
                .HasColumnType("varchar(500)")
                .IsRequired();

            // Relacionamento
            builder.HasMany(p => p.Comments)
                .WithOne(c => c.BlogPost)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
