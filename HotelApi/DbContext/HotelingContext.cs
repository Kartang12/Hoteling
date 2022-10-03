using HotelApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace HotelApi.DbContext
{
    public class HotelingContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public HotelingContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
    }
}
