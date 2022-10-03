using Microsoft.EntityFrameworkCore;
using ReviewApi.Domain;

namespace ReviewApi.DbContext
{
    public class ReviewsContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ReviewsContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Review> Reviews { get; set; }
    }
}
