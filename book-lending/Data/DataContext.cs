using book_lending.Models;
using Microsoft.EntityFrameworkCore;

namespace book_lending.Data
{
    public class DataContext : DbContext
    {
        
        public DbSet<UserModel> Users { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                @"Host=localhost;Port=5432;Database=BookLending;Username=postgres;Password=postgres"); 
        }
    }
}