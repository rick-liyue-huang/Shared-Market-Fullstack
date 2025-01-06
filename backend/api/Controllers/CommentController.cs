using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.DTOs.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
  [Route("api/comment")]
  [ApiController]
  public class CommentController : ControllerBase
  {
    private readonly ICommentRepository _commentRepository;
    private readonly IStockRepository _stockRepository;
    public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
    {
      _commentRepository = commentRepository;
      _stockRepository = stockRepository;
    }

    // GET: api/comment
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var comments = await _commentRepository.GetAllAsync();

      var commentsDTO = comments.Select(c => CommentMappers.ToCommentDto(c));
      return Ok(commentsDTO);
    }

    // GET: api/comment/5
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
      var comment = await _commentRepository.GetByIdAsync(id);

      if (comment == null)
      {
        return NotFound();
      }

      var commentDTO = CommentMappers.ToCommentDto(comment);
      return Ok(commentDTO);
    }


    // POST: api/comment
    [HttpPost("{stockId}")]
    public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentRequestDto commentDto)
    {
      if (!await _stockRepository.StockExistsAsync(stockId))
      {
        return BadRequest("Stock does not exist");
      }

      var commentModel = commentDto.ToCommentFromCreateCommentRequestDto(stockId);

      await _commentRepository.CreateAsync(commentModel);
      return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
    }


    // DELETE: api/comment/5
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
      var comment = await _commentRepository.DeleteAsync(id);

      if (comment == null)
      {
        return NotFound("Comment not found");
      }

      return Ok(comment.ToCommentDto());
    }

    // UPDATE: api/comment/5
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto commentDto)
    {
      var comment = await _commentRepository.UpdateAsync(id, commentDto.ToCommentFromUpdateCommentRequestDto());

      if (comment == null)
      {
        return NotFound("Comment not found");
      }

      return Ok(comment.ToCommentDto());
    }
  }
}