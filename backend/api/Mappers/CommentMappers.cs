using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Comment;
using api.Models;

namespace api.Mappers
{
  public static class CommentMappers
  {
    public static CommentDTO ToCommentDto(this Comment comment)
    {
      return new CommentDTO
      {
        Id = comment.Id,
        StockId = comment.StockId,
        Title = comment.Title,
        Content = comment.Content,
        CreatedOn = comment.CreatedOn
      };
    }


    public static Comment ToCommentFromCreateCommentRequestDto(this CreateCommentRequestDto commentDto, int stockId)
    {
      return new Comment
      {
        StockId = stockId,
        Title = commentDto.Title,
        Content = commentDto.Content
      };
    }

    public static Comment ToCommentFromUpdateCommentRequestDto(this UpdateCommentRequestDto commentDto)
    {
      return new Comment
      {
        Title = commentDto.Title,
        Content = commentDto.Content
      };
    }
  }
}