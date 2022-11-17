using HotelApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace HotelApi.DbContext
{
    public class HotelingContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public HotelingContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>()
                .HasData(
                    new Hotel
                    {
                        Id = Guid.Parse("64B71417-F19D-4B81-ACD3-EB180D0D638F"),
                        Name = "Afterlife",
                        Location = "Little China",
                        RoomsAmount = 10,
                        Rating  = 0,
                        TotalVisitors = 0
                    },
                    new Hotel
                    {
                        Id = Guid.Parse("51DA63EF-1475-43E0-A7D2-C736C1285280"),
                        Name = "Walls motel",
                        Location = "Outlands",
                        RoomsAmount = 20,
                        Rating  = 0,
                        TotalVisitors = 0
                    },
                    new Hotel
                    {
                        Id = Guid.Parse("519316CE-8415-4ABA-97A5-0DBAA25FFCD6"),
                        Name = "Konpeki Plaza",
                        Location = "Martin street",
                        RoomsAmount = 100,
                        Rating  = 0,
                        TotalVisitors = 0
                    }
                );
            
            modelBuilder.Entity<Room>()
                .HasData(
                    new Room
                    {
                        Id = Guid.Parse("1468813B-FF11-4D82-9F88-9C2E7BA0D4BD"),
                        HotelId = Guid.Parse("64B71417-F19D-4B81-ACD3-EB180D0D638F"),
                        Number = 10,
                        Wifi = true,
                        Square = 18
                    }
                );
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
    }
}
