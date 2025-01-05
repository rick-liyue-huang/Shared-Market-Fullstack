using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.Interfaces;
using api.Models;
using api.Mappers;
using api.Dtos.Comment;
using api.Repository;

namespace api.Controllers
{
  [Route("api/comment")]
  [ApiController]
  public class CommentController : ControllerBase
  {
    private readonly ICommentRepository _commentRepository;
    public CommentController(ICommentRepository commentRepository)
    {
      _commentRepository = commentRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var comments = await _commentRepository.GetAllAsync();
      var commentsDto = comments.Select(c => CommentMappers.ToCommentDto(c));
      return Ok(commentsDto);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
      var comment = await _commentRepository.GetByIdAsync(id);
      if (comment == null)
      {
        return NotFound();
      }
      var commentDto = CommentMappers.ToCommentDto(comment);
      return Ok(commentDto);
    }

  }
}