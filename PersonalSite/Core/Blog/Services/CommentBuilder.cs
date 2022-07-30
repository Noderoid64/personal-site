using PersonalSite.Core.Models.Entities;

namespace PersonalSite.Core.Blog.Services;

public class CommentBuilder
{
    public CommentEntity CreateComment(string content, int authorId, int postId)
    {
        return new CommentEntity()
        {
            AuthorId = authorId,
            PostId = postId,
            Content = content,
            CreatedAt = DateTime.Now,
        };
    }
    
}