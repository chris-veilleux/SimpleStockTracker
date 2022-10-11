using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleStockTracker.Models;

namespace SimpleStockTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<SimpleStockTracker.Models.Holding> Holding { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}