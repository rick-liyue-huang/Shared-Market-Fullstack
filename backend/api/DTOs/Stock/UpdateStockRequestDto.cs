using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Stock
{
  public class UpdateStockRequestDto
  {

    [Required]
    [MinLength(1, ErrorMessage = "Symbol must be at least 1 character long")]
    [MaxLength(10, ErrorMessage = "Symbol must be at most 10 characters long")]
    public string Symbol { get; set; } = string.Empty;

    [Required]
    [MinLength(1, ErrorMessage = "Company Name must be at least 1 character long")]
    [MaxLength(20, ErrorMessage = "Company Name must be at most 20 characters long")]
    public string CompanyName { get; set; } = string.Empty;


    [Required]
    [Range(1, 100000000)]
    public decimal Purchase { get; set; }

    [Required]
    [Range(0.00, 100)]
    public decimal LastDiv { get; set; }

    [Required]
    [MaxLength(20, ErrorMessage = "Industry must be at most 20 characters long")]
    public string Industry { get; set; } = string.Empty;

    [Required]
    [Range(1, 50000000000)]
    public long MarketCap { get; set; }
  }
}