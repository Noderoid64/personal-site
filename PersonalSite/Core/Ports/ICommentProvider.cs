using PersonalSite.Core.Models.Entities;

namespace PersonalSite.Core.Ports;

public interface ICommentProvider
{
    public void AddComment(CommentEntity commentEntity);
    public void DeleteComment(CommentEntity commentEntity);
    public Task<CommentEntity> GetCommentAsync(int id);
    public Task<IEnumerable<CommentEntity>> GetCommentsForPostAsync(int postId);
    public Task SaveAsync();
}