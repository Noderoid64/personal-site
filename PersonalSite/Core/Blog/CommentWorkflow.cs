using PersonalSite.Core.Blog.Services;
using PersonalSite.Core.Models.Entities;
using PersonalSite.Core.Ports;
using PersonalSite.Infrastructure.Common.Models;

namespace PersonalSite.Core.Blog;

public class CommentWorkflow
{
    private readonly ICommentProvider _commentProvider;
    private readonly IPostProvider _postProvider;
    private readonly IProfileProvider _profileProvider;
    private readonly CommentBuilder _commentBuilder;

    public CommentWorkflow(
        ICommentProvider commentProvider, 
        IPostProvider postProvider, 
        IProfileProvider profileProvider,
        CommentBuilder commentBuilder)
    {
        _commentProvider = commentProvider;
        _postProvider = postProvider;
        _profileProvider = profileProvider;
        _commentBuilder = commentBuilder;
    }
    
    public async Task<Result<int>> PostCommentAsync(int postId, string content, int authorId)
    {
        var post = await _postProvider.GetPostWithCommentsAsync(postId);
        var author = await _profileProvider.GetProfileAsync(authorId);

        var commentToSave = _commentBuilder.CreateComment(content, author.Id, post.Id);
        
        _commentProvider.AddComment(commentToSave);

        await _commentProvider.SaveAsync();
        
        return Result<int>.Success(commentToSave.Id);
    }

    public async Task<Result<IEnumerable<CommentEntity>>> GetCommentsForPostAsync(int postId)
    {
        var post = await _postProvider.GetPostWithCommentsAsync(postId);
        return Result<IEnumerable<CommentEntity>>.Success(post.Comments);
    }

    public async Task<Result> DeleteComment(int authorId, int commentId)
    {
        var comment = await _commentProvider.GetCommentAsync(commentId);
        
        if (comment.AuthorId != authorId)
            return Result.Fail("Access denied");
        
        _commentProvider.DeleteComment(comment);
        await _commentProvider.SaveAsync();
        
        return Result.Success();

    }
}