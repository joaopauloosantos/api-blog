using Mapster;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace SimpleBlog.Dto.Dto
{
    public class CommentDto
    {
        [SwaggerSchema(ReadOnly = true)]
        [JsonPropertyName("id")]
        public Guid? Id { get; set; }

        [JsonPropertyName("texto")]
        [AdaptMember("Text")]
        public string Texto { get; set; } = string.Empty;

        [JsonPropertyName("post_id")]
        [AdaptMember("PostId")]
        public Guid PostId { get; set; }
    }
}
