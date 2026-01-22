using SimpleBlog.Domain;

namespace SimpleBlog.Repository.Interfaces
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetByPostIdAsync(Guid postId);
    }
}
