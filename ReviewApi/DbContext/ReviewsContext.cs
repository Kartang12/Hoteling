using Microsoft.EntityFrameworkCore;
using ReviewApi.Domain;

namespace ReviewApi.DbContext
{
    public class ReviewsContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Review> Reviews { get; set; }
    }
}
