using Microsoft.EntityFrameworkCore;
using SimpleBlog.Domain;
using SimpleBlog.Dto.Dto;
using SimpleBlog.Repository.Context;
using SimpleBlog.Repository.Interfaces;

namespace SimpleBlog.Repository.Repositories
{
    public class BlogPostRepository(BlogDbContext context) : BaseRepository<BlogPost>(context), IBlogPostRepository
    {
        public async Task<IEnumerable<BlogPostDto>> GetAllWithCommentsAsync()
        {
            var query = _dbSet
                .AsNoTracking()
                .Select(BlogPost => new BlogPostDto
                {
                    Id = BlogPost.Id,
                    Manchete = BlogPost.Title,
                    CorpoTexto = BlogPost.Content,
                    TotalComentarios = BlogPost.Comments!.Count()
                });

            return await query
                .OrderByDescending(p => p.Manchete)
                .ToListAsync();
        }

        public async Task<BlogPost?> GetByIdWithCommentsAsync(Guid id)
        {
            return await _dbSet
                .Include(p => p.Comments)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> ExistsTitleAsync(string title)
        {
            return await _dbSet.AnyAsync(p => p.Title == title);
        }
    }
}
