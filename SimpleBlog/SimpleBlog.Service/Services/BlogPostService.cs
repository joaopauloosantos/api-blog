using Mapster;
using SimpleBlog.Domain;
using SimpleBlog.Dto.Dto;
using SimpleBlog.Dto.Notifications;
using SimpleBlog.Repository.Interfaces;
using SimpleBlog.Service.Interfaces;

namespace SimpleBlog.Service.Services
{
    public class BlogPostService(IBlogPostRepository blogPostRepository, INotifier notifier) : IBlogPostService
    {
        private readonly IBlogPostRepository _blogPostRepository = blogPostRepository;
        private readonly INotifier _notifier = notifier;

        public async Task<IEnumerable<BlogPostDto>> GetAllPostsAsync() =>
            await _blogPostRepository.GetAllWithCommentsAsync();

        public async Task<BlogPostDto?> CreatePostAsync(BlogPostDto dto)
        {
            var existingPost = await _blogPostRepository.ExistsTitleAsync(dto.Manchete);
            if (existingPost)
            {
                _notifier.Handle(new Notification("Já existe um post com essa manchete."));
                return null;
            }

            var postEntity = dto.Adapt<BlogPost>();

            await _blogPostRepository.AddAsync(postEntity);
            await _blogPostRepository.SaveChangesAsync();

            return postEntity.Adapt<BlogPostDto>();
        }

        public async Task<BlogPostDto?> GetByIdWithCommentsAsync(Guid id)
        {
            var post = await _blogPostRepository.GetByIdWithCommentsAsync(id);

            if (post == null)
            {
                _notifier.Handle(new Notification("Post não encontrado."));
                return null;
            }

            return post.Adapt<BlogPostDto>();
        }
    }
}
