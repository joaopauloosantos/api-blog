using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleBlog.Domain;

namespace SimpleBlog.Repository.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("comment");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(c => c.Text)
                .HasColumnName("text")
                .HasMaxLength(500)
                .HasColumnType("varchar(500)")
                .IsRequired();

            builder.Property(c => c.PostId)
                .HasColumnName("postId")
                .IsRequired();

            // Relacionamento
            builder.HasOne(c => c.BlogPost)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId);
        }
    }
}
