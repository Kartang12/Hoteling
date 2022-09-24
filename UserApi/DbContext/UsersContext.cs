using UserApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookingApi.DbContext
{
    public class UsersContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<UserData> Bookings { get; set; }
    }
}
