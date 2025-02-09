using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BB;

namespace BaxiWebApp.Data
{
    public class BaxiWebAppContext(DbContextOptions<BaxiWebAppContext> options) : IdentityDbContext<User>(options)
    {
        public DbSet<Ad> Ads { get; set; }


    }
}
