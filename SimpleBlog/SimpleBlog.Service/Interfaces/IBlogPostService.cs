using SimpleBlog.Dto.Dto;

namespace SimpleBlog.Service.Interfaces
{
    public interface IBlogPostService
    {
        Task<IEnumerable<BlogPostDto>> GetAllPostsAsync();

        Task<BlogPostDto?> CreatePostAsync(BlogPostDto dto);

        Task<BlogPostDto?> GetByIdWithCommentsAsync(Guid id);
    }
}
