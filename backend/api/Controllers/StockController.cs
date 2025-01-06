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
using api.Interfaces;

namespace api.Controllers
{
  [Route("api/stock")]
  [ApiController]
  public class StockController : ControllerBase
  {
    private readonly ApplicationDBContext _context;
    private readonly IStockRepository _stockRepository;
    public StockController(IStockRepository stockRepository, ApplicationDBContext context)
    {
      _context = context;
      _stockRepository = stockRepository;
    }


    // GET: api/stock
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      // var stocks = await _context.Stocks.ToListAsync();
      // var stocksDTO = stocks.Select(s => s.ToStockDto());

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var stocks = await _stockRepository.GetAllAsync();
      var stocksDTO = stocks.Select(s => StockMappers.ToStockDto(s));
      return Ok(stocksDTO);
    }

    // GET: api/stock/5
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
      // var stock = await _context.Stocks.FindAsync(id);

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var stock = await _stockRepository.GetByIdAsync(id);

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

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var stockModel = stockDto.ToStockFromCreateStockRequestDto();
      // await _context.Stocks.AddAsync(stockModel);
      // await _context.SaveChangesAsync();
      await _stockRepository.CreateAsync(stockModel);
      return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }

    // PUT: api/stock/5
    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto)
    {
      // var stockModel = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

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
      return Ok(stockModel.ToStockDto()); // 
    }

    // DELETE: api/stock/5
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
      // var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
      // if (stock == null)
      // {
      //   return NotFound();
      // }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var stock = await _stockRepository.DeleteAsync(id);
      // _context.Stocks.Remove(stock);
      // await _context.SaveChangesAsync();
      return NoContent();
    }
  }
}