using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Dtos.Stock;

namespace api.Interfaces
{
  public interface IStockRepository
  {
    Task<List<Stock>> GetAllAsync();
    Task<Stock?> GetByIdAsync(int id);
    Task<Stock> CreateAsync(Stock stock);
    Task<Stock?> UpdateAsync(int Id, UpdateStockRequestDto stockDto);
    Task<Stock?> DeleteAsync(int id);
  }
}