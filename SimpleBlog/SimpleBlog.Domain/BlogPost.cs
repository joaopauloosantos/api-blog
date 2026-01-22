using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleBlog.Domain
{
    [Table("blog_post")]
    public class BlogPost
    {
        public BlogPost()
        {
            Id = Guid.NewGuid();
            Comments = [];
        }

        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("title")]
        [Required(ErrorMessage = "O título é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O título deve ter no máximo 100 caracteres.")]
        [MinLength(5, ErrorMessage = "O título deve ter pelo menos 5 caracteres.")]
        public string Title { get; set; } = string.Empty;
        
        [Column("content")]
        [Required(ErrorMessage = "O conteúdo do post não pode ficar vazio.")]
        [MaxLength(500, ErrorMessage = "O título deve ter no máximo 500 caracteres.")]
        [MinLength(5, ErrorMessage = "O título deve ter pelo menos 5 caracteres.")]
        public string Content { get; set; } = string.Empty;

        public virtual ICollection<Comment>? Comments { get; set; }
    }
}
