using Mapster;
using SimpleBlog.Domain;
using SimpleBlog.Dto.Dto;
using SimpleBlog.Dto.Notifications;
using SimpleBlog.Repository.Interfaces;
using SimpleBlog.Service.Interfaces;

namespace SimpleBlog.Service.Services
{
    public class CommentService(IBlogPostRepository blogPostRepository, ICommentRepository commentRepository, INotifier notifier) : ICommentService
    {
        private readonly IBlogPostRepository _blogPostRepository = blogPostRepository;
        private readonly ICommentRepository _commentRepository = commentRepository;
        private readonly INotifier _notifier = notifier;

        public async Task<CommentDto?> CreateCommentAsync(CommentDto dto)
        {
            var post = await _blogPostRepository.GetByIdAsync(dto.PostId);
            if (post == null)
            {
                _notifier.Handle(new Notification("Post não encontrado."));
                return null;
            }

            var comment = dto.Adapt<Comment>();

            await _commentRepository.AddAsync(comment);
            await _commentRepository.SaveChangesAsync();

            return comment.Adapt<CommentDto>();
        }

        public async Task<CommentDto?> GetCommentByIdAsync(Guid id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);

            if (comment == null)
            {
                _notifier.Handle(new Notification("Post não encontrado."));
                return null;
            }

            return comment.Adapt<CommentDto>();
        }
    }
}
