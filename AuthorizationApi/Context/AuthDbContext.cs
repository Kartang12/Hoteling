using AuthorizationApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationApi.Context
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Email = "string1234@gmail.com",
                        PWHash = "by[.�#[;M(�i��6g���W��ـ�e37", //string123
                        RefreshToken = "pXX3ZevB7sJMmgj4EkNH0+0YNYxZgUtNrZWkyhvQUOY=",
                        RefreshTokenExpiryTime = DateTime.Parse("2023-10-24 10:39:28.7234190"),
                        Role = UserRolesEnum.User
                    },
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Email = "admin@gmail.com",
                        PWHash = "by[.�#[;M(�i��6g���W��ـ�e37",//string123
                        RefreshToken = "pXX3ZevB7sJMmgj4EkNH0+0YNYxZgUtNrZWkyhvQUOY=",
                        RefreshTokenExpiryTime = DateTime.Parse("2023-10-24 10:39:28.7234190"),
                        Role = UserRolesEnum.Admin
                    }
                ) ;
        }

        public DbSet<User> Users { get; set; }
    }
}
