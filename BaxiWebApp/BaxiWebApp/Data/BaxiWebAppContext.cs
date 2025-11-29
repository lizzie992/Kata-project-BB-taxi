using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BB;
using Microsoft.AspNetCore.Identity;

namespace BaxiWebApp.Data
{
    public class BaxiWebAppContext(DbContextOptions<BaxiWebAppContext> options) : IdentityDbContext<User>(options)
    {
        public DbSet<Ad> Ads { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Message> Messages { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
           
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Regular", NormalizedName = "REGULAR", Id = Guid.Parse("9042ee2d-b216-4f0d-931a-8373f396e37f").ToString(), ConcurrencyStamp = Guid.Parse("37ac6a35-5325-46f8-a8e8-8d6964111cbb").ToString() });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN", Id = Guid.Parse("69180910-077e-4495-b28b-dcdf55b74bdd").ToString(), ConcurrencyStamp = Guid.Parse("5056b3ec-8a8f-4aaf-805b-040e791d285f").ToString() });
        }

    } 

}
