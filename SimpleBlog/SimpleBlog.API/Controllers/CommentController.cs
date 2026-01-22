using Microsoft.AspNetCore.Mvc;
using SimpleBlog.Dto.Dto;
using SimpleBlog.Dto.Notifications;
using SimpleBlog.Service.Interfaces;

namespace SimpleBlog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
    public class CommentController : BaseController
    {
        private readonly ICommentService _commentService;

        public CommentController(INotifier notifier, ICommentService commentService) : base(notifier)
        {
            _commentService = commentService;
        }

        /// <summary>
        /// Adiciona um novo comentário a um post específico
        /// </summary>
        /// <param name="postId">ID do Post (Vem da URL)</param>
        /// <param name="dto">Dados do comentário (Vem do Corpo/JSON)</param>
        /// <returns>O comentário criado</returns>
        [HttpPost("~/api/posts/{postId:guid}/comments")]
        [ProducesResponseType(typeof(CommentDto), 200)]
        [ProducesResponseType(typeof(ErrorResponseDto), 400)]
        public async Task<IActionResult> Create(Guid postId, [FromBody] CommentDto dto)
        {
            var result = await _commentService.CreateCommentAsync(dto);

            if (OperacaoValida() && result != null)
            {
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, new { success = true, data = result });
            }

            return CustomResponse(result);
        }

        /// <summary>
        /// Obtém um post por ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CommentDto), 200)]
        [ProducesResponseType(typeof(ErrorResponseDto), 400)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _commentService.GetCommentByIdAsync(id);
            return CustomResponse(result);
        }
    }
}
