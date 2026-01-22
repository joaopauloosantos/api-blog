using Mapster;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace SimpleBlog.Dto.Dto
{
    public class BlogPostDto
    {
        [SwaggerSchema(ReadOnly = true)]
        [JsonPropertyName("id")]
        public Guid? Id { get; set; }

        [JsonPropertyName("manchete")]
        [AdaptMember("Title")]
        public string Manchete { get; set; } = string.Empty;

        [JsonPropertyName("corpo_texto")]
        [AdaptMember("Content")]
        public string CorpoTexto { get; set; } = string.Empty;

        [SwaggerSchema(ReadOnly = true)]
        [JsonPropertyName("TotalComentarios")]
        public int? TotalComentarios { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        [JsonPropertyName("comentarios")]
        [AdaptMember("Comments")]
        public IEnumerable<CommentDto> Comentarios { get; set; } = [];
    }
}
