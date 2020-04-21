using Microsoft.EntityFrameworkCore;
 
namespace LoginRegistration.Models
{
    public class LRContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public LRContext(DbContextOptions options) : base(options) { }
        public DbSet<User> users {get;set;} 
    }
}