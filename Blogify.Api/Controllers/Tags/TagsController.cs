﻿using Asp.Versioning;
using Blogify.Application.Tags.CreateTag;
using Blogify.Application.Tags.DeleteTag;
using Blogify.Application.Tags.GetAllTags;
using Blogify.Application.Tags.GetTagById;
using Blogify.Application.Tags.UpdateTag;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blogify.Api.Controllers.Tags;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/tags")]
public sealed class TagsController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateTag([FromBody] CreateTagCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure) return BadRequest(result.Error);

        return CreatedAtAction(nameof(GetTagById), new { id = result.Value }, result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTags(CancellationToken cancellationToken)
    {
        var query = new GetAllTagsQuery();

        var result = await sender.Send(query, cancellationToken);

        if (result.IsFailure) return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTagById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetTagByIdQuery(id);

        var result = await sender.Send(query, cancellationToken);

        if (result.IsFailure) return NotFound(result.Error);

        return Ok(result.Value);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTag(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteTagCommand(id);

        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure) return NotFound(result.Error);

        return NoContent();
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTag(Guid id, [FromBody] UpdateTagCommand command, CancellationToken cancellationToken)
    {
        
        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure) return BadRequest(result.Error);

        return NoContent();
    }
}