using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.Data;
using Microsoft.EntityFrameworkCore;
using api.Models;
using api.Mappers;
using api.DTOs.Stock;

namespace api.Controllers
{
  [Route("api/stock")]
  [ApiController]
  public class StockController : ControllerBase
  {
    private readonly ApplicationDBContext _context;
    public StockController(ApplicationDBContext context)
    {
      _context = context;
    }


    // GET: api/stock
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var stocks = await _context.Stocks.ToListAsync();
      // var stocksDTO = stocks.Select(s => s.ToStockDto());
      var stocksDTO = stocks.Select(s => StockMappers.ToStockDto(s));
      return Ok(stocksDTO);
    }

    // GET: api/stock/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
      var stock = await _context.Stocks.FindAsync(id);

      if (stock == null)
      {
        return NotFound();
      }
      return Ok(stock.ToStockDto());
    }

    // POST: api/stock
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
    {
      var stockModel = stockDto.ToStockFromCreateStockRequestDto();
      await _context.Stocks.AddAsync(stockModel);
      await _context.SaveChangesAsync();
      return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }

    // PUT: api/stock/5
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto)
    {
      var stockModel = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
      if (stockModel == null)
      {
        return NotFound();
      }
      stockModel.Symbol = stockDto.Symbol;
      stockModel.CompanyName = stockDto.CompanyName;
      stockModel.Purchase = stockDto.Purchase;
      stockModel.LastDiv = stockDto.LastDiv;
      stockModel.Industry = stockDto.Industry;
      stockModel.MarketCap = stockDto.MarketCap;

      await _context.SaveChangesAsync();
      return Ok(stockModel.ToStockDto()); // 
    }

    // DELETE: api/stock/5
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
      var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
      if (stock == null)
      {
        return NotFound();
      }
      _context.Stocks.Remove(stock);
      await _context.SaveChangesAsync();
      return NoContent();
    }
  }
}