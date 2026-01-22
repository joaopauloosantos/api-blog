using Microsoft.AspNetCore.Mvc;
using SimpleBlog.Dto.Dto;
using SimpleBlog.Dto.Notifications;
using SimpleBlog.Service.Interfaces;

namespace SimpleBlog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogPostController : BaseController
    {
        private readonly IBlogPostService _blogPostService;

        public BlogPostController(INotifier notifier, IBlogPostService blogPostServic) : base(notifier)
        {
            _blogPostService = blogPostServic;
        }

        /// <summary>
        /// Retorna uma lista de todos os posts do blog
        /// </summary>
        [HttpGet("~/api/posts")]
        [ProducesResponseType(typeof(IEnumerable<BlogPostDto>), 200)]
        [ProducesResponseType(typeof(ErrorResponseDto), 400)]
        [ProducesResponseType(typeof(ErrorResponseDto), 500)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _blogPostService.GetAllPostsAsync();
            return CustomResponse(result);
        }

        /// <summary>
        /// Cria um novo post
        /// </summary>
        [HttpPost("~/api/posts")]
        [ProducesResponseType(typeof(BlogPostDto), 201)]
        [ProducesResponseType(typeof(ErrorResponseDto), 400)]
        public async Task<IActionResult> Create([FromBody] BlogPostDto dto)
        {
            var result = await _blogPostService.CreatePostAsync(dto);

            if (OperacaoValida() && result != null)
            {
                return CreatedAtAction(null, new { id = result.Id }, new { success = true, data = result });
            }

            return CustomResponse(result);
        }

        /// <summary>
        /// Obtém um post por ID
        /// </summary>
        [HttpGet("~/api/posts/{id}/comments")]
        [ProducesResponseType(typeof(BlogPostDto), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetByIdWithCommentsAsync(Guid id)
        {
            var result = await _blogPostService.GetByIdWithCommentsAsync(id);
            return CustomResponse(result);
        }
    }
}
