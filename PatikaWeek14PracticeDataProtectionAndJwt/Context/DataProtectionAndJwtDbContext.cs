using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PatikaWeek14PracticeDataProtectionAndJwt.Entities;

namespace PatikaWeek14PracticeDataProtectionAndJwt.Context
{
    public class DataProtectionAndJwtDbContext : DbContext
    {
        public DataProtectionAndJwtDbContext(DbContextOptions<DataProtectionAndJwtDbContext> options) : base(options)
        {
            
        }

        public DbSet<UserEntity> Users => Set<UserEntity>();
    }
}
