using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Comment
{
  public class CreateCommentRequestDto
  {
    [Required]
    [MinLength(3, ErrorMessage = "Title must be at least 3 characters long")]
    [MaxLength(100, ErrorMessage = "Title must be at most 100 characters long")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MinLength(3, ErrorMessage = "Content must be at least 3 characters long")]
    [MaxLength(1000, ErrorMessage = "Content must be at most 1000 characters long")]
    public string Content { get; set; } = string.Empty;
  }
}