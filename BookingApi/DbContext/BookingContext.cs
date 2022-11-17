using BookingApi.Domain;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;

namespace BookingApi.DbContext
{
    public class BookingContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public BookingContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .HasData(
                    new Booking
                    {
                        Id = Guid.Parse("9681C1B5-6CED-4B2D-A158-4A5BF2FE361C"),
                        RoomId = Guid.Parse("1468813B-FF11-4D82-9F88-9C2E7BA0D4BD"),
                        RoomNumber = 10,
                        HotelId = Guid.Parse("64B71417-F19D-4B81-ACD3-EB180D0D638F"),
                        HotelName = "Afterlife",
                        StartDate = DateTime.Parse("2022-10-22 00:00:00.0000000"),
                        EdnDate = DateTime.Parse("2022-12-22 00:00:00.0000000"),
                        GuestsAmount = 10
                    }
                );
        }

        public DbSet<Booking> Bookings { get; set; }
    }
}
