using UserApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace UserApi.DbContext
{
    public class UsersContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public UsersContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<UserData> Users { get; set; }
    }
}
