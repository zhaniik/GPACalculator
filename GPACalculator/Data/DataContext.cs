using GPACalculator.Models;
using Microsoft.EntityFrameworkCore;
namespace GPACalculator.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Course> Courses { get; set; }
    }
}
