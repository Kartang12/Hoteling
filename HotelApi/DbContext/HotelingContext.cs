using HotelApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace HotelApi.DbContext
{
    public class HotelingContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
    }
}
