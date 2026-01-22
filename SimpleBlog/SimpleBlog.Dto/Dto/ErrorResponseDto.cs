using System.Text.Json.Serialization;

namespace SimpleBlog.Dto.Dto
{
    public class ErrorResponseDto
    {
        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("success")]
        public bool Success { get; set; }        

        [JsonPropertyName("errors")]
        public List<string> Errors { get; set; } = [];
    }
}
