using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Stock;
using api.Models;

namespace api.Mappers
{
  public static class StockMappers
  {
    public static StockDTO ToStockDto(this Stock stock)
    {
      return new StockDTO
      {
        Id = stock.Id,
        Symbol = stock.Symbol,
        CompanyName = stock.CompanyName,
        Purchase = stock.Purchase,
        LastDiv = stock.LastDiv,
        Industry = stock.Industry,
        MarketCap = stock.MarketCap
      };
    }

    public static Stock ToStockFromCreateStockRequestDto(this CreateStockRequestDto stockDto)
    {
      return new Stock
      {
        Symbol = stockDto.Symbol,
        CompanyName = stockDto.CompanyName,
        Purchase = stockDto.Purchase,
        LastDiv = stockDto.LastDiv,
        Industry = stockDto.Industry,
        MarketCap = stockDto.MarketCap
      };
    }
  }
}