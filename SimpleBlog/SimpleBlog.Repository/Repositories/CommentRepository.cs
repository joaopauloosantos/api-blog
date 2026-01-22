using Microsoft.EntityFrameworkCore;
using SimpleBlog.Domain;
using SimpleBlog.Repository.Context;
using SimpleBlog.Repository.Interfaces;

namespace SimpleBlog.Repository.Repositories
{
    public class CommentRepository(BlogDbContext context) : BaseRepository<Comment>(context), ICommentRepository
    {
        public async Task<IEnumerable<Comment>> GetByPostIdAsync(Guid postId)
        {
            return await _dbSet
                .Where(c => c.PostId == postId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
