using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleBlog.Domain
{
    [Table("comment")]
    public class Comment
    {
        public Comment()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("text")]
        [Required(ErrorMessage = "O comentário não pode ser vazio.")]
        [MaxLength(500, ErrorMessage = "O comentário não pode exceder 500 caracteres.")]
        public string Text { get; set; } = string.Empty;

        [Column("postId")]
        [Required(ErrorMessage = "O campo PostId é obrigatório.")]        
        public Guid PostId { get; set; }

        [ForeignKey("PostId")]
        public virtual BlogPost? BlogPost { get; set; }
    }
}
