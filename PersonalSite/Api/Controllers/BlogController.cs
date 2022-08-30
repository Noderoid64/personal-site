using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalSite.Api.Dtos;
using PersonalSite.Core.Blog;
using PersonalSite.Core.Blog.Models;
using PersonalSite.Core.Models.Entities;

namespace PersonalSite.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/blog")]
public class BlogController : ApiController
{
    private readonly PostWorkflow _postWorkflow;
    private readonly IMapper _mapper;

    public BlogController(PostWorkflow postWorkflow, IMapper mapper)
    {
        _postWorkflow = postWorkflow;
        _mapper = mapper;
    }

    [HttpGet("all")]
    public async Task<FileObject> GetMyPosts()
    {
        var userId = GetUserId();
        var a = await _postWorkflow.GetUserPostsAsync(userId);
        return a;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPost(int id)
    {
        // var userId = GetUserId();
        var post = await _postWorkflow.GetPostAsync(id);
        if(post.IsSuccess)
            return Ok(_mapper.Map<PostDto>(post.Value));
        return BadRequest(post.ErrorMessage);
    }

    [HttpPost("post")]
    public async Task<IActionResult> PostPost(PostDto postDto)
    {
        var profileId = GetUserId();
        var post = _mapper.Map<FileObjectEntity>(postDto);
        await _postWorkflow.SavePost(profileId, post);
        return Ok();
    }

    [HttpPost("folders/new")]
    public async Task<IActionResult> CreateFolder(string title, int parentId)
    {
        var profileId = GetUserId();
        var result = await _postWorkflow.SaveFolderAsync(profileId, title, parentId);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ErrorMessage);
    }
    
    [HttpDelete()]
    public async Task<IActionResult> DeleteFile(int fileId)
    {
        var profileId = GetUserId();
        await _postWorkflow.DeleteFileAsync(profileId, fileId);
        return Ok();
    }

    [AllowAnonymous]
    [HttpGet("post/find")]
    public async Task<IActionResult> FindPost(string searchString)
    {
        var result = await _postWorkflow.FindPosts(searchString);
        return Ok(result);
    }
    
    
}