using Microsoft.EntityFrameworkCore;
using ReviewApi.Domain;

namespace ReviewApi.DbContext
{
    public class ReviewsContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ReviewsContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>()
                .HasData(
                    new Review
                    {
                        Id = Guid.NewGuid(),
                        UserId = Guid.Parse("A567C4AA-388E-4A9F-60AA-08DAB0276093")
                        HotelId = Guid.Parse("64B71417-F19D-4B81-ACD3-EB180D0D638F"),
                        HotelName = "Afterlife",
                        Feedback = "Great bds, cheap beer",
                        Score = 10
                    }
                );
        }

        public DbSet<Review> Reviews { get; set; }
    }
}
