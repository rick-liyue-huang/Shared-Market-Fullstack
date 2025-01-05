using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Data;
using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Repository
{
  public class CommentRepository : ICommentRepository
  {
    private readonly ApplicationDBContext _context;

    public CommentRepository(ApplicationDBContext context)
    {
      _context = context;
    }

    public async Task<List<Comment>> GetAllAsync()
    {
      return await _context.Comments.ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
      return await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
    }
  }
}