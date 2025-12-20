using Microsoft.EntityFrameworkCore;
using ExpressionEvaluator.Api.Models;

namespace ExpressionEvaluator.Api.Data
{
    public class ExpressionDbContext : DbContext
    {
        public ExpressionDbContext(DbContextOptions<ExpressionDbContext> options)
            : base(options)
        {
        }

        public DbSet<ExpressionRecord> Expressions { get; set; }
    }
}