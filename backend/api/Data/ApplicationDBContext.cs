using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace api.Data
{
  public class ApplicationDBContext : IdentityDbContext<AppUser> // DbContext
  {

    public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {

    }

    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Comment> Comments { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      // modelBuilder.Entity<Comment>().HasOne(c => c.Stock).WithMany(s => s.Comments).HasForeignKey(c => c.StockId);
      List<IdentityRole> roles = new List<IdentityRole>
      {
        new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
        new IdentityRole { Name = "User", NormalizedName = "USER" }
      };

      modelBuilder.Entity<IdentityRole>().HasData(roles);
    }
  }
}