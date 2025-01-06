using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using api.DTOs.Stock;
using api.Data;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
  public class StockRepository : IStockRepository
  {

    private readonly ApplicationDBContext _context;
    public StockRepository(ApplicationDBContext context)
    {
      _context = context;
    }

    public async Task<List<Stock>> GetAllAsync()
    {
      return await _context.Stocks.ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
      return await _context.Stocks.FindAsync(id);
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
  }
}