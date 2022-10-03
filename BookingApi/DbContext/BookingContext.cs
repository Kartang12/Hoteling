using BookingApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookingApi.DbContext
{
    public class BookingContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public BookingContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }
    }
}
