using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using api.DTOs.Stock;
using api.DTOs.Comment;
using api.Data;
using Microsoft.EntityFrameworkCore;
using api.Helpers;

namespace api.Repositories
{
  public class StockRepository : IStockRepository
  {

    private readonly ApplicationDBContext _context;
    public StockRepository(ApplicationDBContext context)
    {
      _context = context;
    }

    public async Task<List<Stock>> GetAllAsync(QueryObject query)
    {
      var stocks = _context.Stocks.Include(s => s.Comments).AsQueryable();

      if (!string.IsNullOrWhiteSpace(query.CompanyName))
      {
        stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
      }

      if (!string.IsNullOrWhiteSpace(query.Symbol))
      {
        stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
      }

      if (!string.IsNullOrWhiteSpace(query.SortBy))
      {
        if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
        {
          stocks = query.IsDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
        }
      }

      var skipNumber = (query.PageNumber - 1) * query.PageSize;

      return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
      return await _context.Stocks.Include(s => s.Comments).FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Stock> CreateAsync(Stock stock)
    {
      await _context.Stocks.AddAsync(stock);
      await _context.SaveChangesAsync();
      return stock;
    }

    public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
    {
      var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
      if (stock == null)
      {
        return null;
      }

      stock.Symbol = stockDto.Symbol;
      stock.CompanyName = stockDto.CompanyName;
      stock.Purchase = stockDto.Purchase;
      stock.LastDiv = stockDto.LastDiv;
      stock.Industry = stockDto.Industry;
      stock.MarketCap = stockDto.MarketCap;

      await _context.SaveChangesAsync();
      return stock;
    }

    public async Task<Stock?> DeleteAsync(int id)
    {
      var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
      if (stock == null)
      {
        return null;
      }

      _context.Stocks.Remove(stock);
      await _context.SaveChangesAsync();
      return stock;
    }


    public async Task<bool> StockExistsAsync(int id)
    {
      return await _context.Stocks.AnyAsync(s => s.Id == id);
    }
  }
}