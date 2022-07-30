using Microsoft.EntityFrameworkCore;
using PersonalSite.Core.Models.Entities;
using PersonalSite.Core.Ports;

namespace PersonalSite.Infrastructure.EF.Providers;

public class CommentProvider : _BaseProvider, ICommentProvider
{
    public CommentProvider(ApplicationContext context) : base(context)
    {
    }

    public void AddComment(CommentEntity commentEntity)
    {
        _context.Comments.Add(commentEntity);
    }

    public void DeleteComment(CommentEntity commentEntity)
    {
        _context.Comments.Remove(commentEntity);
    }

    public async Task<CommentEntity> GetCommentAsync(int id)
    {
        return await _context.Comments.FirstAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<CommentEntity>> GetCommentsForPostAsync(int postId)
    {
        return await _context.Comments
            .Include(x => x.Author)
            .Include(x => x.Post)
            .Where(x => x.PostId == postId)
            .ToListAsync();
    }
}