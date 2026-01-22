using SimpleBlog.Domain;
using SimpleBlog.Dto.Dto;

namespace SimpleBlog.Repository.Interfaces
{
    public interface IBlogPostRepository : IBaseRepository<BlogPost>
    {
        Task<IEnumerable<BlogPostDto>> GetAllWithCommentsAsync();

        Task<BlogPost?> GetByIdWithCommentsAsync(Guid id);

        Task<bool> ExistsTitleAsync(string title);
    }
}
