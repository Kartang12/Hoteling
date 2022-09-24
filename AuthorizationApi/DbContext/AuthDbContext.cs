using AuthorizationApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationApi.DbContext
{
    public class AuthDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
