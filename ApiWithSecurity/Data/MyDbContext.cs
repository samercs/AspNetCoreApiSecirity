using ApiWithSecurity.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiWithSecurity.Data
{
    public class MyDbContext: IdentityDbContext<ApplicationUser, IdentityRole<int>,int>
    {
        public MyDbContext() : base()
        {

        }

        public MyDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseSqlServer("Server=THQLT1MABUHUMAI\\SQL2016;Database=ProjectTemplate.Db;User ID=sa; Password=P@ssw0rd;MultipleActiveResultSets=true;");

            //}
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
