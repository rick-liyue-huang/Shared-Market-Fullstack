using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Mappers;
using api.Dtos.Stock;
using Microsoft.EntityFrameworkCore;
using api.Interfaces;


namespace api.Controllers
{
  // [Route("[controller]")]
  [Route("api/stock")]
  [ApiController]
  public class StockController : ControllerBase
  {
    // private readonly ApplicationDBContext _context;
    private readonly IStockRepository _stockRepository;

    public StockController(IStockRepository stockRepository /* , ApplicationDBContext context*/ )
    {
      // _context = context;
      _stockRepository = stockRepository;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      // var stocks = await _context.Stocks.ToListAsync();
      var stocks = await _stockRepository.GetAllAsync();
      var stocksDto = stocks.Select(s => StockMappers.ToStockDto(s));
      return Ok(stocksDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
      // var stock = await _context.Stocks.FindAsync(id);
      var stock = await _stockRepository.GetByIdAsync(id);
      if (stock == null)
      {
        return NotFound();
      }
      return Ok(stock.ToStockDto());

    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
    {
      var stockModel = stockDto.ToStockFromCreateRequestStockDto();
      // await _context.Stocks.AddAsync(stockModel);
      await _stockRepository.CreateAsync(stockModel);
      // await _context.SaveChangesAsync();
      return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto)
    {
      // var stockModel = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);

      var stockModel = await _stockRepository.UpdateAsync(id, stockDto);

      if (stockModel == null)
      {
        return NotFound();
      }

      // stockModel.Symbol = stockDto.Symbol;
      // stockModel.CompanyName = stockDto.CompanyName;
      // stockModel.Purchase = stockDto.Purchase;
      // stockModel.LastDiv = stockDto.LastDiv;
      // stockModel.Industry = stockDto.Industry;
      // stockModel.MarketCap = stockDto.MarketCap;

      // await _context.SaveChangesAsync();
      return Ok(stockModel.ToStockDto());
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
      // var stockModel = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);

      var stockModel = await _stockRepository.DeleteAsync(id);

      if (stockModel == null)
      {
        return NotFound();
      }

      // _context.Stocks.Remove(stockModel);
      // await _context.SaveChangesAsync();
      return NoContent();
    }
  }
}