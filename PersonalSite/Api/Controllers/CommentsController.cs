using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalSite.Api.Dtos;
using PersonalSite.Core.Blog;

namespace PersonalSite.Api.Controllers;

[Authorize]
[ApiController]
[Route("comments")]
public class CommentsController : ApiController
{
    private readonly CommentWorkflow _commentWorkflow;
    private readonly IMapper _mapper;

    public CommentsController(CommentWorkflow commentWorkflow, IMapper mapper)
    {
        _commentWorkflow = commentWorkflow;
        _mapper = mapper;
    }
    
    [HttpPut("posts/{id}")]
    public async Task<IActionResult> PostComment(int id, CommentCreationDto commentCreationDto)
    {
        var userId = GetUserId();
        var result = await _commentWorkflow.PostCommentAsync(id, commentCreationDto.Content, userId);
        return BuildResponse(result);
    }

    [HttpGet("posts/{id}")]
    public async Task<IActionResult> GetCommentsForPost(int id)
    {
        var result = await _commentWorkflow.GetCommentsForPostAsync(id);
        if (result.IsSuccess)
            return Ok(_mapper.Map<CommentDto[]>(result.Value));
        return BadRequest(result.ErrorMessage);
    }
}