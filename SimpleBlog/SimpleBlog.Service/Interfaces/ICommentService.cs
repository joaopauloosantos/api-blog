using SimpleBlog.Dto.Dto;

namespace SimpleBlog.Service.Interfaces
{
    public interface ICommentService
    {
        Task<CommentDto?> CreateCommentAsync(CommentDto dto);
        Task<CommentDto?> GetCommentByIdAsync(Guid id);
    }
}
