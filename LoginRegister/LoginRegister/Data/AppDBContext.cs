using LoginRegister.Models;
using Microsoft.EntityFrameworkCore;
namespace LoginRegister.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) :
             base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
