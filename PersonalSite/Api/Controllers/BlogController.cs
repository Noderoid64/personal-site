﻿using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalSite.Api.Dtos;
using PersonalSite.Core.Blog;
using PersonalSite.Core.Entities;

namespace PersonalSite.Api.Controllers;

[Authorize]
[ApiController]
[Route("blog")]
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
    public async Task<List<PostDto>> GetMyPosts()
    {
        var userId = GetUserId();
        List<PostEntity> posts = await _postWorkflow.GetUserPostsAsync(userId);
        var result = _mapper.Map<List<PostDto>>(posts);
        return result;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPost(int id)
    {
        var userId = GetUserId();
        var post = await _postWorkflow.GetPostAsync(userId, id);
        if(post.IsSuccess)
            return Ok(_mapper.Map<PostDto>(post));
        return BadRequest(post.ErrorMessage);
    }

    [HttpPost("post")]
    public async Task<IActionResult> PostPost(PostDto postDto)
    {
        var profileId = GetUserId();
        var post = _mapper.Map<PostEntity>(postDto);
        await _postWorkflow.SavePost(profileId, post);
        return Ok();
    }
    
    
}